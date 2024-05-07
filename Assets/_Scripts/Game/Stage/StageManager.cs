using FrameWork.Resource;
using FrameWork.Utils;
using UnityEngine;

public class StageManager
{
    private const string _checkPointPath = "Prefabs/CheckPoint";
    private const string _mapDataPath = "MapDatas/MapData_1";
    
    private GameObject _checkPointPrefab;

    private MapSO _mapSo;
    public StageManager()
    {
        DebugLogger.Log("Constrct StageManager");
        _checkPointPrefab = ResManager.Instance.GetAssetCache<GameObject>(_checkPointPath);
        _mapSo = (MapSO)ResManager.Instance.GetAssetCache<ScriptableObject>(_mapDataPath);
        if (!_checkPointPrefab)
        {
            DebugLogger.Log("CheckPoint is null");
        }

        if (!_mapSo)
        {
            DebugLogger.Log("MapSo is null");
        }
    }

    public void InitCheckPoint()
    {
        foreach (var mapData in _mapSo.MapDatas)
        {
            GameObject.Instantiate(_checkPointPrefab, mapData.Pos, Quaternion.identity);
        }
    }
}