using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

[RequireComponent(typeof(Canvas))]
public class ResultUI : MonoBehaviour
{
    [Inject]
    private WinLoseConditions _conditions;

    [Inject]
    private SceneController _sceneController;

    private Canvas _canvas;

    [SerializeField] private TextMeshProUGUI _labelMesh;
    [SerializeField] private Button _restartButton;


    private void Awake()
    {
        _canvas = GetComponent<Canvas>();   
    }

    private void Start()
    {
        _canvas.enabled = false;
        _conditions.OnGameStateChanged += OnStateChanged;
        _restartButton.onClick.AddListener(ReloadScene);
    }

    private void ReloadScene()
    {
        _sceneController.ReloadScene();
    }

    private void OnStateChanged(GameState newState)
    {
        _canvas.enabled = true;
        _labelMesh.text = newState.ToString();
    }

    private void OnDestroy()
    {
        _conditions.OnGameStateChanged -= OnStateChanged;
        _restartButton.onClick.RemoveAllListeners();
    }
}
