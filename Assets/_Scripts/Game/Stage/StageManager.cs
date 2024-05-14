using FrameWork.Factories;
using FrameWork.Resource;
using FrameWork.Utils;
using Unity.Mathematics;
using UnityEngine;

public enum MapEnum
{
    Tutorial,
    Map1,
}
public class StageManager
{
    //データパス
    private const string _stageManagerGameObject = "Prefabs/Managers/StageManager";
    private const string _stageDataPath = "StageData/StageData";
    private GameObject _mapParent;
    
    private StageSO _stageSo;           //ステージデータ
    private AreaData _currentArea;      //現在エリアデータ
    private int _currentMapIndex = 0;
    private int _currentAreaIndex = 0;  //現在エリアインデックス
    private int _previousAreaIndex = 0; // 以前のステージインデックス
    private int _maxAreaCount = 0;

    public StageManager()
    {
        DebugLogger.Log("Constrct StageManager");
        _mapParent = ObjectFactory.Instance.CreateGameObject(_stageManagerGameObject);
        _mapParent.SetActive(true);
        //必要なもののロード
        _stageSo = (StageSO)ResManager.Instance.GetAssetCache<ScriptableObject>(_stageDataPath);
        if (!_stageSo)
        {
            DebugLogger.Log("StageSo is null");
        }
    }

    public void GamePlayerEnter()
    {
        //テスト
        GeneratorMap((int)MapEnum.Map1);
    }
    
    /// <summary>
    /// マップを生成する
    /// </summary>
    private void GeneratorMap(int stageIndex)
    {
        //マップデータをゲット
        var areaArray = _stageSo.Maps[stageIndex].AreaDatas;
        _currentMapIndex = stageIndex;
        
        //数値のリセット
        _currentAreaIndex = 0;
        _previousAreaIndex = _currentAreaIndex - 1;
        _maxAreaCount = areaArray.Length;
        //マップデータからマップを生成する
        foreach (var mapData in areaArray)
        {
            var map = GameObject.Instantiate(mapData.MapPrefab, mapData.Pos, Quaternion.identity,_mapParent.transform);
            DebugLogger.Log("MapDone");
        }

        _currentArea = areaArray[_currentAreaIndex];
        
    }
    
    /// <summary>
    /// 現在のマップを返す
    /// </summary>
    /// <returns></returns>
    public AreaData GetCurrentMapData()
    {
        return _currentArea;
    }
    
    /// <summary>
    /// 現在のマップの仕様がプレイヤーにカメラをついていくか
    /// </summary>
    /// <returns></returns>
    public bool IsFollowPlayer() => _stageSo.Maps[_currentMapIndex].IsFollowPlayer;
}