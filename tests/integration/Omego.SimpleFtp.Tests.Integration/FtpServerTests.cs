namespace Omego.SimpleFtp.Tests.Integration
{
    using System.IO;
    using System.IO.Abstractions;

    using NUnit.Framework;

    [TestFixture]
    public class FtpServerTests
    {
        private readonly string ftpHomeDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        [OneTimeSetUp]
        public void Setup()
        {
            Directory.CreateDirectory(ftpHomeDirectory);
        }

        [Test]
        public void FilesShouldBeListed()
        {
            var server = GetFtpServer();

            server.Start();
        }

        private FtpServer GetFtpServer()
        {
            return new FtpServer(new FtpConfiguration(ftpHomeDirectory, 3435), new FileSystem(), new OperatingSystem());
        }
    }
}
