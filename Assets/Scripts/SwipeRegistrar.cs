using UnityEngine;

// Used to read user input

public class SwipeRegistrar : MonoBehaviour
{
    [SerializeField] private SausageMover _sausage;
    [SerializeField] private float _maxDelta = 100f;

    private Vector3 _startPosition, _endPosition, _delta;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            _endPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            _delta = _startPosition - _endPosition;
            UpdateSausageDirection(_delta);
        }
        if (Input.GetMouseButtonUp(0))
        {
            UpdateSausageDirection(_delta);
        }
    }

    private void UpdateSausageDirection(Vector3 delta)
    {
        _sausage.UpdateDirection(new Vector3(delta.x % _maxDelta, delta.y % _maxDelta, 0));
    }
}
