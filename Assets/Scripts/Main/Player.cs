using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public bool IsDead { get; private set; }

    public int LevelsVisited { get; private set; }

    public void SetRenderDistance(int renderDistance)
    {
        _renderDistance = renderDistance;
        _recentCollisions = new Queue<Transform>(2 * _renderDistance);
    }
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

    public void Reset()
    {
        IsDead = false;
        transform.position = new Vector2(0f, 0.3f);
        _rigidbody.AddForce(_rigidbody.velocity * -1f, ForceMode2D.Impulse);
    }

    public void Move(float horizontalMovementNormalized)
    {
        var newHorizontalVelocity = Vector2.ClampMagnitude(new Vector2(_rigidbody.velocity.x + horizontalMovementNormalized * _moveForce, 0f), _maxHorizontalSpeed);
        _rigidbody.AddForce(newHorizontalVelocity);

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
        if (_isInAir)
        {
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x * -1f * _airTorque, 0f));
        }

        var speed = _isInAir ? 0f : Mathf.Abs(_rigidbody.velocity.x);
        _animator.SetFloat("Speed", speed);
        if (_recentCollisions.Count > _renderDistance * 2 - 1)
        {
            _recentCollisions.Dequeue();
        }
    }

    protected void OnBecameInvisible()
    {
        IsDead = true;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("LevelFloor"))
        {
            var contactPointNormal = collision.GetContact(0).normal;
            if (contactPointNormal.y < 0 || contactPointNormal.x != 0)
            {
                return;
            }
            if (_isInAir)
            {
                _isInAir = false;
                _animator.SetTrigger("EndJump");
            }
            if (_recentCollisions.Contains(collision.transform.parent))
            {
                return;
            }
            _recentCollisions.Enqueue(collision.transform.parent);
            LevelsVisited++;
        }

        if (collision.collider.CompareTag("Bullet"))
        {
            IsDead = true;
            LevelsVisited = 0;
        }
    }

    [SerializeField]
    private float _jumpForce = default;

    [SerializeField]
    private float _moveForce = default;

    [SerializeField]
    private float _maxHorizontalSpeed = default;

    [SerializeField]
    private float _airTorque = default;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Queue<Transform> _recentCollisions;
    private int _renderDistance;

    private bool _isInAir;
}
