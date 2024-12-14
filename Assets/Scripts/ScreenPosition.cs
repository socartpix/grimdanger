using UnityEngine;
using System.Collections;

public class ScreenPosition : MonoBehaviour
{
    private Camera mainCamera;
    public float rightEdgeOffset = 1f; // Distancia desde el borde derecho

    public bool canPosition = true;


    public float waitTime = 2f;      // Tiempo que espera antes de moverse
    public float moveSpeed = 5f;     // Velocidad de movimiento
    
    public bool isMoving = false;   // Si está en movimiento o esperando
    
    private float timer = 0f;        // Contador de tiempo
    public Rigidbody2D rb;
    void Start()
    {
       
        mainCamera = Camera.main;
        SetPositionToRightEdge();
        if(rb != null)
        {
            rb = GetComponent<Rigidbody2D>();
        
            // Inicialmente la velocidad es 0
            rb.linearVelocity = Vector2.zero;
        }
        
    }

    void Update()
    {
        if(canPosition == true)
        {
            SetPositionToRightEdge();}

            // Si aún no está en movimiento, cuenta el tiempo
        if (!isMoving && gameObject.name != "EnemySpawner" && gameObject.name != "GemSpawner")
        {
            timer += Time.deltaTime;

            // Cuando el tiempo de espera se cumple, comienza a moverse
            if (timer >= waitTime)
            {
                StartMoving();
            }
        }
        
    }

    void SetPositionToRightEdge()
    {
        if (mainCamera != null)
        {
            // Obtiene el punto más a la derecha de la vista de la cámara
            Vector3 rightEdgePosition = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));
            
            // Mantiene la posición Y original del enemigo
            float currentY = transform.position.y;
            
            // Establece la nueva posición
            transform.position = new Vector3(rightEdgePosition.x - rightEdgeOffset, 
                                          currentY, 
                                          transform.position.z);
        }
    }

    void StartMoving()
    {
        StartCoroutine("MoveCoroutine");;
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
        canPosition = false;
        isMoving = true;
        rb.linearVelocity = new Vector2(-moveSpeed, 0f);
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}