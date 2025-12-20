using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraResize : MonoBehaviour
{
    [SerializeField] private float _pixelsToUnitRatio = 108;
    [SerializeField] private Vector2 _targetResolution = new(1920f, 1080f);
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        float screenAspect = Screen.width / (float)Screen.height;

        var unitsDimensions = _targetResolution / _pixelsToUnitRatio;
        float orthoSizeY = (unitsDimensions.y / 2.0f) * screenAspect / _camera.aspect;
        float orthoSizeX = (unitsDimensions.x / 2.0f) / _camera.aspect;

        Camera.main.orthographicSize = Mathf.Max(orthoSizeY, orthoSizeX);
    }
}
