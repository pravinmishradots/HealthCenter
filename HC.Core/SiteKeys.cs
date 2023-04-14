using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Core
{
    public static class SiteKeys
    {
        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        public static string Domain => _configuration["Domain"];
        public static string PasswordHashUpdateDate => _configuration["PasswordHashUpdateDate"];
        public static string PasswordOldHashExpiry => _configuration["PasswordOldHashExpiry"];
        public static string UserId;



        public static void SaveLog(string LogMessage, string LogFullPath = "D:\\local\\Dropship Manager\\DSDropShip\\DSDropShip\\Logs")
        {
            try
            {
                LogMessage = $"{LogMessage} - {DateTime.Now.ToString("dd-mmm-yyyy HH:mm:ss tt")}";
                string name = DateTime.Now.Date.Ticks + ".txt";
                string filepath = LogFullPath + name;
                if (!Directory.Exists(LogFullPath))
                    Directory.CreateDirectory(LogFullPath);
                else
                {
                    if (!File.Exists(filepath))
                    {
                        FileStream f = File.Create(filepath);
                        f.Dispose();
                    }
                    using (StreamWriter w = File.AppendText(filepath))
                    {
                        w.WriteLine(LogMessage + Environment.NewLine);
                    }
                }
            }
            catch { }
        }
    }
}
