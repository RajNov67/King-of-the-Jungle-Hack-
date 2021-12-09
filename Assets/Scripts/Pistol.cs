using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pistol : MonoBehaviour
{

    public GameObject projectile;

    //Starting point of the projectile
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public int maxAmmo = 10;
    private int currentAmmo;


    PhotonView View;

    private void Start()
    {
        View = GetComponent<PhotonView>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        {
            if (View.IsMine)
            {
                faceMouse();
                Shoot();

            }

            //faceMouse();
            //Shoot();
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
            if (timeBtwShots <= 0)
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
}
