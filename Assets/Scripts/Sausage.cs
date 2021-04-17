using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Sausage : MonoBehaviour
{
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;
    private bool _isPressed = false;
    private Vector3 _direction;

    private Vector3 mousePos;

    public void UpdateDirection(Vector2 delta)
    {
        if (_isPressed)
        {
            _direction = new Vector3(-delta.x, -delta.y, 0);
            _direction = (_direction * 100).normalized;
            Debug.Log(_direction);
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isPressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isPressed = false;
            _rigidbody.AddForce(_direction * _force, ForceMode.Impulse);
        }
    }
}
