using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextFlashcardHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 initialLocation;

    private void Start()
    {
        initialLocation = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; //Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initialLocation;
    }
}
