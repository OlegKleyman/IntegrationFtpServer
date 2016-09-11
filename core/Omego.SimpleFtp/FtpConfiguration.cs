namespace Omego.SimpleFtp
{
    using System;

    public class FtpConfiguration
    {
        public string HomeDirectory { get; private set; }

        public int Port { get; private set; }

        public FtpConfiguration(string homeDirectory, int port)
        {
            if (homeDirectory == null) throw new ArgumentNullException(nameof(homeDirectory));
            if (homeDirectory.Length == 0) throw new ArgumentException("Value cannot be empty.", nameof(homeDirectory));
            if (homeDirectory.Trim(' ').Length == 0) throw new ArgumentException("Value cannot be only whitespace.", nameof(homeDirectory));
            if(port == 0) throw new ArgumentException("Value cannot be 0.", nameof(port));

            HomeDirectory = homeDirectory;
            Port = port;
        }
    }
}