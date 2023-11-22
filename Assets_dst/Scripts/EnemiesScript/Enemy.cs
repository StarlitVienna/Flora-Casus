using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //private bool dead;

    public Rigidbody2D rb;

    private Transform enemyTransform;
    public Transform playerTransform;

    public PlayerController player;

    //public PlayerController aaaaaaa;

    private Vector3 enemyRotation;
    private Vector2 moveDirection;
    //private Vector2 offsetVector = new Vector2(5, 5);
    private float moveAngle;
    private float moveSpeed = 40f;

    private Animator enemyAnimator;

    private float health = 10f;
    private float aggroDistance = 6;
    private float distanceFromPlayer;

    private bool move;
    private bool playerIsClose;
    private bool playerLastPosSet = false;
    private Vector2 playerLastPos;

    private float damage = 5;
    private Vector2 contactKnockbackVector = new Vector2(5, 5);
    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            if (health <= 0)
            {
                //dead = true;
                enemyAnimator.SetTrigger("Death");
                //player.addKill();
            }
        }
    }

    // make a timer after enemy death so it will disappear even if the animation does not roll for some reason

    public void movementLock()
    {
        move = false;
        playerLastPosSet = false;
    }

    public void movementUnlock()
    {
        move = true;
    }

    public void getPlayerDirection()
    {
        moveDirection = playerTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //PlayerController player = collision.GetComponent<PlayerController>();
            if (!player.dead)
            {
                contactKnockbackVector = moveDirection * 10;
                player.takeDamage(damage);
                player.takeKnockback(contactKnockbackVector);
            }
        }
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        enemyTransform = gameObject.GetComponent<Transform>();
        enemyAnimator = gameObject.GetComponent<Animator>();
        //dead = false;


        /////////////////////
        playerIsClose = true;
        /////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Angle(enemyTransform.position, aaaaaaa.playerTransform.position));
        //Debug.Log(Mathf.Atan2(playerTransform.position.y - enemyTransform.position.y, playerTransform.position.x - enemyTransform.position.x));
    }

    void aggroAction()
    {
        distanceFromPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);

        if ((enemyTransform.position.x - playerTransform.position.x) > 0 && playerIsClose)
        {
            enemyRotation.y = 180;
            enemyTransform.eulerAngles = enemyRotation;
        }
        else
        {
            enemyRotation.y = 0;
            enemyTransform.eulerAngles = enemyRotation;
        }

        if (distanceFromPlayer <= aggroDistance && playerLastPosSet)
        {
            enemyAnimator.SetBool("PlayerNear", true);
            if (move)
            {
                // player box collider offset -0.1655711f*2 X and 0.005916424f*3 Y
                moveAngle = Mathf.Atan2(playerLastPos.y + -0.1655711f * 3 - enemyTransform.position.y, playerLastPos.x + 0.005916424f * 2 - enemyTransform.position.x);
                moveDirection.x = Mathf.Cos(moveAngle);
                moveDirection.y = Mathf.Sin(moveAngle);
                //Debug.Log(moveDirection);
                //getPlayerDirection();
                rb.AddForce((moveDirection) * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse); // slime jumps --> impulse
            }
        }
        else
        {
            playerLastPos.x = playerTransform.position.x;
            playerLastPos.y = playerTransform.position.y;
            playerLastPosSet = true;
            enemyAnimator.SetBool("PlayerNear", false);
        }
    }

    private void FixedUpdate()
    {
        if (!player.dead)
        {
            aggroAction();
        } else
        {
            enemyAnimator.SetBool("PlayerNear", false);
        }
        
    }

    public void destroyEntity()
    {
        Destroy(gameObject);
    }

    public void takeDamage(float damage)
    {
        Health -= damage;
        enemyAnimator.SetTrigger("TakeDamage");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }

    public void takeKnockback(Vector2 knockbackVector)
    {
        rb.AddForce(knockbackVector, ForceMode2D.Impulse);
    }
}
