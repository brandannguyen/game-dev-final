using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class sunCam : MonoBehaviour
{
    private Camera sunCamera;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        sunCamera = GameObject.Find("SunCamera").GetComponent<Camera>();
        //sunCamera = Camera.main;
        sunCamera.rect = new Rect(.82f, .9f, .1f, .1f);

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 camPos = this.transform.position;

        float angle = Mathf.Atan( (camPos.y - playerPos.y) / (camPos.x - playerPos.x) );
        float camX;
        float camY;

        if (camPos.x - playerPos.x > 0)
        {
            camY = (Mathf.Sin(angle) + 1f) / 2f * .9f;
            camX = (Mathf.Cos(angle) + 1f) / 2f * .82f;
        }
        else
        {
            camY = (-Mathf.Sin(angle) + 1f) / 2f * .9f;
            camX = (-Mathf.Cos(angle) + 1f) / 2f * .82f;
        }

        sunCamera.rect = new Rect(camX, camY, .1f, .1f);
    }
}
