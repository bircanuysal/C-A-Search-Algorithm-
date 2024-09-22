using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSoldier : UnitSoldier
{
    public ArcherSoldier()
    {
        this.unitType = UnitType.ArcherSoldier;
    }
    protected override void Start()
    {
        base.Start();
    }
}
