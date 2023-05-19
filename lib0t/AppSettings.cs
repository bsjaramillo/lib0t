using iconnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace lib0t
{
    public class ExtraSettings
    {
        public bool canCustomnames { get; set; }
        public uint doOnce { get; set; }
        public string ip { get; set; }
        public string urlText { get; set; }
        public string urlLink { get; set; }
        public string urlWebAPI { get; set; }
        public string topic { get; set; }
        public string guid { get; set; }
        public uint torTime { get; set; }
        [JsonIgnore]
        public byte[] GUID { get { return !String.IsNullOrEmpty(this.guid) ? Guid.Parse(this.guid).ToByteArray() : null; } set { this.guid = new Guid(value).ToString(); } }

    }
    public class AvatarsSettings
    {
        public string serverAvatar { set; get; }
        public string defaultAvatar { get; set; }
    }
    public class AdvancedSettings
    {
        public bool enableFileBrowsing { get; set; }
        public bool hideIpAddresses { get; set; }
        public bool enableRoomSearch { get; set; }
        public bool localClientsAutoLogin { get; set; }
        public string ib0tChannelListReceiverScript { get; set; }
        public string UDPHostAddress { get; set; }
        public int preferredLanguage { get; set; }
        public bool restrictions { get; set; }
        public int minimumAge { get; set; }
        public bool rejectMale { get; set; }
        public bool rejectFemale { get; set; }
        public bool rejectUnknown { get; set; }
        public bool autoClearBans { get; set; }
        public int intervalClearBans { get; set; }
        public bool captchaEnabled { get; set; }
        public int captchaMode { get; set; }
        public bool enableJavascriptEngine { get; set; }
        public bool enableInRoomScripting { get; set; }
        public bool scriptsCanChangeLevel { get; set; }
        public int scriptingLevel { get; set; }

    }
    public class LinkingSettings
    {
        public string yourLeafIdentifier { get; set; }
        public int linkMode { get; set; }
        public bool autoReconnect { get; set; }
        public bool allowLinkedAdmin { get; set; }
        public string[] trustedLeaves { get; set; }
    }
    public class AdminSettings
    {
        
        public ILevel addGreetMsg { get { return Reginux.StringToLevel(this._addGreetMsg); } set { this._addGreetMsg = Reginux.LevelToString(value); } }
        public ILevel remGreetMsg { get { return Reginux.StringToLevel(this._remGreetMsg); } set { this._remGreetMsg = Reginux.LevelToString(value); } }
        public ILevel listGreetMsg { get { return Reginux.StringToLevel(this._listGreetMsg); } set { this._listGreetMsg = Reginux.LevelToString(value); } }
        public ILevel pmGreetMsg { get { return Reginux.StringToLevel(this._pmGreetMsg); } set { this._pmGreetMsg = Reginux.LevelToString(value); } }
        public ILevel caps { get { return Reginux.StringToLevel(this._caps); } set { this._caps = Reginux.LevelToString(value); } }
        public ILevel anon { get { return Reginux.StringToLevel(this._anon); } set { this._anon = Reginux.LevelToString(value); } }
        public ILevel general { get { return Reginux.StringToLevel(this._general); } set { this._general = Reginux.LevelToString(value); } }
        public ILevel scribbles { get { return Reginux.StringToLevel(this._scribbles); } set { this._scribbles = Reginux.LevelToString(value); } }
        public ILevel audios { get { return Reginux.StringToLevel(this._audios); } set { this._audios = Reginux.LevelToString(value); } }
        public ILevel buzzes { get { return Reginux.StringToLevel(this._buzzes); } set { this._buzzes = Reginux.LevelToString(value); } }
        public ILevel url { get { return Reginux.StringToLevel(this._url); } set { this._url = Reginux.LevelToString(value); } }
        public ILevel addUrl { get { return Reginux.StringToLevel(this._addUrl); } set { this._addUrl = Reginux.LevelToString(value); } }
        public ILevel remUrl { get { return Reginux.StringToLevel(this._remUrl); } set { this._remUrl = Reginux.LevelToString(value); } }
        public ILevel listUrls { get { return Reginux.StringToLevel(this._listUrls); } set { this._listUrls = Reginux.LevelToString(value); } }
        public ILevel roomInfo { get { return Reginux.StringToLevel(this._roomInfo); } set { this._roomInfo = Reginux.LevelToString(value); } }
        public ILevel status { get { return Reginux.StringToLevel(this._status); } set { this._status = Reginux.LevelToString(value); } }
        public ILevel lastSeen { get { return Reginux.StringToLevel(this._lastSeen); } set { this._lastSeen = Reginux.LevelToString(value); } }
        public ILevel history { get { return Reginux.StringToLevel(this._history); } set { this._history = Reginux.LevelToString(value); } }
        public ILevel filter { get { return Reginux.StringToLevel(this._filter); } set { this._filter = Reginux.LevelToString(value); } }
        public ILevel adminAnnounce { get { return Reginux.StringToLevel(this._adminAnnounce); } set { this._adminAnnounce = Reginux.LevelToString(value); } }
        public ILevel loadTemplate { get { return Reginux.StringToLevel(this._loadTemplate); } set { this._loadTemplate = Reginux.LevelToString(value); } }
        public ILevel listQuarantined { get { return Reginux.StringToLevel(this._listQuarantined); } set { this._listQuarantined = Reginux.LevelToString(value); } }
        public ILevel unQuarantine { get { return Reginux.StringToLevel(this._unQuarantine); } set { this._unQuarantine = Reginux.LevelToString(value); } }
        public ILevel ban { get { return Reginux.StringToLevel(this._ban); } set { this._ban = Reginux.LevelToString(value); } }
        public ILevel ban10 { get { return Reginux.StringToLevel(this._ban10); } set { this._ban10 = Reginux.LevelToString(value); } }
        public ILevel ban60 { get { return Reginux.StringToLevel(this._ban60); } set { this._ban60 = Reginux.LevelToString(value); } }
        public ILevel unBan { get { return Reginux.StringToLevel(this._unBan); } set { this._unBan = Reginux.LevelToString(value); } }
        public ILevel listBans { get { return Reginux.StringToLevel(this._listBans); } set { this._listBans = Reginux.LevelToString(value); } }
        public ILevel kick { get { return Reginux.StringToLevel(this._kick); } set { this._kick = Reginux.LevelToString(value); } }
        public ILevel muzzle { get { return Reginux.StringToLevel(this._muzzle); } set { this._muzzle = Reginux.LevelToString(value); } }
        public ILevel customname { get { return Reginux.StringToLevel(this._customname); } set { this._customname = Reginux.LevelToString(value); } }
        public ILevel unCustomname { get { return Reginux.StringToLevel(this._unCustomname); } set { this._unCustomname = Reginux.LevelToString(value); } }
        public ILevel kewlText { get { return Reginux.StringToLevel(this._kewlText); } set { this._kewlText = Reginux.LevelToString(value); } }
        public ILevel lower { get { return Reginux.StringToLevel(this._lower); } set { this._lower = Reginux.LevelToString(value); } }
        public ILevel unLower { get { return Reginux.StringToLevel(this._unLower); } set { this._unLower = Reginux.LevelToString(value); } }
        public ILevel kiddy { get { return Reginux.StringToLevel(this._kiddy); } set { this._kiddy = Reginux.LevelToString(value); } }
        public ILevel unKiddy { get { return Reginux.StringToLevel(this._unKiddy); } set { this._unKiddy = Reginux.LevelToString(value); } }
        public ILevel echo { get { return Reginux.StringToLevel(this._echo); } set { this._echo = Reginux.LevelToString(value); } }
        public ILevel paint { get { return Reginux.StringToLevel(this._paint); } set { this._paint = Reginux.LevelToString(value); } }
        public ILevel rangeBan { get { return Reginux.StringToLevel(this._rangeBan); } set { this._rangeBan = Reginux.LevelToString(value); } }
        public ILevel rangeUnBan { get { return Reginux.StringToLevel(this._rangeUnBan); } set { this._rangeUnBan = Reginux.LevelToString(value); } }
        public ILevel listRangeBans { get { return Reginux.StringToLevel(this._listRangeBans); } set { this._listRangeBans = Reginux.LevelToString(value); } }
        public ILevel asnBan { get { return Reginux.StringToLevel(this._asnBan); } set { this._asnBan = Reginux.LevelToString(value); } }
        public ILevel asnUnBan { get { return Reginux.StringToLevel(this._asnUnBan); } set { this._asnUnBan = Reginux.LevelToString(value); } }
        public ILevel listAsnBans { get { return Reginux.StringToLevel(this._listAsnBans); } set { this._listAsnBans = Reginux.LevelToString(value); } }
        public ILevel cbans { get { return Reginux.StringToLevel(this._cbans); } set { this._cbans = Reginux.LevelToString(value); } }
        public ILevel pmRoom { get { return Reginux.StringToLevel(this._pmRoom); } set { this._pmRoom = Reginux.LevelToString(value); } }
        public ILevel loadMotd { get { return Reginux.StringToLevel(this._loadMotd); } set { this._loadMotd = Reginux.LevelToString(value); } }
        public ILevel disableAdmins { get { return Reginux.StringToLevel(this._disableAdmins); } set { this._disableAdmins = Reginux.LevelToString(value); } }
        public ILevel stealth { get { return Reginux.StringToLevel(this._stealth); } set { this._stealth = Reginux.LevelToString(value); } }
        public ILevel cloak { get { return Reginux.StringToLevel(this._cloak); } set { this._cloak = Reginux.LevelToString(value); } }
        public ILevel disableAvatar { get { return Reginux.StringToLevel(this._disableAvatar); } set { this._disableAvatar = Reginux.LevelToString(value); } }
        public ILevel changeMessage { get { return Reginux.StringToLevel(this._changeMessage); } set { this._changeMessage = Reginux.LevelToString(value); } }
        public ILevel clearScreen { get { return Reginux.StringToLevel(this._clearScreen); } set { this._clearScreen = Reginux.LevelToString(value); } }
        public ILevel banStats { get { return Reginux.StringToLevel(this._banStats); } set { this._banStats = Reginux.LevelToString(value); } }
        public ILevel colors { get { return Reginux.StringToLevel(this._colors); } set { this._colors = Reginux.LevelToString(value); } }
        public ILevel vspy { get { return Reginux.StringToLevel(this._vspy); } set { this._vspy = Reginux.LevelToString(value); } }
        public ILevel customnames { get { return Reginux.StringToLevel(this._customnames); } set { this._customnames = Reginux.LevelToString(value); } }
        public ILevel urban { get { return Reginux.StringToLevel(this._urban); } set { this._urban = Reginux.LevelToString(value); } }
        public ILevel define { get { return Reginux.StringToLevel(this._define); } set { this._define = Reginux.LevelToString(value); } }
        public ILevel trace { get { return Reginux.StringToLevel(this._trace); } set { this._trace = Reginux.LevelToString(value); } }
        public ILevel whois { get { return Reginux.StringToLevel(this._whois); } set { this._whois = Reginux.LevelToString(value); } }
        public ILevel announce { get { return Reginux.StringToLevel(this._announce); } set { this._announce = Reginux.LevelToString(value); } }
        public ILevel clone { get { return Reginux.StringToLevel(this._clone); } set { this._clone = Reginux.LevelToString(value); } }
        public ILevel move { get { return Reginux.StringToLevel(this._move); } set { this._move = Reginux.LevelToString(value); } }
        public ILevel changename { get { return Reginux.StringToLevel(this._changename); } set { this._changename = Reginux.LevelToString(value); } }
        public ILevel oldname { get { return Reginux.StringToLevel(this._oldname); } set { this._oldname = Reginux.LevelToString(value); } }
        public ILevel bandSend { get { return Reginux.StringToLevel(this._bandSend); } set { this._bandSend = Reginux.LevelToString(value); } }
        public ILevel logSend { get { return Reginux.StringToLevel(this._logSend); } set { this._logSend = Reginux.LevelToString(value); } }
        public ILevel ipSend { get { return Reginux.StringToLevel(this._ipSend); } set { this._ipSend = Reginux.LevelToString(value); } }
        public ILevel stats { get { return Reginux.StringToLevel(this._stats); } set { this._stats = Reginux.LevelToString(value); } }
        public ILevel whowas { get { return Reginux.StringToLevel(this._whowas); } set { this._whowas = Reginux.LevelToString(value); } }
        public ILevel adminMsg { get { return Reginux.StringToLevel(this._adminMsg); } set { this._adminMsg = Reginux.LevelToString(value); } }
        public ILevel link { get { return Reginux.StringToLevel(this._link); } set { this._link = Reginux.LevelToString(value); } }
        public ILevel unLink { get { return Reginux.StringToLevel(this._unLink); } set { this._unLink = Reginux.LevelToString(value); } }
        public ILevel admins { get { return Reginux.StringToLevel(this._admins); } set { this._admins = Reginux.LevelToString(value); } }
        public ILevel roomSearch { get { return Reginux.StringToLevel(this._roomSearch); } set { this._roomSearch = Reginux.LevelToString(value); } }
        public ILevel mtimeout { get { return Reginux.StringToLevel(this._mtimeout); } set { this._mtimeout = Reginux.LevelToString(value); } }
        public ILevel redirect { get { return Reginux.StringToLevel(this._redirect); } set { this._redirect = Reginux.LevelToString(value); } }
        public ILevel shareFiles { get { return Reginux.StringToLevel(this._shareFiles); } set { this._shareFiles = Reginux.LevelToString(value); } }
        public ILevel idle { get { return Reginux.StringToLevel(this._idle); } set { this._idle = Reginux.LevelToString(value); } }
        public ILevel clock { get { return Reginux.StringToLevel(this._clock); } set { this._clock = Reginux.LevelToString(value); } }
        public ILevel addTopic { get { return Reginux.StringToLevel(this._addTopic); } set { this._addTopic = Reginux.LevelToString(value); } }
        public ILevel remTopic { get { return Reginux.StringToLevel(this._remTopic); } set { this._remTopic = Reginux.LevelToString(value); } }

        public ILevel greetMsg { get { return Reginux.StringToLevel(this._greetMsg); } set { this._greetMsg = Reginux.LevelToString(value); } }

        public bool enableBuiltInCommands { get; set; }
        public bool checkPasswordsAgainstClients { get; set; }
        public string ownerPassword { get; set; }


        private string _addGreetMsg { get; set; }
        private string _remGreetMsg { get; set; }
        private string _listGreetMsg { get; set; }
        private string _pmGreetMsg { get; set; }
        private string _caps { get; set; }
        private string _anon { get; set; }
        private string _general { get; set; }
        private string _scribbles { get; set; }
        private string _audios { get; set; }
        private string _buzzes { get; set; }
        private string _url { get; set; }
        private string _addUrl { get; set; }
        private string _remUrl { get; set; }
        private string _listUrls { get; set; }
        private string _roomInfo { get; set; }
        private string _status { get; set; }
        private string _lastSeen { get; set; }
        private string _history { get; set; }
        private string _filter { get; set; }
        private string _adminAnnounce { get; set; }
        private string _loadTemplate { get; set; }
        private string _listQuarantined { get; set; }
        private string _unQuarantine { get; set; }
        private string _ban { get; set; }
        private string _ban10 { get; set; }
        private string _ban60 { get; set; }
        private string _unBan { get; set; }
        private string _listBans { get; set; }
        private string _kick { get; set; }
        private string _muzzle { get; set; }
        private string _customname { get; set; }
        private string _unCustomname { get; set; }
        private string _kewlText { get; set; }
        private string _lower { get; set; }
        private string _unLower { get; set; }
        private string _kiddy { get; set; }
        private string _unKiddy { get; set; }
        private string _echo { get; set; }
        private string _paint { get; set; }
        private string _rangeBan { get; set; }
        private string _rangeUnBan { get; set; }
        private string _listRangeBans { get; set; }
        private string _asnBan { get; set; }
        private string _asnUnBan { get; set; }
        private string _listAsnBans { get; set; }
        private string _cbans { get; set; }
        private string _pmRoom { get; set; }
        private string _loadMotd { get; set; }
        private string _disableAdmins { get; set; }
        private string _stealth { get; set; }
        private string _cloak { get; set; }
        private string _disableAvatar { get; set; }
        private string _changeMessage { get; set; }
        private string _clearScreen { get; set; }
        private string _banStats { get; set; }
        private string _colors { get; set; }
        private string _vspy { get; set; }
        private string _customnames { get; set; }
        private string _urban { get; set; }
        private string _define { get; set; }
        private string _trace { get; set; }
        private string _whois { get; set; }
        private string _announce { get; set; }
        private string _clone { get; set; }
        private string _move { get; set; }
        private string _changename { get; set; }
        private string _oldname { get; set; }
        private string _bandSend { get; set; }
        private string _logSend { get; set; }
        private string _ipSend { get; set; }
        private string _stats { get; set; }
        private string _whowas { get; set; }
        private string _adminMsg { get; set; }
        private string _link { get; set; }
        private string _unLink { get; set; }
        private string _admins { get; set; }
        private string _roomSearch { get; set; }
        private string _mtimeout { get; set; }
        private string _redirect { get; set; }
        private string _shareFiles { get; set; }
        private string _idle { get; set; }
        private string _clock { get; set; }
        private string _addTopic { get; set; }
        private string _remTopic { get; set; }

        private string _greetMsg { get; set; }


    }
    public class MainSettings
    {
        public string roomName { get; set; }
        public ushort roomPort { get; set; }
        public string botName { get; set; }
        public bool chatLogginEnabled { get; set; }
        public bool roomScribblesEnabled { get; set; }
        public bool showRoomInChannelList { get; set; }
        public bool supportVoiceChat { get; set; }
        public bool ib0tSupportEnabled { get; set; }
        public bool fontsEnabled { get; set; }
    }
    public class AppSettings
    {
        public MainSettings MainSettings { get; set; }
        public AdminSettings AdminSettings { get; set; }
        public LinkingSettings LinkingSettings { get; set; }
        public AdvancedSettings AdvancedSettings { get; set; }
        public AvatarsSettings AvatarsSettings { get; set; }
        public ExtraSettings ExtraSettings { get; set; }
    }
}
