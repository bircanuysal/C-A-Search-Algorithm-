using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : LocalSingleton<GameManager>
{
    [SerializeField] private MapManager mapManager;
    public MapManager MapManager { get { return mapManager; } }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
