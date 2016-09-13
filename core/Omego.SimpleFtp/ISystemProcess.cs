namespace Omego.SimpleFtp
{
    /// <summary>
    /// Represents a system process.
    /// </summary>
    public interface ISystemProcess
    {
        /// <summary>
        /// Kills the <see cref="ISystemProcess"/>.
        /// </summary>
        void Kill();

        /// <summary>
        /// Gets <see cref="HasExited"/>.
        /// </summary>
        /// <value>Whether or not the process has exited.</value>
        bool HasExited { get; }
    }
}