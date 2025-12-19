using UnityEngine;

[CreateAssetMenu(fileName = "Level Settings", menuName = "Scriptable Objects/Level Settings")]
public class LevelSettings : ScriptableObject
{
    public int MaxItemsAvailable;    
    public int TimerInSeconds;    
}
