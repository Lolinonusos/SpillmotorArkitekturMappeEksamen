using UnityEngine;
using Random = UnityEngine.Random;

public class RainManager : MonoBehaviour {
    [SerializeField] GameObject rainDropPrefab;
    [SerializeField] Vector2 spawnArea = Vector2.one * 10;

    [SerializeField] private float lifeTime = 120f; // Seconds
    [SerializeField] private float spawnRate = 12f; // Seconds 
    
    Vector3 GetRandomRainPosition() {
        float x = Random.Range(-spawnArea.x, spawnArea.x);
        float z = Random.Range(-spawnArea.y, spawnArea.y);

        Vector3 position = transform.position;
        position.x += x;
        position.z += z;
        return position;
    }

    void spawnRain() {
        GameObject rainDropGameObject = Instantiate(rainDropPrefab, GetRandomRainPosition(), Quaternion.identity);
        float randomScale = Random.Range(1f, 5f);
        rainDropGameObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        Destroy(rainDropGameObject, lifeTime);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Vector3 gizmoSize = new Vector3(spawnArea.x, 1, spawnArea.y);
        Gizmos.DrawWireCube(transform.position, gizmoSize * 2);
    }

    void Start() {
        InvokeRepeating("spawnRain", 0.0f, spawnRate);
        spawnRain();
    }
}
