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
    }
    
    public void GamePlayerEnter()
    {
        _player.SetActive(true);
    }
    
    public void GamePlayerExit()
    {
        _player.SetActive(false);
    }
    
    
    
}