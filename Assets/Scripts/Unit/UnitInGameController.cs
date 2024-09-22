using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInGameController
{
    private bool _canMoveSoldier;
    private GameObject _selectedUnitGameObject;
    public GameObject selectedUnitGameObject
    {
        get { return _selectedUnitGameObject; }
        set { _selectedUnitGameObject = value; }
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_canMoveSoldier)
            {
                MoveSelectedSoldier();
            }
            else
            {
                DetectObjectUnderMouse();
            }
        }
    }

    private void MoveSelectedSoldier()
    {
        if (_selectedUnitGameObject && _selectedUnitGameObject.TryGetComponent<CharacterMovementHandler>(out CharacterMovementHandler soldier))
        {
            soldier.SetTargetPosition(Extensions.GetMouseWorldPosition());
            soldier.ShadowControl(false);
            _selectedUnitGameObject = null;
            _canMoveSoldier = false;
        }
    }

    private void DetectObjectUnderMouse()
    {
        GameObject selectedObj = Extensions.RayCast();
        if (selectedObj && selectedObj.TryGetComponent<Unit>(out Unit unit))
        {
            _selectedUnitGameObject = selectedObj;
            EventManager.ClickEvents.OnMouseClicked.Invoke(_selectedUnitGameObject);

            if (_selectedUnitGameObject.TryGetComponent<CharacterMovementHandler>(out CharacterMovementHandler soldier))
            {
                soldier.ShadowControl(true);
                _canMoveSoldier = true;
                EventManager.ClickEvents.CloseInformationUi.Invoke();
            }
        }
    }
}
