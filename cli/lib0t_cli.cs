using core;
using ImageMagick;
using lib0t;
using System.Text;

namespace cli
{
    internal class lib0t_cli
    {
        private ServerCore server { get; set; }
        
        private byte[] FileToSizedImageSource(string path, int x, int y)
        {
            byte[] result = null;
            using (var raw = new MagickImage(path))
            {
                raw.Resize(x, y);
                result =raw.ToByteArray();
            }
            return result;
        }
        private void CreateImporter1()
        {
            String path = Reginux.Sb0tunixPath + "/TEMPLATE IMPORTER";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, "README.txt");

            if (!File.Exists(path))
            {
                List<String> lines = new List<String>();
                lines.Add("lib0t 6 - TEMPLATE IMPORTER");
                lines.Add(String.Empty);
                lines.Add("You can import template files from old version of sb0t.");
                lines.Add(String.Empty);
                lines.Add("- To do this, simply paste your old template into this folder.");
                lines.Add("- Then start lib0t.");
                lines.Add("- Or type #loadtemplate");
                lines.Add("- lib0t will then attempt to read your old template.txt file.");
                lines.Add("- A file called LOG.txt will be created.");
                lines.Add("- This log file will confirm the success of the import.");
                lines.Add("- sb0t will then delete the old template from this import folder.");
                lines.Add(String.Empty);
                lines.Add("The new template file for this version of sb0t is called strings.txt");
                lines.Add("and is located in the main data folder.");
                lines.Add(String.Empty);

                try { File.WriteAllLines(path, lines.ToArray()); }
                catch { }
            }
        }

        private void CreateImporter2()
        {
            String path = Reginux.Sb0tunixPath + "/FILTER IMPORTER";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, "README.txt");

