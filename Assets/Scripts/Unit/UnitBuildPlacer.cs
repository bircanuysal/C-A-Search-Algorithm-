using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPlaceable
{
    bool SnapToCell();
}
public interface IColorChangeable
{
    void UpdateSpriteColors(bool canBuild);
}
public interface IAnimatable
{
    void PlayAnimation();
    void StopAnimation();
}

public class UnitBuildPlacer : MonoBehaviour, IPlaceable, IColorChangeable, IAnimatable
{
    private int unitWidth;
    private int unitHeight;
    private SpriteRenderer[] spriteRenderers;
    private MapManager mapManager;
    private float duration = .5f;
    private float startAlphaValue = 50f;
    private float endAlphaValue = 150f;
    private Tween fadeTween;
    private Color baseColor;
    private bool _canBuild;
    private List<Vector2> buildedGrids = new();
    private bool onDataUploaded = false;
    public bool canBuild
    {
        get { return _canBuild; }
        set
        {
            if (_canBuild != value)
            {
                _canBuild = value;
                UpdateSpriteColors(_canBuild);
            }
        }
    }

    private void Start()
    {
        mapManager = MapManager.Instance;
        StartCoroutine(WaitToDataAndInitialize());
    }
    private IEnumerator WaitToDataAndInitialize()
    {
        yield return new WaitUntil(() => GetComponent<UnitBuild>() != null);

        GetMySize();

        yield return StartCoroutine(FindAllSpriteRenderersInChildren());

        PlayAnimation();

        if (spriteRenderers.Length > 0)
        {
            baseColor = spriteRenderers[0].color;
        }
        onDataUploaded = true;
    }

    private IEnumerator FindAllSpriteRenderersInChildren()
    {
        while (true)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            if (spriteRenderers.Length > 0)
            {
                break;
            }
            yield return null;
        }
    }
    private void OnEnable()
    {
        EventManager.BuildsEvents.UnitBuild.AddListener(Build);
    }

    private void OnDisable()
    {
        EventManager.BuildsEvents.UnitBuild.RemoveListener(Build);
    }

    private void Update()
    {
        if (onDataUploaded)
            SnapToCell();
    }

    private void GetMySize()
    {
        UnitBuild unitBuild = GetComponent<UnitBuild>();
        unitWidth = (int)(unitBuild.size.x);
        unitHeight = (int)(unitBuild.size.y);
    }

    public bool SnapToCell()
    {       
        buildedGrids.Clear();

        Grid<PathNode> grid = mapManager.Pathfinding.GetGrid();
        float cellSize = grid.GetCellSize();

        int objectWidth = unitWidth;
        int objectHeight = unitHeight;

        int snappedX;
        int snappedY;
        Vector3 mouseWorldPosition = Extensions.GetMouseWorldPosition();

        mapManager.Pathfinding.GetGrid().GetXY(mouseWorldPosition, out snappedX, out snappedY);

        Vector3 snappedPosition = new Vector3(snappedX * cellSize + (objectWidth * cellSize) / 2f,
                         snappedY * cellSize + (objectHeight * cellSize) / 2f,
                         0);
        if (snappedX >= 0 && snappedX < grid.GetWidth() && snappedY >= 0 && snappedY < grid.GetHeight())
        {
            transform.position = snappedPosition;
        }
        
        if (snappedX + objectWidth > grid.GetWidth() || snappedY + objectHeight > grid.GetHeight())
        {
            canBuild = false;
            return false;
        }
        for (int x = snappedX; x < snappedX + objectWidth+1; x++)
        {
            for (int y = snappedY; y < snappedY + objectHeight+1; y++)
            {
                if (grid.GetGridObject(x, y) != null)
                {
                    if (!grid.GetGridObject(x, y).isBuildable)
                    {
                        canBuild = false;
                        return false;
                    }
                }
                else
                {
                    //sebebini bul!!!
                    return false;
                }
            }
        }
        canBuild = true;
        for (int x = snappedX; x < snappedX + objectWidth; x++)
        {
            for (int y = snappedY; y < snappedY + objectHeight; y++)
            {
                buildedGrids.Add(mapManager.GetVisualNode(x, y).transform.position);
            }
        }
        return true;
    }


    public void UpdateSpriteColors(bool canBuild)
    {
        Color targetColor = canBuild ? Color.green : Color.red;
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = targetColor;
        }
    }

    public void PlayAnimation()
    {
        startAlphaValue /= 255f;
        endAlphaValue /= 255f;

        foreach (var spriteRenderer in spriteRenderers)
        {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = startAlphaValue;
            spriteRenderer.color = spriteColor;

            fadeTween = spriteRenderer.DOFade(endAlphaValue, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }
    }

    public void StopAnimation()
    {
        if (fadeTween != null)
        {
            fadeTween.Kill();
        }

        foreach (var spriteRenderer in spriteRenderers)
        {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = 1;
            spriteRenderer.color = spriteColor;
        }
    }

    private void Build()
    {
        StopAnimation();

        foreach (var spriteRenderer in spriteRenderers)
        {
            Color spriteColor = baseColor;
            spriteColor.a = 1f;
            spriteRenderer.color = spriteColor;
        }
        Grid<PathNode> grid = mapManager.Pathfinding.GetGrid();
        int objectWidth = unitWidth;
        int objectHeight = unitHeight;

        int snappedX;
        int snappedY;
        Vector3 mouseWorldPosition = Extensions.GetMouseWorldPosition();
        mapManager.Pathfinding.GetGrid().GetXY(mouseWorldPosition, out snappedX, out snappedY);
        for (int x = snappedX; x < snappedX + objectWidth; x++)
        {
            for (int y = snappedY; y < snappedY + objectHeight; y++)
            {
                if (x >= 0 && x < grid.GetWidth() && y >= 0 && y < grid.GetHeight())
                    grid.GetGridObject(x, y).isWalkable = false;
            }
        }

        for (int x = snappedX; x < snappedX + objectWidth + 1; x++)
        {
            for (int y = snappedY; y < snappedY + objectHeight + 1; y++)
            {
                if (x >= 0 && x < grid.GetWidth() && y >= 0 && y < grid.GetHeight())
                {
                    grid.GetGridObject(x, y).isBuildable = false;
                }
            }
        }
        EventManager.BuildsEvents.BuildedGrid.Invoke(buildedGrids);

        Destroy(this);
    }

}


