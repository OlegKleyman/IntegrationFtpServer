namespace Omego.SimpleFtp
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an FTP server
    /// </summary>
    public interface IFtpServer
    {
        /// <summary>
        /// Gets <see cref="FtpServerStatus"/>.
        /// </summary>
        /// <value>The <see cref="FtpServerStatus"/> of the server.</value>
        FtpServerStatus Status { get; }

        /// <summary>
        /// Starts the FTP server.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the FTP server.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets the files on the FTP server.
        /// </summary>
        /// <param name="relativePath">The path to the directory relative to the home directory to get the files in.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="string"/> containing the file names.</returns>
        IEnumerable<string> GetFiles(string relativePath);
    }
}