            if (!File.Exists(path))
            {
                List<String> lines = new List<String>();
                lines.Add("sb0t 5 - FILTER IMPORTER");
                lines.Add(String.Empty);
                lines.Add("You can import filter files from old version of sb0t (4.xx).");
                lines.Add(String.Empty);
                lines.Add("- To do this, simply paste your old filter files (wordfilters.xml, joinfilters.xml,");
                lines.Add("  filefilters.xml, pmfilters.xml) into this folder then start sb0t.");
                lines.Add("- sb0t will then attempt to read your old filter files.");
                lines.Add("- A file called LOG.txt will be created.");
                lines.Add("- This log file will confirm the success of the import.");
                lines.Add("- sb0t will then delete the old filter files from this import folder.");
                lines.Add(String.Empty);
                lines.Add("The new filter files for this version of sb0t will be");
                lines.Add("located in the main data folder.");
                lines.Add(String.Empty);

                try { File.WriteAllLines(path, lines.ToArray()); }
                catch { }
            }
        }
        private void SetLinkIdent()
        {
            List<byte> list = new List<byte>();

            if (Settings.Get<byte[]>("GUID", "ExtraSettings") != null)
            {
                byte[] name = Encoding.UTF8.GetBytes(Settings.Get<String>("roomName","MainSettings"));
                list.Add((byte)name.Length);
                list.AddRange(name);
                list.AddRange(Settings.Get<byte[]>("GUID", "ExtraSettings"));
                list.Reverse();
                Settings.Set("yourLeafIdentifier", "LinkingSettings", "sblnk://" + Convert.ToBase64String(list.ToArray()));
            }
        }

        private void AddLinks()
        {
            try{
                core.LinkHub.TrustedLeavesManager.Init();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            string[] links = Settings.Get<string[]>("trustedLeaves", "LinkingSettings");
            foreach (string link in links)
            {
                try
                {
                    if (link.StartsWith("sblnk://"))
                    {
                        string text = link.Substring(8);
                        byte[] buffer = Convert.FromBase64String(text);
                        Array.Reverse(buffer);
                        byte len = buffer[0];
                        String name = Encoding.UTF8.GetString(buffer, 1, len);
                        Guid guid = new Guid(buffer.Skip(1 + len).ToArray());
                        core.LinkHub.TrustedLeafItem item = new core.LinkHub.TrustedLeafItem
                        {
                            Guid = guid,
                            Name = name
                        };
                        core.LinkHub.TrustedLeavesManager.AddItem(item);
                    }
                }
                catch (Exception ex)
                {
                    LogUpdate(null, new ServerLogEventArgs { Message = ex.Message });
                }


            }
        }
        private void InitServer()
        {
            this.server = new ServerCore();
            core.Extensions.ExtensionManager.Setup();
            ServerCore.LogUpdate += this.LogUpdate;
            if (String.IsNullOrEmpty(Reginux.appSettings.MainSettings.roomName))
                Settings.Set("roomName", "MainSettings", "my chatroom");
            if (String.IsNullOrEmpty(Reginux.appSettings.ExtraSettings.topic))
                Settings.Set("topic", "ExtraSettings", "my chatroom");
            if (String.IsNullOrEmpty(Reginux.appSettings.AdminSettings.ownerPassword))
                Settings.Set("ownerPassword", "AdminSettings", "123456");
            if (String.IsNullOrEmpty(Reginux.appSettings.MainSettings.roomPort.ToString()))
                Settings.Set("roomPort", "MainSettings", 54321);
            if (String.IsNullOrEmpty(Reginux.appSettings.AdvancedSettings.livescriptEndpoint))
                Settings.Set("livescriptEndpoint", "AdvancedSettings", "http://198.58.100.116:3000/");

            byte[] link_guid = Settings.Get<byte[]>("GUID","ExtraSettings");

            if (link_guid == null)
            {
                link_guid = Guid.NewGuid().ToByteArray();
                Settings.Set("GUID", "ExtraSettings",link_guid);
            }
            this.CreateImporter1();
            this.CreateImporter2();
            this.SetLinkIdent();
            Reginux.SaveAppSettings();
            
            try
            {
                this.AddLinks();
                String path = Reginux.Sb0tunixPath+"/Avatars/"+Reginux.appSettings.AvatarsSettings.serverAvatar;

                if (File.Exists(path))
                {
                    byte[] data = this.FileToSizedImageSource(path, 48, 48);
                    if(data!=null)
                        Avatars.UpdateServerAvatar(data);
                }
            }

            catch (Exception e){
                throw (e);
            }
            try
            {
                String path = Reginux.Sb0tunixPath + "/Avatars/" + Reginux.appSettings.AvatarsSettings.defaultAvatar;
                if (File.Exists(path))
                {
                    byte[] data = this.FileToSizedImageSource(path, 48, 48);
                    if (data != null)
                        Avatars.UpdateDefaultAvatar(data);
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            if (!this.server.Open())
            {
                throw new Exception("No se pudo iniciar o crear la sala de chat");
            }
            else
            {
                Console.WriteLine("Sala {0} creada con éxito con puerto {1}", Reginux.appSettings.MainSettings.roomName, Reginux.appSettings.MainSettings.roomPort);
            }

        }
        
        static void Main(string[] args)
        {
            try
            {
                if (args.Length==0)
                {
                    Console.WriteLine("Error creando la sala, se necesita el nombre del workspace");
                    Environment.Exit(0);
                }
                Reginux.Sb0tunixPath = args[0];
                if (!Directory.Exists(Reginux.Sb0tunixPath))
                {
                    Console.WriteLine("Error creando la sala, no existe el workspace: "+ Reginux.Sb0tunixPath);
                    Environment.Exit(0);
                }
                if (Reginux.LoadAppSettings() && Reginux.LoadCommandsSettings())
                {
                    lib0t_cli sb0TunixObj = new lib0t_cli();
                    sb0TunixObj.InitServer();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("No se pudo inciar la sala de chat, motivo: {0}",e.Message);
                Environment.Exit(0);
            }
        }

        private void LogUpdate(object sender, ServerLogEventArgs e)
        {
            String str = "";

            if (!String.IsNullOrEmpty(e.Message))
                str = e.Message;

            if (e.Error != null)
                str += "\r\n" + e.Error.Message + "\r\n" + e.Error.StackTrace;

            try
            {
                String path = Reginux.Sb0tunixPath;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path += "/serverlog.txt";

                using (StreamWriter writer = File.Exists(path) ? File.AppendText(path) : File.CreateText(path))
                    writer.WriteLine(DateTime.UtcNow + " " + str);
            }
            catch { }
        }
    }
}
