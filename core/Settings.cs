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
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using lib0t;
using iconnect;
using scripting;

namespace core
{
    public class Settings
    {
        public const String RELEASE_URL = "https://hub.docker.com/r/bsjaramillo/lib0t";
        public const String VERSION_CHECK_URL = "https://api.github.com/repos/bsjaramillo/lib0t/releases";
        public const String VERSION_NUMBER = "6.1.1";

        public const String VERSION = "lib0t " + VERSION_NUMBER;
        public const ushort LINK_PROTO = 500;

        public static bool RUNNING { get; set; }
        public static String WebPath { get; set; }

        public static void Reset()
        {
            externalip = null;
            port = 0;
            name = null;
            language = 0;
            hide_ips = 0;

            WebPath = Reginux.Sb0tunixPath+ "/Style";

            if (!Directory.Exists(WebPath))
            {
                Directory.CreateDirectory(WebPath);
                WebPath += "/";
            }
            else WebPath += "/";

            DoOnce.Run();
        }

        public static void ScriptCanLevel(bool can)
        {
            Events.ScriptCanLevel(can);
        }

        private static int hide_ips = 0;
        public static bool HideIps
        {
            get
            {
                if (hide_ips == 0)
                    hide_ips = Get<bool>("hideIpAddresses", "AdvancedSettings") ? 2 : 1;

                return hide_ips == 2;
            }
        }

        private static IPAddress externalip { get; set; }
        public static IPAddress ExternalIP
        {
            get
            {
                if (externalip != null)
                    return externalip;

                byte[] buffer = IPAddress.Parse(Get<string>("ip", "ExtraSettings")).GetAddressBytes();

                if (buffer != null)
                    externalip = new IPAddress(buffer);
                else
                    externalip = IPAddress.Loopback;

                return externalip;
            }
            set
            {
                externalip = value;
                Set("ip","ExtraSettings", externalip.ToString());
            }
        }

        private static ushort port { get; set; }
        public static ushort Port
        {
            get
            {
                if (port == 0)
                    port = Get<ushort>("roomPort", "MainSettings");

                return port;
            }
        }

        private static String name { get; set; }
        public static String Name
        {
            get
            {
                if (name == null)
                    name = Get<String>("roomName","MainSettings");

                return name;
            }
        }

        private static byte language { get; set; }
        public static byte Language
        {
            get
            {
                if (language == 0)
                    language = Convert.ToByte(Get<int>("preferredLanguage", "AdvancedSettings"));

                return language;
            }
        }

        private static String topic { get; set; }
        public static String Topic
        {
            get
            {
                if (topic == null)
                    topic = Get<String>("topic","ExtraSettings");

                if (topic.Length < 2)
                {
                    topic = "welcome to my room";
                    Set("topic", "ExtraSettings", topic);
                }

                return topic;
            }
            set
            {
                topic = value;
                Set("topic","ExtraSettings", topic);
            }
        }

        private static Type[] AcceptableTypes = 
        {
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(String),
            typeof(byte[]),
            typeof(bool)
        };

        public static T Get<T>(String name, String type)
        {
            switch (type)
            {
                case "MainSettings":
                    return (T)Reginux.appSettings.MainSettings.GetType().GetProperty(name).GetValue(Reginux.appSettings.MainSettings,null);
                case "AdminSettings":
                    return (T)Reginux.appSettings.AdminSettings.GetType().GetProperty(name).GetValue(Reginux.appSettings.AdminSettings, null);
                case "LinkingSettings":
                    return (T)Reginux.appSettings.LinkingSettings.GetType().GetProperty(name).GetValue(Reginux.appSettings.LinkingSettings, null);
                case "AdvancedSettings":
                    return (T)Reginux.appSettings.AdvancedSettings.GetType().GetProperty(name).GetValue(Reginux.appSettings.AdvancedSettings, null);
                case "AvatarsSettings":
                    return (T)Reginux.appSettings.AvatarsSettings.GetType().GetProperty(name).GetValue(Reginux.appSettings.AvatarsSettings, null);
                case "ExtraSettings":
                    return (T)Reginux.appSettings.ExtraSettings.GetType().GetProperty(name).GetValue(Reginux.appSettings.ExtraSettings, null);
                case "CommandsSettings":
                    return (T)Reginux.commandsSettings.GetType().GetProperty(name).GetValue(Reginux.commandsSettings, null);
                default:
                    return (T)Reginux.appSettings.ExtraSettings.GetType().GetProperty(name).GetValue(Reginux.appSettings.ExtraSettings, null);
            }

        }

        public static bool Set(String name,String type, object value)
        {
            switch (type)
            {
                case "MainSettings":
                    Reginux.appSettings.MainSettings.GetType().GetProperty(name).SetValue(Reginux.appSettings.MainSettings, value, null);
                    break;
                case "AdminSettings":
                    Reginux.appSettings.AdminSettings.GetType().GetProperty(name).SetValue(Reginux.appSettings.AdminSettings, value, null);
                    break;
                case "LinkingSettings":
                    Reginux.appSettings.LinkingSettings.GetType().GetProperty(name).SetValue(Reginux.appSettings.LinkingSettings, value, null);
                    break;
                case "AdvancedSettings":
                    Reginux.appSettings.AdvancedSettings.GetType().GetProperty(name).SetValue(Reginux.appSettings.AdvancedSettings, value, null);
                    break;
                case "AvatarsSettings":
                    Reginux.appSettings.AvatarsSettings.GetType().GetProperty(name).SetValue(Reginux.appSettings.AvatarsSettings, value, null);
                    break;
                case "ExtraSettings":
                    Reginux.appSettings.ExtraSettings.GetType().GetProperty(name).SetValue(Reginux.appSettings.ExtraSettings, value, null);
                    break;
                case "CommandsSettings":
                    Reginux.commandsSettings.GetType().GetProperty(name).SetValue(Reginux.commandsSettings, value, null);
                    break;
                default:
                    Reginux.appSettings.ExtraSettings.GetType().GetProperty(name).SetValue(Reginux.appSettings.ExtraSettings, value, null);
                    break;
            }
            if (type == "CommandSettings")
            {
                Reginux.SaveCommandsSettings();
            }
            else
            {
                Reginux.SaveAppSettings();
            }
            return true;
        }

        public static IPAddress LocalIP
        {
            get
            {
                foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        return ip;

                return IPAddress.Loopback;
            }
        }
    }
}
