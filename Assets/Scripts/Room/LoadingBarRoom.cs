using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarRoom : MonoBehaviour
{
    public float MaxTime = 30.0f;
    private Slider slider;
    private bool endLoading = false;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = MaxTime;
        slider.value = MaxTime;
    }

    private void Update()
    {
        if (!endLoading)
        {
            SliderIncrease();
        }
    }

    private void SliderIncrease()
    {
        slider.value -= Time.deltaTime;
        if (slider.value <= 0.0f)
        {
            endLoading = true;
            EnemySpawner.LoadingBarFinished = true;
            slider.value = 0.0f;
        }
    }
}
