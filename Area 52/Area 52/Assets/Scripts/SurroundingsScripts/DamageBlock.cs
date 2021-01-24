using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script can be assigned to block and it will start to deal damage every interval of time
public class DamageBlock : MonoBehaviour
{
    private GameObject steppedObject = null;
    public bool isEnabled = true;
    public float damage = 10f;
    public float interval = 2f;

    void Start()
    {
        //Start the coroutine we define below named DealDamage.
        StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage()
    {
        //we create the WaitForSeconds object
        var wait = new WaitForSeconds(interval);

        //if the block is enabled we reduce the health depending on if its a player or enemy
        while (isEnabled)
        {
            if (steppedObject)
            {
                if (steppedObject.name.Contains("Player"))
                {
                    steppedObject.GetComponent<PlayerHealth>().reduceHealthPlayer(damage);
                }
                else
                {
                    steppedObject.transform.Find("EnemyObject").GetComponent<PlayerHealth>().reduceHeathEnemy(damage);
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
