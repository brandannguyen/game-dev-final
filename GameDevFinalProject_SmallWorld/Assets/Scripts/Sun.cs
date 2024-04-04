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

    private Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // color changing stuff probably goes here lol
    }


}
