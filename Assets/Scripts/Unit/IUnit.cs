using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUnit
{
    public UnitType unitType { get; }
    public Sprite image { get; set; }
    public int health { get; set; }
    public bool selectable {  get; set; }
    public bool canMove {  get; set; }
    public PoolableObjectTypes poolableObjectTypes { get; set; }

}
