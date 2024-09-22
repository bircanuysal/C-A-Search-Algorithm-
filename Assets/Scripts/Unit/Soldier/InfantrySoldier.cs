using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantrySoldier : UnitSoldier
{
    public InfantrySoldier()
    {
        this.unitType = UnitType.InfantrySoldier;
    }
    protected override void Start()
    {
        base.Start();
    }
}
