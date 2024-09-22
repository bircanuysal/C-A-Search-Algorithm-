using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantBuild : UnitBuild
{
    public PowerPlantBuild()
    {
        this.unitType = UnitType.PowerPlant;
    }
    protected override void Start()
    {
        base.Start();
    }
}
