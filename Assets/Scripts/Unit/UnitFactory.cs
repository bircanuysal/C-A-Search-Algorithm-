using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitFactory
{
    private static ObjectPooler _objectPooler = ObjectPooler.Instance;

    public static GameObject CreateBuild(PoolableObjectTypes unitType , Vector3 spawnPos)
    {
        GameObject build = _objectPooler.GetPooledObject(unitType, spawnPos);
        build.AddComponent<UnitBuildPlacer>();
        return build;
    }
}
