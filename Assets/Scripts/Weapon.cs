/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Add custom offset for each weapon in Unity inspector
     public float offset;

    public GameObject projectile;
    public Transform shotPoint;

    // Add shake effect to main camera
    public Animator camAnim;

    private float timeBtwShots;
    public float startTimeBtwShots;

    // Update each frame
     void Update()
    {
        faceMouse();
        Shoot();
    }

    void faceMouse()
    {
         // Handles the weapon rotation
        //Store mouse position 
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Change weapon rotation with custom offset
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }
    void Shoot()
    {

        //Checks if timing is valid
        if(timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {

                // Shake screen after trigger
                camAnim.SetTrigger("shake");

                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                Debug.Log("Testing");

            }
        }
        else
        {
            //Decrease timing
            timeBtwShots -= Time.deltaTime;
        }

        
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float offset;

    public GameObject projectile;
   // public GameObject shotEffect;
    public Transform shotPoint;
   // public Animator camAnim;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        // Handles the weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }

       
    }
}
