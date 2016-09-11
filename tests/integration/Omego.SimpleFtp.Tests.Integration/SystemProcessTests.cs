namespace Omego.SimpleFtp.Tests.Integration
{
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

        public static SystemProcess GetSystemProcess(Process process)
        {
            return new SystemProcess(process);
        }
    }
}
