using UnityEngine;
using System.Collections;

public class EnemyDelayedMovement : MonoBehaviour
{
    public float waitTime = 2f;      // Tiempo que espera antes de moverse
    public float moveSpeed = 5f;     // Velocidad de movimiento
    
    public bool isMoving = false;   // Si está en movimiento o esperando
    
    private float timer = 0f;        // Contador de tiempo
    private Rigidbody2D rb;

    public ScreenPosition screenPosition;

    void Start()
    {
        screenPosition = GameObject.FindWithTag("Enemy").GetComponent<ScreenPosition>();
        rb = GetComponent<Rigidbody2D>();
        
        // Inicialmente la velocidad es 0
        rb.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        // Si aún no está en movimiento, cuenta el tiempo
        if (!isMoving)
        {
            timer += Time.deltaTime;

            // Cuando el tiempo de espera se cumple, comienza a moverse
            if (timer >= waitTime)
            {
                StartMoving();
            }
        }
    }

    void StartMoving()
    {
        StartCoroutine(MoveCoroutine());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el jugador toca la gema
        if (other.CompareTag("Player"))
        {
            // Aquí puedes añadir lógica de recolección
            // Por ejemplo: aumentar puntuación, reproducir sonido, etc.
            Destroy(gameObject);
        }
    }

    IEnumerator MoveCoroutine() {
        screenPosition.canPosition = false;
        isMoving = true;
        rb.linearVelocity = new Vector2(-moveSpeed, 0f);
        yield return new WaitForSeconds(10f);
        screenPosition.canPosition = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}