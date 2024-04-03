using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player;
    public float distnaceFromPlayer;
    [Tooltip("This is the value that is how much the camera zooms out in proportion to how much the player grows")]
    public float zoomFactor;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 1, distnaceFromPlayer * zoomFactor * player.localScale.x);
    }
}
