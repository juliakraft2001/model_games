using UnityEngine;
using UnityEngine.UI;

namespace Julia
{
    public class ModalDialog : MonoBehaviour
    {
        public GameObject gameOverPanel;
        public Button restartButton;

        void Start()
        {
            // Hide the game over panel initially
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }

            // Set up the restart button
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(RestartGame);
            }
            else
            {
                Debug.LogError("RestartButton reference is not set in the Inspector.");
            }
        }

        void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        public void ShowGameOverPanel()
        {
            // Show the game over panel
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
            else
            {
                Debug.LogError("GameOverPanel reference is not set in the Inspector.");
            }
        }
    }
}
