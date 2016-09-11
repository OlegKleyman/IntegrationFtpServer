namespace Omego.SimpleFtp.Tests.Unit
{
    using System;
    using System.Collections;
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
            var server = GetFtpServer();

            server.Start();

            server.Status.ShouldBeEquivalentTo(FtpServerStatus.Running);
        }

        [Test]
        public void StopShouldStopProcess()
        {
            var server = GetFtpServer();
            server.Stop();
        }

        [Test]
        public void StopShouldThrowInvalidOperationExceptionIfServerHasNotStarted()
        {
            var server = GetFtpServer();
            Action stop = () => server.Stop();

            stop.ShouldThrow<InvalidOperationException>().WithMessage("Server is not running.");
        }

        private FtpServer GetFtpServer()
        {
            var path = Substitute.For<PathBase>();
            path.GetTempPath().Returns(@"C:\temp");
            path.GetTempFileName().Returns("something");

            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.Path.Returns(path);

            var operatingSystem = Substitute.For<IOperatingSystem>();

            return new FtpServer(new FtpConfiguration("Home", 21), fileSystem, operatingSystem);
        }

        private class FtpServerTestsSource
        {
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
        }
    }
}
