using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildSpawnButton : MonoBehaviour
{
    public UnitType unitType;
    private Button button;

    [SerializeField]
    float fillDuration;

    Image imageToFill;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        imageToFill = GetComponent<Image>();
    }   
    private void OnButtonClick()
    {
        Vector3 mousePos = Extensions.GetMouseWorldPosition();
        UnitFactory.CreateBuild(unitType, mousePos);
        StartCoroutine(FillImage());
    }
    IEnumerator FillImage()
    {
        imageToFill.fillAmount = 1f;
        button.interactable = false;
        float elapsedTime = 0f;
        while (elapsedTime < fillDuration)
        {
            imageToFill.fillAmount = elapsedTime / fillDuration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        button.interactable = true;
        imageToFill.fillAmount = 1f;
    }
}
