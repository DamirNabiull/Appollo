using System.IO;

namespace Appolo.Helpers
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
