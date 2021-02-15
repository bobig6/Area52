using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *  This script handles rotation and shooting of the turret.
 *  The following parameter should be set:
 *      1) Range - the range of the gun
 *      2) Target - the player
 *      3) BulletPrefab - the prefab object
 *      4) angleOfView - the maximum angle of rotation of the gun
 *      5) damage - the damage of the gun
 *      6) bulletSpeed - the speed of the bullet
 *      7) Shooting Period - time between each shooting
 *      8) isEnabled - boolean that can stop the shooting
 */
public class TurretScript : StoppableObject
{
    public float Range;
    public Transform Target;
    public GameObject BulletPrefab;
    public float angleOfView = 60;
    public float damage = 10f;
    public float bulletSpeed = 1f;
    public float ShootingPeriod = 1f;



    Vector2 Direction;
    bool Detected = false;
    GameObject Gun;
    Transform shootingPoint;
    // Start is called before the first frame update
    void Start()
    {
        // assigning the Turret gun and shooting point that are children of the turret
        Gun = transform.Find("TurretGun").gameObject;
        shootingPoint = Gun.transform.Find("shootingPoint");
        // repeating the shooting for every ShootingPeriod seconds
        InvokeRepeating("HandleShooting", 1f, ShootingPeriod);
    }

    // Update is called once per frame
    void Update()
    {
        //gets the player position
        Vector2 targetPos = Target.position;
        //calculates the direction
        Direction = targetPos - (Vector2)transform.position;
        //sends raycast to the player in certain range
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);
        //changing the Detected variable depending on the reading
        if (rayInfo)
        {
            if (rayInfo.collider.gameObject.name == "Player")
            {
                if(!Detected)
                {
                    Detected = !Detected;
                }
            }
            else
            {
                if (Detected)
                {
                    Detected = !Detected;
                }
            }
        }
        //rotates the gun to the player
        if (Detected)
        {
            if (Vector3.Angle(Vector3.down, Direction) < angleOfView && Vector3.Angle(Vector3.down, Direction) > -angleOfView)
            {
                Gun.transform.right = Direction;       
            }
        }
        else
        {
            //resets the gun when player gets out of range
            Gun.transform.right = Vector3.down;
        }
    }
    //this function is for shooting in certain period
    private void HandleShooting()
    {
        //if the gun is enabled, the angle is correct and the player is detected, we shoot
        if (getEnabled() && Vector3.Angle(Vector3.down, Direction) < angleOfView && Vector3.Angle(Vector3.down, Direction) > -angleOfView && Detected)
        {
            shoot();
        }
    }
    //this function handles shooting
    public void shoot()
    {
        //instantiating the bullet, giving it the right direction and damage and shooting it 
        GameObject bullet = Instantiate(BulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<BulletScript>().damage = damage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootingPoint.right * bulletSpeed, ForceMode2D.Impulse);
    }

    //This function draws the range of the turret for visualization 
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
