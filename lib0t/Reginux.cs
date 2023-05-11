using iconnect;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace lib0t
{
    public static class Reginux
    {
        private static string sb0tunixPath = Path.GetFullPath("lib0t");
        public static string Sb0tunixPath
        {
            get { return sb0tunixPath; }
        }

        private static string sb0tunixPathSettings = Path.GetFullPath(sb0tunixPath + "/Settings");
        private static string sb0tunixPathScripting = Path.GetFullPath(sb0tunixPath + "/scripting");
        private static string filenameScripting = Path.GetFullPath(sb0tunixPathScripting + "/scripting.json");
        public static string Sb0tunixPathSettings
        {
            get { return sb0tunixPathSettings; }
        }
        private static string filenamePathAppSettings = Path.GetFullPath(sb0tunixPathSettings + "/AppSettings.json");
        private static string filenamePathCommandsSettings = Path.GetFullPath(sb0tunixPathSettings + "/CommandsSettings.json");
        public static AppSettings appSettings { get; set; }
        public static CommandsSettings commandsSettings { get; set; }
        public static bool LoadAppSettings()
        {
            try
            {
                string jsonString = File.ReadAllText(filenamePathAppSettings);
                appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonString);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static List<String> GetScripts()
        {
            List<String> results = new List<String>();
            try
            {
                if (!Directory.Exists(sb0tunixPathScripting))
                {
                    return results;
                }
                string jsonString = File.ReadAllText(filenameScripting);
                Dictionary<string, string> jo = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                results=jo.Keys.ToList<String>();
                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return results;
            }
        }
        public static string GetScript(string script)
        {

            try
            {
                if (!Directory.Exists(sb0tunixPathScripting))
                {
                    return String.Empty;
                }
                string jsonString = File.ReadAllText(filenameScripting);
                Dictionary<string, string> jo = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                
                return jo.GetValueOrDefault(script,String.Empty);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return String.Empty;
            }
        }
        public static bool SetScript(string script,string value)
        {

            try
            {
                if (!Directory.Exists(sb0tunixPathScripting))
                {
                    Directory.CreateDirectory(sb0tunixPathScripting);
                }
                if (!File.Exists(filenameScripting))
                {
                    File.Create(filenameScripting);
                }
                string jsonString = File.ReadAllText(filenameScripting);
                Dictionary<string, string> jo = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                jo.Add(script, value);
                string outString=JsonConvert.SerializeObject(jo,Formatting.Indented);
                File.WriteAllText(filenameScripting,outString);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool DeleteScript(string script)
        {

            try
            {
                if (!Directory.Exists(sb0tunixPathScripting))
                {
                    return false;
                }
                if (!File.Exists(filenameScripting))
                {
                    return false;
                }
                string jsonString = File.ReadAllText(filenameScripting);
                Dictionary<string, string> jo = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                jo.Remove(script);
                string outString = JsonConvert.SerializeObject(jo, Formatting.Indented);
                File.WriteAllText(filenameScripting, outString);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool DeleteScript()
        {

            try
            {
                if (!Directory.Exists(sb0tunixPathScripting))
                {
                    return false;
                }
                if (!File.Exists(filenameScripting))
                {
                    return false;
                }
                string jsonString = File.ReadAllText(filenameScripting);
                Dictionary<string, string> jo = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                jo.Clear();
                string outString = JsonConvert.SerializeObject(jo, Formatting.Indented);
                File.WriteAllText(filenameScripting, outString);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool LoadCommandsSettings()
        {
            try
            {
                string jsonString = File.ReadAllText(filenamePathCommandsSettings);
                commandsSettings = JsonConvert.DeserializeObject<CommandsSettings>(jsonString);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static void SaveCommandsSettings()
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(commandsSettings, Formatting.Indented);
                File.WriteAllText(filenamePathCommandsSettings, jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void SaveAppSettings()
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
                File.WriteAllText(filenamePathAppSettings, jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static String LevelToString(ILevel level)
        {
            switch (level)
            {
                case ILevel.Administrator:
                    return "Administrator";

                case ILevel.Host:
                    return "Host";

                case ILevel.Moderator:
                    return "Moderator";

                case (ILevel)254:
                    return "Disabled";

                default:
                    return "Regular";
            }
        }

        public static ILevel StringToLevel(String level)
        {
            switch (level)
            {
                case "Administrator":
                    return ILevel.Administrator;

                case "Host":
                    return ILevel.Host;

                case "Moderator":
                    return ILevel.Moderator;

                case "Disabled":
                    return (ILevel)254;

                case "Regular":
                    return ILevel.Regular;

                default:
                    return (ILevel)255; // not found
            }
        }
    }
}