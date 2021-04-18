using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeRegistrar : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Sausage _sausage;
    [SerializeField] private Vector2 _delta;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Don't touch
        _delta = eventData.delta;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _sausage.UpdateDirection(_delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _delta = eventData.delta - _delta;
        _sausage.UpdateDirection(_delta);
    }
}
