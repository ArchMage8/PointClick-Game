using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private int maxLeft;
    [SerializeField] private int maxRight;

    [SerializeField] private GameObject leftVisual;
    [SerializeField] private GameObject rightVisual;

    private bool ReachedLeft;
    private bool ReachedRight;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    void Update()
    {
        if (isMovingLeft)
        {
            MoveLeft();
        }
        else if (isMovingRight)
        {
            MoveRight();
        }

        if (ReachedLeft && !ReachedRight)
        {
            SpriteRenderer LeftRenderer = leftVisual.GetComponent<SpriteRenderer>();
            LeftRenderer.enabled = false;
        }

        else if (ReachedRight && !ReachedLeft)
        {
            SpriteRenderer RightRenderer = leftVisual.GetComponent<SpriteRenderer>();
            RightRenderer.enabled = false;
        }

        else if(!ReachedLeft && !ReachedRight) { 
        
                SpriteRenderer LeftRenderer = leftVisual.GetComponent<SpriteRenderer>();
                LeftRenderer.enabled = true;

                SpriteRenderer RightRenderer = leftVisual.GetComponent<SpriteRenderer>();
                RightRenderer.enabled = true;
        }
    }
    public void GoingLeft()
    {
        isMovingLeft = true;
    }
    public void StoppingLeft()
    {
        isMovingLeft = false;
    }
    public void GoingRight()
    {
        isMovingRight = true;
    }
    public void StoppingRight()
    {
        isMovingRight = false;
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if (transform.position.x > maxLeft)
        {
            ReachedLeft = false;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else
        {
            ReachedLeft = true;
        }
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (transform.position.x < maxRight)
        {
            ReachedRight = false;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            ReachedRight = true;
        }
    }
}