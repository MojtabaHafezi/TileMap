using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider mySlider;

    private void Awake()
    {
        mySlider = GetComponent<Slider>();
    }

    public void SetHealth(int health)
    {
        mySlider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        mySlider.maxValue = health;
        mySlider.value = health;
    }
}