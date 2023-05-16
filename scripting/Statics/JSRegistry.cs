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
using Microsoft.Win32;
using Jurassic;
using Jurassic.Library;
using lib0t;

namespace scripting.Statics
{
    class JSRegistry : ObjectInstance
    {
        public JSRegistry(ScriptEngine engine)
            : base(engine)
        {
            this.PopulateFunctions();

            DefineProperty(Engine.Symbol.ToString(), new PropertyDescriptor("Registry", PropertyAttributes.Sealed), true);
        }

        [JSFunction(Name = "exists", Flags = JSFunctionFlags.HasEngineParameter, IsWritable = false, IsEnumerable = true)]
        public static bool Exists(ScriptEngine eng, String key)
        {
            String script = eng.GetGlobalValue("UserData").ToString();
            String str = Reginux.GetScript(key);

            if (str == null)
                return false;

            return true;
        }

        [JSFunction(Name = "getValue", Flags = JSFunctionFlags.HasEngineParameter, IsWritable = false, IsEnumerable = true)]
        public static String GetValue(ScriptEngine eng, String key)
        {
            String script = eng.GetGlobalValue("UserData").ToString();


            String str = Reginux.GetScript(key);

            if (str.Length > 1)
                return str.Substring(2);

            return null;
        }

        [JSFunction(Name = "getKeys", Flags = JSFunctionFlags.HasEngineParameter, IsWritable = false, IsEnumerable = true)]
        public static Objects.JSRegistryKeyCollection GeyKeys(ScriptEngine eng)
        {
            List<String> results = Reginux.GetScripts();

            return new Objects.JSRegistryKeyCollection(eng.Object.InstancePrototype, results.ToArray(), eng.GetGlobalValue("UserData").ToString());
        }

        [JSFunction(Name = "setValue", Flags = JSFunctionFlags.HasEngineParameter, IsWritable = false, IsEnumerable = true)]
        public static bool SetValue(ScriptEngine eng, String key, object value)
        {
            String script = eng.GetGlobalValue("UserData").ToString();

            if (value is int || value is String || value is double || value is ConcatenatedString)
            {

                return Reginux.SetScript(script, "ST" + value.ToString());
            }

            return false;
        }

        [JSFunction(Name = "deleteValue", Flags = JSFunctionFlags.HasEngineParameter, IsWritable = false, IsEnumerable = true)]
        public static bool DeleteValue(ScriptEngine eng, String key)
        {
            String script = eng.GetGlobalValue("UserData").ToString();
            try
            {
                Reginux.DeleteScript(key);
            }
            catch  // value doesn't exist
            {
                return false;
            }

            return true;
        }

        [JSFunction(Name = "clear", Flags = JSFunctionFlags.HasEngineParameter, IsWritable = false, IsEnumerable = true)]
        public static bool Clear(ScriptEngine eng)
        {
            String script = eng.GetGlobalValue("UserData").ToString();

            try
            {
                return Reginux.DeleteScript();
            }
            catch // script subkey doesn't exist
            {
                return false;
            }

            return true;
        }
    }
}
