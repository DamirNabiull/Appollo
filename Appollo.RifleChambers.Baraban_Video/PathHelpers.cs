using System.IO;

namespace Appollo.RifleChambers.Baraban_Video
{
    public class PathHelpers
    {
        public static string ExecutableDirectory()
        {
            var executableFile = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(executableFile);
        }
    }
}
