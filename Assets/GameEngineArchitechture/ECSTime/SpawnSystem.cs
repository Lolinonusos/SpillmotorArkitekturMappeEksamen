using System.Diagnostics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

[BurstCompile]
public partial struct SpawnSystem : ISystem {
	public void OnCreate(ref SystemState state) { }
	
	public void OnDestroy(ref SystemState state) { }

	[BurstCompile]
	public void OnUpdate(ref SystemState state) {

		foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>()) {
			ProcessSpawner(ref state, spawner);
		}
	}

	private void ProcessSpawner(ref SystemState state, RefRW<Spawner> spawner) {

		if (spawner.ValueRO.spawnTimer < SystemAPI.Time.ElapsedTime) {
			
			Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.prefabComp);
		
			state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(spawner.ValueRO.spawnPosition));

			spawner.ValueRW.spawnTimer = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;
		}
	}
}
