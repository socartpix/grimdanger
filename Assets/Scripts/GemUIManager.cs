using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemUIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject uiPanel;                    // Panel principal que contiene toda la UI
    public TextMeshProUGUI gemCountText;         // Texto que muestra el contador
    
    [Header("Settings")]
    public KeyCode toggleKey = KeyCode.Q;        // Tecla para mostrar/ocultar
    public string textPrefix = "Gemas: ";        // Texto antes del número
    
    private GemCollector gemCollector;           // Referencia al colector de gemas
    
    void Start()
    {
        // Buscar el GemCollector en la escena
        gemCollector = FindObjectOfType<GemCollector>();
        
        // Ocultar el panel al inicio
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
        
        UpdateUI();
    }
    
    void Update()
    {
       
        
        // Actualizar el contador
        UpdateUI();
    }
    
    void UpdateUI()
    {
        if (gemCountText != null && gemCollector != null)
        {
            int gemsCollected = gemCollector.GetScore() / gemCollector.pointsPerGem;
            gemCountText.text = textPrefix + gemsCollected.ToString();
        }
    }
    
    void ToggleUI()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(!uiPanel.activeSelf);
        }
    }
}