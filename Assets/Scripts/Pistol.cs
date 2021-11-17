using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{

    public GameObject projectile;

    //Starting point of the projectile
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

     void Update()
    {
        faceMouse();
        Shoot();
    }

    void faceMouse()
    {
        //Store mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        //Store mouse position from screen space to world space
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //Get difference between mouse pointer and the gun position
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.right = direction;

    }

    void Shoot()
    {

        //Checks if timing is valid
        if(timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
            }
        }
        else
        {
            //Decrease timing
            timeBtwShots -= Time.deltaTime;
        }

        
    }
}
