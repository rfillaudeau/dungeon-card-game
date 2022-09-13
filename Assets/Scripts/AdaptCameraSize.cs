using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AdaptCameraSize : MonoBehaviour
{
    private Camera _camera;
    private float _defaultSize;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _defaultSize = _camera.orthographicSize;
    }

    private void Start()
    {
        if (_camera.aspect >= 0.55f)
        {
            _camera.orthographicSize = _defaultSize;
        }
        else if (_camera.aspect >= 0.48f)
        {
            _camera.orthographicSize = 27f;
        }
        else if (_camera.aspect >= 0.46f)
        {
            _camera.orthographicSize = 29f;
        }
        else if (_camera.aspect >= 0.44f)
        {
            _camera.orthographicSize = 31f;
        }
        else if (_camera.aspect >= 0.40f)
        {
            _camera.orthographicSize = 32f;
        }
        else
        {
            _camera.orthographicSize = _defaultSize;
        }
    }
}
