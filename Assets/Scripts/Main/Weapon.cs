using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Range => _range;
    public void Fire(Vector2 direction)
    {
        if (_cooldown > 0)
        {
            return;
        }
        var bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * _firePower, ForceMode2D.Impulse);
        _cooldown = _firingCooldown;

    }

    protected void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }
    }

    [SerializeField]
    private GameObject _bulletPrefab = default;

    [SerializeField]
    private Transform _firePoint = default;

    [SerializeField]
    private float _firingCooldown = default;

    [SerializeField]
    private float _firePower = default;

    [SerializeField]
    private float _range = default;

    private float _cooldown;
}
