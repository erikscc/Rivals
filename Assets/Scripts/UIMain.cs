using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ESCape
{
    public class UIMain : MonoBehaviour
    {
        /// <summary>
        /// Window object for loading screen between connecting and scene switch.
        /// </summary>
        public GameObject loadingWindow;

        /// <summary>
        /// Window object for displaying errors with the connection or timeouts.
        /// </summary>
        public GameObject connectionErrorWindow;

        /// <summary>
        /// Window object for displaying errors with the billing actions.
        /// </summary>
        public GameObject billingErrorWindow;

        /// <summary>
		/// Settings: input field for the player name.
		/// </summary>
		public InputField nameField;

        /// <summary>
        /// Settings: dropdown selection for network mode.
        /// </summary>
        public Dropdown networkDrop;

        /// <summary>
        /// Dropdown selection for preferred game mode.
        /// </summary>
        public Dropdown gameModeDrop;

        /// <summary>
		/// Settings: input field for manual server address,
        /// hosting a server in a private network (Photon only).
		/// </summary>
		public InputField serverField;

        /// <summary>
        /// Settings: checkbox for playing background music.
        /// </summary>
        public Toggle musicToggle;

        /// <summary>
        /// Settings: slider for adjusting game sound volume.
        /// </summary>
        public Slider volumeSlider;


        //initialize player selection in Settings window
        //if this is the first time launching the game, set initial values
        void Start()
        {
            //set initial values for all settings
            if (!PlayerPrefs.HasKey(PrefsKeys.playerName)) PlayerPrefs.SetString(PrefsKeys.playerName, "User" + System.String.Format("{0:0000}", Random.Range(1, 9999)));
            if (!PlayerPrefs.HasKey(PrefsKeys.networkMode)) PlayerPrefs.SetInt(PrefsKeys.networkMode, 0);
            if (!PlayerPrefs.HasKey(PrefsKeys.gameMode)) PlayerPrefs.SetInt(PrefsKeys.gameMode, 0);
            if (!PlayerPrefs.HasKey(PrefsKeys.serverAddress)) PlayerPrefs.SetString(PrefsKeys.serverAddress, "127.0.0.1");
            if (!PlayerPrefs.HasKey(PrefsKeys.playMusic)) PlayerPrefs.SetString(PrefsKeys.playMusic, "true");
            if (!PlayerPrefs.HasKey(PrefsKeys.appVolume)) PlayerPrefs.SetFloat(PrefsKeys.appVolume, 1f);
            if (!PlayerPrefs.HasKey(PrefsKeys.activeTank)) PlayerPrefs.SetString(PrefsKeys.activeTank, Encryptor.Encrypt("0"));

            PlayerPrefs.Save();

            //read the selections and set them in the corresponding UI elements
            nameField.text = PlayerPrefs.GetString(PrefsKeys.playerName);
            networkDrop.value = PlayerPrefs.GetInt(PrefsKeys.networkMode);
            gameModeDrop.value = PlayerPrefs.GetInt(PrefsKeys.gameMode);
            serverField.text = PlayerPrefs.GetString(PrefsKeys.serverAddress);
            musicToggle.isOn = bool.Parse(PlayerPrefs.GetString(PrefsKeys.playMusic));
            volumeSlider.value = PlayerPrefs.GetFloat(PrefsKeys.appVolume);

            //call the onValueChanged callbacks once with their saved values
            OnMusicChanged(musicToggle.isOn);
            OnVolumeChanged(volumeSlider.value);

            //listen to network connection and IAP billing errors
            NetworkManagerCustom.connectionFailedEvent += OnConnectionError;
            UnityIAPManager.purchaseFailedEvent += OnBillingError;
        }


        /// <summary>
        /// Tries to enter the game scene. Sets the loading screen active while connecting to the
        /// Matchmaker and starts the timeout coroutine at the same time.
        /// </summary>
        public void Play()
        {
            loadingWindow.SetActive(true);
            NetworkManagerCustom.StartMatch((NetworkMode)PlayerPrefs.GetInt(PrefsKeys.networkMode));
            StartCoroutine(HandleTimeout());
        }


        //coroutine that waits 10 seconds before cancelling joining a match
        IEnumerator HandleTimeout()
        {
            yield return new WaitForSeconds(10);

            //timeout has passed, we would like to stop joining a game now
            Photon.Pun.PhotonNetwork.Disconnect();
            //display connection issue window
            OnConnectionError();
        }


        //activates the connection error window to be visible
        void OnConnectionError()
        {
            //game shut down completely
            if (this == null)
                return;

            StopAllCoroutines();
            loadingWindow.SetActive(false);
            connectionErrorWindow.SetActive(true);
        }


        //activates the billing error window to be visible
        void OnBillingError(string error)
        {
            //get text label to display billing failed reason
            Text errorLabel = billingErrorWindow.GetComponentInChildren<Text>();
            if (errorLabel)
                errorLabel.text = "Purchase failed.\n" + error;

            billingErrorWindow.SetActive(true);
        }


        /// <summary>
        /// Allow additional input of server address only in network mode LAN.
        /// Otherwise, the input field will be hidden in the settings (Photon only).
        /// </summary>
        public void OnNetworkChanged(int value)
        {
            serverField.gameObject.SetActive((NetworkMode)value == NetworkMode.LAN ? true : false);
        }


        /// <summary>
        /// Save newly selected GameMode value to PlayerPrefs in order to check it later.
        /// Called by DropDown onValueChanged event.
        /// </summary>
        public void OnGameModeChanged(int value)
        {
            PlayerPrefs.SetInt(PrefsKeys.gameMode, value);
            PlayerPrefs.Save();
        }


        /// <summary>
        /// Modify music AudioSource based on player selection.
        /// Called by Toggle onValueChanged event.
        /// </summary>
        public void OnMusicChanged(bool value)
        {
            AudioManager.GetInstance().musicSource.enabled = musicToggle.isOn;
            AudioManager.PlayMusic(0);
        }


        /// <summary>
        /// Modify global game volume based on player selection.
        /// Called by Slider onValueChanged event.
        /// </summary>
        public void OnVolumeChanged(float value)
        {
            volumeSlider.value = value;
            AudioListener.volume = value;
        }


        /// <summary>
        /// Saves all player selections chosen in the Settings window on the device.
        /// </summary>
        public void CloseSettings()
        {
            PlayerPrefs.SetString(PrefsKeys.playerName, nameField.text);
            PlayerPrefs.SetInt(PrefsKeys.networkMode, networkDrop.value);
            PlayerPrefs.SetString(PrefsKeys.serverAddress, serverField.text);
            PlayerPrefs.SetString(PrefsKeys.playMusic, musicToggle.isOn.ToString());
            PlayerPrefs.SetFloat(PrefsKeys.appVolume, volumeSlider.value);
            PlayerPrefs.Save();
        }

        public void RateApp()
        {
           
        }
    }
}