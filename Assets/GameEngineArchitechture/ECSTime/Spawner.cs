using Unity.Entities;
using Unity.Mathematics;


public struct Spawner : IComponentData {
	public Entity prefabComp;
	public float3 spawnPosition;
	public float spawnRate;
	public float spawnTimer;
}
