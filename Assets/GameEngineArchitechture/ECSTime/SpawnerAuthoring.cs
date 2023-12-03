using UnityEngine;
using Unity.Entities;

public class SpawnerAuthoring : MonoBehaviour {
	public GameObject prefab;
	public float spawnRate;
}

class SpawnBaker : Baker<SpawnerAuthoring> {
	public override void Bake(SpawnerAuthoring authoring) {
		var entity = GetEntity(TransformUsageFlags.None);
		AddComponent(entity, new Spawner {
			prefabComp = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
			spawnPosition = authoring.transform.position,
			spawnRate = authoring.spawnRate,
			spawnTimer = 0.0f
		});
	}
}
