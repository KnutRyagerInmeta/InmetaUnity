using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] int levelNumber;
    [SerializeField] int checkPointReached;
    [SerializeField] bool unlocked;
    [SerializeField] SpawnLocation[] spawnLocations;

    [SerializeField] Checkpoint[] checkpoints;

    void Start()
    {
        spawnLocations = GetComponentsInChildren<SpawnLocation>();

    }

    void Update()
    {
        if (unlocked)
        {
        }
    }

    public void Unlock()
    {
        unlocked = true;
    }

    public void Respawn(WorldObject obj)
    {
        obj.SetTransform(spawnLocations[checkPointReached].transform);
        obj.GetComponentsInChildren<Initiable>().Run(x => x.Init());
    }
}
