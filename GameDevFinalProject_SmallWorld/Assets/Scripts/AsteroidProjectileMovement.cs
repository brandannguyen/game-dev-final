using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProjectileMovement : MonoBehaviour
{
    [Tooltip("Speed towards sun")]
    public float moveSpeed;

    private GameObject sun;
    private Vector3 currSunPos;
    private float totalTime;

    // Start is called before the first frame update
    void Start()
    {
        sun = GameObject.FindGameObjectWithTag("Sun");
        currSunPos = sun.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currSunPos = sun.transform.position;

        var step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, currSunPos, step);
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sun"))
        {
            // destroy asteroid
            Destroy(this.gameObject);
        }
    }
}
