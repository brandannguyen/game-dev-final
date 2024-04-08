using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidBeltDecrement : MonoBehaviour
{
    private GameObject asteroidBeltSpawnManager;
    private AsteroidBeltSpawnManager abSpawnManager;

    void Start()
    {
        asteroidBeltSpawnManager = GameObject.FindGameObjectWithTag("AsteroidBeltSpawnManager");
        abSpawnManager = asteroidBeltSpawnManager.GetComponent<AsteroidBeltSpawnManager>();
    }

    private void OnDestroy()
    {
        abSpawnManager.currAsteroids_DONOTSET -= 1;
    }
}
