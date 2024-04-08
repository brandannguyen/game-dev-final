using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBeltSpawnManager : MonoBehaviour
{
    [Tooltip("The central position where you want your asteroids to spawn at.")]
    public Vector2 spawnPosition;
    [Tooltip("+- the spawnPosition x that asteroids are allowed to spawn at.")]
    public float spawnOffsetX;
    [Tooltip("+- the spawnPosition y that asteroids are allowed to spawn at.")]
    public float spawnOffsetY;
    [Tooltip("How often you want an asteroid to spawn.")]
    public float spawnTime;
    [Tooltip("The orbit rate of your asteroid.")]
    public float movementSpeed;
    [Tooltip("+- the movement speed to have different speeds of asteroids")]
    public float movementSpeedOffset;
    [Tooltip("The maximum number of asteroids allowed in the belt.")]
    public int maxAsteroids;
    [Tooltip("Put you asteroid prefabs here.")]
    public List<GameObject> asteroids;

    [Tooltip("The current number of asteroids spawned in the belt. Used for internal processes.")]
    public  int currAsteroids_DONOTSET;

    private float timer;
    private float x, y;
    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        currAsteroids_DONOTSET = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime && currAsteroids_DONOTSET < maxAsteroids)
        {
            // spawn asteroid
            currAsteroids_DONOTSET++;
            // pick the GO to spawn
            int index = Random.Range(0, asteroids.Count);
            GameObject e = asteroids[index];
            

            //set the position
            offset = Random.Range(spawnOffsetX * -1, spawnOffsetX);
            x = offset + spawnPosition.x;
            offset = Random.Range(spawnOffsetY * -1, spawnOffsetY);
            y = offset + spawnPosition.y;
            Vector3 pos = new Vector3(x, y, 0.0f);
            //spawn it
            GameObject instance = Instantiate(e, pos, Quaternion.identity);

            // set movement speed
            instance.AddComponent<SunOrbitMovement>();
            offset = Random.Range(movementSpeedOffset * -1, movementSpeedOffset);
            instance.GetComponent<SunOrbitMovement>().orbitRate = movementSpeed + offset;

            // add on destroy to object in order to track how many asteroids there are
            instance.AddComponent<AsteroidBeltDecrement>();

            // reset timer
            timer = 0;
        }
    }
}
