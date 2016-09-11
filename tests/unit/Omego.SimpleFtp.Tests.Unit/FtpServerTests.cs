namespace Omego.SimpleFtp.Tests.Unit
{
    using System;
    using System.Collections;
    using System.IO.Abstractions.TestingHelpers;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class FtpServerTests
    {
        [Test]
        public void ConstructorShouldSetConfigurationProperty()
        {
            var ftpConfiguration = new FtpConfiguration("Home", 21);

            var server = new FtpServer(ftpConfiguration, new MockFileSystem());

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
            Action constructor = () => new FtpServer(configuration, new MockFileSystem());

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

        private FtpServer GetFtpServer()
        {
            return new FtpServer(new FtpConfiguration("Home", 21), new MockFileSystem());
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
