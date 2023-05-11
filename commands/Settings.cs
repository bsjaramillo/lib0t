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
using lib0t;
using Microsoft.Win32;

namespace commands
{
    class Settings
    {
        public static bool DisableAdmins { get; set; }

        public static bool AdminAnnounce
        {
            get { return Get<bool>("adminAnnounce"); }
            set { Set("adminAnnounce", value); }
        }

        public static bool Colors
        {
            get { return Get<bool>("colors"); }
            set { Set("colors", value); }
        }

        public static bool Stealth
        {
            get { return Get<bool>("stealth"); }
            set { Set("stealth", value); }
        }

        public static bool Url
        {
            get { return Get<bool>("url"); }
            set { Set("url", value); }
        }

        public static byte MuzzleTimeout
        {
            get { return Convert.ToByte(Get<uint>("muzzleTimeout")); }
            set { Set("muzzleTimeout", value); }
        }

        public static bool Clock
        {
            get { return Get<bool>("clock"); }
            set { Set("clock", value); }
        }

        public static bool AnonMonitoring
        {
            get { return Get<bool>("anonMonitoring"); }
            set { Set("anonMonitoring", value); }
        }

        public static bool ShareFileMonitoring
        {
            get { return Get<bool>("shareFileMonitoring"); }
            set { Set("shareFileMonitoring", value); }
        }

        public static bool Filtering
        {
            get { return Get<bool>("filtering"); }
            set { Set("filtering", value); }
        }

        public static bool CapsMonitoring
        {
            get { return Get<bool>("capsMonitoring"); }
            set { Set("capsMonitoring", value); }
        }

        public static bool IdleMonitoring
        {
            get { return Get<bool>("idleMonitoring"); }
            set { Set("idleMonitoring", value); }
        }

        public static bool General
        {
            get { return Get<bool>("general"); }
            set { Set("general", value); }
        }
        public static bool Scribbles
        {
            get { return Get<bool>("scribbles"); }
            set { Set("scribbles", value); }
        }
        public static bool Audios
        {
            get { return Get<bool>("audios"); }
            set { Set("audios", value); }
        }
        public static bool Buzzes
        {
            get { return Get<bool>("buzzes"); }
            set { Set("buzzes", value); }
        }
        public static bool GreetMsg
        {
            get { return Get<bool>("greetMsg"); }
            set { Set("greetMsg", value); }
        }

        public static bool PMGreetMsg
        {
            get { return Get<bool>("pmGreetMsg"); }
            set { Set("pmGreetMsg", value); }
        }

        public static bool RoomInfo
        {
            get { return Get<bool>("roomInfo"); }
            set { Set("roomInfo", value); }
        }

        public static bool LastSeen
        {
            get { return Get<bool>("lastSeen"); }
            set { Set("lastSeen", value); }
        }

        public static bool History
        {
            get { return Get<bool>("history"); }
            set { Set("history", value); }
        }

        public static String PMGreetMsgText
        {
            get { return Get<String>("pmGreetMsgText"); }
            set { Set("pmGreetMsgText", value); }
        }

        public static String Status
        {
            get { return Get<String>("status"); }
            set { Set("status", value); }
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

        public static T Get<T>(String name)
        {
            return (T)Reginux.commandsSettings.GetType().GetProperty(name).GetValue(Reginux.commandsSettings, null);
        }

        public static bool Set(String name, object value)
        {
            Reginux.commandsSettings.GetType().GetProperty(name).SetValue(Reginux.commandsSettings, value, null);
            Reginux.SaveCommandsSettings();
            return true;
        }
    }
}
