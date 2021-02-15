using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Item thisWeapon;
    public float damage;
    public float fireDelay;
    public float bulletSpeed;
    public GameObject BulletPrefab;

    private Transform shootingPoint;

    private void Start()
    {
        shootingPoint = transform.Find("shootPoint");
    }

    public void shoot()
    {

            GameObject bullet = Instantiate(BulletPrefab, shootingPoint.position, shootingPoint.rotation);
            bullet.GetComponent<BulletScript>().damage = damage;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootingPoint.right * bulletSpeed, ForceMode2D.Impulse);


    }
}
