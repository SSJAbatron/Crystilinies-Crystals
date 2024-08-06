using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBehaviour : MonoBehaviour
{

    public Transform[] crystalSpawnPoints;
    public GameObject crystal;

    private void Start()
    {
        if (crystal != null && crystalSpawnPoints.Length > 0)
            Instantiate(crystal, crystalSpawnPoints[UnityEngine.Random.Range(0, crystalSpawnPoints.Length)]);
    }

}
