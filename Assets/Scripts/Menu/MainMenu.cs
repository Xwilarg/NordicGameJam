using NordicGameJam.Translation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NordicGameJam.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadGame()
        {
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
    }
}
