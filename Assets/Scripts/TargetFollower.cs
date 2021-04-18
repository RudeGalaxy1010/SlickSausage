using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _targetToFollow;

    private Vector3 _offset;
    private Vector3 _targetPosition;

    private void Start()
    {
        _offset = transform.position - _targetToFollow.position;
    }

    private void Update()
    {
        if (transform.position != _targetToFollow.position)
        {
            _targetPosition = new Vector3(_targetToFollow.position.x, 0, 0) + _offset;
            var smoothTargetPosition = Vector3.MoveTowards(transform.position, _targetPosition, 0.05f);
            transform.position = smoothTargetPosition;
        }
    }
}
