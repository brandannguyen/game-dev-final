using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("The players minimum max speed. This value is what will be scaled in accordance to the velocityScalar value to determine movement speed based on size.")]
    public float minimumMaxVelocity;
    [Tooltip("This is what will determine how the movement speed changes in relation to the player's size")]
    public float velocityScaler;
    [Tooltip("This variable controls how far the mouse can be from the player until they are at max speed.")]
    public float distanceThreshold;

    private Vector3 mousePosition;
    private float currentMaxVelocity;
    private float currentVelocity;
    private float currentSize;
    private float distanceToMouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // player movement
        // calculates the maximum speed the player could go based off current size
        currentSize = this.transform.localScale.x;
        currentMaxVelocity = minimumMaxVelocity * (velocityScaler * currentSize);

        // gets the mouse's postion in game and finds the distance betweent the player and there
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 20f));
        distanceToMouse = Mathf.Sqrt(
            (mousePosition.x - this.transform.position.x) * (mousePosition.x - this.transform.position.x) + 
            (mousePosition.y - this.transform.position.y) * (mousePosition.y - this.transform.position.y));

        // determines the current velocuty based on the distance the player is from the mouse
        if (distanceToMouse > distanceThreshold)
        {
            currentVelocity = currentMaxVelocity;
        }
        else
        {
            currentVelocity = (distanceToMouse / distanceThreshold) * currentMaxVelocity;
        }

        // calcultates the players new coordinates and sets the players transform to them
        this.transform.position = new(mousePosition.normalized.x * currentVelocity, 
                                      mousePosition.normalized.y * currentVelocity, 20f);

    }
}
