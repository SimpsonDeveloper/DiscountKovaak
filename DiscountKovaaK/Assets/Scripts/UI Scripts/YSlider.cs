using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YSlider : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = GameManager.Instance.ySensitivity / Conversions.SliderModifier;
        slider.onValueChanged.AddListener(UpdateGameManager);
    }

    void UpdateGameManager(float value)
    {
        GameManager.Instance.SetYSensitivity(value);
    }

    public void MatchX(Slider xSlider)
    {
        slider.value = xSlider.value;
    }
}
