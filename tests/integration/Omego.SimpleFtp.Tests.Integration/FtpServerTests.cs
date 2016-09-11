namespace Omego.SimpleFtp.Tests.Integration
{
    using NUnit.Framework;

    [TestFixture]
    public class FtpServerTests
    {
        [Test]
        public void FilesShouldBeListed()
        {
            var server = FtpServer.Start("Home", 9898);
        }
    }
}
