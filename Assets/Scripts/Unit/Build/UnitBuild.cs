using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static EventManager;

public class UnitBuild : Unit, IUnitBuild
{

    private string _buildName;
    private Vector2 _size;
    private bool _canProduceSoldiers;
    private Vector3 _spawnPos;
    private BoxCollider2D boxCollider2d;
    private static GameManager gameManager = GameManager.Instance;
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
    public Vector3 spawnPos
    {
        get => _spawnPos;
        set => _spawnPos = value;
    }
    protected override void Start()
    {
        base.Start();
        boxCollider2d = GetComponent<BoxCollider2D>();
        ApplySize();
    }
    private void OnEnable()
    {
        EventManager.BuildsEvents.UnitBuild.AddListener(Build);
        EventManager.ClickEvents.OnMouseClicked.AddListener(SelectedUnit);
    }
    private void OnDisable()
    {
        EventManager.BuildsEvents.UnitBuild.RemoveListener(Build);
        EventManager.ClickEvents.OnMouseClicked.RemoveListener(SelectedUnit);
    }
    protected override void Initialize()
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
    protected override void SetSprite()
    {
        SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image; // Direk SpriteRenderer üzerindeki sprite'ý güncelleyin.
    }
    private void ApplySize()
    {
        boxCollider2d.size = size * 10;
        float scaleRate = GameManager.Instance.spriteScaleRate;
        Vector3 newSize = new Vector3(size.x, size.y, 1);
        newSize.x *= scaleRate;
        newSize.y *= scaleRate;
        transform.GetChild(0).transform.localScale = newSize;
    }

    protected virtual void Build()
    {
        spawnPos = FindNearestWalkableNode().transform.position;
        //Extensions.Debug(gameObject);
    }
    protected override void SelectedUnit(GameObject gameObject)
    {
        if (gameObject != this.gameObject)
            return;
        base.SelectedUnit(gameObject);
        var matchedBuild = UnitManager.Instance.Units.FirstOrDefault(b => b.type == unitType);
        EventManager.ClickEvents.OpenInformationUi.Invoke(matchedBuild);
    }
    public GameObject FindNearestWalkableNode()
    {
        GameObject nearestNode = null;
        float minDistance = Mathf.Infinity;
        Vector3 characterPosition = transform.position;
        GameObject[,] visualNodeArray = mapManager.visualNodeArray;
        Grid<PathNode> grid = mapManager.Pathfinding.GetGrid();
        for (int x = 0; x < visualNodeArray.GetLength(0); x++)
        {
            for (int y = 0; y < visualNodeArray.GetLength(1); y++)
            {
                if (grid.GetGridObject(x, y).isWalkable)
                {
                    float distance = Vector3.Distance(characterPosition, mapManager.GetVisualNode(x,y).transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestNode = mapManager.GetVisualNode(x, y);
                        
                    }
                }
            }
        }
        if (nearestNode == null)
        {
            Extensions.Debug("Hiçbir walkable nokta bulunamadý.");
        }
        return nearestNode;
    }

}
