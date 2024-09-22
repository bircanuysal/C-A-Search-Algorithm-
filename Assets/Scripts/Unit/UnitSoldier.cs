using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class UnitSoldier : Unit , IUnitSoldier
{
    public int attackDamageValue { get; set; }

    private SpriteRenderer mySprite;

    protected override void Start()
    {
        mySprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        base.Start();
    }
    private void OnEnable()
    {
        EventManager.ClickEvents.OnMouseClicked.AddListener(SelectedUnit);
    }
    private void OnDisable()
    {
        EventManager.ClickEvents.OnMouseClicked.RemoveListener(SelectedUnit);
    }
    protected override void Initialize()
    {
        var matchedBuild = UnitManager.Instance.Units.FirstOrDefault(b => b.type == unitType);
        if (matchedBuild.type != UnitType.None)
        {
            image = matchedBuild.image;
            health = matchedBuild.health;
        }
        else
        {
            Debug.LogError($"No soldier found for UnitType: {unitType}");
        }
        canMove = true;
        selectable = true;
    }
    protected override void SetSprite()
    {
        mySprite.sprite = image;
    }
    public void PerformAttack()
    {
    }
    protected override void SelectedUnit(GameObject gameObject)
    {
        if (gameObject != this.gameObject)
            return;
        base.SelectedUnit(gameObject);
    }

}

public class SoldierLevel1 : ISoldierState
{
    public void Attack(IUnitSoldier soldier)
    {
        soldier.PerformAttack();
    }
}

public class SoldierLevel2 : ISoldierState
{
    public void Attack(IUnitSoldier soldier)
    {
        //Level atladikca saldirizi hizi , damage value , particleler vs degisebilir.
        soldier.PerformAttack();
    }
}
