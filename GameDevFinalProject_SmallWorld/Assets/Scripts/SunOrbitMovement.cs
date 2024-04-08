using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunOrbitMovement : MonoBehaviour
{
    [Tooltip("Can not be zero. Make it a negative number to reverse orbit direction.")]
    public float orbitRate;

    private GameObject sun;
    private Vector3 currSunPos;
    private float totalTime;
    private Vector3 startingPos;
    private float xDis;
    private float yDis;

    // Start is called before the first frame update
    void Start()
    {
        sun = GameObject.FindGameObjectWithTag("Sun");
        currSunPos = sun.transform.position;
        startingPos = this.transform.position;
        xDis = startingPos.x - currSunPos.x;
        yDis = startingPos.y - currSunPos.y;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        currSunPos = sun.transform.position;
        Vector3 newPos = new(currSunPos.x + (xDis) * Mathf.Cos(totalTime * orbitRate) - (yDis) * Mathf.Sin(totalTime * orbitRate),
                             currSunPos.y + (xDis) * Mathf.Sin(totalTime * orbitRate) + (yDis) * Mathf.Cos(totalTime * orbitRate),
                             0);
        this.transform.position = newPos;
    }
}
