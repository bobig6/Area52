using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
//This script can be assigned to block and it will start to deal damage every interval of time
public class DamageBlock : StoppableObject
{
    public Sprite on;
    public Sprite off;
    private GameObject steppedObject = null;
    public float damage = 10f;
    public float interval = 2f;

    void Start()
    {
        //Start the coroutine we define below named DealDamage.
        StartCoroutine(DealDamage());
    }

    private void Update()
    {

        if (getEnabled())
        {
            GetComponent<SpriteRenderer>().sprite = on;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            if (off != null)
            {
                GetComponent<SpriteRenderer>().sprite = off;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    IEnumerator DealDamage()
    {
        //we create the WaitForSeconds object
        var wait = new WaitForSeconds(interval);

        //if the block is enabled we reduce the health depending on if its a player or enemy
        while (getEnabled())
        {
            if (steppedObject)
            {
                if (steppedObject.name.Contains("Player"))
                {
                    steppedObject.GetComponent<PlayerHealth>().reduceHealthPlayer(damage);
                }
                else if(steppedObject.name.Contains("Enemy"))
                {
                    steppedObject.transform.Find("EnemyObject").GetComponent<PlayerHealth>().reduceHeathEnemy(damage);
                }
                else
                {
                    //add dog damage logic
                    //steppedObject.transform.Find("DogObject").GetComponent<PlayerHealth>().reduceHeathEnemy(damage);
                }
            }
            yield return wait;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        steppedObject = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        steppedObject = null;
    }
}
