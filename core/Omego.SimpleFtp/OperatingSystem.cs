namespace Omego.SimpleFtp
{
    using System.Diagnostics;

    public class OperatingSystem : IOperatingSystem
    {
        public ISystemProcess StartProcess(string path, string arguments)
        {
            return new SystemProcess(Process.Start(path, arguments));
        }
    }
}