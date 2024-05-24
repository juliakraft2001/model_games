using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Julia
{
    public class Blase : MonoBehaviour
    {
        public float speed = 1.5f;
        private Renderer renderer;
        private float upperLimit = 20f; // Upper limit for bubble movement
        private float lowerLimit = -5f; // Lower limit for bubble reset

        private void Start()
        {
            // Setze die anfängliche Farbe der Blase
            renderer = GetComponent<Renderer>();
            renderer.material.color = Color.blue; // Beispielhaft Blau als Standardfarbe
        }

        private void Update()
        {
            // Bewegt die Blase nach oben
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // Wenn die Blase den oberen Bildschirmrand erreicht, wird sie nach unten zurückgesetzt
            if (transform.position.y > upperLimit)
            {
                ResetPosition();
            }
        }

        private void ResetPosition()
        {
            // Setzt die Blase auf die untere Position zurück
            transform.position = new Vector3(transform.position.x, lowerLimit, transform.position.z);
        }

        private void OnMouseDown()
        {
            // Wenn die Blase angeklickt wird, ändert sie ihre Farbe
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
