using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FrameWork.Interface;
using Unity.Mathematics;
using UnityEngine;

public class MapManager : MonoBehaviour, IInitializable
{
    private MapStruct[] _mapArray;
    private List<TextAsset> _dataList;

    public void Init()
    {
        _dataList = new List<TextAsset>();
        _mapArray = new MapStruct[]
        {
            new MapStruct("Stage1", "Map/MapPrefab/Stage1", "Map/MapTextData/Stage1", 0, 0),
        };
        LoadDataAsync().Forget();
    }

    // Start is called before the first frame update
    private async UniTaskVoid LoadDataAsync()
    {
        Debug.Log("Start Load");
        List<UniTask<(TextAsset, GameObject)>> tasks = new List<UniTask<(TextAsset, GameObject)>>();

        foreach (var mapData in _mapArray)
        {
            tasks.Add(LoadAllResources(mapData));
        }
        try
        {
            var results = await UniTask.WhenAll(tasks);

            foreach (var result in results)
            {
                if (result.Item1 != null && result.Item2 != null)
                {
                    Debug.Log($"Loaded resources for {result.Item1.name} successfully.");
                    GameObject.Instantiate(result.Item2, new Vector3(0, 0, 0), quaternion.identity);
                }
                else
                {
                    Debug.LogError("Failed to load one or more resources.");
                }
            }

            Debug.Log("All maps loaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while loading maps: {ex.Message}");
        }
    }

    private async UniTask<(TextAsset, GameObject)> LoadAllResources(MapStruct mapStruct)
    {
        var textAssetTask = Resources.LoadAsync<TextAsset>(mapStruct.MapDataPath).ToUniTask();
        var gameObjectTask = Resources.LoadAsync<GameObject>(mapStruct.MapPrefabPath).ToUniTask();

        var (loadedTextAsset, loadedGameObject) = await UniTask.WhenAll(textAssetTask, gameObjectTask);

        return ((TextAsset, GameObject))(loadedTextAsset, loadedGameObject);
    }
}

public struct MapStruct
{
    public readonly string MapName; //マップ名前
    public readonly string MapPrefabPath; //マップのプレハブパス
    public readonly string MapDataPath; //マップのデータパス
    public readonly int X; //マップのx位置
    public readonly int Y; //マップのy位置

    public MapStruct(string mapName, string mapPrefabPath, string mapDataPath, int x, int y)
    {
        this.MapName = mapName;
        this.MapPrefabPath = mapPrefabPath;
        this.MapDataPath = mapDataPath;
        this.X = x;
        this.Y = y;
    }
}