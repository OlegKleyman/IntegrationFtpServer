﻿namespace Omego.SimpleFtp.Tests.Integration
{
    using NUnit.Framework;

    [TestFixture]
    public class FtpServerTests
    {
        [Test]
        public void FilesShouldBeListed()
        {
            var server = GetFtpServer();

            server.Start();
        }

        private FtpServer GetFtpServer()
        {
            return new FtpServer(new FtpConfiguration("Home", 3435));
        }
    }
}
