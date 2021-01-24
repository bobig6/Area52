using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Transform firePoint;
    public GameObject BulletPrefab;
    public float distance;
    public GameObject gun;
    public Player player;
    public float ShootingPeriod = 1f;
    public float movingSpeed;
    public Transform point1;
    public Transform point2;

    private bool isTurnedLeft = false;
    private bool isDead = false;
    private float nextShootingTime = 0.0f;
    private Transform aimTransform;
    // Start is called before the first frame update
    void Start()
    {
        aimTransform = transform.Find("Aim");
        InvokeRepeating("HandleShooting", 1f, ShootingPeriod);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < distance)
            {
                HandleAiming();
            }
            else
            {
                if (point1 != null && point2 != null)
                {
                    Patrol();
                }
            }
        }
    }

    public void die()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.name += "Chest";
        isDead = true;
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
        Vector3 playerPosition = player.transform.position;
        //Getting direction by subtracting current position from desired position
        Vector3 aimDirection = (playerPosition - transform.position).normalized;
        //turning direction into angle using tg
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //applying angle on object and setting x and y rotations to 0
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        Vector3 enemyLocalScale = Vector3.one;
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
        if (Vector3.Distance(transform.position, player.transform.position) < distance && !isDead)
        {
            nextShootingTime += ShootingPeriod;
            gun.GetComponent<Weapon>().shoot();

        }
    }
}
