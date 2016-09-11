namespace Omego.SimpleFtp
{
    public interface ISystemProcess
    {
        void Kill();

        bool HasExited { get; }
    }
}