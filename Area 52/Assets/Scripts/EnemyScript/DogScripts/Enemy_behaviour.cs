using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is responsible for the dog enemy.
 */
public class Enemy_behaviour : MonoBehaviour
{
    // we need a refference to the player to flip the dog to his direction
    public Transform player;
    // this is the initial point for the raycast
    public Transform rayCast;
    // this is which layer the raycast is going to detect
    public LayerMask raycastMask;
    // the lenght of raycast( should be bigger than the collider size)
    public float rayCastLenght;
    // this is the distance at which the dog starts attacking
    public float attackDistance;
    // the speed of the dog
    public float moveSpeed;
    // a timer for the attack period of the dog
    public float timer;
    // how much damage per hit the dog deals
    public float damage;

    private Vector2 direction;
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    private bool isDead = false;

    private void Awake()
    {
        // initialising the timer with the default value
        intTimer = timer;
        // assigning the animtor object
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // checking if the dog is killed
        if (!isDead)
        {
            Vector3 enemyLocalScale = gameObject.transform.localScale;
            // Flipping the dog to be in the direction of the player
            // the player is on right side
            if (player.transform.position.x > transform.position.x)
            {
                // turn the dog right
                enemyLocalScale.x = 6f;
                direction = Vector2.right;
            }// the player is on left side
            else if(player.transform.position.x < transform.position.x)
            {
                // turn the dog left
                enemyLocalScale.x = -6f;
                direction = Vector2.left;
            }
            // turning the dog
            gameObject.transform.localScale = enemyLocalScale;
            
            // this bool is true when the player enteres the collider
            if (inRange)
            {
                //we shoot a raycast to the player
                hit = Physics2D.Raycast(rayCast.position, direction, rayCastLenght, raycastMask);
                // this function is to display the raycast and it's only for debugging
                RaycastDebugger();
            }

        
            //When Player is detedted
            if (hit.collider != null)
            { 
                EnemyLogic();
            }else
            {
                // if we lose the player he is no longer in range
                inRange = false;
            }

            if(inRange == false)
            {
                //if the enemy isn't in range we stop the walking animation and the attack
                anim.SetBool("canWalk", false);
                StopAttack();
            }
        }
        //if the dog is killed we play the death animation
        else
        {
            anim.SetBool("isDead", true);
        }
    }

    // When the player triggeres with the collider we set the player as target and we set him in range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("Player"))
        {
            target = collision.gameObject;
            inRange = true;
        }
    }

    // This function handles the logic when the dog detects the player
    void EnemyLogic()
    {
        // Calculating the distance between the dog and the player
        distance = Vector2.Distance(transform.position, target.transform.position);

        // if the player is further than the attack distance
        if (distance > attackDistance)
        {
            // the dog moves towards the player and stops attacking
            Move();
            StopAttack();
        }
        // if the dog is close enough to the player it attacks
        // the cooling variable indicates when the dog is attacking
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        // if that variable is true this means that the dog is waiting between attacks
        if (cooling)
        {
            // the cooldown func is called and the attack animation is deactivated
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    // this function is responsible for moving the dog
    void Move()
    {
        // we activate the walking animation
        anim.SetBool("canWalk", true);
        // we check the current animation playing and if it's not the attack animation we move the dog
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("AttackingDog"))
        {
            // we get the x value of the player and the y value of the dog
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            // and we move the dog towards this position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed*Time.deltaTime);
        }
    }

    // this function is responsible for attacking
    void Attack()
    {
        // we reset the timer and put the dog in attack mode
        timer = intTimer;
        attackMode = true;

        // we deactivate the walking animation and activate the attack animation
        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    // this function reduces the timer
    void Cooldown()
    {
        // We reduce the timer
        timer -= Time.deltaTime;
        // if the timer hits zero 
        if(timer <= 0 && cooling && attackMode)
        {
            // we set the cooling variable to be false and we reset the timer
            cooling = false;
            timer = intTimer;
            // then we deal damage to the player
            target.GetComponent<PlayerHealth>().reduceHealthPlayer(damage);
        }
    }

    // this function stops the attacking
    void StopAttack()
    {
        // we set the cooling and attack mode to false and deacticvate the attack animation
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    // This function is only for debugging and it draws the raycast of the dog
    private void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, direction * rayCastLenght, Color.red);
        }
        else
        {
            Debug.DrawRay(rayCast.position, direction * rayCastLenght, Color.green);
        }
    }

    // this function is called in the dog animation as event and it resets the cooling
    public void TriggerCooling()
    {
        cooling = true;
    }

    // this function is for when the dog collides with something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the dog is hit by a bullet the health is reduced
        if (collision.gameObject.name.Contains("Bullet"))
        {
            GetComponent<PlayerHealth>().reduceHeathEnemy(collision.gameObject.GetComponent<BulletScript>().damage);
        }
        // if the player touches the dog a damage is dealt to him if the dog is alive
        // this is done so the player cannot just jump on the dog and stay there
        else if (collision.gameObject.name.Contains("Player"))
        {
            if (!isDead)
            {
                collision.gameObject.GetComponent<PlayerHealth>().reduceHealthPlayer(damage);
            }
        }
    }

    // this function is called when the dog dies
    public void die()
    {
        isDead = true;
    }
}
