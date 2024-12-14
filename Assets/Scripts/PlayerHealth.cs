using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI damageText;
    
    [Header("Death Effects")]
    public GameObject deathEffectPrefab;     // Efecto de partículas
    public AudioClip deathSound;             // Sonido de muerte
    
    private AudioSource audioSource;
    
    void Start()
    {
        gameManager = GameManager.Instance;
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        UpdateHealthDisplay();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //ebug.Log("Collision Detected");
        
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet"))
        {
            int damageAmount = 10;
            gameManager.TakeDamage(damageAmount);
            
            UpdateHealthDisplay();
            
            Debug.Log($"¡Daño recibido! Vida restante: {gameManager.PlayerHealth}");
            
            CheckDeath();
        }
    }

    public  void Update() {
        UpdateHealthDisplay();
    }

    void UpdateHealthDisplay()
    {
        if (damageText != null)
        {
            damageText.text = $"Life: {gameManager.PlayerHealth}";
        }
    }
    
    void CheckDeath()
    {
        if (gameManager.PlayerHealth <= 0)
        {
            // Reproducir sonido de muerte
            if (deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);
            }
            
            // Instanciar efecto de partículas
            if (deathEffectPrefab != null)
            {
                Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            }
            
            // Destruir el objeto del jugador
            Destroy(gameObject);
        }
    }
}