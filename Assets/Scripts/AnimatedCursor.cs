using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AnimatedCursor : MonoBehaviour
{
    public Texture2D[] defaultCursorTextures;
    public Texture2D[] interactableCursorTextures;
    private ClickObjects clickObjects;
    private CameraMover camMover;


    public float frameRate = 0.1f;
    private Vector2 hotSpot = Vector2.zero;

    [SerializeField] private Vector2 DefaultHotSpot = new Vector2(7,0);
    [SerializeField] private Vector2 HoverHotSpot;

    public LayerMask interactableLayer;

    private Texture2D[] currentCursorTextures;
    private int currentFrame;
    private float timer;
    private HasBeenInteractedHolder hasBeenInteractedHolder;


    void Start()
    {
        clickObjects = FindObjectOfType<ClickObjects>();
        camMover = FindObjectOfType<CameraMover>();

        currentCursorTextures = defaultCursorTextures;
        if (currentCursorTextures.Length > 0)
        {
            Cursor.SetCursor(currentCursorTextures[0], hotSpot, CursorMode.Auto);
        }
    }

    void Update()
    {
        UpdateCursorAnimation();
    }

    void UpdateCursorAnimation()
    {
        CheckForInteractable();

        if (currentCursorTextures.Length > 1)
        {
            timer += Time.deltaTime;

            if (timer >= frameRate)
            {
                timer = 0f;
                currentFrame = (currentFrame + 1) % currentCursorTextures.Length;
                Cursor.SetCursor(currentCursorTextures[currentFrame], hotSpot, CursorMode.Auto);
            }
        }
    }

    void CheckForInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, interactableLayer);

        if (hit.collider != null && !camMover.isMoving)
        {
            hasBeenInteractedHolder = hit.collider.gameObject.GetComponent<HasBeenInteractedHolder>();

            if (clickObjects.CanClick)
            {
                hotSpot = HoverHotSpot;
                currentCursorTextures = interactableCursorTextures;
            }
        }
        else
        {
            hotSpot = DefaultHotSpot;
            currentCursorTextures = defaultCursorTextures;
        }
    }

    void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
