using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Data/Map", fileName = "MapData")]
public class MapSO : ScriptableObject
{
    //生成マップデータ
    public AreaData[] AreaDatas;
}

[Serializable]
public class AreaData
{
    /// <summary>
    /// マッププレハブ
    /// </summary>
    public GameObject MapPrefab;
    /// <summary>
    /// マップの生成座標
    /// </summary>
    public Vector2 Pos;
    /// <summary>
    /// マップにおけるプレイヤーの生成位置
    /// </summary>
    public Vector2 PlayerSpawnPos;
}