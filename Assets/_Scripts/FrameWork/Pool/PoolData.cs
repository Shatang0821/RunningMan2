using FrameWork.Pool;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool/PoolData",fileName = "newPoolData")]
public class PoolData : ScriptableObject
{
    public PoolCategory[] allPools;
}

[System.Serializable]
public class PoolCategory
{
    public string categoryName;
    public UnityObjectPool[] pools;
}