using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class Sun : MonoBehaviour
{
    [Tooltip("Time until sun explodes in seconds.")]
    public float totalGameTime;
    public Color startingColor;
    public Color endingColor;
    public Color currColor;

    private Renderer render;
    private float redSlope;
    private float greenSlope;
    private float blueSlope;
    private float currTime;


    // Start is called before the first frame update
    void Start()
    {
        render = this.GetComponent<Renderer>();
        render.material.color = startingColor;

        redSlope = (endingColor.r - startingColor.r) / totalGameTime;
        greenSlope = (endingColor.g - startingColor.g) / totalGameTime;
        blueSlope = (endingColor.b - startingColor.b) / totalGameTime;
        currTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if(currTime >= totalGameTime) {
            Debug.Log("Time is up, you lose");
        }
        else
        {
            currColor = new Color(startingColor.r + redSlope * currTime, 
                                  startingColor.g + greenSlope * currTime, 
                                  startingColor.b + blueSlope * currTime);
            render.material.color = currColor;
        }
        
    }


}
