namespace Omego.SimpleFtp.Extensions
{
    using System;
    using System.IO;

    using PathAbstractions = System.IO.Abstractions.PathBase;

    public static class PathBase
    {
        public static string GetTempFilePath(this PathAbstractions path, string extension)
        {
            if(path == null) throw new ArgumentNullException(nameof(path));

            var tempDirectory = path.GetTempPath();
            var tempName = Path.GetFileNameWithoutExtension(path.GetTempFileName());
            var tempFileName = Path.ChangeExtension(tempName, extension);

            return Path.Combine(tempDirectory, tempName, tempFileName);
        }
    }
}