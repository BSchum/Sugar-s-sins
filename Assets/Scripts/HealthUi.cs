using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealthUi : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Text healthText;
    Stats stats;

    void Start()
    {
        stats = GetComponent<Stats>();
        healthBar.maxValue = stats.GetMaxHealth();
        HealthChanged sliderValue = ChangeUIHealthValues;
        stats.Subscribe(sliderValue);
    }

    void ChangeUIHealthValues(float value)
    {
        Debug.Log("Je change la valeur de mon UI a"+value);
        healthBar.value = value;
        healthText.text = value.ToString();
    }

    
}
