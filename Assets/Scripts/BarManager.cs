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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            IncreaseSliderValue(15.73f);
        }
    }

    public void IncreaseSliderValue(float value){
        slider.value += value;
        valueText.text = slider.value.ToString("F1");
        CheckSliderValue();
    }
    public void DecreaseSliderValue(float value){
        slider.value -= value;
        valueText.text = slider.value.ToString("F1");
        CheckSliderValue();
    }
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
