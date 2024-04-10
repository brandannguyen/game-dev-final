using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSprite : MonoBehaviour
{
    [Tooltip("Effects how fast the player character model spins")]
    public float spinSpeed;

    // Update is called once per frame
    void Update()
    {
        //player spin
        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }
}
