namespace Omego.SimpleFtp.Tests.Integration
{
    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class OperatingSystemTests
    {
        [Test]
        public void StartProcessShouldStartTheProcess()
        {
            var operatingSystem = GetOperatingSystem();

            var systemProcess = operatingSystem.StartProcess("cmd.exe", null);

            systemProcess.Should().NotBeNull();

            systemProcess.Kill();
        }

        private static OperatingSystem GetOperatingSystem()
        {
            return new OperatingSystem();
        }
    }
}
