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
    private int wallNum;  // right is 1, top is 2, left is 3, bottom is 4
    private Vector3 wallPos;

    // Start is called before the first frame update
    void Start()
    {
        wallNum = 0;
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
        Vector3 newPosOffset = new(twoDPos.normalized.x * currentVelocity, twoDPos.normalized.y * currentVelocity, 0);
        Vector3 newPos = this.transform.position + newPosOffset;
        switch (wallNum)  // don't love those magic numbers (+- 15). Done this way to assure new collision occurs
        {
            case 1:  // right wall
                if(newPos.x < wallPos.x)
                {
                    newPos.x = wallPos.x - 15;

                }
                break;
            case 2:  // top wall
                if (newPos.y < wallPos.y)
                {
                    newPos.y = wallPos.y - 15;

                }
                break;
            case 3:  // left wall
                if (newPos.x > wallPos.x)
                {
                    newPos.x = wallPos.x + 15;

                }
                break;
            case 4:  // bottom wall
                if (newPos.y > wallPos.y)
                {
                    newPos.y = wallPos.y + 15;

                }
                break;
            default:
                break;
        }
        this.transform.position = newPos;
        wallNum = 0;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Bounds"))
        {
            // jank af
            switch (collision.gameObject.name) // right is 1, top is 2, left is 3, bottom is 4
            {
                case "RightWall":
                    wallNum = 1;
                    wallPos = collision.gameObject.transform.position; break;
                case "LeftWall":
                    wallNum = 3;
                    wallPos = collision.gameObject.transform.position; break;
                case "TopWall":
                    wallNum = 2;
                    wallPos = collision.gameObject.transform.position; break;
                case "BottomWall":
                    wallNum = 4;
                    wallPos = collision.gameObject.transform.position; break;
            }
        }
        else if (collision.gameObject.CompareTag("AstralBody"))
        {
            Debug.Log("Astral");
            // Get the current size of the player
            Vector3 currScale = this.transform.localScale;
            // Calculate the change of scale that will be added to the players scale
            float delta = collision.transform.localScale.x * growthPercentage *
                            ((currScale.x - collision.transform.localScale.x) / Mathf.Abs(currScale.x - collision.transform.localScale.x));
            this.transform.localScale = new(currScale.x + delta, currScale.y + delta, currScale.z + delta);
            //I think we need a more drastic change in size -Brandan
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Sun"))
        {
            if (this.transform.localScale.x > collision.transform.localScale.x)
            {
                //Debug.Log("YOU WIN");
            }
            else
            {
                //Debug.Log("YOU LOSE");
            }
        }

    }
}
