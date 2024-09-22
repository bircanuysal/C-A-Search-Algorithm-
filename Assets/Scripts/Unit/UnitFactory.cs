using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitFactory
{
    private static ObjectPooler objectPooler = ObjectPooler.Instance;
    private static GameManager gameManager = GameManager.Instance;
    public static GameObject CreateBuild(UnitType unitType, Vector3 spawnPos)
    {
        // Efficiently determine script based on UnitType
        Type scriptType;
        switch (unitType)
        {
            case UnitType.Barracks:
                scriptType = typeof(BarrackBuild);
                break;
            case UnitType.PowerPlant:
                scriptType = typeof(PowerPlantBuild);
                break;
            case UnitType.MachineGunBuild:
                scriptType = typeof(MachineGunBuild);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(unitType), unitType, "Invalid UnitType provided.");
        }

        GameObject build = objectPooler.GetPooledObject(PoolableObjectTypes.Build, spawnPos);
        build.AddComponent<UnitBuildPlacer>();
        build.AddComponent(scriptType);     
        return build;
    }
    public static void CreateSoldier(UnitType unitType)
    {
        Type scriptType;
        switch (unitType)
        {
            case UnitType.InfantrySoldier:
                scriptType = typeof(InfantrySoldier);
                break;
            case UnitType.SwordSoldier:
                scriptType = typeof(SwordSoldier);
                break;
            case UnitType.ArcherSoldier:
                scriptType = typeof(ArcherSoldier);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(unitType), unitType, "Invalid UnitType provided.");
        }
        Vector3 spawnPos = Vector3.zero;
        if (gameManager.GetSelectedUnitGameObject().TryGetComponent<UnitBuild>(out UnitBuild unitBuild))
        {
            spawnPos = unitBuild.spawnPos;
        }
        GameObject soldier = objectPooler.GetPooledObject(PoolableObjectTypes.Soldier, spawnPos);
        soldier.AddComponent(scriptType);
    }
}
