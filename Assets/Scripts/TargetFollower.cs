using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _targetToFollow;

    private Vector3 _offset;
    private Vector3 _target;

    private void Start()
    {
        _offset = transform.position - _targetToFollow.position;
    }

    private void Update()
    {
        if (transform.position != _targetToFollow.position)
        {
            _target = new Vector3(_targetToFollow.position.x, 0, 0) + _offset;
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }
    }
}
