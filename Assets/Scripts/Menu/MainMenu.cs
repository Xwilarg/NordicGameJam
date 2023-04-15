using NordicGameJam.Translation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NordicGameJam.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _hubStatusText, _lightStatusText, _playersStatusText, _batteryText;

        [SerializeField]
        private Button _playLegoButton;

        private int _playerCount;
        private bool _lightStatus, _hubStatus;

        public void PlayLego()
        {
            SceneManager.LoadScene("Main");
        }

        public void PlayKeyboard()
        {
            LEGOManager.Instance.Disable();
            SceneManager.LoadScene("Main");
        }

        public void SetLanguage(string value)
        {
            Translate.Instance.CurrentLanguage = value;
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void Awake()
        {
            UpdateText();
            Translate.Instance.OnTranslationChange += (_, _2) =>
            {
                UpdateText();
            };
        }

        private void UpdateText()
        {
            _hubStatusText.text = $"{Translate.Instance.Tr("legoHub")} <color={(_hubStatus ? "green" : "red")}>{Translate.Instance.Tr(_hubStatus ? "ok" : "waiting")}</color>";
            _lightStatusText.text = $"{Translate.Instance.Tr("legoLight")} <color={(_lightStatus ? "green" : "red")}>{Translate.Instance.Tr(_lightStatus ? "ok" : "waiting")}</color>";
            _playersStatusText.text = $"{Translate.Instance.Tr("legoButton")} <color={(_playerCount > 0 ? "green" : "red")}>{_playerCount}</color>";
            _batteryText.gameObject.SetActive(_hubStatus);
            if (_hubStatus)
            {
                _batteryText.text = $"{Translate.Instance.Tr("battery")} {LEGOManager.Instance.HUB.BatteryLevel}%";
            }

            _playLegoButton.interactable = _hubStatus && _lightStatus && _playerCount > 0;
        }

        public void OnHubConnected(bool status)
        {
            _hubStatus = status;
            UpdateText();
        }

        public void OnLightConnected(bool status)
        {
            _lightStatus = status;
            UpdateText();
        }

        public void OnButtonConnected(bool status)
        {
            _playerCount += status ? 1 : -1;
            UpdateText();
        }
    }
}
