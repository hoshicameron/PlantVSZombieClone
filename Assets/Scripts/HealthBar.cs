using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider=null;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fillImage;

    private void Start()
    {
        fillImage.color = gradient.Evaluate(1f);
    }

    private void UpdateHealth(int amount)
    {
        SetHealth(amount);
    }

    public void SetHealth(int value)
    {
        healthSlider.value = value;

        fillImage.color = gradient.Evaluate(healthSlider.normalizedValue);
    }

    public void SetMaxHealth(int value)
    {
        healthSlider.maxValue = value;
    }
}
