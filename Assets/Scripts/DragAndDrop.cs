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

    private Vector2 _previousPosition;
    private Vector2 _velocity;
    [SerializeField] private float _velocityMult = 0.1f;

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
    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3)_dragOffset;

        // Calculate velocity based on the difference in position between the current and previous frames
        _velocity = (newPosition - _previousPosition) / Time.deltaTime;

        // Clamp the position within the bounds of the outer object
        Vector3 clampedPosition = _boundsCollider.bounds.ClosestPoint(newPosition);
        transform.position = clampedPosition;

        // Set the connected anchor of the hinge joint to the current position of the object
        _hingeJoint.connectedAnchor = transform.position;

        // Update previous position for next frame's velocity calculation
        _previousPosition = newPosition;
    }

    private void OnMouseUp()
    {
        _hingeJoint.enabled = true;

        // Apply final velocity to the object's rigidbody
        _rigidbody2D.velocity = _velocity * _velocityMult;
    }

    public void Release()
    {
        _hingeJoint.enabled = false;
        transform.position = _initialPosition;
    }
}
