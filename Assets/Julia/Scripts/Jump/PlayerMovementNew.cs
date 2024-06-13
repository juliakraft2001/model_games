using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Julia
{
    public class PlayerMovementNew : MonoBehaviour
    {
        
        //the object to move
        public Transform moveThis;
//the layers the ray can hit
        public LayerMask hitLayers;
        
        public float jumpForce = 10f;
        public float gravityScale = 3f; // Skaliere die Schwerkraft
        public Rigidbody rb;
        private bool isGrounded;
        private Camera mainCamera;
        private Quaternion defaultRotation;

        // Reference to the Game Over Text
        public Text gameOverText;
        // Reference to the score Text
        public Text scoreText;
        private int score = 0;

        // Modal Dialog UI elements
        public GameObject gameOverPanel;
        public Button restartButton;

        // Winning UI elements
        public Text winningText;
        public GameObject winningPanel;
        public Button continueButton;
        public Text timeText; // Only time Text

        // Timer variables
        private float startTime;
        private bool isTimerRunning;

        // Number of collectibles to win
        private int collectiblesToWin = 20;

        // Timer positions
        public Vector3 timerInitialPosition;

        void Start()
        {
            //rb = GetComponent<Rigidbody>();
            mainCamera = Camera.main;
            defaultRotation = Quaternion.Euler(0f, 0f, 0f);

            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("GameOverText reference is not set in the Inspector.");
            }

            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
                PositionUIElement(scoreText, new Vector2(1, 1), new Vector2(-100, -30));
            }
            else
            {
                Debug.LogError("ScoreText reference is not set in the Inspector.");
            }

            // Ensure the Rigidbody is using gravity
            rb.useGravity = true;
            rb.mass = 1f; // Standard Masse

            // Hide the game over panel initially
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }
            else
            {
                Debug.LogError("GameOverPanel reference is not set in the Inspector.");
            }

            // Set up the restart button
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(RestartGame);
                restartButton.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("RestartButton reference is not set in the Inspector.");
            }

            // Hide the winning panel initially
            if (winningPanel != null)
            {
                winningPanel.SetActive(false);
            }
            else
            {
                Debug.LogError("WinningPanel reference is not set in the Inspector.");
            }

            // Set up the continue button
            if (continueButton != null)
            {
                continueButton.onClick.AddListener(RestartGame);
                continueButton.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("ContinueButton reference is not set in the Inspector.");
            }

            // Initialize timer
            startTime = Time.time;
            isTimerRunning = true;

            // Set initial position of the timer
            if (timeText != null)
            {
                PositionUIElement(timeText, new Vector2(1, 1), new Vector2(-100, -60));
                timeText.text = "Time: 00:00.00"; // Initial text for the timer
            }
            else
            {
                Debug.LogError("TimeText reference is not set in the Inspector.");
            }
        }

        void Update()
        {
            // Player movement while the mouse button is held down, move/roll there not beam

            if (Input.GetMouseButton(0))
            {
                
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
                {
                    moveThis.transform.position = hit.point;
                }
                
                //rb.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                
                /*
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                float distance;

                if (plane.Raycast(ray, out distance))
                {
                    Vector3 targetPosition = ray.GetPoint(distance);
                    rb.MovePosition(targetPosition);
                    //transform.position = targetPosition;
                }
                */
            }



            // Player jumps when the mouse button is released
            if (Input.GetMouseButtonUp(0) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

            // Ensure the player remains upright
            transform.rotation = defaultRotation;

            // Update timer if it's still running
            if (isTimerRunning)
            {
                float currentTime = Time.time - startTime;
                string minutes = ((int)currentTime / 60).ToString("00");
                string seconds = (currentTime % 60).ToString("00.00");
                timeText.text = "Time: " + minutes + ":" + seconds;
            }
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
            else if (other.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Game Over");
                GameOver();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Collectible"))
            {
                Destroy(other.gameObject);
                score++;
                if (scoreText != null)
                {
                    scoreText.text = "Score: " + score;
                }
                CheckWinCondition();
            }
        }
        
        void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                // Ensure the player does not pass through the wall
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        void CheckWinCondition()
        {
            if (score >= collectiblesToWin)
            {
                ShowWinningText();
                // Disable player movement
                enabled = false;
                // Stop any physics interactions
                rb.isKinematic = true;
                // Stop timer
                isTimerRunning = false;
            }
        }

        void ShowWinningText()
        {
            if (winningText != null)
            {
                // Set winning text
                winningText.text = "You won!";
                winningText.gameObject.SetActive(true);

                // Show the winning panel
                if (winningPanel != null)
                {
                    winningPanel.SetActive(true);
                }

                // Show the continue button
                if (continueButton != null)
                {
                    continueButton.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogError("WinningText reference is not set in the Inspector.");
            }
        }

        void ShowGameOverText()
        {
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true);
                // Show the game over panel
                if (gameOverPanel != null)
                {
                    gameOverPanel.SetActive(true);
                }
                // Show the restart button
                if (restartButton != null)
                {
                    restartButton.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogError("GameOverText reference is not set in the Inspector.");
            }
        }

        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void GameOver()
        {
            ShowGameOverText();
            // Disable player movement
            enabled = false;
            // Stop any physics interactions
            rb.isKinematic = true;
            // Stop timer
            isTimerRunning = false;
        }

        void FixedUpdate()
        {
            // Apply custom gravity
            rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
        }

        void PositionUIElement(Text uiElement, Vector2 anchor, Vector2 offset)
        {
            RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
            rectTransform.anchorMin = anchor;
            rectTransform.anchorMax = anchor;
            rectTransform.anchoredPosition = offset;
        }
    }
}
