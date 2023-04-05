using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;

[Serializable]
public class CustomEvents : UnityEvent { }

public class EventTest : MonoBehaviour
{

    public CustomEvents OnEvent;

}