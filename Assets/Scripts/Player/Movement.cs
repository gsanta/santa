using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D _rigidBody;

    private Animator _animator;

    private Vector2 _velocity;

    private Vector2 _direction;

    private Vector2 _lastVelocity = Vector2.zero;

    public float VerticalMovement { get; set; }

    public float HorizontalMovement { get; set; }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
        _direction.Normalize();
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _velocity = _direction * speed;

        if (_velocity.magnitude > 0)
        {
            _lastVelocity = _velocity;
        }

        _animator.SetFloat("horizontalMovement", _lastVelocity.x);
        _animator.SetFloat("verticalMovement", _lastVelocity.y);
        _animator.SetBool("isMoving", _velocity.magnitude > 0);
    }

    void FixedUpdate()
    {
        _rigidBody.AddForce(_velocity * speed, ForceMode2D.Impulse);
    }
}
