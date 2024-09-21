using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : LocalSingleton<GameManager>
{
    [SerializeField]
    private MapManager _mapManager;

    public MapManager mapManager { get { return _mapManager; } }

    [SerializeField]
    private UnitManager _buildManager;

    public UnitManager buildManager { get { return _buildManager; } }

    private float _spriteScaleRate = 2;
    public float spriteScaleRate {  get { return _spriteScaleRate; } }
}
