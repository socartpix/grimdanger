using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;     // Prefab de la bala enemiga
    public Transform shootPoint;        // Punto de disparo
    public float shootInterval = 2f;    // Tiempo entre disparos
    
    private float nextShootTime;

    void Start()
    {
        // Inicializa el tiempo del primer disparo
        nextShootTime = Time.time + shootInterval;
    }

    void Update()
    {
        // Dispara automáticamente cada intervalo
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
        else
        {
            Debug.LogWarning("¡Falta asignar el prefab de la bala o el punto de disparo!");
        }
    }
}