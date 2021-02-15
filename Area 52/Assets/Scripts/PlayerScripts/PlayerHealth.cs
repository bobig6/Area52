using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float max_health;
    public float health;

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
        if (GetComponent<Player>().currentArmor)
        {
            damage -= (GetComponent<Player>().currentArmor.protection / 100) * damage;
        }
        if (GetComponent<Player>().currentHelmet)
        {
            damage -= (GetComponent<Player>().currentHelmet.protection / 100) * damage; 
        }
        if (health > damage)
        {
            Debug.Log(damage);
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
            if (gameObject.name.Contains("Enemy"))
            {
                GetComponent<EnemyScript>().die();
            }else if (gameObject.name.Contains("Dog"))
            {
                GetComponent<Enemy_behaviour>().die();
            }
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
