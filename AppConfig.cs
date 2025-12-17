using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Budget
{
    internal class AppConfig
    {
        public string LastBudgetPath { get; set; } = "";

        public static AppConfig LoadConfig()
        {
            if (!File.Exists("config.json"))
                return new AppConfig();

            return JsonSerializer.Deserialize<AppConfig>(File.ReadAllText("config.json"));

        }

        public static void SaveConfig(AppConfig config)
        {
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("config.json", json);
        }
    }
}
