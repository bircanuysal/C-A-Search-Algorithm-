using UnityEngine;

[CreateAssetMenu(fileName = "NewMapData", menuName = "Map Data", order = 1)]
public class MapData : ScriptableObject
{
    public int cellXSize;
    public int cellYSize;
}

