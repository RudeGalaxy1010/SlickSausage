using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Landscape : MonoBehaviour
{
    [SerializeField] private List<GameObject> _segmentPrefabs = new List<GameObject>();
    [SerializeField] private float _segmentLenght = 7;
    [SerializeField] private int _startSegmentsCount = 3;
    [SerializeField] private float _distanceToCameraToDestroy;

    [SerializeField] private List<GameObject> _segments;
    private Transform _mainCameraTransform;
    private float _mainCameraPositionX;
    private float _lastSegmentPositionX;

    // Initialize segments, fill the list from the end
    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
        _segments = new List<GameObject>(_startSegmentsCount);

        // Spawn initial segment
        var initialSegment = SpawnSegment(_segmentPrefabs[0], Vector3.zero);
        _segments.Add(initialSegment);

        var halfSegmentsCount = Mathf.FloorToInt(_startSegmentsCount / 2);
        // From -maxCount + 1 (initial segment already spawned) to maxCount
        for (int i = -halfSegmentsCount + 1, index = 0; i <= halfSegmentsCount; i++, index++)
        {
            var randomPrefab = GetRandomPrefab();
            var newSegment = SpawnSegmentNextTo(randomPrefab, _segments[index]);
            _segments.Add(newSegment);
        }
        _lastSegmentPositionX = _segments[0].transform.position.x;
    }

    private void Update()
    {
        _mainCameraPositionX = _mainCameraTransform.position.x;

        // Check if segment need to be destroyed
        if (_mainCameraPositionX - _lastSegmentPositionX > _distanceToCameraToDestroy)
        {
            // Destroy segment
            var segmentToDestroy = _segments[0];
            _segments.Remove(segmentToDestroy);
            Destroy(segmentToDestroy);

            // Spawn new segment
            var newSegment = SpawnSegmentNextTo(GetRandomPrefab(), _segments.Last());
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

    private GameObject GetRandomPrefab()
    {
        return _segmentPrefabs[Random.Range(0, _segmentPrefabs.Count)];
    }
}
