namespace Omego.SimpleFtp.Tests.Unit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.IO.Abstractions.TestingHelpers;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class FtpServerTests
    {
        [Test]
        public void ConstructorShouldSetConfigurationProperty()
        {
            var ftpConfiguration = new FtpConfiguration("Home", 21);

            var server = new FtpServer(ftpConfiguration, new MockFileSystem(), Substitute.For<IOperatingSystem>());

            server.Configuration.ShouldBeEquivalentTo(ftpConfiguration);
        }

        [TestCaseSource(typeof(FtpServerTestsSource),
             nameof(FtpServerTestsSource.ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases))]
        public void ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalid(
            string expectedParameterName,
            string expectedMessage,
            Type expectedType,
            FtpConfiguration configuration)
        {
            Action constructor = () => new FtpServer(configuration, new MockFileSystem(), Substitute.For<IOperatingSystem>());

            constructor.ShouldThrow<ArgumentException>()
                .WithMessage(expectedMessage)
                .Where(
                    exception => exception.ParamName == expectedParameterName,
                    "the parameter name should be of the problematic parameter")
                .And.Should()
                .BeOfType(expectedType);
        }

        [Test]
        public void StartShouldStartServer()
        {
            var server = FtpServerTestsSource.GetFtpServer();

            server.Start();

            server.Status.ShouldBeEquivalentTo(FtpServerStatus.Running);
        }

        [Test]
        public void StopShouldStopProcess()
        {
            var server = FtpServerTestsSource.GetFtpServer();
            server.Start();
            server.Stop();
            server.Status.Should().Be(FtpServerStatus.Stopped);
        }

        [TestCaseSource(typeof(FtpServerTestsSource),
             nameof(FtpServerTestsSource.StopShouldThrowInvalidOperationExceptionIfServerIsStoppedCases))]
        public void StopShouldThrowInvalidOperationExceptionIfServerIsStopped(FtpServer server)
        {
            Action stop = server.Stop;

            stop.ShouldThrow<InvalidOperationException>().WithMessage("Server is not running.");
        }

        [TestCaseSource(typeof(FtpServerTestsSource), nameof(FtpServerTestsSource.StatusShouldReturnServerStatusCases))]
        public void StatusShouldReturnServerStatus(FtpServer server, FtpServerStatus expectedStatus)
        {
            server.Status.Should().Be(expectedStatus);
        }

        [TestCaseSource(typeof(FtpServerTestsSource), nameof(FtpServerTestsSource.GetFilesShouldReturnFilesCases))]
        public void GetFilesShouldReturnFiles(FtpServer server, string path, IEnumerable<string> expectedFiles)
        {
            server.GetFiles(path).ShouldAllBeEquivalentTo(expectedFiles);
        }

        private class FtpServerTestsSource
        {
            public static FtpServer GetFtpServer()
            {
                var mockFileSystem =
                    new MockFileSystem(
                        new Dictionary<string, MockFileData>
                            {
                                    { @"c:\home\someFile.csv", new MockFileData(string.Empty) },
                                    { @"c:\home\TestFile1.txt", new MockFileData(string.Empty) },
                                    { @"c:\home\testDirectory\innerFile.exe", new MockFileData(string.Empty) },
                                    { @"c:\Home", new MockDirectoryData() }
                            });

                var path = Substitute.For<PathBase>();
                path.GetTempPath().Returns(@"C:\temp");
                path.GetTempFileName().Returns("something");
                path.GetDirectoryName(Arg.Any<string>())
                    .Returns(info => mockFileSystem.Path.GetDirectoryName((string)info[0]));

                var fileSystem = Substitute.For<IFileSystem>();
                fileSystem.Path.Returns(path);
                fileSystem.Directory.Returns(mockFileSystem.Directory);

                var systemProcess = Substitute.For<ISystemProcess>();
                systemProcess.HasExited.Returns(false);
                systemProcess.When(process => process.Kill()).Do(info => systemProcess.HasExited.Returns(true));

                var operatingSystem = Substitute.For<IOperatingSystem>();
                operatingSystem.StartProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(systemProcess);

                return new FtpServer(new FtpConfiguration(@"C:\Home", 21), fileSystem, operatingSystem);
            }

            private static FtpServer StartServer(FtpServer ftpServer)
            {
                ftpServer.Start();

                return ftpServer;
            }

            private static FtpServer StopServer(FtpServer ftpServer)
            {
                ftpServer.Start();
                ftpServer.Stop();

                return ftpServer;
            }

            public static IEnumerable ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases
                =>
                new[]
                    {
                        new object[]
                            {
                                "configuration", "Value cannot be null.\r\nParameter name: configuration",
                                typeof(ArgumentNullException), null
                            }
                    };

            public static IEnumerable StatusShouldReturnServerStatusCases
                =>
                new[]
                    {
                        new object[] { GetFtpServer(), FtpServerStatus.Stopped },
                        new object[] { StartServer(GetFtpServer()), FtpServerStatus.Running },
                        new object[] { StopServer(GetFtpServer()), FtpServerStatus.Stopped }
                    };

            public static IEnumerable StopShouldThrowInvalidOperationExceptionIfServerIsStoppedCases
                => new[] { new object[] { GetFtpServer() }, new object[] { StopServer(GetFtpServer()) } };

            public static IEnumerable GetFilesShouldReturnFilesCases
                =>
                new[]
                    {
                        new object[] { GetFtpServer(), ".", new[] { "someFile.csv", "TestFile1.txt" } },
                        new object[] { GetFtpServer(), "testDirectory", new[] { "innerFile.exe" } }
                    };
        }
    }
}
