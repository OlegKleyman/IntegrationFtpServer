namespace Omego.SimpleFtp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;

    using Omego.SimpleFtp.Extensions;

    /// <summary>
    /// Represents an FTP server.
    /// </summary>
    public class FtpServer : IFtpServer
    {
        private readonly IFileSystem fileSystem;

        private readonly IOperatingSystem operatingSystem;

        private ISystemProcess process;

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpServer"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="FtpConfiguration"/> to configure the server with.</param>
        /// <param name="fileSystem">The <see cref="IFileSystem"/> to use for file operations.</param>
        /// <param name="operatingSystem">The <see cref="IOperatingSystem"/> to use for OS level operations.</param>
        public FtpServer(FtpConfiguration configuration, IFileSystem fileSystem, IOperatingSystem operatingSystem)
        {
            if(configuration == null) throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;
            this.fileSystem = fileSystem;
            this.operatingSystem = operatingSystem;
        }

        /// <summary>
        /// Gets <see cref="Configuration"/>.
        /// </summary>
        /// <value>The <see cref="FtpConfiguration"/> the server is using.</value>
        public FtpConfiguration Configuration { get; }

        /// <summary>
        /// Gets <see cref="FtpServerStatus"/>.
        /// </summary>
        /// <value>The <see cref="FtpServerStatus"/> of the server.</value>
        public FtpServerStatus Status
            => process == null || process.HasExited ? FtpServerStatus.Stopped : FtpServerStatus.Running;

        /// <summary>
        /// Starts the FTP server.
        /// </summary>
        public void Start()
        {
            var tempFilePath = fileSystem.Path.GetTempFilePath("exe");

            if (!fileSystem.Directory.Exists(fileSystem.Path.GetDirectoryName(tempFilePath)))
            {
                fileSystem.Directory.CreateDirectory(fileSystem.Path.GetDirectoryName(tempFilePath));
            }

            fileSystem.File.WriteAllBytes(tempFilePath, Assets.ftpdmin);

            process = operatingSystem.StartProcess(tempFilePath, $"-p {Configuration.Port} \"{Configuration.HomeDirectory}\"");
        }

        /// <summary>
        /// Stops the FTP server.
        /// </summary>
        public void Stop()
        {
            if (Status == FtpServerStatus.Stopped) throw new InvalidOperationException("Server is not running.");

            process.Kill();
        }

        /// <summary>
        /// Gets the files on the FTP server.
        /// </summary>
        /// <param name="relativePath">The path to the directory relative to the home directory to get the files in.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="string"/> containing the file names.</returns>
        public IEnumerable<string> GetFiles(string relativePath)
        {
            foreach (var file in fileSystem.Directory.GetFiles(Path.Combine(Configuration.HomeDirectory, relativePath)))
            {
                yield return Path.GetFileName(file);
            }
        }
    }
}