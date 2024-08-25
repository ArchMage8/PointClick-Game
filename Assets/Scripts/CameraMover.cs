using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private int maxLeft;
    [SerializeField] private int maxRight;

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
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (transform.position.x < maxRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        }
    }
}