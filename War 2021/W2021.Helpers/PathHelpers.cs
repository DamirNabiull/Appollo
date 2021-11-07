using System.IO;

namespace W2021.Helpers
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
