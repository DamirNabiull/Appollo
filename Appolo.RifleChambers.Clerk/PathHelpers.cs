using System.IO;

namespace Appolo.RifleChambers.Clerk
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
