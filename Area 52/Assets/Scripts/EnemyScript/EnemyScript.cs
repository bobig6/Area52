using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// This script is responsible for the movement and the shooting of the enemy
public class EnemyScript : MonoBehaviour
{
    // The point of the gun
    public Transform firePoint;
    // The bullet prefab that the enemy shoots
    public GameObject BulletPrefab;
    // The distance that the enemy can detect the player
    public float distance;
    // The gun of the enemy
    public GameObject gun;
    // The player so we can point to him
    public Player player;
    // The period between each shot
    public float ShootingPeriod = 1f;
    // The speed of movement
    public float movingSpeed;
    // The starting and ending point of the enemy
    public Transform point1;
    public Transform point2;

    private Animator anim;
    private bool isTurnedLeft = false;
    private bool isDead = false;
    private Transform aimTransform;
    // Start is called before the first frame update
    void Start()
    {
        //we find the animator component and the aimTransform, which are the hands and the gun of the enemy
        anim = GetComponent<Animator>();
        aimTransform = transform.Find("Aim");
        // We call the Handle shooting every shooting period of time after 1 second
        InvokeRepeating("HandleShooting", 1f, ShootingPeriod);
    }

    // Update is called once per frame
    void Update()
    {
        // if the enemy ist dead
        if (!isDead)
        {
            // We mesure if the distacne between the player and the enemy is less than what we've set
            if (Vector3.Distance(transform.position, player.transform.position) < distance)
            {
                // if it is we stop walking animation and start aiming at the player
                anim.SetBool("isWalking", false);
                HandleAiming();
            }
            else
            {
                // if the points are set we activate the walking animation and we start patroling
                if (point1 != null && point2 != null)
                {
                    anim.SetBool("isWalking", true);
                    Patrol();
                }
            }
        }
    }

    // this function is called when the player dies
    public void die()
    {
        // we set the collider to trigger, so the player can detect it
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        // we add the chest keyword so the player can treat it as chest
        gameObject.name += "Chest";
        // we set the bool to true and activate the dead animation
        isDead = true;
        anim.SetBool("isDead", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the enemy is hit by a bullet the health is reduced
        if (collision.gameObject.name.Contains("Bullet"))
        {
            GetComponent<PlayerHealth>().reduceHeathEnemy(collision.gameObject.GetComponent<BulletScript>().damage);
        }
    }

    private void HandleAiming()
    {
        // getting the position if the player
        Vector3 playerPosition = player.transform.position;
        //Getting direction by subtracting current position from desired position
        Vector3 aimDirection = (playerPosition - transform.position).normalized;
        //turning direction into angle using tg
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //applying angle on object and setting x and y rotations to 0
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        // setting the enemy's and his hands's local scale to the vector one which is he default value
        Vector3 aimLocalScale = Vector3.one;
        Vector3 enemyLocalScale = Vector3.one;
        // if the angle becomes too big we flip the enemy and his arms
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
            aimLocalScale.x = -1f;
            enemyLocalScale.x = -1f;
        }
        else
        {
            aimLocalScale.y = 1f;
            aimLocalScale.x = 1f;
            enemyLocalScale.x = 1f;
        }
        // setting the newly calculated scales to the enemy and his hands
        aimTransform.localScale = aimLocalScale;
        gameObject.transform.localScale = enemyLocalScale;

    }
    //This function moves the enemy between 2 points
    public void Patrol()
    {
        
        Vector3 enemyLocalScale = Vector3.one;

        //checking direction which the enemy is facing
        if (isTurnedLeft)
        {
            //moving the enemy
            transform.position = new Vector2(transform.position.x + movingSpeed * Time.deltaTime, transform.position.y);
            //flipping the enemy if he reached the destination point
            if (transform.position.x >= point2.position.x)
            {
                enemyLocalScale.x = -1f;
                isTurnedLeft = !isTurnedLeft;
                gameObject.transform.localScale = enemyLocalScale;
            }
        }
        else
        {
            transform.position = new Vector2(transform.position.x - movingSpeed * Time.deltaTime, transform.position.y);
            if (transform.position.x <= point1.position.x)
            {
                enemyLocalScale.x = 1f;
                isTurnedLeft = !isTurnedLeft;
                gameObject.transform.localScale = enemyLocalScale;
            }
        }
    }


    private void HandleShooting()
    {
        // if the distance between the player and the enemy is less than what we have set and the enemy is not dead 
        if (Vector3.Distance(transform.position, player.transform.position) < distance && !isDead)
        {
            // the enemy shots
            gun.GetComponent<Weapon>().shoot();
        }
    }
}
