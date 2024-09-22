using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationUi : MonoBehaviour
{
    [SerializeField]
    private GameObject informationPanel;

    [SerializeField]
    private GameObject soldierSpawnButtonArea; // bu areadaki buttonlar soldier ureten binalar cesitlenirse kod ile butonlarý daha esnek hale getirebiliriz.
                                               // su anlik sadece barrack icin uretebildigi soldier butonlarini koydum
    [SerializeField]
    private TextMeshProUGUI unitNameText;

    [SerializeField]
    private Image unitImg;

    private void OnEnable()
    {
        EventManager.ClickEvents.OpenInformationUi.AddListener(OpenInformationPanel);
        EventManager.ClickEvents.CloseInformationUi.AddListener(CloseInformationPanel);

    }
    private void OnDisable()
    {
        EventManager.ClickEvents.OpenInformationUi.RemoveListener(OpenInformationPanel);
        EventManager.ClickEvents.CloseInformationUi.RemoveListener(CloseInformationPanel);
    }

    private void OpenInformationPanel(Units units)
    {
        unitNameText.text = units.name;
        unitImg.sprite = units.image;
        soldierSpawnButtonArea.SetActive(units.canSpawnSoldier);
        informationPanel.SetActive(true);
    }
    private void CloseInformationPanel()
    {
        informationPanel.SetActive(false);
    }
}
