namespace Omego.SimpleFtp
{
    using System;
    using System.IO.Abstractions;

    using Omego.SimpleFtp.Extensions;

    public class FtpServer : IFtpServer
    {
        private readonly IFileSystem fileSystem;

        private readonly IOperatingSystem operatingSystem;

        private ISystemProcess process;

        public FtpServer(FtpConfiguration configuration, IFileSystem fileSystem, IOperatingSystem operatingSystem)
        {
            if(configuration == null) throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;
            this.fileSystem = fileSystem;
            this.operatingSystem = operatingSystem;
        }

        public FtpConfiguration Configuration { get; }

        public FtpServerStatus Status { get; private set; }

        public void Start()
        {
            var tempFilePath = fileSystem.Path.GetTempFilePath("exe");

            if (!fileSystem.Directory.Exists(fileSystem.Path.GetDirectoryName(tempFilePath)))
            {
                fileSystem.Directory.CreateDirectory(fileSystem.Path.GetDirectoryName(tempFilePath));
            }

            fileSystem.File.WriteAllBytes(tempFilePath, Assets.ftpdmin);

            operatingSystem.StartProcess(tempFilePath, $"-p {Configuration.Port} \"{Configuration.HomeDirectory}\"");

            Status = FtpServerStatus.Running;
        }

        public void Stop()
        {
            if (process == null || process.HasExited) throw new InvalidOperationException("Server is not running.");

            process.Kill();
        }
    }
}