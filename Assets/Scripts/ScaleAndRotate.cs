using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleAndRotate : MonoBehaviour
{
    private Slider scaleSlider;
    public float scaleMinValue = 0.1f;
    public float scaleMaxValue = 1;

    private Slider rotateSlider;
    public float rotateMinValue = 0;
    public float rotateMaxValue = 360;

    void Start()
    {
        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();
        scaleSlider.minValue = scaleMinValue;
        scaleSlider.maxValue = scaleMaxValue;
        scaleSlider.onValueChanged.AddListener(delegate { ScaleSliderUpdate(); });

        rotateSlider = GameObject.Find("RotateSlider").GetComponent<Slider>();
        rotateSlider.minValue = rotateMinValue;
        rotateSlider.maxValue = rotateMaxValue;
        rotateSlider.onValueChanged.AddListener(RotateSliderUpdate);
    }

    public void ScaleSliderUpdate()
    {
        float value = scaleSlider.value;
        transform.localScale = new Vector3(value, value, value);
    }

    public void RotateSliderUpdate(float value)
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, value, transform.rotation.z);
    }
}
