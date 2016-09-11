namespace Omego.SimpleFtp
{
    using System;

    public class FtpServer : IFtpServer
    {
        public FtpServer(FtpConfiguration configuration)
        {
            if(configuration == null) throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;
        }

        public FtpConfiguration Configuration { get; }

        public void Start()
        {
            throw new System.NotImplementedException();
        }
    }
}