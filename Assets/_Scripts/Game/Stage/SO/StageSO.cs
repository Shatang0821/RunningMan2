using UnityEngine;

[CreateAssetMenu(menuName = "Data/Stage", fileName = "StageData")]
public class StageSO : ScriptableObject
{
    public MapSO[] Maps;
}