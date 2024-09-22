using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSoldier : UnitSoldier
{
    public SwordSoldier()
    {
        this.unitType = UnitType.SwordSoldier;
    }
    protected override void Start()
    {
        base.Start();
    }
}
