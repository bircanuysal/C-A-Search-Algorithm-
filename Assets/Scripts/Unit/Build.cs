using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    [SerializeField] private UnitType unitType;
    private string buildName;
    private Image buildImage;
    private Vector2 buildSize;
    private bool canSpawnSoldier;

    private void Start()
    {
        DataUpload();
    }
    private void DataUpload()
    {
        var matchedBuild = BuildManager.Instance.Builds.FirstOrDefault(b => b.type == unitType);
        if (matchedBuild.type != UnitType.None)
        {
            buildName = matchedBuild.name;
            buildImage = matchedBuild.image;
            buildSize = matchedBuild.size;
            canSpawnSoldier = matchedBuild.canSpawnSoldier;
            Debug.Log($"Build Name: {buildName}, Can Spawn Soldier: {canSpawnSoldier}");
        }
        else
        {
            Debug.LogWarning($"No build found for UnitType: {unitType}");
        }
    }
}
