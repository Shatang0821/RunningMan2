using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FrameWork.Interface;
using FrameWork.Utils;
using Unity.Mathematics;
using UnityEngine;

public class MapManager : UnitySingleton<MapManager>, IInitializable
{
    private MapStruct[] _mapArray;
    public void Init()
    {
        _mapArray = new MapStruct[]
        {
            new MapStruct("Stage1", "Map/MapPrefab/Stage1", "Map/MapTextData/Stage1", 0, 0,null),
        };
        LoadDataAsync().Forget();
    }

    // Start is called before the first frame update
    private async UniTaskVoid LoadDataAsync()
    {
        Debug.Log("Start Load");
        List<UniTask<(TextAsset, GameObject,int[,])>> tasks = new List<UniTask<(TextAsset, GameObject,int[,])>>();

        foreach (var mapData in _mapArray)
        {
            tasks.Add(LoadAllResources(mapData));
        }
        try
        {
            var results = await UniTask.WhenAll(tasks);
            var stageIndex = 0;
            foreach (var result in results)
            {
                if (result.Item1 != null && result.Item2 != null)
                {
                    Debug.Log($"Loaded resources for {result.Item1.name} successfully.");
                    GameObject.Instantiate(result.Item2, new Vector3(0, 0, 0), quaternion.identity);
                    _mapArray[stageIndex].MapBlock = result.Item3;
                }
                else
                {
                    Debug.LogError("Failed to load one or more resources.");
                }

                stageIndex++;
            }

            Debug.Log("All maps loaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while loading maps: {ex.Message}");
        }
    }

    private async UniTask<(TextAsset, GameObject, int[,])> LoadAllResources(MapStruct mapStruct)
    {
        var textAssetTask = Resources.LoadAsync<TextAsset>(mapStruct.MapDataPath).ToUniTask();
        var gameObjectTask = Resources.LoadAsync<GameObject>(mapStruct.MapPrefabPath).ToUniTask();
        var (loadedTextAsset, loadedGameObject) = await UniTask.WhenAll(textAssetTask, gameObjectTask);

        // CSVデータをint[,]に変換
        int[,] mapData = ParseCsvData((TextAsset)loadedTextAsset);
        
        return ((TextAsset,GameObject,int[,]))(loadedTextAsset, loadedGameObject, mapData);
    }
    private int[,] ParseCsvData(TextAsset csvData)
    {
        string[] lines = csvData.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        int numRows = lines.Length;
        int numCols = lines[0].Split(',').Length;

        int[,] map = new int[numRows, numCols];

        for (int i = 0; i < numRows; i++)
        {
            string[] items = lines[i].Split(',');
            for (int j = 0; j < numCols; j++)
            {
                map[i, j] = items[j].Trim() == "Empty" ? 0 : 1;
            }
        }

        return map;
    }
}



public struct MapStruct
{
    public readonly string MapName; //マップ名前
    public readonly string MapPrefabPath; //マップのプレハブパス
    public readonly string MapDataPath; //マップのデータパス
    public readonly int X; //マップのx位置
    public readonly int Y; //マップのy位置
    public int[,] MapBlock; 

    public MapStruct(string mapName, string mapPrefabPath, string mapDataPath, int x, int y,int[,] mapBlock)
    {
        this.MapName = mapName;
        this.MapPrefabPath = mapPrefabPath;
        this.MapDataPath = mapDataPath;
        this.X = x;
        this.Y = y;
        this.MapBlock = mapBlock;
    }
}