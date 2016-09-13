namespace Omego.SimpleFtp.Tests.Unit.Extensions
{
    using System;
    using System.Collections;
    using System.IO.Abstractions.TestingHelpers;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    using Omego.SimpleFtp.Extensions;

    using PathBase = System.IO.Abstractions.PathBase;

    [TestFixture]
    public class PathBaseTests
    {
        [Test]
        public void GetTempFilePathShouldReturnFilePath()
        {
            var path = Substitute.For<PathBase>();
            path.GetTempPath().Returns(@"C:\temp");
            path.GetTempFileName().Returns("something");

            path.GetTempFilePath("exe").ShouldBeEquivalentTo(@"C:\temp\something\something.exe");
        }

        [TestCaseSource(typeof(PathBaseTestsSource),
             nameof(PathBaseTestsSource.GetTempFilePathShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases))]
        public void GetTempFilePathShouldThrowArgumentExceptionWhenArgumentsAreInvalid(
            string expectedParameterName,
            string expectedMessage,
            Type expectedType,
            PathBase path,
            string extension)
        {
            Action getTempFilePath = () => path.GetTempFilePath(extension);

            getTempFilePath.ShouldThrow<ArgumentException>()
                .WithMessage(expectedMessage)
                .Where(
                    exception => exception.ParamName == expectedParameterName,
                    "the parameter name should be of the problematic parameter")
                .And.Should()
                .BeOfType(expectedType);
        }

        private class PathBaseTestsSource
        {
            public static IEnumerable GetTempFilePathShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases
                =>
                new[]
                    {
                        new object[]
                            {
                                "path", "Value cannot be null.\r\nParameter name: path", typeof(ArgumentNullException),
                                null, null
                            }
                    };
        }
    }
}