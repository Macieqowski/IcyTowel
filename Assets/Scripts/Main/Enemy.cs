using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected void Awake()
    {
        _weapon = Instantiate(_weaponPrefab, _weaponHolder).GetComponent<Weapon>();
    }

    protected void Update()
    {
        var rayLeft = Physics2D.Raycast(_weapon.transform.position, Vector2.left, _weapon.Range, LayerMask.GetMask("Player"));
        var rayRight = Physics2D.Raycast(_weapon.transform.position, Vector2.right, _weapon.Range, LayerMask.GetMask("Player"));

        if (rayLeft.rigidbody != null)
        {
            if (rayLeft.rigidbody.CompareTag("Player"))
            {
                _weapon.Fire(Vector2.left);
            }
        }

        if (rayRight.rigidbody != null)
        {
            if (rayRight.rigidbody.CompareTag("Player"))
            {
                _weapon.Fire(Vector2.right);
            }
        }
    }

    [SerializeField]
    private GameObject _weaponPrefab = default;

    [SerializeField]
    private Transform _weaponHolder = default;

    private Weapon _weapon;
}