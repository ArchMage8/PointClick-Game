using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCursor : MonoBehaviour
{
    public Texture2D[] defaultCursorTextures;
    public Texture2D[] interactableCursorTextures;
    private ClickObjects clickObjects;


    public float frameRate = 0.1f;
    public Vector2 hotSpot = Vector2.zero;
    public LayerMask interactableLayer;

    private Texture2D[] currentCursorTextures;
    private int currentFrame;
    private float timer;

    void Start()
    {
        clickObjects = FindObjectOfType<ClickObjects>();

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

        if (hit.collider != null && clickObjects.CanClick)
        {
            currentCursorTextures = interactableCursorTextures;
        }
        else
        {
            currentCursorTextures = defaultCursorTextures;
        }
    }

    void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
