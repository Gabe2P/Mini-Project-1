using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Copyable aCopy;

    public Mob SpawnMob(Mob aPrototype)
    {
        aCopy = aPrototype.Copy();
        return (Mob)aCopy;
    }
}
