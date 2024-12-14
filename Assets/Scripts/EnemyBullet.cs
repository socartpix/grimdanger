using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;    // Velocidad de la bala
    public float lifeTime = 2f;        // Tiempo antes de destruirse
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Establece la velocidad hacia la izquierda
        rb.linearVelocity = new Vector2(-bulletSpeed, 0f);
        
        // Destruye la bala después del tiempo especificado
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si golpea al jugador o una pared, destruye la bala
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}