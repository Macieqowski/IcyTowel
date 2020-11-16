using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public void InjectTransformToFollow(Transform followTransform)
    {
        _followTransform = followTransform;
        _camera.transform.position = _followTransform.position;
    }
    protected void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    protected void Update()
    {
        _camera.transform.position = Vector2.Lerp(_camera.transform.position, _followTransform.position, _delay * Time.deltaTime);
    }

    [SerializeField]
    private float _delay = default;

    private Transform _followTransform;
    private Camera _camera;
}
