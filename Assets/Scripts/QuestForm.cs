using UnityEngine;

public class QuestForm : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    private GameObject _pageRef;

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
            transform.position = curPosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    public void SetPageRef(GameObject pageref)
    {
        _pageRef = pageref;
    }

    public void ExitButton()
    {
        _pageRef.GetComponent<DragAndDrop>().ExitButton();
    }

}