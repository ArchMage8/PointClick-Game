using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private float maxLeft;
    [SerializeField] private float maxRight;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private ClickObjects clickObjects;

    private void Start()
    {
        clickObjects = FindObjectOfType<ClickObjects>();
        if (leftButton != null && rightButton != null)
        {
            leftButton.onClick.AddListener(GoingLeft);
            rightButton.onClick.AddListener(GoingRight);
        }
    }

    private void Update()
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

        UpdateButtonStates();
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
        else
        {
            StoppingLeft();
        }
    }

    private void MoveRight()
    {
        if (transform.position.x < maxRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            StoppingRight();
        }
    }

    private void UpdateButtonStates()
    {
        if (leftButton != null)
        {
            leftButton.interactable = transform.position.x > maxLeft;
        }
        if (rightButton != null)
        {
            rightButton.interactable = transform.position.x < maxRight;
        }
    }
}
