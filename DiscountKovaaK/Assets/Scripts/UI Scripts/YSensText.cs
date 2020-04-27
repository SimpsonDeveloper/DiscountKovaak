using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YSensText : MonoBehaviour
{
    private Text valueText;
    // Start is called before the first frame update
    void Start()
    {
        valueText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSens(float value)
    {
        valueText.text = "Y Sensitivity: " + (value * Conversions.SliderModifier).ToString("0.00");
    }
}
