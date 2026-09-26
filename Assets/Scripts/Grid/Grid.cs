using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Material _material;
    private Camera _camera;

    private void Start()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    private void LateUpdate()
    {
        if (_camera == null || _material == null)
            return;

        Vector3 cameraPosition =
            _camera.transform.position;

        float height =
            _camera.orthographicSize * 2f;

        float width =
            height * _camera.aspect;

        transform.position = new Vector3(
            cameraPosition.x,
            cameraPosition.y,
            transform.position.z
        );

        transform.localScale = new Vector3(
            width,
            height,
            1f
        );

        _material.SetVector(
            "_CameraPosition",
            cameraPosition
        );

        _material.SetFloat(
            "_CameraZoom",
            _camera.orthographicSize
        );

        _material.SetFloat(
            "_CameraAspect",
            _camera.aspect
        );
    }
}