using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float max_health;
    private float health;

    public Slider slider;
    void Start()
    {
        health = max_health;
        slider.maxValue = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
    }
    
    public void reduceHealthPlayer(float damage)
    {
        if (health > damage)
        {
            health -= damage;
        }
        else
        {
            health = 0;
            SceneManagerScript.die();
        }
    }

    public void reduceHeathEnemy(float damage)
    {
        if(health > damage)
        {
            health -= damage;
        }
        else
        {
            health = 0;
            GetComponent<EnemyScript>().die();
        }
    }

    public void addHealth(float damage)
    {
        health += damage;
        if(health > 100)
        {
            health = 100f;
        }
    }

    public float getHealth()
    {
        return health;
    }

}
