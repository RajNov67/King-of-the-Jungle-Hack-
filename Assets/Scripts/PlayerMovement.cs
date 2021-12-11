using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    
    //SerializeField allows speed variable to be directly editable in editor
    [SerializeField] private float speed;

    private Animator anim;

    private void Start()
    {
        View = GetComponent<PhotonView>();
    }


    //Keep track if player is on the ground
    private bool grounded;

    PhotonView View;



    //Awake method called when script loaded
    private void Awake()
    {
        //Takes RigidBody component and puts it in variable.
        body = GetComponent<Rigidbody2D>();

        //Gets animator component
        anim = GetComponent<Animator>();
    }


    //Update method runs every frame
    //Every frame inputs are recorded
    private void Update()
    {
        //Code only runs if its your player character.
        if (View.IsMine)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            //To change speed/direction of player body
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

            //Flips player left and right
            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

            //Checks if space key pressed for jumping
            if (Input.GetKey(KeyCode.Space) && grounded)
                Jump();



            //Set animator parameters
            //Goes to run animation when key input is not equal to zero meaning idle
            anim.SetBool("run", horizontalInput != 0);
            anim.SetBool("grounded", grounded);
        }

        //float horizontalInput = Input.GetAxis("Horizontal");

        ////To change speed/direction of player body
        //body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        ////Flips player left and right
        //if (horizontalInput > 0.01f)
        //    transform.localScale = Vector3.one;
        //else if (horizontalInput < -0.01f)
        //    transform.localScale = new Vector3(-1, 1, 1);

        ////Checks if space key pressed for jumping
        //if (Input.GetKey(KeyCode.Space) && grounded)
        //    Jump();
            
            

        ////Set animator parameters
        ////Goes to run animation when key input is not equal to zero meaning idle
        //anim.SetBool("run", horizontalInput != 0);
        //anim.SetBool("grounded", grounded);
        
    }

    private void Jump()
    {
        //Change player body position horizontally
        body.velocity = new Vector2(body.velocity.x, speed/1.3f);
        anim.SetTrigger("Jump");
        grounded = false;
        Debug.Log("Player Jump");

    }

    //OnCollision method called every time when 2D object with collider...
    //touches another object with rigidBody
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //"Ground" is the tag for the ground
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }


}
