namespace Omego.SimpleFtp
{
    using System;
    using System.Diagnostics;

    public class SystemProcess : ISystemProcess
    {
        private readonly Process process;

        public SystemProcess(Process process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));

            this.process = process;
        }

        public void Kill()
        {
            process.Kill();
            process.WaitForExit();
        }

        public bool HasExited => process.HasExited;
    }
}