using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pistol : MonoBehaviour
{

    public GameObject projectile;

    public float offset;

    //Starting point of the projectile
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    PhotonView View;

    private void Start()
    {
        View = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (View.IsMine)
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
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        //if (View.IsMine)
        //{
        //    faceMouse();
        //    Shoot();
        //}
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
                //######
                PhotonNetwork.Instantiate(projectile.name, shotPoint.position, transform.rotation);
            }
        }
        else
        {
            //Decrease timing
            timeBtwShots -= Time.deltaTime;
        }

        
    }
}
