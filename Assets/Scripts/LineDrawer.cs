using UnityEngine;

// Used to draw direction in which sausage will jump

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private void Awake()
    {
        ResetLine();
    }

    public void DrawNewLine(Vector3 startPosition, Vector3 endPosition)
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
    }

    public void ResetLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
