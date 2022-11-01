using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputPanel : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    private Camera _camera;

    [HideInInspector] public UnityEvent<Vector3> OnDown;    
    [HideInInspector] public UnityEvent<Vector3> OnUp;
    [HideInInspector] public UnityEvent<Vector3> OnDrag;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var position = _camera.ScreenToWorldPoint(eventData.position);
        OnDown?.Invoke(position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var position = _camera.ScreenToWorldPoint(eventData.position);
        OnUp?.Invoke(position);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        var position = _camera.ScreenToWorldPoint(eventData.position);
        OnDrag?.Invoke(position);
    }
}
