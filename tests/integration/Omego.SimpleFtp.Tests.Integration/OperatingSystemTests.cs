namespace Omego.SimpleFtp.Tests.Integration
{
    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class OperatingSystemTests
    {
        private static OperatingSystem GetOperatingSystem()
        {
            return new OperatingSystem();
        }

        [Test]
        public void StartProcessShouldStartTheProcess()
        {
            var operatingSystem = GetOperatingSystem();

            var systemProcess = operatingSystem.StartProcess("cmd.exe", null);

            systemProcess.Should().NotBeNull();

            systemProcess.Kill();
        }
    }
}