using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string gameSceneName = "Game"; // Nombre de tu escena de juego
    public AudioClip clickSound; // Opcional: sonido al hacer clic
    
    private AudioSource audioSource;
    
    void Start()
    {
        // Si hay un sonido asignado, nos aseguramos de tener un AudioSource
        if (clickSound != null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }
    
    public void StartGame()
    {
        // Reproducir sonido si existe
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        
        // Cargar la escena del juego
        SceneManager.LoadScene(gameSceneName);
    }
}