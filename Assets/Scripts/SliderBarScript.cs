using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarScript : MonoBehaviour
{
    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;

    // Set the number that it considers the max
    public void SetMaxHealth(float health)
    {
        Slider.maxValue = health;
        Slider.value = health;

        Fill.color = Gradient.Evaluate(1f);
    }

    // When you take damage it will shrink the fill
    public void SetHealth(float health)
    {
        Slider.value = health;

        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}
