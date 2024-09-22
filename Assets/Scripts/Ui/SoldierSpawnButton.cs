using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSpawnButton : MonoBehaviour
{
    public UnitType unitType;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }
    private void OnButtonClick()
    {
        UnitFactory.CreateSoldier(unitType);
    }
}
