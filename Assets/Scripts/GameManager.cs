using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Playerprefs unity
    public static GameManager Instance;
    
    [SerializeField] private int maxPlayerHealth = 100;
    public int currentPlayerHealth;
    public int currentGems;
    
    public int Gems { get; private set; }
    public int RMB { get; private set; }

    public int PlayerHealth 
    { 
        get { return currentPlayerHealth; }
        private set { currentPlayerHealth = Mathf.Clamp(value, 0, maxPlayerHealth); }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeGame()
    {
        PlayerHealth = maxPlayerHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        PlayerHealth -= damageAmount;
        
        if (PlayerHealth <= 0)
        {
            GameOver();
        }
    }

    public void SetPlayerHealth(int healAmount)
    {
        PlayerHealth += healAmount;
        Debug.Log("Player Health: " + PlayerHealth);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        // Aquí puedes agregar la lógica de game over
    }
}