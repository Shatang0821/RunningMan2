using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FrameWork.Pool;
using FrameWork.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private Transform _trapContainer;
    [SerializeField] private Tilemap    _tilemap;
    [SerializeField] private TileBase   _tileBase;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3    _spawnPos;

    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private void OnEnable()
    {
        DebugLogger.Log("TileGenerator OnEnable");
        GeneratorAsync().Forget();
    }

    private void OnDisable()
    {
        ClearGeneratedObjectsAsync().Forget();
    }

    /// <summary>
    /// 指定したタイルに指定したプレハブを生成
    /// </summary>
    public async UniTaskVoid GeneratorAsync()
    {
        foreach (var pos in _tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (!_tilemap.HasTile(localPlace)) continue;

            TileBase tile = _tilemap.GetTile(localPlace);
            if (tile != null && tile == _tileBase)
            {
                Vector3 worldPosition = _tilemap.CellToWorld(localPlace);
                
                // オブジェクトを表示する
                GameObject spawnedObject = PoolManager.Release(_prefab, worldPosition + _spawnPos, Quaternion.identity);
                _spawnedObjects.Add(spawnedObject);
                // 正しい位置に回転させる
                Matrix4x4 tileMatrix = _tilemap.GetTransformMatrix(localPlace);
                spawnedObject.transform.rotation = tileMatrix.rotation;
                
                // 非同期の待機
                await UniTask.Yield();
            }
            
        }
    }
    
    /// <summary>
    /// 生成したすべてのオブジェクトを削除（非同期）
    /// </summary>
    public async UniTaskVoid ClearGeneratedObjectsAsync()
    {
        foreach (var obj in _spawnedObjects)
        {
            if(!obj)
                continue;
            obj.SetActive(false);
            await UniTask.Yield();
        }
        _spawnedObjects.Clear();
    }
}
