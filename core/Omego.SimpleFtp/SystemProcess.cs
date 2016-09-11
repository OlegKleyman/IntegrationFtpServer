namespace Omego.SimpleFtp
{
    using System.Diagnostics;

    public class SystemProcess : ISystemProcess
    {
        private readonly Process process;

        public SystemProcess(Process process)
        {
            this.process = process;
        }

        public void Kill()
        {
            process.Kill();
            process.WaitForExit();
        }
    }
}