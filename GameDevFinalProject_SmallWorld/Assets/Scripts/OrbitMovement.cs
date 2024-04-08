using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    [Tooltip("The object this object is to orbit around.")]
    public GameObject planet;
    [Tooltip("Can not be zero. Make it a negative number to reverse orbit direction.")]
    public float orbitRate;

    private Vector3 currPlanetPos;
    private float totalTime;
    private Vector3 startingPos;
    private float xDis;
    private float yDis;

    // Start is called before the first frame update
    void Start()
    {
        currPlanetPos = planet.transform.position;
        startingPos = this.transform.position;
        xDis = startingPos.x - currPlanetPos.x;
        yDis = startingPos.y - currPlanetPos.y;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        currPlanetPos = planet.transform.position;
        Vector3 newPos = new(currPlanetPos.x + (xDis) * Mathf.Cos(totalTime * orbitRate) - (yDis) * Mathf.Sin(totalTime * orbitRate),
                                currPlanetPos.y + (xDis) * Mathf.Sin(totalTime * orbitRate) + (yDis) * Mathf.Cos(totalTime * orbitRate),
                                0);
        this.transform.position = newPos;
    }
}
