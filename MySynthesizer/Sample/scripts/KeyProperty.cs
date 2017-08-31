﻿
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KeyProperty : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private bool downed;
    public Sprite normal;
    public Sprite pushed;
    private readonly UnityEvent onKeyDownEvent;
    private readonly UnityEvent onKeyUpEvent;
    public KeyProperty()
    {
        onKeyDownEvent = new UnityEvent();
        onKeyUpEvent = new UnityEvent();
    }
    public UnityEvent OnKeyDownEvent
    {
        get
        {
            return onKeyDownEvent;
        }
    }
    public UnityEvent OnKeyUpEvent
    {
        get
        {
            return onKeyUpEvent;
        }
    }
    void Start()
    {
        image = gameObject.GetComponent<Image> ();
        if (normal != null)
        {
            image.sprite = normal;
        }
        downed = false;
    }
    private void down()
    {
        if (!downed)
        {
            downed = true;
            onKeyDownEvent.Invoke();
            if (pushed != null)
            {
                image.sprite = pushed;
            }
        }
    }
    private void up()
    {
        if (downed)
        {
            downed = false;
            onKeyUpEvent.Invoke();
            if (normal != null)
            {
                image.sprite = normal;
            }
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        down();
    }
    public void OnPointerUp(PointerEventData data)
    {
        up();
    }
    public void OnPointerEnter(PointerEventData data)
    {
        if(data.eligibleForClick)
        {
            data.pointerPress = gameObject;
            data.pointerDrag = gameObject;
            //data.rawPointerPress = gameObject;
            down();
        }
    }
    public void OnPointerExit(PointerEventData data)
    {
        up();
    }
}
