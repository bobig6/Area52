using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStationScript : MonoBehaviour
{
    bool isPlayerIn = false;
    public GameObject player;
    public Slider slider;
    public float max_health = 100f;
    float currentHealth;
   

    private void Start()
    {
        currentHealth = max_health;
        slider.maxValue = max_health;
    }

    private void Update()
    {
        slider.value = currentHealth;
        if (Input.GetKeyDown(KeyCode.H) && isPlayerIn)
        {
            getHealth();
        }
    }

    public void getHealth()
    {
        if(currentHealth >= 10)
        {
            player.GetComponent<PlayerHealth>().addHealth(10);
            currentHealth -= 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerIn = false;
    }
}
