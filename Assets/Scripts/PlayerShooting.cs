using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;    // Arrastra el prefab de la bala aquí en el Inspector
    public Transform shootPoint;       // Punto desde donde saldrá la bala
    public float shootCooldown = 0.5f; // Tiempo entre disparos
    
    private float nextShootTime = 0f;

    void Update()
    {
        // Si presionas el botón de disparo (puedes cambiar "Fire1" por la tecla que prefieras)
        if (Input.GetButton("Fire1") && Time.time > nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void Shoot()
    {
        // Crea una instancia de la bala en la posición del punto de disparo
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