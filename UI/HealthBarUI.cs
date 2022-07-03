using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Slider healBar;

    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    public void SetMaxHealth(float health)
    {
        healBar.maxValue = health;
        healBar.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        healBar.value = health;
        fill.color = gradient.Evaluate(healBar.normalizedValue);
    }
}
