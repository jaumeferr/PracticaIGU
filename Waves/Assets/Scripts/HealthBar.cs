using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private float maxTime;
    private float attack;
    private float nextFireTime;
    private float attackTime;
    Rigidbody player;
    public Color initialColor;
    public Color AttackColor;

    public float EPSILON { get; private set; }

    public void SetMaxHealth(int maxHealth) 
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealthBar(int health)
    {
        slider.value = health;
    }
}
