using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitSoldier
{
    public int attackDamageValue { get; set; }
    public void PerformAttack();
}
public interface ISoldierState
{
    void Attack(IUnitSoldier soldier);
}
