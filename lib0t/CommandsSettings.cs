using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib0t
{
    public class CommandsSettings
    {
        public bool adminAnnounce { get; set; }
        public bool colors { get; set; }
        public bool stealth { get; set; }
        public bool url { get; set; }
        public uint muzzleTimeout { get; set; }
        public bool clock { get; set; }
        public bool anonMonitoring { get; set; }
        public bool shareFileMonitoring { get; set; }
        public bool filtering { get; set; }
        public bool capsMonitoring { get; set; }
        public bool idleMonitoring { get; set; }
        public bool general { get; set; }
        public bool scribbles { get; set; }
        public bool audios { get; set; }
        public bool buzzes { get; set; }
        public bool greetMsg { get; set; }
        public bool pmGreetMsg { get; set; }
        public bool roomInfo { get; set; }
        public bool lastSeen { get; set; }
        public bool history { get; set; }
        public string pmGreetMsgText { get; set; }
        public string status { get; set; }

    }
}
