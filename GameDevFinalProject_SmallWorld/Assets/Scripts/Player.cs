using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("The players minimum max speed. This value is what will be scaled in accordance to the velocityScalar value to determine movement speed based on size.")]
    public float minimumVelocity;
    [Tooltip("This is what will determine how the movement speed changes in relation to the player's size")]
    public float velocityScaler;

    private Vector3 mousePosition;
    private float currentMaxVelocity;
    private float currentSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentMaxVelocity = minimumVelocity * (velocityScaler * currentSize);

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, currentMaxVelocity);

        Debug.Log(Input.mousePosition);
    }
}
