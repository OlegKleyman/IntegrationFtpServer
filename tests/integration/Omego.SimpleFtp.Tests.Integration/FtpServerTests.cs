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
        }

        private FtpServer GetFtpServer()
        {
            return new FtpServer("Home", 9898);
        }
    }
}
