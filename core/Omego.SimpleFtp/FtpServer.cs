﻿namespace Omego.SimpleFtp
{
    using System;
    using System.IO.Abstractions;

    using Omego.SimpleFtp.Extensions;

    public class FtpServer : IFtpServer
    {
        private readonly IFileSystem fileSystem;

        private readonly IOperatingSystem operatingSystem;

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

            fileSystem.File.WriteAllBytes(tempFilePath, Assets.ftpdmin);

            operatingSystem.StartProcess(tempFilePath, $"-p {Configuration.Port} \"{Configuration.HomeDirectory}\"");

            Status = FtpServerStatus.Running;
        }
    }
}