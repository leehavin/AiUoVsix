using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AiUoVsix.Command.EntityFrameworkCore.Models
{
    public class AppConfiguration
    {
        public ObservableCollection<DatabaseConnection> DatabaseConnections { get; set; } = new();
    }
    
    public static class ConfigurationManager
    {
        private static readonly string ConfigFilePath = "aiuo.visx.json";
        
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };
        
        public static AppConfiguration LoadConfiguration()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    var config = JsonSerializer.Deserialize<AppConfiguration>(json, JsonOptions);
                    return config ?? new AppConfiguration();
                }
            }
            catch (Exception ex)
            {
                // 记录错误日志，这里简单处理
                Console.WriteLine($"加载配置文件失败: {ex.Message}");
            }
            
            return new AppConfiguration();
        }
        
        public static void SaveConfiguration(AppConfiguration configuration)
        {
            try
            {
                var json = JsonSerializer.Serialize(configuration, JsonOptions);
                File.WriteAllText(ConfigFilePath, json);
            }
            catch (Exception ex)
            {
                // 记录错误日志，这里简单处理
                Console.WriteLine($"保存配置文件失败: {ex.Message}");
                throw;
            }
        }
    }
}