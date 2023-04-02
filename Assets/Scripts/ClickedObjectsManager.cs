using System.Collections.Generic;
using UnityEngine;

public class ClickedObjectsManager : MonoBehaviour
{
    // List of clicked objects
    public List<GameObject> clickedObjects;

    void Update()
    {
        // LMB
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Get the collider of the object at the mouse position
            Collider2D clickedCollider = Physics2D.OverlapPoint(mousePos);

            // If an object was clicked and has DragAndDrop component
            if (clickedCollider != null && clickedCollider.GetComponent<DragAndDrop>() != null)
            {
                GameObject clickedObject = clickedCollider.gameObject;

                // If the object has not been clicked before
                if (!clickedObjects.Contains(clickedObject))
                {
                    // Add the object to the list of clicked objects
                    clickedObjects.Add(clickedObject); 
                }

                // Reorganize the list of clicked objects based on their recency in the list
                clickedObjects.Remove(clickedObject);
                clickedObjects.Add(clickedObject);

                // Update the order layer of the clicked objects based on their recency in the list
                for (int i = 0; i < clickedObjects.Count; i++)
                {
                    clickedObjects[i].GetComponent<SpriteRenderer>().sortingOrder = (i * 2) + 2;
                    clickedObjects[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = clickedObjects[i].GetComponent<SpriteRenderer>().sortingOrder - 1;
                }
            }
        }
    }
}
