using System.IO;
using System.Text.Json;

namespace Appollo.RifleChambers.Baraban_Video
{
    public class Config<T>
    {
        private static Config<T> _instance;
        private T value;

        private Config()
        {
            var configPath = Path.Combine(PathHelpers.ExecutableDirectory(), "config.json");

            value = JsonSerializer.Deserialize<T>(File.ReadAllText(configPath));
        }

        public static void Save()
        {
            if (_instance == null)
            {
                _instance = new Config<T>();
            }

            var configPath = Path.Combine(PathHelpers.ExecutableDirectory(), "config.json");

            File.WriteAllText(configPath, JsonSerializer.Serialize(_instance.value, new JsonSerializerOptions() { WriteIndented = true }));
        }

        public static T Value
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Config<T>();
                }

                return _instance.value;
            }
        }
    }
}
