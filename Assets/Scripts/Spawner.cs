using UnityEngine;

public class Spawner : MonoBehaviour 
{
    public enum SpawnType
    {
        Enemy,
        Gem
    }

    public SpawnType spawnType;              // Tipo de spawner
    public GameObject[] prefabsToSpawn;      // Array de prefabs para spawnear
    public float spawnInterval = 2f;         // Tiempo entre spawns

    // Variables para Enemy
    public Transform[] enemySpawnPoints;     // Puntos de spawn para enemigos

    // Variables para Gem
    public float minX = -5f;                // Altura mínima para gemas
    public float maxX = 5f;                 // Altura máxima para gemas
    public float spawnY = 2.49f;              // Posición X donde aparecerán las gemas
    
    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnObject()
    {
        if (prefabsToSpawn.Length == 0)
        {
            Debug.LogWarning("¡No hay prefabs asignados!");
            return;
        }

        // Elige un prefab aleatorio
        int randomPrefabIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject selectedPrefab = prefabsToSpawn[randomPrefabIndex];

        if (selectedPrefab == null) return;

        // Spawn según el tipo
        switch (spawnType)
        {
            case SpawnType.Gem:
                SpawnGem(selectedPrefab);
                break;

            case SpawnType.Enemy:
                SpawnEnemy(selectedPrefab);
                break;
        }
    }

    void SpawnGem(GameObject prefab)
    {
        if (enemySpawnPoints == null || enemySpawnPoints.Length == 0)
        {
            Debug.LogWarning("¡No hay puntos de spawn para enemigos!");
            return;
        }

        // Elige un punto de spawn aleatorio
        int randomIndex = Random.Range(0, enemySpawnPoints.Length);
        Transform spawnPoint = enemySpawnPoints[randomIndex];

        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemy(GameObject prefab)
    {
        // Genera una posición Y aleatoria entre el rango especificado
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(spawnY, randomX, 0);

        Instantiate(prefab, spawnPosition, transform.rotation);
    }
}