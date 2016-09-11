namespace Omego.SimpleFtp.Tests.Unit
{
    using System;
    using System.Collections;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class FtpConfigurationTests
    {
        [TestCase("Home", 21)]
        public void ConstructorShouldSetProperties(string homeDirectory, int port)
        {
            var config = new FtpConfiguration(homeDirectory, port);

            config.HomeDirectory.ShouldBeEquivalentTo(homeDirectory);
            config.Port.ShouldBeEquivalentTo(port);
        }

        [TestCaseSource(typeof(FtpConfigurationTestsSource),
             nameof(FtpConfigurationTestsSource.ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases))]
        public void ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalid(
            string expectedParameterName,
            string expectedMessage,
            Type expectedType,
            string homeDirectory,
            int port)
        {
            Action constructor = () => new FtpConfiguration(homeDirectory, port);

            constructor.ShouldThrow<ArgumentException>()
                .WithMessage(expectedMessage)
                .Where(
                    exception => exception.ParamName == expectedParameterName,
                    "the parameter name should be of the problematic parameter")
                .And.Should()
                .BeOfType(expectedType);
        }

        private class FtpConfigurationTestsSource
        {
            public static IEnumerable ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases
                =>
                new[]
                    {
                        new object[]
                            {
                                "homeDirectory", "Value cannot be null.\r\nParameter name: homeDirectory",
                                typeof(ArgumentNullException), null, default(int)
                            },
                        new object[]
                            {
                                "homeDirectory", "Value cannot be empty.\r\nParameter name: homeDirectory",
                                typeof(ArgumentException), string.Empty, default(int)
                            },
                        new object[]
                            {
                                "homeDirectory", "Value cannot be only whitespace.\r\nParameter name: homeDirectory",
                                typeof(ArgumentException), "  ", default(int)
                            },
                        new object[]
                            {
                                "port", "Value cannot be 0.\r\nParameter name: port", typeof(ArgumentException),
                                "test", default(int)
                            }
                    };
        }
    }
}
