using UnityEngine;
using UnityEngine.SceneManagement;

namespace NordicGameJam.Menu
{
    public class GameOver : MonoBehaviour
    {
        public void LoadMenu()
        {
            Destroy(LEGOManager.Instance.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
