using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAndInteract : MonoBehaviour
{
    private float lastClickTime = 0f;
    private float doubleClickTimeThreshold = 0.15f;
    private bool _waitingForSecondClick = false;

    private SpriteRenderer _sr;
    private GameObject _child;

    public CustomEvents SingleClickEvent;
    public CustomEvents DoubleClickEvent;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();

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
    }

    private void OnMouseUp()
    {

        if (!_waitingForSecondClick)
        {
            _waitingForSecondClick = true;
            Invoke(nameof(SingleClick), doubleClickTimeThreshold);
        }
        else
        {
            _waitingForSecondClick = false;
        }
    }

    private void SingleClick()
    {
        Debug.Log("Single");
        _waitingForSecondClick = false;
        // Do single click actions here
        SingleClickEvent.Invoke();
    }

    private void DoubleClick()
    {
        Debug.Log("Double");
        // Do double click actions here
        DoubleClickEvent.Invoke();
    }

    private void OnMouseEnter()
    {
        _child.SetActive(true);
    }

    private void OnMouseExit()
    {
        _child.SetActive(false);
    }

}
