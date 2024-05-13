using FrameWork.Factories;
using FrameWork.Resource;
using FrameWork.Utils;
using Unity.Mathematics;
using UnityEngine;

public enum StageEnum
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
    private int _currentAreaIndex = 0;  //現在エリアインデックス
    private int _previousIndex = 0;             // 以前のステージインデックス
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
        GeneratorMap((int)StageEnum.Tutorial);
    }
    
    /// <summary>
    /// マップを生成する
    /// </summary>
    private void GeneratorMap(int stageIndex)
    {
        //マップデータをゲット
        var areaArray = _stageSo.Maps[stageIndex].AreaDatas;
        //数値のリセット
        _currentAreaIndex = 0;
        _previousIndex = _currentAreaIndex - 1;
        _maxAreaCount = areaArray.Length;
        DebugLogger.Log(_maxAreaCount);
        //マップデータからマップを生成する
        foreach (var mapData in areaArray)
        {
            var map = GameObject.Instantiate(mapData.MapPrefab, mapData.Pos, Quaternion.identity,_mapParent.transform);
            DebugLogger.Log("MapDone");
            //map.SetActive(false);
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
    // public void InitCheckPoint()
    // {
    //     foreach (var mapData in _mapSo.MapDatas)
    //     {
    //         GameObject.Instantiate(_checkPointPrefab, mapData.Pos, Quaternion.identity);
    //     }
    // }
}