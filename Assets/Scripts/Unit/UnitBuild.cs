using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static EventManager;

public class UnitBuild : Unit, IUnitBuild
{

    private string _buildName;
    private Vector2 _size;
    private bool _canProduceSoldiers;


    public string buildName
    {
        get => _buildName;
        set => _buildName = value;
    }
    public Vector2 size
    {
        get => _size;
        set => _size = value;
    }
    public bool canProduceSoldiers
    {
        get => _canProduceSoldiers;
        set => _canProduceSoldiers = value;
    }
    protected override void Start()
    {
        base.Start();
        InitializeBuild();
        SetSprite();
        ApplySize();
    }
    private void OnEnable()
    {
        EventManager.BuildsEvents.UnitBuild.AddListener(Build);
    }
    private void OnDisable()
    {
        EventManager.BuildsEvents.UnitBuild.RemoveListener(Build);
    }
    private void InitializeBuild()
    {
        var matchedBuild = UnitManager.Instance.Units.FirstOrDefault(b => b.type == unitType);
        if (matchedBuild.type != UnitType.None)
        {
            buildName = matchedBuild.name;
            image = matchedBuild.image;
            size = matchedBuild.size;
            health = matchedBuild.health;
            canProduceSoldiers = matchedBuild.canSpawnSoldier;
            //Debug.Log($"Build Name: {buildName}, Can Spawn Soldier: {canProduceSoldiers}");
        }
        else
        {
            Debug.LogError($"No build found for UnitType: {unitType}");
        }
        canMove = false;
        selectable = false;
    }
    private void SetSprite()
    {
        SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image; // Direk SpriteRenderer üzerindeki sprite'ý güncelleyin.
    }
    private void ApplySize()
    {
        float scaleRate = GameManager.Instance.spriteScaleRate;
        Vector3 newSize = new Vector3(size.x, size.y, 1);
        newSize.x *= scaleRate;
        newSize.y *= scaleRate;
        transform.GetChild(0).transform.localScale = newSize;
    }

    private void Build()
    {
        //Extensions.Debug(gameObject);
    }
}
