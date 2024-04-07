using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class Player : MonoBehaviour
{
    [Tooltip("The players minimum max speed. This value is what will be scaled in accordance to the velocityScalar value to determine movement speed based on size.")]
    public float minimumMaxVelocity;
    [Tooltip("This is what will determine how the movement speed changes in relation to the player's size")]
    public float velocityScaler;
    [Tooltip("This variable controls how far the mouse can be from the player until they are at max speed.")]
    public float distanceThreshold;
    [Tooltip("The percentage of the collision's scale to add or subtract to the player.")]
    public float growthPercentage;

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

        // gets the mouse's postion in game and finds the distance between the player and there
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 20f));
        Vector2 mousePos = new(mousePosition.x, mousePosition.y);
        Vector2 currPos = new(this.transform.position.x, this.transform.position.y);
        distanceToMouse = Vector2.Distance(mousePos, currPos);

        // determines the current velocuty based on the distance the player is from the mouse
        if (distanceToMouse > distanceThreshold)
        {
            currentVelocity = currentMaxVelocity;
        }
        else
        {
            currentVelocity = (distanceToMouse / distanceThreshold) * currentMaxVelocity;
        }

        // calculates the players new coordinates and adds them to their current position
        Vector2 twoDPos = new(mousePosition.x - this.transform.position.x, mousePosition.y - this.transform.position.y);
        Vector3 newPos = new(twoDPos.normalized.x * currentVelocity, twoDPos.normalized.y * currentVelocity, 0);
        this.transform.position += newPos;

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "AstralBody") {
            // Get the current size of the player
            Vector3 currScale = this.transform.localScale;
            // Calculate the change of scale that will be added to the players scale
            float delta = collision.transform.localScale.x * growthPercentage *
                            ((currScale.x - collision.transform.localScale.x) / Mathf.Abs(currScale.x - collision.transform.localScale.x));
            this.transform.localScale = new(currScale.x + delta, currScale.y + delta, currScale.z + delta);

            // Remove collided gameObject if it is not the sun
            Destroy(collision.gameObject);
        }
        else  // win and lose conditions here (assumes collision with sun)
        {
            if(this.transform.localScale.x > collision.transform.localScale.x)
            {
                //Debug.Log("YOU WIN");
            }
            else
            {
                //Debug.Log("YOU LOSE");
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //This code broken pls fix <3 -Brandan
        if (collision.CompareTag("Bounds"))
        {
            Debug.Log("Touched Collider");
            currentVelocity = 0.0f;
        }

        //Checks for Asteroid Collision
        if (collision.CompareTag("Asteroid"))
        {
            Debug.Log("Touched Asteroid");
            //TODO: Reduce Player Size when this happens
            //Additionally, we can handle this collision in the asteroid script if it is easier
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("AstralBody"))
        {
            // Get the current size of the player
            Vector3 currScale = this.transform.localScale;
            // Calculate the change of scale that will be added to the players scale
            float delta = collision.transform.localScale.x * growthPercentage *
                            ((currScale.x - collision.transform.localScale.x) / Mathf.Abs(currScale.x - collision.transform.localScale.x));
            this.transform.localScale = new(currScale.x + delta, currScale.y + delta, currScale.z + delta);
            //I think we need a more drastic change in size -Brandan
            Destroy(collision.gameObject);
        }

    }
}
