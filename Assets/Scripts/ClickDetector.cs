using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pointerPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var ray = Physics2D.Raycast(pointerPosition, Vector2.zero);
            HandleClick(ray);
        }
    }

    private void HandleClick(RaycastHit2D ray)
    {
        if (ray.collider == null) return;
        if (!ray.collider.TryGetComponent<HiddenItem>(out var item)) return;
        item.Collect();
    }
}
