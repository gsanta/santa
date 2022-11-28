using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D _rigidBody;

    private Animator _animator;

    private Vector2 _velocity;

    private Vector2 _lastVelocity = Vector2.zero;

    public float VerticalMovement { get; set; }

    public float HorizontalMovement { get; set; }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        _velocity = new Vector2(x, y) * speed;

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
