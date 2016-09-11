namespace Omego.SimpleFtp
{
    public interface IOperatingSystem
    {
        ISystemProcess StartProcess(string path, string arguments);
    }
}