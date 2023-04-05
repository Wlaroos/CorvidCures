using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class DragAndDrop : MonoBehaviour
{
    public delegate void DragEventHandler(Vector2 delta);

    public event DragEventHandler DragEvent;

    private void OnMouseDragEvent(Vector2 delta)
    {
        DragEvent?.Invoke(delta);
    }

    private static bool _isDragging = false;
    private float lastClickTime = 0f;
    private float doubleClickTimeThreshold = 0.2f;
    private bool _waitingForSecondClick = false;

    private Vector2 _initialPosition;
    private Vector2 _dragOffset;

    private Rigidbody2D _rb;
    private HingeJoint2D _hj;
    private GameObject _child;

    private BoxCollider2D _boundsCollider;

    private Vector2 _previousPosition;
    private Vector2 _velocity;
    [SerializeField] private float _velocityMult = 0.1f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _hj = GetComponent<HingeJoint2D>();

        _boundsCollider = GetComponentInParent<BoxCollider2D>(); // Assumes the bounds are on the parent object

        _child = transform.GetChild(0).gameObject;
        _child.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (Time.time - lastClickTime < doubleClickTimeThreshold)
        {
            // Double click event
            DoubleClick();
            CancelInvoke(nameof(SingleClick));
            return;
        }

        lastClickTime = Time.time;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (!_isDragging && hit.collider != null && hit.collider.gameObject == gameObject)
        {
            _isDragging = true;
            _initialPosition = transform.position;
            _dragOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void SingleClick()
    {
        Debug.Log("Single");
        _waitingForSecondClick = false;
        // Do single click actions here
    }

    private void DoubleClick()
    {
        Debug.Log("Double");
        // Do double click actions here
    }

    private void OnMouseDrag()
    {
        if (!_isDragging)
        {
            return;
        }

        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3)_dragOffset;

        // Calculate velocity based on the difference in position between the current and previous frames
        _velocity = (newPosition - _previousPosition) / Time.deltaTime;

        // Clamp the position within the bounds of the outer object
        Vector3 clampedPosition = _boundsCollider.bounds.ClosestPoint(newPosition);
        transform.position = clampedPosition;

        // Set the connected anchor of the hinge joint to the current position of the object
        _hj.connectedAnchor = transform.position;

        // Update previous position for next frame's velocity calculation
        _previousPosition = newPosition;

        // Call drag event
        OnMouseDragEvent(newPosition - _previousPosition);
    }

    private void OnMouseUp()
    {
        if (!_isDragging)
        {
            return;
        }

        _isDragging = false;
        _hj.enabled = true;

        if (!_waitingForSecondClick && Vector2.Distance(transform.position, _initialPosition) < 0.01f)
        {
            _waitingForSecondClick = true;
            Invoke(nameof(SingleClick), doubleClickTimeThreshold);
        }
        else
        {
            _waitingForSecondClick = false;
        }

        _rb.velocity = _velocity * _velocityMult;
    }

    private void OnMouseEnter()
    {
        if (!_isDragging)
        {
            _child.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!_isDragging)
        {
            _child.SetActive(false);
        }
        else
        {
            Invoke(nameof(DelayMouseExit), .1f);
        }
    }

    public void Release()
    {
        if (!_isDragging)
        {
            return;
        }

        _isDragging = false;
        _hj.enabled = false;
        transform.position = _initialPosition;
    }

    private void DelayMouseExit()
    {
        if (!_isDragging)
        {
            _child.SetActive(false);
        }
    }
}
