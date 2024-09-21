using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitFactory
{
    private static ObjectPooler _objectPooler = ObjectPooler.Instance;

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

        GameObject build = _objectPooler.GetPooledObject(PoolableObjectTypes.Build, spawnPos);
        build.AddComponent<UnitBuildPlacer>();
        build.AddComponent(scriptType);

        return build;
    }

}
