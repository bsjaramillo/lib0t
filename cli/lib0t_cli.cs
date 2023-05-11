using core;
using lib0t;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml.Media.Imaging;

namespace cli
{
    internal class lib0t_cli
    {
        private ServerCore server { get; set; }
        //private RenderTargetBitmap FileToSizedImageSource(String file, int width, int height)
        //{
        //    byte[] data = File.ReadAllBytes(file);
        //    return this.FileToSizedImageSource(data, width, height);
        //}
        //private RenderTargetBitmap FileToSizedImageSource(byte[] data, int width, int height)
        //{
        //    RenderTargetBitmap resizedImage = new RenderTargetBitmap(width, height, 96, 96);
        //    using (MemoryStream ms = new MemoryStream(data))
        //    {
        //        BitmapImage img = new BitmapImage();
        //        img.BeginInit();
        //        img.StreamSource = ms;
        //        img.EndInit();
        //        Rect rect = new Rect(0, 0, width, height);
        //        DrawingVisual drawingVisual = new DrawingVisual();

        //        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
        //            drawingContext.DrawImage(img, rect);

        //        resizedImage.Render(drawingVisual);
        //    }

        //    return resizedImage;
        //}
        //private byte[] BitmapSourceToArray(BitmapSource img)
        //{
        //    byte[] result;

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        //        encoder.Frames.Add(BitmapFrame.Create(img));
        //        encoder.Save(ms);
        //        result = ms.ToArray();
        //    }

        //    return result;
        //}
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
                String path = Reginux.Sb0tunixPath+"/Avatars/server";

                //if (File.Exists(path))
                //{
                //    RenderTargetBitmap resizedImage = this.FileToSizedImageSource(path, 90, 90);
                //    byte[] data = this.BitmapSourceToArray(resizedImage);
                //    Avatars.UpdateServerAvatar(data);
                //}
            }
            
            catch (Exception e){
                throw (e);
            }
            try
            {
                String path = Reginux.Sb0tunixPath + "/Avatars/default";
                //if (File.Exists(path))
                //{
                //    RenderTargetBitmap resizedImage = this.FileToSizedImageSource(path, 90, 90);
                //    byte[] data = this.BitmapSourceToArray(resizedImage);
                //    Avatars.UpdateDefaultAvatar(data);
                //}
            }
            catch (Exception e)
            {
                throw (e);
            }
            if (!this.server.Open())
                Environment.Exit(0);
            Console.WriteLine("Sala {0} creada con éxito con puerto {1}", Reginux.appSettings.MainSettings.roomName, Reginux.appSettings.MainSettings.roomPort);
        }
        static void Main(string[] args)
        {


            try
            {
                if (Reginux.LoadAppSettings() && Reginux.LoadCommandsSettings())
                {
                    lib0t_cli sb0TunixObj = new lib0t_cli();
                    sb0TunixObj.InitServer();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
