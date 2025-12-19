using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Settings", menuName = "Scriptable Objects/Level Settings")]
public class LevelSettings : ScriptableObject
{
    public int MaxItemsAvailable;    
    public int TimerInSeconds;
    public bool ShowIcons;
    public bool ShowNames;
    public List<ItemInfo> ItemInfos;
}
