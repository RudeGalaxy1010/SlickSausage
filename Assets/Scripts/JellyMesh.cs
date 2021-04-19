using UnityEngine;

// This code was here: https://youtu.be/Kwh4TkQqqf8

public class JellyMesh : MonoBehaviour
{
    [SerializeField] private float _intensity = 1f;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _stiffness = 1f;
    [SerializeField] private float _damping = 0.75f;

    private Mesh _originalMesh, _cloneMesh;
    private MeshRenderer _meshRenderer;
    private JellyVertex[] _jellyVertexes;
    private Vector3[] _vertexArray;


    private void Start()
    {
        _originalMesh = GetComponent<MeshFilter>().sharedMesh;
        _cloneMesh = Instantiate(_originalMesh);
        GetComponent<MeshFilter>().sharedMesh = _cloneMesh;
        _meshRenderer = GetComponent<MeshRenderer>();
        _jellyVertexes = new JellyVertex[_cloneMesh.vertices.Length];

        for (int i = 0; i < _jellyVertexes.Length; i++)
        {
            _jellyVertexes[i] = new JellyVertex(i, transform.TransformPoint(_cloneMesh.vertices[i]));
        }
    }

    private void FixedUpdate()
    {
        _vertexArray = _originalMesh.vertices;

        for (int i = 0; i < _jellyVertexes.Length; i++)
        {
            Vector3 target = transform.TransformPoint(_vertexArray[_jellyVertexes[i].Id]);
            float intensity = (1 - (_meshRenderer.bounds.max.y - target.y) / _meshRenderer.bounds.size.y) * _intensity;
            _jellyVertexes[i].Shake(target, _mass, _stiffness, _damping);
            target = transform.InverseTransformPoint(_jellyVertexes[i].Position);
            _vertexArray[_jellyVertexes[i].Id] = Vector3.Lerp(_vertexArray[_jellyVertexes[i].Id], target, intensity);
        }

        _cloneMesh.vertices = _vertexArray;
    }
}

public class JellyVertex
{
    public int Id;
    public Vector3 Position;
    public Vector3 Velocity, Force;

    public JellyVertex(int id, Vector3 position)
    {
        Id = id;
        Position = position;
    }

    public void Shake(Vector3 target, float mass, float stiffness, float damping)
    {
        Force = (target - Position) * stiffness;
        Velocity = (Velocity + Force / mass) * damping;
        Position += Velocity;

        if ((Velocity + Force * (1 + 1 / mass)).magnitude < 0.001f)
        {
            Position = target;
        }
    }
}
