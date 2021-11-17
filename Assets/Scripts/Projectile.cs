using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public float lifeTime;


    private void Start()
    {

        //Invoke used to call method to apply life time
        //Call method not used as invoke allows method to be called later
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {

        //Will move projectile foreward
        transform.Translate(transform.right * speed * Time.deltaTime);
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
