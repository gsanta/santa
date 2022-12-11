using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    [SerializeField] private float jabDistance = 0.8f;

    private Rigidbody2D _rigidBody;

    private Animator _animator;

    private Character _character;

    private Vector2 _velocity;

    private Vector2 _direction;

    private Vector2 _lastVelocity = Vector2.zero;

    private bool _isJabbing = false;

    private bool _isMoving = false;

    public float VerticalMovement { get; set; }

    public float HorizontalMovement { get; set; }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
        _direction.Normalize();
    }

    public void SetJabbing()
    {
        if (_isJabbing) { return; }

        _isJabbing = true;
        Invoke("ResetJabbing", 0.3f);
        _animator.SetBool("isJabbing", true);
        var opponent = CharacterStore.GetInstance().GetClosestOpponent(_character);

        if (Vector2.Distance(_character.GetPosition(), opponent.GetPosition()) < jabDistance)
        {
            _lastVelocity = (opponent.GetPosition() - _character.GetPosition()).normalized;
            opponent.Rigidbody2D.AddForce(_lastVelocity * 50f, ForceMode2D.Impulse);
        }
    }

    private void ResetJabbing()
    {
        _isJabbing = false;
        _animator.SetBool("isJabbing", false);
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        _velocity = _direction * speed;

        if (_velocity.magnitude > 0)
        {
            _lastVelocity = _velocity;
            _isMoving = true;
        }

        _animator.SetFloat("horizontalMovement", _lastVelocity.x);
        _animator.SetFloat("verticalMovement", _lastVelocity.y);
        _animator.SetBool("isMoving", _velocity.magnitude > 0);
    }

    void FixedUpdate()
    {
        if (_isMoving)
        {
            _rigidBody.AddForce(_velocity * speed, ForceMode2D.Impulse);
        }
    }
}
