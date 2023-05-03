using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragObject : MonoBehaviour
{
    public UnityEvent OnEndEvent;

    private Camera mainCamera;
    private Vector3 mouseOffset;
    private bool click = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public Vector3 GetMouseWorldPosition() { return mainCamera.ScreenToWorldPoint(Input.mousePosition); }

    private void OnMouseDown()
    {
        click = true;
        mouseOffset = gameObject.transform.position - GetMouseWorldPosition();
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mouseOffset;
    }

    private void OnMouseUp()
    {
        if (click)
        {
            click = false;
            OnEndEvent?.Invoke();
        }
    }
}
