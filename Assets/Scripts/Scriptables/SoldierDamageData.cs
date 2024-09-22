using UnityEngine;

[System.Serializable]
public struct SoldierDamages
{
    public UnitType type;
    public float damage;
}
[CreateAssetMenu(fileName = "NewSoldierDamageData", menuName = "Soldier Damage Data", order = 1)]
public class SoldierDamageData : ScriptableObject
{
    public SoldierDamages[] soldierDamages;
}
