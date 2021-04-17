using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeRegistrar : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private Sausage _sausage;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        _sausage.UpdateDirection(delta);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Don't touch
    }
}
