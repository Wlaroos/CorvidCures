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

    private GameManager _gm;

    public CustomEvents SingleClickEvent;
    public CustomEvents DoubleClickEvent;

    private void Awake()
    {
        _gm = GameObject.FindObjectOfType<GameManager>();

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
        if (_gm.GetMenu() == false)
        {
            Debug.Log("Single");
            _waitingForSecondClick = false;
            // Do single click actions here
            SingleClickEvent.Invoke();
        }
    }

    private void DoubleClick()
    {
        if (_gm.GetMenu() == false)
        {
            Debug.Log("Double");
            // Do double click actions here
            DoubleClickEvent.Invoke();
        }
    }

    private void OnMouseEnter()
    {
        if (_gm.GetMenu() == false)
        {
            _child.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
            _child.SetActive(false);
    }

}
