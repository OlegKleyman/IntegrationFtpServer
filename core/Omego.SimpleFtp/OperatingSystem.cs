namespace Omego.SimpleFtp
{
    using System.Diagnostics;

    /// <summary>
    /// Represents an operating system.
    /// </summary>
    public class OperatingSystem : IOperatingSystem
    {
        /// <summary>
        /// Starts a process.
        /// </summary>
        /// <param name="path">The path to the process.</param>
        /// <param name="arguments">The arguments to pass to the process.</param>
        /// <returns>A started <see cref="ISystemProcess"/>.</returns>
        public ISystemProcess StartProcess(string path, string arguments)
        {
            return new SystemProcess(Process.Start(path, arguments));
        }
    }
}