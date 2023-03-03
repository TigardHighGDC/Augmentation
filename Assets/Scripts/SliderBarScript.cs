using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarScript : MonoBehaviour
{
    public Slider slider;
    // for color
    public Gradient gradient;

    public Image fill;

    // at start to set the number that it considers the max
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    // for when you take damage it will shrink the fill
    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
