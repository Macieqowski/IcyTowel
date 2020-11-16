using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public void Jump()
    {
        if (_isInAir)
        {
            return;
        }

        _rigidbody.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        _animator.SetTrigger("Jump");
        _isInAir = true;
    }

    public void Move(float horizontalMovementNormalized)
    {
        var currentHorizontalVelocity = _rigidbody.velocity.x;
        var newHorizontalVelocity = horizontalMovementNormalized * _moveForce;
        if (Mathf.Abs(currentHorizontalVelocity + newHorizontalVelocity) > _maxHorizontalSpeed)
        {
            return;
        }
        _rigidbody.AddForce(new Vector2(newHorizontalVelocity, 0f));

        var angle = Mathf.Atan2(0f, horizontalMovementNormalized) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        var speed = _isInAir ? 0f : Mathf.Abs(_rigidbody.velocity.x);
        _animator.SetFloat("Speed", speed);
    }

    //todo: sprawdzić co się stanie jak uderzy głową w podłogę od spodu
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("LevelFloor"))
        {
            if (_isInAir)
            {
                _isInAir = false;
                _animator.SetTrigger("EndJump");
            }
        }
    }

    [SerializeField]
    private float _jumpForce = default;

    [SerializeField]
    private float _moveForce = default;

    [SerializeField]
    private float _maxHorizontalSpeed = default;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool _isInAir;
}
