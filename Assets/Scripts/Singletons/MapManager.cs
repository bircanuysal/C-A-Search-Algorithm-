using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapManager : LocalSingleton<MapManager>
{
    [SerializeField]
    private MapData mapData;

    private Pathfinding pathfinding;
    public Pathfinding Pathfinding { get { return pathfinding; } }

    private List<GameObject> visualNodeList = new();

    private GameObject[,] visualNodeArray;

    [SerializeField]
    private GameObject cellPrefab;
    private void Start()
    {
        pathfinding = new Pathfinding(mapData.cellXSize , mapData.cellYSize);

        Grid<PathNode> grids = pathfinding.GetGrid();

        Setup(grids);
    }
    public void Setup(Grid<PathNode> grid)
    {
        visualNodeArray = new GameObject[grid.GetWidth(), grid.GetHeight()];

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                Vector3 gridPosition = new Vector3(x, y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f;
                GameObject visualNode = CreateVisualNode(gridPosition);
                visualNodeArray[x, y] = visualNode;
                visualNodeList.Add(visualNode);
            }
        }
    }
    private GameObject CreateVisualNode(Vector3 position)
    {
        GameObject visualNodeTransform = Instantiate(cellPrefab, position, Quaternion.identity);        
        return visualNodeTransform;
    }
    public GameObject GetVisualNode(int x, int y)
    {
        return visualNodeArray[x, y];
    }
}
