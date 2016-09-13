namespace Omego.SimpleFtp.Tests.Integration
{
    using System.IO;
    using System.IO.Abstractions;
    using System.Net;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class FtpServerTests
    {
        [SetUp]
        public void ClearFtpDirectory()
        {
            foreach (var file in Directory.GetFiles(ftpHomeDirectory)) File.Delete(file);

            foreach (var file in Directory.GetFiles(Path.Combine(ftpHomeDirectory, "someDirectory"))) File.Delete(file);
        }

        [TearDown]
        public void StopServer()
        {
            if ((server != null) && (server.Status == FtpServerStatus.Running)) server.Stop();
        }

        private readonly string ftpHomeDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        private FtpServer server;

        [OneTimeSetUp]
        public void Setup()
        {
            Directory.CreateDirectory(ftpHomeDirectory).CreateSubdirectory("someDirectory");
        }

        private FtpServer GetFtpServer()
        {
            return new FtpServer(new FtpConfiguration(ftpHomeDirectory, 3435), new FileSystem(), new OperatingSystem());
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

        [Test]
        public void UploadingFileShouldUploadFile()
        {
            server = GetFtpServer();

            server.Start();

            var ftpRequest = (FtpWebRequest)WebRequest.Create(@"ftp://localhost:3435/someDirectory/someFile.csv");

            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            using (var request = ftpRequest.GetRequestStream())
            {
                using (var writer = new StreamWriter(request))
                {
                    writer.WriteLine("test");
                }
            }

            server.GetFiles("someDirectory").ShouldAllBeEquivalentTo(new[] { "someFile.csv" });
        }
    }
}