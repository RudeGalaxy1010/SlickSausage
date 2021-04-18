using System.Collections.Generic;
using UnityEngine;

// Used to generate new and remove old segments according to main camera position

public class Landscape : MonoBehaviour
{
    [SerializeField] private List<GameObject> _segmentPrefabs = new List<GameObject>();
    [SerializeField] private float _segmentLenght = 7;
    [SerializeField] private int _startSegmentsCount = 3;
    [SerializeField] private float _distanceToCameraToDestroy;

    private List<GameObject> _segments;
    private Transform _mainCameraTransform;
    private float _mainCameraPositionX;
    private float _lastSegmentPositionX;

    // Initialize segments, fill the list from the end
    private void Awake()
    {
        _mainCameraTransform = Camera.main.transform;
        _segments = new List<GameObject>(_startSegmentsCount);

        // Spawn initial segment
        var initialSegment = SpawnSegment(_segmentPrefabs[0], Vector3.zero);
        _segments.Add(initialSegment);
        _lastSegmentPositionX = 0;

        var halfSegmentsCount = (int)Mathf.Floor(_startSegmentsCount / 2);

        // From -maxCount [Inclusive] to maxCount [Exclusive] - initial segment already spawned
        for (int i = -halfSegmentsCount, lastSegmentIndex = 0; i < halfSegmentsCount; i++, lastSegmentIndex++)
        {
            var randomPrefab = GetRandomPrefab();
            var lastSegment = _segments[lastSegmentIndex];
            var newSegment = SpawnSegmentNextTo(randomPrefab, lastSegment);
            _segments.Add(newSegment);
        }
    }

    private void Update()
    {
        _mainCameraPositionX = _mainCameraTransform.position.x;

        // Check if segment need to be destroyed
        if (_mainCameraPositionX - _lastSegmentPositionX > _distanceToCameraToDestroy)
        {
            DestroySegment(_segments[0]);

            // Spawn new segment
            var lastSegment = _segments[_segments.Count - 1];
            var newSegment = SpawnSegmentNextTo(GetRandomPrefab(), lastSegment);
            _segments.Add(newSegment);

            _lastSegmentPositionX = _segments[0].transform.position.x;
        }
    }

    private GameObject SpawnSegmentNextTo(GameObject segmentPrefab, GameObject targetSegment)
    {
        var newSegmentPosition = targetSegment.transform.position + Vector3.right * _segmentLenght;
        var newSegment = SpawnSegment(segmentPrefab, newSegmentPosition);
        return newSegment;
    }

    private GameObject SpawnSegment(GameObject segmentPrefab, Vector3 position)
    {
        var newSegment = Instantiate(segmentPrefab, position, Quaternion.identity, transform);
        return newSegment;
    }

    private void DestroySegment(GameObject segment)
    {
        _segments.Remove(segment);
        Destroy(segment);
    }

    private GameObject GetRandomPrefab()
    {
        return _segmentPrefabs[Random.Range(0, _segmentPrefabs.Count)];
    }
}
