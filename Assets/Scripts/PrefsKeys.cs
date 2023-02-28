namespace ESCape
{
    public class PrefsKeys
    {
        /// <summary>
		/// PlayerPrefs key for player name: UserXXXX
		/// </summary>
        public const string playerName = "TM_playerName";

        /// <summary>
        /// PlayerPrefs key for selected network mode: 0, 1 or 2
        /// </summary>
        public const string networkMode = "TM_networkMode";

        /// <summary>
        /// PlayerPrefs key for selected game mode.
        /// </summary>
        public const string gameMode = "TM_gameMode";

        /// <summary>
        /// Server address for manual connection, e.g. in LAN games.
        /// This is only used when using Photon Networking, as Netcode
        /// does support broadcast and automatic server discovery.
        /// </summary>
        public const string serverAddress = "TM_serverAddress";

        /// <summary>
        /// PlayerPrefs key for background music state: true/false
        /// </summary>
        public const string playMusic = "TM_playMusic";

        /// <summary>
        /// PlayerPrefs key for global audio volume: 0-1 range
        /// </summary>
        public const string appVolume = "TM_appVolume";
      
        /// <summary>
        /// PlayerPrefs key for selected player model: 0/1/2 etc.
        /// </summary>
        public const string activeTank = "TM_activeTank";
    }
}
