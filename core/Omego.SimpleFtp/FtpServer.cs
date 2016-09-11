namespace Omego.SimpleFtp
{
    public class FtpServer : IFtpServer
    {
        public FtpServer(FtpConfiguration ftpConfiguration)
        {
            Configuration = ftpConfiguration;
        }

        public FtpConfiguration Configuration { get; }

        public void Start()
        {
            throw new System.NotImplementedException();
        }
    }
}