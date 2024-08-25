using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        if (transform.position.x <= maxLeft)
        {
            EventSystem.current.SetSelectedGameObject(null);
            LeftButton.SetActive(false);
        }

        else
        {
            
            LeftButton.SetActive(true);
        }

        if (transform.position.x >= maxRight)
        {
            EventSystem.current.SetSelectedGameObject(null);
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
            ReachedLeft = false;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
       
    }

    private void MoveRight()
    {
        if (transform.position.x < maxRight)
        {
            ReachedRight = false;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        }

       
    }
}
