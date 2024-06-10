using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource; // Referenz auf die AudioSource-Komponente

    void Start()
    {
        // Finde die AudioSource-Komponente, falls sie nicht zugewiesen ist
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Stelle sicher, dass die Musik abgespielt wird, wenn das Spiel startet
        PlayMusic();
    }

    void PlayMusic()
    {
        // Starte die Wiedergabe der Musik
        audioSource.Play();
    }

    void StopMusic()
    {
        // Stoppe die Wiedergabe der Musik
        audioSource.Stop();
    }
}
