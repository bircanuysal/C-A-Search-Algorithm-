using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : LocalSingleton<GameManager>
{
    private float _spriteScaleRate = 2;
    public float spriteScaleRate => _spriteScaleRate;

    private UnitInGameController _unitInGameController;

    private void Start()
    {
        _unitInGameController = new UnitInGameController();
    }

    private void Update()
    {
        _unitInGameController.HandleInput();
    }
    public GameObject GetSelectedUnitGameObject()
    {
        return _unitInGameController.selectedUnitGameObject;
    }
    public void SetSelectedUnitGameObject(GameObject obj)
    {
        _unitInGameController.selectedUnitGameObject = obj;
    }
}
