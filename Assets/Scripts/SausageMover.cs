using UnityEngine;
using UnityEngine.SceneManagement;

// Make sausage jump
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SausageMover : MonoBehaviour
{
    [SerializeField] private float _force = 7f;
    [SerializeField] private LineDrawer _directionDrawer;

    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Jump(_direction);
        }

        if (transform.position.y < -5)
        {
            // Reloading level
            // Temporary solution
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Jump(Vector3 direction)
    {
        if (CheckGround())
        {
            _rigidbody.AddForce(direction * _force, ForceMode.Impulse);
            _directionDrawer.ResetLine();
        }
    }

    public void UpdateDirection(Vector2 direction)
    {
        if (Input.GetMouseButton(0) && CheckGround())
        {
            _direction = direction;
            _directionDrawer.DrawNewLine(transform.position, transform.position + _direction);
        }
    }

    private bool CheckGround()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, 1);
        if (raycastHit.point != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
