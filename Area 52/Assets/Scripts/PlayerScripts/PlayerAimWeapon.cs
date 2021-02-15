using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//This class is assigned to the player and rotates weapon + shoots
public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private float fireElapsedTime = 0;

    private bool allowShooting = true;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        // if the inventory is inactive we can aim and shoot
        if (!PlayerInput.isInventoryActive)
        {
            HandleAiming();
            HandleShooting();
        }
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        //Getting direction by subtracting current position from desired position
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        //turning direction into angle using tg
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //applying angle on object and setting x and y rotations to 0
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        // if the angle is too big we flip the player sprite
        Vector3 aimLocalScale = Vector3.one;
        Vector3 playerLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
            aimLocalScale.x = -1f;
            playerLocalScale.x = -1f;
        }
        else
        {
            aimLocalScale.y = 1f;
            aimLocalScale.x = 1f;
            playerLocalScale.x = 1f;
        }
        aimTransform.localScale = aimLocalScale;
        gameObject.transform.localScale = playerLocalScale;

    }

    // this function shoots a bullet if the shooting period timer of the gun is elapsed
    private void HandleShooting()
    {
        if (!allowShooting)
        {
            // We increase the timer every second 
            fireElapsedTime += Time.deltaTime;
            if (fireElapsedTime >= GetComponent<Player>().currentWeapon.fireDelay)
            {
                // if the timer is elapsed we set it to 0 and we allow the shooting
                fireElapsedTime = 0;
                allowShooting = true;
            }
        }
        // if the shootng is allowed and the fire button is pressed 
        if (Input.GetButtonDown("Fire1") && allowShooting)
        {
            // we shoot and we disable the shooting
            GetComponent<Player>().currentWeapon.shoot();
            allowShooting = false;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        //Gets position of mouse in the world point from main camera
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }

}
