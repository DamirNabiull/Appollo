using System.IO;

namespace Appollo.VkusvillNewYearEvent
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
