using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Wichtig für die Verwendung von UI-Komponenten

namespace Julia
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 7f;
        public float jumpForce = 10f;
        public float maxJumpHeight = 5f;
        private Rigidbody rb;
        private bool isGrounded;
        private float jumpStartY;

        // Referenz zum Game Over Text
        public Text gameOverText;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("GameOverText reference is not set in the Inspector.");
            }
        }

        void Update()
        {
            // Horizontale Bewegung (nur X- und Z-Achsen)
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            // Sicherstellen, dass der Spieler immer aufrecht bleibt
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            // Springen
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                jumpStartY = transform.position.y;
            }

            // Springen fortsetzen, solange die Sprungtaste gehalten wird und die maximale Höhe nicht erreicht ist
            if (Input.GetButton("Jump") && !isGrounded)
            {
                if (transform.position.y - jumpStartY < maxJumpHeight)
                {
                    rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
                }
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
                ShowGameOverText();
            }
        }

        void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }

        void ShowGameOverText()
        {
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true);
                StartCoroutine(RestartGame());
            }
            else
            {
                Debug.LogError("GameOverText reference is not set in the Inspector.");
            }
        }

        IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(1f); // Warte 3 Sekunden, bevor das Spiel neu startet
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
