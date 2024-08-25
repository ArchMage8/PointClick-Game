using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private int maxLeft;
    [SerializeField] private int maxRight;

    [SerializeField] private GameObject LeftButton;
    [SerializeField] private GameObject RightButton;

    private bool ReachedLeft;
    private bool ReachedRight;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private ClickObjects clickObjects;

    private void Start()
    {
        clickObjects = FindObjectOfType<ClickObjects>();
    }

    void Update()
    {
        if (clickObjects.CanClick)
        {
            if (isMovingLeft)
            {
                MoveLeft();
            }
            else if (isMovingRight)
            {
                MoveRight();
            }
        }

        if (ReachedLeft)
        {
            LeftButton.SetActive(false);
        }

        else
        {
            LeftButton.SetActive(true);
        }

        if (ReachedRight)
        {
            RightButton.SetActive(false);
        }

        else
        {
            RightButton.SetActive(true);
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
        if (transform.position.x > maxLeft)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else if(transform.position.x <= maxLeft)
        {
            ReachedLeft = true;
        }
    }

    private void MoveRight()
    {
        if (transform.position.x < maxRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        }

        else if (transform.position.x >= maxRight)
        {
            ReachedRight = true;
        }
    }
}
