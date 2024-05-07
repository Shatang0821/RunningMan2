using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Maps", fileName = "MapData")]
public class MapSO : ScriptableObject
{
    public List<MapData> MapDatas;
}

[Serializable]
public class MapData
{
    public bool Enable;
    public Vector2 Pos;
}