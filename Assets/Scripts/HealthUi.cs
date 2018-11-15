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
    Health hp;

    void Start()
    {
        hp = GetComponent<Health>();
        healthBar.maxValue = hp.GetMaxHealth();
        HealthChanged sliderValue = ChangeUIHealthValues;
        hp.Subscribe(sliderValue);
    }

    void ChangeUIHealthValues(float value)
    {
        Debug.Log("Je change la valeur de mon UI");
        healthBar.value = value;
        healthText.text = value.ToString();
    }

    
}
