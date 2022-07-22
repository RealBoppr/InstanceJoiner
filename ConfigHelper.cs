using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace ComfyUtils
{
    public class ConfigHelper<T> where T : class
    {
        public event Action OnConfigUpdated;
        private readonly string ConfigPath;
        public T Config;
        public ConfigHelper(string configPath)
        {
            ConfigPath = configPath;

            if (!File.Exists(ConfigPath))
            {
                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Activator.CreateInstance(typeof(T)), Formatting.Indented));
            }
            Config = JsonConvert.DeserializeObject<T>(File.ReadAllText(ConfigPath));
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Config, Formatting.Indented));

            FileSystemWatcher watcher = new(Path.GetDirectoryName(ConfigPath), Path.GetFileName(ConfigPath))
            {
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };
            watcher.Changed += FileUpdated;
        }
        private void FileUpdated(object obj, FileSystemEventArgs args)
        {
            T fileconfig = JsonConvert.DeserializeObject<T>(File.ReadAllText(ConfigPath));
            foreach (PropertyInfo property in fileconfig.GetType().GetProperties())
            {
                PropertyInfo property0 = Config.GetType().GetProperty(property.Name);
                if (property0 == null)
                {
                    continue;
                }
                if (property.GetValue(fileconfig) != property0.GetValue(Config))
                {
                    Config = fileconfig;
                    OnConfigUpdated?.Invoke();
                    break;
                }
            }
        }
        public void SaveConfig()
        {
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Config, Formatting.Indented));
        }
    }
}
