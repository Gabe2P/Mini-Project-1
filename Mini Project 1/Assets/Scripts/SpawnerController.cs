using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]

public class SpawnerController : MonoBehaviour
{
    public Mob enemy;
    public float spawnRate;
    public float curSpawnTime;
    public float spawnLife;
    public int maxSpawnQuantity = 1;
    private int curSpawnQuantity = 0;

    private Spawner emitter;


    // Start is called before the first frame update
    void Start()
    {
        emitter = GetComponent<Spawner>();
        for (int idx = 0; idx < maxSpawnQuantity; ++idx)
        {
            var clone = emitter.SpawnMob(enemy);
            clone.transform.position = this.transform.position;
        }
       
    }
}
