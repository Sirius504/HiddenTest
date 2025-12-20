using System;
using System.Collections;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class HiddenItem : MonoBehaviour
{
    [Inject] private WinLoseConditions _winLoseConditions;
    [SerializeField] private ItemInfo _itemInfo;
    [SerializeField] private float _animationTime = 1f;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private bool _interactable = false;
    private bool _collected;
    private Coroutine _dissapearCoroutine;

    public string Name => gameObject.name;
    public bool Collected => _collected;
    public event Action<HiddenItem> OnCollected;
    public event Action<HiddenItem> OnDestroyed;

    public bool Interactable
    {
        get => _interactable;
        set => SetInteractable(value);
    }
    public ItemInfo ItemInfo => _itemInfo;

    void Awake()
    {
        if (_itemInfo == null) throw new NullReferenceException($"ItemInfo for object {Name} is empty.");
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
        if (_winLoseConditions.CurrentState != GameState.Running
            || _collected 
            || !Interactable) return;
        SetInteractable(false);
        _dissapearCoroutine = StartCoroutine(DissapearCoroutine());
        OnCollected?.Invoke(this);
        _collected = true;
    }

    private IEnumerator DissapearCoroutine()
    {
        float elapsedTime = 0f;

        var startColor = _spriteRenderer.color;
        while (elapsedTime < _animationTime)
        {            
            var newColor = startColor;
            elapsedTime += Time.deltaTime;
            var alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / _animationTime);
            newColor.a = alpha;
            _spriteRenderer.color = newColor;
            yield return null;
        }

        _dissapearCoroutine = null;
        _spriteRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
        StopAllCoroutines();
        _dissapearCoroutine = null;
    }
}
