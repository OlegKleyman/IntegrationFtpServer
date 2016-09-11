namespace Omego.SimpleFtp.Tests.Unit
{
    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class FtpServerTests
    {
        [Test]
        public void ConstructorShouldSetConfigurationProperty()
        {
            var ftpConfiguration = new FtpConfiguration("Home", 21);

            var server = new FtpServer(ftpConfiguration);

            server.Configuration.ShouldBeEquivalentTo(ftpConfiguration);
        }
    }
}
