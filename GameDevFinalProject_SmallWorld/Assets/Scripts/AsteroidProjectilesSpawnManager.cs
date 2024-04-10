using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AsteroidProjectilesSpawnManager : MonoBehaviour
{
    [Tooltip("Put you asteroid prefabs here.")]
    public List<GameObject> asteroids;

    [Tooltip("How often you want an asteroid to spawn.")]
    public float spawnTime;

    [Tooltip("The speed of your asteroid.")]
    public float movementSpeed;
    [Tooltip("+- the movement speed to have different speeds of asteroids")]
    public float movementSpeedOffset;

    [Tooltip("The corner position where you want your asteroids to spawn at.")]
    public Vector2 spawnPosition;
    [Tooltip("+- the spawnPosition x that asteroids are allowed to spawn at.")]
    public float spawnOffsetX;
    [Tooltip("+- the spawnPosition y that asteroids are allowed to spawn at.")]
    public float spawnOffsetY;

    private float timer;
    private float x, y;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            // pick the GO to spawn
            int index = Random.Range(0, asteroids.Count);
            GameObject e = asteroids[index];

            // along top or bottom
            float num = Random.Range(0f, 2f);
            bool topOrSide = num > 1f;

            // set position
            if (topOrSide)
            {
                offset = Random.Range(0, spawnOffsetX);
                x = offset + spawnPosition.x;
                y = spawnPosition.y;
            }
            else
            {
                x = spawnPosition.x;
                offset = Random.Range(spawnOffsetY, 0);
                y = offset + spawnPosition.y;
            }
            Vector3 pos = new Vector3(x, y, 0.0f);

            //spawn it
            GameObject instance = Instantiate(e, pos, Quaternion.identity);

            // set movement speed
            instance.AddComponent<AsteroidProjectileMovement>();
            offset = Random.Range(movementSpeedOffset * -1, movementSpeedOffset);
            instance.GetComponent<AsteroidProjectileMovement>().moveSpeed = movementSpeed + offset;

            // reset timer
            timer = 0;
        }
    }
}
