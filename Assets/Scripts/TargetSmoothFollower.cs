using UnityEngine;

// Used to smoothly move object to target
// Make camera follow the sausage !only by X axis

public class TargetSmoothFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;

    private Vector3 _offset;
    private Vector3 _targetPosition;

    private void Start()
    {
        _offset = transform.position - _target.position;
    }

    private void Update()
    {
        if (transform.position != _target.position)
        {
            _targetPosition = new Vector3(_target.position.x, 0, 0) + _offset;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 0.05f);
        }
    }
}
