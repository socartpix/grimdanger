using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject healthPanel;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    
    [Header("Settings")]
    public string healthPrefix = "Vida: ";
    
    private GameManager gameManager;
    
    void Start()
    {
        // Obtener referencia al GameManager
        gameManager = GameManager.Instance;
        
        if (healthSlider != null)
        {
            healthSlider.maxValue = 100; // Asumiendo que maxPlayerHealth es 100
            healthSlider.value = gameManager.PlayerHealth;
        }
        
        UpdateUI();
    }
    
    void Update()
    {
        UpdateUI();
    }
    
    void UpdateUI()
    {
        if (gameManager != null)
        {
            // Actualizar texto de vida
            if (healthText != null)
            {
                healthText.text = healthPrefix + gameManager.PlayerHealth.ToString();
            }
            
            // Actualizar slider de vida
            if (healthSlider != null)
            {
                healthSlider.value = gameManager.PlayerHealth;
            }
        }
    }
}