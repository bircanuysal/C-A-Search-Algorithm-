using CodeMonkey.Utils;
using UnityEngine;

public class UnitBasePlacer : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private int unitWidth = 1; // Unit'in geni�li�i
    [SerializeField] private int unitHeight = 1; // Unit'in y�ksekli�i

    private MapManager mapManager;

    private void Start()
    {
        mapManager = MapManager.Instance; // MapManager singleton'� kullanarak eri�im
    }

    private void Update()
    {
        bool a = SnapToCell();
        Debug.LogError(a);
    }


    public bool SnapToCell() // Snaplenme ve kontrol fonksiyonu
    {

        Grid<PathNode> grid = mapManager.Pathfinding.GetGrid();
        float cellSize = grid.GetCellSize();

        int objectWidth = unitWidth;
        int objectHeight = unitHeight;

        int snappedX;
        int snappedY;
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        mapManager.Pathfinding.GetGrid().GetXY(mouseWorldPosition, out snappedX, out snappedY);

        // Snaplenmenin s�n�rlar�n� kontrol et
        if (snappedX + objectWidth > grid.GetWidth() || snappedY + objectHeight > grid.GetHeight())
        {
            return false;
        }

        // T�m kaplanan h�creleri kontrol et
        for (int x = snappedX; x < snappedX + objectWidth; x++)
        {
            for (int y = snappedY; y < snappedY + objectHeight; y++)
            {
                if (!grid.GetGridObject(x, y).isWalkable)
                {
                    return false;
                }
            }
        }

        // E�er t�m h�creler ge�ilebilirse, snaple ve true d�nd�r
        Vector3 snappedPosition = new Vector3(snappedX * cellSize, snappedY * cellSize, 0);
        transform.position = snappedPosition;
        return true;
    }
}
