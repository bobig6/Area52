using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is assigned to every bullet 
public class BulletScript : MonoBehaviour
{
    public float timeRemaining = 2;

    public float damage = 0;

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
