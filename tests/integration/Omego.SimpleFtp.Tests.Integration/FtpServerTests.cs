namespace Omego.SimpleFtp.Tests.Integration
{
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class FtpServerTests
    {
        private readonly string ftpHomeDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        private FtpServer server;

        [OneTimeSetUp]
        public void Setup()
        {
            Directory.CreateDirectory(ftpHomeDirectory);
        }

        [SetUp]
        public void ClearFtpDirectory()
        {
            foreach (var file in Directory.GetFiles(ftpHomeDirectory))
            {
                File.Delete(file);
            }
        }

        [Test]
        public void GetFilesShouldListFiles()
        {
            server = GetFtpServer();

            server.Start();

            File.Create(Path.Combine(ftpHomeDirectory, "someFile.csv")).Dispose();
            File.Create(Path.Combine(ftpHomeDirectory, "TestFile1.txt")).Dispose();

            server.GetFiles(".").ShouldAllBeEquivalentTo(new[] { "someFile.csv", "TestFile1.txt" });
        }

        [TearDown]
        public void StopServer()
        {
            if (server != null && server.Status == FtpServerStatus.Running)
            {
                server.Stop();
            }
        }

        private FtpServer GetFtpServer()
        {
            return new FtpServer(new FtpConfiguration(ftpHomeDirectory, 3435), new FileSystem(), new OperatingSystem());
        }
    }
}
