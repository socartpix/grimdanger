using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 10f;  // Velocidad de la bala
    public float lifeTime = 2f;      // Tiempo que dura la bala antes de destruirse
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Establece la velocidad hacia la derecha
        rb.linearVelocity= new Vector2(bulletSpeed, 0f);
        
        // Destruye la bala después del tiempo especificado
        Destroy(gameObject, lifeTime);
    }

    
}