using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUnitBuild
{
    public string buildName { get; set; }
    public Vector2 size { get; set; }
    public bool canProduceSoldiers { get; set; }

    public Vector3 spawnPos { get; set; }

}

