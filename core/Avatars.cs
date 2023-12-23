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
using System.IO;
using System.Windows.Markup;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace core
{
    public class Avatars
    {
        private static byte[] server_avatar { get; set; }
        private static byte[] default_avatar { get; set; }

        public static void UpdateServerAvatar(byte[] data)
        {
            server_avatar = data;

            if (UserPool.AUsers != null)
                UserPool.AUsers.ForEachWhere(x => x.SendPacket(TCPOutbound.BotAvatar(x, server_avatar)),
                    x => x.LoggedIn);
        }

        public static void UpdateDefaultAvatar(byte[] data)
        {
            default_avatar = data;

            if (UserPool.AUsers != null)
                UserPool.AUsers.ForEachWhere(x => x.Avatar = default_avatar, x => x.DefaultAvatar);
        }

        internal static byte[] Server(AresClient client)
        {
            if (server_avatar == null)
                return TCPOutbound.BotAvatarCleared(client);
            else
                return TCPOutbound.BotAvatar(client, server_avatar);
        }

        internal static bool GotServerAvatar
        {
            get { return server_avatar != null; }
        }

        internal static byte[] Server(ib0t.ib0tClient client)
        {
            if (server_avatar == null)
                return ib0t.WebOutbound.AvatarClearTo(client, Settings.Get<String>("botName","MainSettings"));
            else
                return ib0t.WebOutbound.AvatarTo(client, Settings.Get<String>("botName", "MainSettings"), server_avatar);
        }

        public static byte[] GetServerAvatar()
        {
            return server_avatar;
        }
        public static void CheckAvatars(ulong time)
        {
            if (default_avatar == null)
                return;

            UserPool.AUsers.ForEachWhere(x =>
            {
                x.Avatar = default_avatar;
                x.OrgAvatar = default_avatar;
                x.AvatarReceived = true;
                x.DefaultAvatar = true;
            },
            x => !x.AvatarReceived &&
                 x.LoggedIn &&
                 time > (x.AvatarTimeout + 10000));
        }

        private static byte[] Default
        {
            get
            {
                if (default_avatar == null)
                    return new byte[] { };
                else
                    return default_avatar;
            }
        }

        private static byte[] Scale(byte[] data)
        {
            byte[] result = null;
            using (Image image = Image.Load<Rgba32>(data))
            {
                Size size = new Size(48, 48);
                image.Mutate(x => x.Resize(size));
                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, new PngEncoder());
                    result = Zip.Compress(memoryStream.ToArray());
                }
            }
            return result;
        }
    }
}
