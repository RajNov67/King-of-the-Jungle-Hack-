using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class PlayerController : MonoBehaviour
{


    public Canvas gameOver;

    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private float speed;

    private bool dead = false; 

    //Keep track if player is on the ground
    private bool grounded;
    PhotonView View;

    //Health
    public float health;
    public float maxHealth = 100; 
    public HealthBarBehaviour healthBar; 



    private void Start()
    {
        View = GetComponent<PhotonView>();
        health = maxHealth;
        healthBar.SetSize(maxHealth, health);
    }

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

            CheckAlive();

           


        }

        

    }

    private void CheckAlive()
    {
        if (health <= 0)
        {
            dead = true;
            endGame();
            Destroy(gameObject);
            Debug.Log("Player " + PhotonNetwork.NickName + " has died");
            
        }

    }

    [PunRPC]
    void RPC_endGame()
    {
        FindObjectOfType<GameOverScreen>().EndGame(PhotonNetwork.NickName);
    }

    void endGame()
    {
        View.RPC("RPC_endGame", RpcTarget.All);
    }

    //[PunRPC]
    //void RPC_checkWinLose()
    //{
    //    //if (dead == true)
    //    //{
    //    //    PhotonNetwork.LoadLevel("GameOverLose");
    //    //}
    //    //if (dead == false)
    //    //{
    //    //    PhotonNetwork.LoadLevel("GameOverWin");
    //    //}
    //    //FindObjectOfType<GameOverScreen>().EndGame(PhotonNetwork.NickName);

    //}

    //public void endGame()
    //{
    //    View.RPC("RPC_checkWinLose", RpcTarget.Others);
    //}

    //[PunRPC]
    //void RPC_EndGameWin()
    //{
    //    PhotonNetwork.LoadLevel("GameOverWin");

    //}



    public void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log("Damage Taken. Health now: " + health);
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

        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(25f);
            healthBar.SetSize(maxHealth, health);
            enemy_damage();
        }
    }

    [PunRPC]
    void RPC_TakeDamage()
    {
        TakeDamage(25f);
        healthBar.SetSize(maxHealth, health);
        CheckAlive(); 
    }

    public void enemy_damage()
    {
        View.RPC("RPC_TakeDamage", RpcTarget.Others);
    }

}
