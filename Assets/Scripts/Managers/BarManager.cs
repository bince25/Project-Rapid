using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 25;
        valueText.text = slider.value.ToString("F1");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            IncreaseSliderValue(15.73f);
        }
    }

    /// <summary>
    /// Increases the slider value by the given value
    /// </summary>
    public void IncreaseSliderValue(float value){
        slider.value += value;
        valueText.text = slider.value.ToString("F1");
        CheckSliderValue();
    }
    /// <summary>
    /// Decreases the slider value by the given value
    /// </summary>
    public void DecreaseSliderValue(float value){
        slider.value -= value;
        valueText.text = slider.value.ToString("F1");
        CheckSliderValue();
    }
    /// <summary>
    /// Checks if the slider value is greater than or equal to 100 or less than or equal to 0
    /// </summary>
    void CheckSliderValue(){
        if(slider.value >= 100){
            slider.value = 100;
            Debug.Log("You Win");
        }
        else if (slider.value <= 0){
            slider.value = 0;
            Debug.Log("You Lose");
        }
    }
}