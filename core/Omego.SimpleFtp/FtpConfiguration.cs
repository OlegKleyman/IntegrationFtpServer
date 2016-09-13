namespace Omego.SimpleFtp
{
    using System;

    /// <summary>
    /// Represents an FTP configuration.
    /// </summary>
    public class FtpConfiguration
    {
        /// <summary>
        /// Gets the <see cref="HomeDirectory"/>.
        /// </summary>
        /// <value>The home directory for the FTP server.</value>
        public string HomeDirectory { get; }

        /// <summary>
        /// Gets the <see cref="Port"/>.
        /// </summary>
        /// <value>The FTP server port.</value>
        public int Port { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpConfiguration"/> class.
        /// </summary>
        /// <param name="homeDirectory">The home directory of the FTP server.</param>
        /// <param name="port">The FTP server port.</param>
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