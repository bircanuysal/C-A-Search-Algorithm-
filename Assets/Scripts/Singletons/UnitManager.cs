using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public enum UnitType
{
    Barracks,
    PowerPlant,
    MachineGunBuild,
    SoldierUnit,
    None
}
[Serializable]
public struct Units
{
    public UnitType type;
    public string name;
    public Sprite image;
    public Vector2 size;
    public int health;
    public bool canSpawnSoldier;
}
public class UnitManager : LocalSingleton<UnitManager>
{
    [SerializeField] Units[] units;

    public Units[] Units {  get { return units; } }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Build.UnitBuild.Invoke();
        }
    }
}
