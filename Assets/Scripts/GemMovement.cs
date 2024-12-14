using UnityEngine;
using UnityEngine.UI;

public class GemCollector : MonoBehaviour 
{
    [Header("Gem Collection Settings")]
    public int pointsPerGem = 10;
    public AudioClip collectSound;

    [Header("Audio Settings")]
    [Range(0.8f, 1.2f)] public float minPitch = 0.9f;
    [Range(0.8f, 1.2f)] public float maxPitch = 1.1f;
    [Range(0f, 1f)] public float baseVolume = 1f;
    [Range(0f, 0.2f)] public float volumeVariation = 0.1f;

    [Header("Gem Effect Settings")]
    public float redGemDamageMultiplier = 2f;    // Multiplicador de daño para gema roja
    public float blueGemSpeedBoost = 5f;         // Aumento de velocidad para gema azul
    public int yellowGemHealthRestore = 20;       // Cantidad de vida que restaura la gema amarilla

    private int score = 0;
    private AudioSource audioSource;
    private PlayerController playerController;
    private float originalMoveSpeed;
    public GameManager gameManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && collectSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            originalMoveSpeed = playerController.moveSpeed;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "EnemySpawner") return;

        if (other.CompareTag("Gem_Blue"))
        {
            CollectGem(other.gameObject);
            ApplyBlueGemEffect();
            Debug.Log("Gema azul recogida - Velocidad aumentada");
        }
        else if (other.CompareTag("Gem_Yellow"))
        {
            CollectGem(other.gameObject);
            ApplyYellowGemEffect();
            Debug.Log("Gema amarilla recogida - Vida restaurada");
        }
        else if (other.CompareTag("Gem_Red"))
        {
            CollectGem(other.gameObject);
            ApplyRedGemEffect();
            Debug.Log("Gema roja recogida - Daño aumentado");
        }
    }

    private void CollectGem(GameObject gemObject)
    {
        score += pointsPerGem;

        if (collectSound != null && audioSource != null)
        {
            PlayRandomizedSound();
        }

        // Manejo de partículas
        if (gemObject.GetComponent<ParticleSystem>() != null)
        {
            ParticleSystem particles = gemObject.GetComponent<ParticleSystem>();
            particles.transform.parent = null;
            particles.Play();
            Destroy(particles.gameObject, particles.main.duration);
        }

        Destroy(gemObject);
    }

    private void ApplyBlueGemEffect()
    {
        if (playerController != null)
        {
            playerController.moveSpeed = originalMoveSpeed + blueGemSpeedBoost;
            // Opcional: Crear una corrutina para revertir el efecto después de un tiempo
            StartCoroutine(ResetSpeedAfterDelay(5f)); // 5 segundos de efecto
        }
    }

    private void ApplyYellowGemEffect()
    {
        
        gameManager.SetPlayerHealth(yellowGemHealthRestore);
        
    }

    private void ApplyRedGemEffect()
    {
        EnemyHealth.damage = 20;
        StartCoroutine(ResetDamageAfterDelay(10f)); // 5 segundos de efecto
    }

    private System.Collections.IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerController != null)
        {
            playerController.moveSpeed = originalMoveSpeed;
        }
    }

    private System.Collections.IEnumerator ResetDamageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnemyHealth.damage = 10;
    }

    void PlayRandomizedSound()
    {
        float originalPitch = audioSource.pitch;
        float originalVolume = audioSource.volume;

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.volume = baseVolume + Random.Range(-volumeVariation, volumeVariation);

        audioSource.PlayOneShot(collectSound);

        audioSource.pitch = originalPitch;
        audioSource.volume = originalVolume;
    }

    public int GetScore()
    {
        return score;
    }

    // Método público para verificar si el daño está aumentado
    public bool IsDamageIncreased { get; private set; }
}