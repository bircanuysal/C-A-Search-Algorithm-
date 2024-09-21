using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public enum UnitType
{
    InfantryBarracks,
    HeavyBarracks,
    SniperBarracks,
    PowerPlant,
    SoldierUnit,
    None
}
[Serializable]
public struct Builds
{
    public UnitType type;
    public string name;
    public Image image;
    public Vector2 size;
    public int health;
    public bool canSpawnSoldier;
}
public class BuildManager : LocalSingleton<BuildManager>
{
    [SerializeField] Builds[] builds;

    public Builds[] Builds {  get { return builds; } }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 mousePos = Extensions.GetMouseWorldPosition();
            UnitFactory.CreateBuild(PoolableObjectTypes.InfantryBarracks, mousePos);
        }
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Build.UnitBuild.Invoke();
        }
    }
}
