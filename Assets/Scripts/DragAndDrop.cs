using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DragAndDrop : MonoBehaviour
{
    private Vector2 _initialPosition;
    private Vector2 _dragOffset;

    private Rigidbody2D _rigidbody2D;
    private HingeJoint2D _hingeJoint;

    private BoxCollider2D _boundsCollider;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _hingeJoint = GetComponent<HingeJoint2D>();

        _boundsCollider = GetComponentInParent<BoxCollider2D>(); // Assumes the bounds are on the parent object
    }

    private void OnMouseDown()
    {
        _initialPosition = transform.position;
        _dragOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _rigidbody2D.isKinematic = true;
    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3)_dragOffset;

        // Clamp the position within the bounds of the outer object
        Vector3 clampedPosition = _boundsCollider.bounds.ClosestPoint(newPosition);
        transform.position = clampedPosition;

        // Set the connected anchor of the hinge joint to the current position of the object
        _hingeJoint.connectedAnchor = transform.position;
    }

    private void OnMouseUp()
    {
        _rigidbody2D.isKinematic = false;
        _hingeJoint.enabled = true;
    }

    public void Release()
    {
        _rigidbody2D.isKinematic = false;
        _hingeJoint.enabled = false;
        transform.position = _initialPosition;
    }
}
