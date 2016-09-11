namespace Omego.SimpleFtp.Tests.Integration
{
    using System;
    using System.Collections;
    using System.Diagnostics;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class SystemProcessTests
    {
        [Test]
        public void KillShouldKillTheProcess()
        {
            var process = Process.Start("cmd.exe");

            GetSystemProcess(process).Kill();

            process.HasExited.Should().BeTrue();
        }

        [TestCaseSource(typeof(SystemProcessTestCases), nameof(SystemProcessTestCases.ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases))]
        public void ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalid(
            string expectedParameterName,
            string expectedMessage,
            Type expectedType,Process process)
        {
            Action constructor = () => new SystemProcess(process);

            constructor.ShouldThrow<ArgumentException>()
                .WithMessage(expectedMessage)
                .Where(
                    exception => exception.ParamName == expectedParameterName,
                    "the parameter name should be of the problematic parameter")
                .And.Should()
                .BeOfType(expectedType);
        }

        public static SystemProcess GetSystemProcess(Process process)
        {
            return new SystemProcess(process);
        }

        private class SystemProcessTestCases
        {
            public static IEnumerable ConstructorShouldThrowArgumentExceptionWhenArgumentsAreInvalidCases
                =>
                new[]
                    {
                        new object[]
                            {
                                "process", "Value cannot be null.\r\nParameter name: process",
                                typeof(ArgumentNullException), null
                            }
                    };
        }
    }
}
