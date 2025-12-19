using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class HiddenItem : MonoBehaviour
{
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private bool _interactable = false;
    private bool _collected;

    public string Name => gameObject.name;
    public bool Collected => _collected;
    public event Action<HiddenItem> OnCollected;
    public event Action<HiddenItem> OnDestroyed;

    public bool Interactable
    {
        get => _interactable;
        set => SetInteractable(value);
    }

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void SetInteractable(bool interactable)
    {
        if (_collected) return;
        if (interactable == _interactable) return;

        _collider.enabled = interactable;
        _interactable = interactable;
    }

    public void Collect()
    {
        if (_collected) return;
        SetInteractable(false);
        _spriteRenderer.enabled = false;
        OnCollected?.Invoke(this);
        _collected = true;
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
