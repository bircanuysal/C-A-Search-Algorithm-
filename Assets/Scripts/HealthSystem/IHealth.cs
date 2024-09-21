using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }

    public void TakeDamage(float damageValue);
    public void UnitOnDie();

}
