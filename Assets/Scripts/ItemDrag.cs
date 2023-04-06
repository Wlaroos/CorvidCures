using UnityEngine;

public class ItemDrag : MonoBehaviour
{
    public Vector2 bounds;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector2 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = new Vector3(
                Mathf.Clamp(curPosition.x, originalPosition.x - bounds.x, originalPosition.x + bounds.x),
                Mathf.Clamp(curPosition.y, originalPosition.y - bounds.y, originalPosition.y + bounds.y),
                curPosition.z
            );
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(originalPosition, new Vector3(bounds.x * 2, bounds.y * 2, 0));
    }
}
