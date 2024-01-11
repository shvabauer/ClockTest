using UnityEngine;
using UnityEngine.EventSystems;

public class Rotatable : MonoBehaviour, IPointerUpHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField] private RectTransform _transform;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _startPos;
    private Vector2 _lastMousePosition;

    private void OnEnable()
    {
        _rectTransform.position = _startPos.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.position = _startPos.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - _lastMousePosition;

        Vector3 newPosition = _rectTransform.position + new Vector3(diff.x, diff.y, transform.position.z);
        
        _rectTransform.position = newPosition;
        _lastMousePosition = currentMousePosition;

        Vector3 dir = newPosition - _transform.position;
        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        _transform.rotation = rot;
    }
}
