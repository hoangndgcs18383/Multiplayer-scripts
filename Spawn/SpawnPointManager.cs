using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager Instance;

    public SpawnPoint[] spawnPoints;

    private void Awake()
    {
        Instance = this;
    }

    public Transform GetSpawnPoint(int i)
    {
        Debug.Log(i);
        return spawnPoints[i].transform;
    }
}
