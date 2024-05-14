using System;
using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using TMPro;
using UnityEngine;

public class HintManager : UnitySingleton<HintManager>
{
    private Dictionary<GameObject, TextMeshProUGUI> _tmpTables;

    protected override void Awake()
    {
        base.Awake();
        _tmpTables = new Dictionary<GameObject, TextMeshProUGUI>();
    }

    private void Start()
    {
        InitializeTMPTable();
    }

    public bool InitializeTMPTable()
    {
        foreach (Transform child in transform)
        {
            // TextMeshPro コンポーネントを取得
            var tmp = child.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                // _tmpTables に追加
                _tmpTables.Add(child.gameObject, tmp);
                DebugLogger.Log(child.name);
                tmp.enabled = false;
            }
            else
            {
                //問題があります
                DebugLogger.Log("TMPの初期化にTMPじゃないオブジェクトがあります" + child.name);
                return false;
            }   
        }

        return true;
    }

    
    public void EnableTmp(GameObject gameObject,bool isEnable)
    {
        if (_tmpTables.TryGetValue(gameObject, out TextMeshProUGUI tmp))
        {
            tmp.enabled = isEnable;
        }
    }
}
