using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Tooltip("If the player's size/scale becomes smaller than this, they lose")]
    public float minSize;
    

    public AudioClip hitSound;
    public AudioClip absorbSound;
    public GameObject hitEffect;

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
        else if (collision.gameObject.CompareTag("AstralBody") || collision.gameObject.CompareTag("Asteroid"))
        {
            // Get the current size of the player
            Vector3 currScale = this.transform.localScale;
            float currSize = this.GetComponent<CircleCollider2D>().bounds.size.magnitude;
            // Calculate the change of scale that will be added to the players scale
            float delta;
            float collidedSize;
            if (collision.gameObject.CompareTag("Asteroid"))  // polygon collider
            {
                //Math to play particleFX where the player was hit, rotate relative position on circle collider
                Vector3 hitPos = collision.contacts[0].point;
                Vector3 hitDir = (hitPos - this.transform.position).normalized;
                float angle = Mathf.Atan2(hitDir.y, hitDir.x) * Mathf.Rad2Deg;
                Instantiate(hitEffect, hitPos, Quaternion.Euler(0,0,angle));
                //SoundFX for getting hit
                AudioSource.PlayClipAtPoint(hitSound, hitPos);

                collidedSize = collision.gameObject.GetComponent<PolygonCollider2D>().bounds.size.magnitude;
                delta = growthPercentage * collidedSize;
                if(collidedSize > currSize)
                {
                    delta *= -1;
                }
            }
            else if (collision.gameObject.CompareTag("AstralBody"))  // circle collider
            {
                //SoundFX for absorbing planet
                Vector3 absorbPos = this.transform.position;
                AudioSource.PlayClipAtPoint(absorbSound, absorbPos);

                collidedSize = collision.gameObject.GetComponent<CircleCollider2D>().bounds.size.magnitude;
                delta = growthPercentage * collidedSize;
                if (collidedSize > currSize)
                {
                    delta *= -1;
                }
            }
            else  // just in case lol
            {
                delta = collision.transform.localScale.x * growthPercentage *
                            ((currScale.x - collision.transform.localScale.x) / Mathf.Abs(currScale.x - collision.transform.localScale.x));
                
            }
            Debug.Log(delta);
            // set size and destroy collided object
            this.transform.localScale = new(currScale.x + delta, currScale.y + delta, currScale.z + delta);
            Destroy(collision.gameObject);

            if (this.transform.localScale.x <= minSize)
            {
                SceneManager.LoadScene("LoseScreen");
            }
        }
        else if (collision.gameObject.CompareTag("Sun"))
        {
            if (this.transform.localScale.x > collision.transform.localScale.x)
            {
                SceneManager.LoadScene("WinScreen");
            }
            else
            {
                SceneManager.LoadScene("LoseScreen");
            }
        }

    }
}
