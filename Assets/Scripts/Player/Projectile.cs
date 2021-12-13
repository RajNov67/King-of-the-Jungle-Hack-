using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject player;
    public LayerMask whatIsSolid;
    private float damage = 20f;


    private void Start()
    {

        //Invoke used to call method to apply life time
        //Call method not used as invoke allows method to be called later
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        //if (transform.localScale.x == -1)
        //{
        //    transform.Translate(-transform.right * speed * Time.deltaTime);
        //    Debug.Log("called left shot");
        //}
        //else
        //{
        //    //move projectile forward if facing right
        //    transform.Translate(transform.right * speed * Time.deltaTime);
        //    Debug.Log("called right shot");

        //}

        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        DestroyProjectile();

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
