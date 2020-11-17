using UnityEngine;

public class ProjectileAutoDestructor : MonoBehaviour
{
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}