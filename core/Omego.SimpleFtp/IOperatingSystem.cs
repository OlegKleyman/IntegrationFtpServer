namespace Omego.SimpleFtp
{
    /// <summary>
    /// Represents an operating system.
    /// </summary>
    public interface IOperatingSystem
    {
        /// <summary>
        /// Starts a process.
        /// </summary>
        /// <param name="path">The path to the process.</param>
        /// <param name="arguments">The arguments to pass to the process.</param>
        /// <returns>A started <see cref="ISystemProcess"/>.</returns>
        ISystemProcess StartProcess(string path, string arguments);
    }
}