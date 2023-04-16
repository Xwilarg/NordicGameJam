using NordicGameJam.Translation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NordicGameJam.Menu
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _score;

        private void Awake()
        {
            if (_score != null)
            {
                _score.text = $"{Translate.Instance.Tr("score")} {ScoreManager.Score}";
            }
        }

        public void LoadMenu()
        {
            Destroy(LEGOManager.Instance.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
