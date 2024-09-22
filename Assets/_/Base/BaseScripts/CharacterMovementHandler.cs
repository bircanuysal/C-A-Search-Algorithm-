using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementHandler : MonoBehaviour {

    private const float speed = 40f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    private bool isMoving = false;
    public bool IsMoving {  get { return isMoving; } }

    [SerializeField]
    private GameObject shadow;

    private Vector3 targetPos;
    private void Update()
    {
        HandleMovement();
    }
    
    private void HandleMovement() 
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f) 
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
                isMoving = true;
                if (!GetTargetIsWalkable())
                {
                    StopMoving();
                }
            } 
            else 
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) 
                {
                    StopMoving();
                }
            }
        }
    }
    private bool GetTargetIsWalkable()
    {
        MapManager mapManager = MapManager.Instance;
        int targetX;
        int targetY;
        Grid<PathNode> grid = mapManager.Pathfinding.GetGrid();
        grid.GetXY(targetPos, out targetX, out targetY);
        if (grid.GetGridObject(targetX, targetY).isWalkable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void StopMoving() 
    {
        isMoving = false;
        pathVectorList = null;
    }

    public Vector3 GetPosition() 
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        targetPos = targetPosition;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
    public void ShadowControl(bool open)
    {
        shadow.SetActive(open);
    }
}