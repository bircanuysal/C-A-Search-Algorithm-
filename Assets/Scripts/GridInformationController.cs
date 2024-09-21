using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInformationController : MonoBehaviour
{
    [SerializeField]
    Color walkableColor;

    [SerializeField]
    Color nonWalkableColor;

    private bool builded;
    private void OnEnable()
    {
        EventManager.BuildsEvents.BuildedGrid.AddListener(SetBuilded);
    }
    private void OnDisable()
    {
        EventManager.BuildsEvents.BuildedGrid.RemoveListener(SetBuilded);
    }

    void SetBuilded(List<Vector2> gridPoses)
    {
        builded = true;
        if (gridPoses.Contains(transform.position))
        {
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in spriteRenderers)
            {
                sprite.color = nonWalkableColor;
            }
        }
    }
}
