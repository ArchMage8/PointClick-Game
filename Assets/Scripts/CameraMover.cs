using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public static CameraMover Instance { get; private set; }

    public float moveSpeed = 5f;
    [SerializeField] private int maxLeft;
    [SerializeField] private int maxRight;

    [SerializeField] private GameObject leftVisual;
    [SerializeField] private GameObject rightVisual;

    private bool ReachedLeft;
    private bool ReachedRight;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    public bool isMoving;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
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
            leftVisual.SetActive(false);
        }
        else if (ReachedRight && !ReachedLeft)
        {
            rightVisual.SetActive(false);
        }
        else if (!ReachedLeft && !ReachedRight)
        {
            leftVisual.SetActive(true);
            rightVisual.SetActive(true);
        }
    }

    public void GoingLeft()
    {
        ReachedRight = false;
        isMovingLeft = true;
        isMoving = true;
    }

    public void StoppingLeft()
    {
        isMovingLeft = false;
        isMoving = false;
    }

    public void GoingRight()
    {
        ReachedLeft = false;
        isMovingRight = true;
        isMoving = true;
    }

    public void StoppingRight()
    {
        isMovingRight = false;
        isMoving = false;
    }

    private void MoveLeft()
    {
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
