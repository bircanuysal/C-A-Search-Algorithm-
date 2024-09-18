using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : LocalSingleton<MapManager>
{
    public MapData mapData;

    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    private Pathfinding pathfinding;

    private void Start()
    {
        pathfinding = new Pathfinding(mapData.cellXSize , mapData.cellYSize);

        Grid<PathNode> grids = pathfinding.GetGrid();

        pathfindingDebugStepVisual.Setup(grids);
        pathfindingVisual.SetGrid(grids);
    }

}
