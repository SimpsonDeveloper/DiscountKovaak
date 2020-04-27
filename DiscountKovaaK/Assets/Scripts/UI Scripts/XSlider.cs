using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XSlider : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = GameManager.Instance.xSensitivity / Conversions.SliderModifier;
        slider.onValueChanged.AddListener(UpdateGameManager);
    }

    void UpdateGameManager(float value)
    {
        GameManager.Instance.SetXSensitivity(value);
    }

    public void MatchY(Slider ySlider)
    {
        slider.value = ySlider.value;
    }
}
