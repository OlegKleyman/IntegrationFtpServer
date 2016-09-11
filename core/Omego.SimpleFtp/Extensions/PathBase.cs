namespace Omego.SimpleFtp.Extensions
{
    using System.IO;

    using PathAbstractions = System.IO.Abstractions.PathBase;

    public static class PathBase
    {
        public static string GetTempFilePath(this PathAbstractions path, string extension)
        {
            var tempDirectory = path.GetTempPath();
            var tempName = Path.GetFileNameWithoutExtension(path.GetTempFileName());
            var tempFileName = Path.ChangeExtension(tempName, extension);

            return Path.Combine(tempDirectory, tempName, tempFileName);
        }
    }
}