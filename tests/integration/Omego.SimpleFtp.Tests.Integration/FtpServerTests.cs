namespace Omego.SimpleFtp.Tests.Integration
{
    using NUnit.Framework;

    [TestFixture]
    public class FtpServerTests
    {
        [Test]
        public void FilesShouldBeListed()
        {
            var server = GetFtpServer();

            server.Start("Home", 9898);
        }

        private FtpServer GetFtpServer()
        {
            return new FtpServer();
        }
    }
}
