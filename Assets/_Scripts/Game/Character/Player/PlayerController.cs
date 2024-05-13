using System;
using FrameWork.EventCenter;
using FrameWork.Factories;
using FrameWork.Utils;
using UnityEngine;

public class PlayerController
{
    
    private const string PrefabPath = "Prefabs/Player";
    
    //モデル
    private GameObject _player;

    public PlayerController()
    {
        _player = ObjectFactory.Instance.CreateGameObject(PrefabPath);
        _player.SetActive(false);
    }
    
    public void GamePlayerEnter(Vector3 position)
    {
        DebugLogger.Log("Player Spawn");
        _player.transform.position = position;
        _player.SetActive(true);
    }
    
    public void GamePlayerExit()
    {
        _player.SetActive(false);
    }

    /// <summary>
    /// プレイヤーのインスタンスを返す
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlayer()
    {
        return _player;
    }

}