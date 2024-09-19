using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public enum UnitType
{
    Barrack,
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
    public bool canSpawnSoldier;
}
public class BuildManager : LocalSingleton<BuildManager>
{
    [SerializeField] Builds[] builds;

    public Builds[] Builds {  get { return builds; } }
}
