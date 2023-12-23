/*
    sb0t ares chat server
    Copyright (C) 2017  AresChat

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using Jurassic;
using Jurassic.Library;
using System.IO;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SharpDX;

namespace scripting.Instances
{
    class JSScribbleInstance : ObjectInstance
    {
        private bool busy = false;

        public JSScribbleInstance(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFunctions();

            DefineProperty(Engine.Symbol.ToString(), new PropertyDescriptor("Scribble", PropertyAttributes.Sealed), true);
        }

        [JSProperty(Name = "src")]
        public String Source { get; set; }

        [JSProperty(Name = "oncomplete")]
        public UserDefinedFunction Callback { get; set; }

        [JSFunction(Name = "download", IsWritable = false, IsEnumerable = true)]
        public bool Download(object a)
        {
            if (this.busy)
                return false;

            Thread thread = new Thread(new ThreadStart(() =>
            {
                this.busy = true;
                String arg = String.Empty;

                if (!(a is Undefined) && a != null)
                    arg = a.ToString();

                Objects.JSScribbleImage result = new Objects.JSScribbleImage(this.Engine.Object.InstancePrototype)
                {
                    Callback = this.Callback,
                    Data = null,
                    ScriptName = this.Engine.GetGlobalValue("UserData").ToString(),
                    Arg = arg,
                    URL = this.Source
                };

                try
                {
                    WebRequest request = WebRequest.Create(this.Source);
                    List<byte> bytes_in = new List<byte>();

                    using (WebResponse response = request.GetResponse())
                    {
                        int received = 0;
                        byte[] buf = new byte[1024];

                        using (Stream stream = response.GetResponseStream())
                            while ((received = stream.Read(buf, 0, 1024)) > 0)
                                bytes_in.AddRange(buf.Take(received));
                    }
                    using (Image image = Image.Load<Rgba32>(bytes_in.ToArray()))
                    {
                        int img_x = image.Width;
                        int img_y = image.Height;

                        if (img_x > 384)
                        {
                            img_x = 384;
                            img_y = image.Height - (int)Math.Floor(Math.Floor((double)image.Height / 100) * Math.Floor(((double)(image.Width - 384) / image.Width) * 100));
                        }

                        if (img_y > 384)
                        {
                            img_x -= (int)Math.Floor(Math.Floor((double)img_x / 100) * Math.Floor(((double)(img_y - 384) / img_y) * 100));
                            img_y = 384;
                        }
                        Size size = new Size(img_x, img_y);
                        image.Mutate(x => x.Resize(size));
                        using (var memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, new PngEncoder());
                            result.Height = image.Height;
                            result.Data = Server.Compression.Compress(memoryStream.ToArray());
                            bytes_in.Clear();
                        }
                    }
                }
                catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine(e);
                }
                ScriptManager.Callbacks.Enqueue(result);
                this.busy = false;
            }));

            thread.Start();

            return true;
        }

        private String[] bad_chars = new String[]
        {
            "/",
            "\\",
            " ",
        };

        [JSFunction(Name = "load", IsWritable = false, IsEnumerable = true)]
        public Objects.JSScribbleImage Load(object a)
        {
            Objects.JSScribbleImage scr = new Objects.JSScribbleImage(this.Engine.Object.InstancePrototype);

            if (a is String || a is ConcatenatedString)
            {
                String filename = a.ToString();

                if (filename.Length > 1)
                    if (bad_chars.Count<String>(x => filename.Contains(x)) == 0)
                    {
                        String path = Path.Combine(Server.DataPath, this.Engine.GetGlobalValue("UserData").ToString(), "data", filename);

                        try
                        {
                            if (File.Exists(path))
                            {
                                byte[] data = File.ReadAllBytes(path);

                                try
                                {
                                    using (Image image = Image.Load(path))
                                    {
                                        int img_x = image.Width;
                                        int img_y = image.Height;

                                        if (img_x > 384)
                                        {
                                            img_x = 384;
                                            img_y = image.Height - (int)Math.Floor(Math.Floor((double)image.Height / 100) * Math.Floor(((double)(image.Width - 384) / image.Width) * 100));
                                        }

                                        if (img_y > 384)
                                        {
                                            img_x -= (int)Math.Floor(Math.Floor((double)img_x / 100) * Math.Floor(((double)(img_y - 384) / img_y) * 100));
                                            img_y = 384;
                                        }
                                        Size size = new Size(img_x, img_y);
                                        image.Mutate(x => x.Resize(size));
                                        using (var memoryStream = new MemoryStream())
                                        {
                                            image.Save(memoryStream, new PngEncoder());
                                            scr.Height = image.Height;
                                            scr.Data = Server.Compression.Compress(memoryStream.ToArray());
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
            }

            return scr;
        }
    }
}
