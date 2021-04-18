using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Vector3 _startPosition = new Vector3(5, 3, 0);

    private void Start()
    {
        ResetLine();
    }

    public void DrawLine(Vector3 startPosition, Vector3 direction)
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, direction);
    }

    public void ResetLine()
    {
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position + _startPosition);
    }
}
