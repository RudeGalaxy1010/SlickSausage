using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Sausage : MonoBehaviour
{
    [SerializeField] private float _force = 10f;
    [SerializeField] private float _maxForce = 100f;
    [SerializeField] private DirectionDrawer _directionDrawer;

    private Rigidbody _rigidbody;
    private bool _isPressed = false;
    private Vector3 _direction;

    public void UpdateDirection(Vector2 delta)
    {
        if (_isPressed)
        {
            _direction = new Vector3(-delta.x % _maxForce, -delta.y % _maxForce, 0);

            if (CheckGround())
            {
                _directionDrawer.DrawLine(transform.position, _direction);
            }
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
            
            if (CheckGround())
            {
                _rigidbody.AddForce(_direction * _force, ForceMode.Impulse);
                _directionDrawer.ResetLine();
            }
        }

        if (transform.position.y < -5)
        {
            // Reload level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private bool CheckGround()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, 100);
        if (raycastHit.distance <= 1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
