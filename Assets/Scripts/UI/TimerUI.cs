using System;
using TMPro;
using UnityEngine;
using VContainer;

public class TimerUI : MonoBehaviour
{
    [Inject]
    private Timer _timer;

    [SerializeField] private TextMeshProUGUI _textMesh;

    public void Update()
    {
        _textMesh.text = TimeSpan.FromSeconds(_timer.TimeRemaining).ToString(@"mm\:ss");
    }
}
