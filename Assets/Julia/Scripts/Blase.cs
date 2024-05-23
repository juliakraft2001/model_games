using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Julia {
public class Blase : MonoBehaviour
{
    public float speed = 1.5f;
    private Renderer renderer;

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

        // Wenn die Blase den oberen Bildschirmrand erreicht, wird sie zerstört
        if (transform.position.y > 20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        // Wenn die Blase angeklickt wird, ändert sie ihre Farbe
        renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
}
