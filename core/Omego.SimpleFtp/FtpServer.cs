namespace Omego.SimpleFtp
{
    using System;
    using System.IO.Abstractions;

    public class FtpServer : IFtpServer
    {
        private readonly IFileSystem fileSystem;

        public FtpServer(FtpConfiguration configuration, IFileSystem fileSystem)
        {
            if(configuration == null) throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;
            this.fileSystem = fileSystem;
        }

        public FtpConfiguration Configuration { get; }

        public FtpServerStatus Status { get; }

        public void Start()
        {
            fileSystem.File.WriteAllBytes("ftpdmin.exe", Assets.ftpdmin);
        }
    }
}