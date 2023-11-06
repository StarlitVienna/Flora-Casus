using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class orangeEnemy : MonoBehaviour
{


    //public Rigidbody2D rb;

    public Transform enemyTransform;
    public Transform playerTransform;

    public Animator enemyAnimator;

    public PlayerController player;


    public enemyCooldownHandler cooldownHandler;

    public Collider2D enemyCollider;

    //public Vector3 distanceFromPlayer;

    private float health = 10f;



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
                dead = true;
                enemyAnimator.SetTrigger("Death");
                enemyCollider.isTrigger = true;
                player.addKill();
                //destroyEntity();
            }
        }
    }

    private float aggroDistance = 10;
    private float distanceFromPlayer;

    private bool playerIsClose;
    private bool playerIsHostile;
    private float damage = 1;


    public bool dead;




    public void destroyEntity()
    {
        Destroy(gameObject);
    }

    public void takeDamage(float damage)
    {
        Health -= damage;
        enemyAnimator.SetTrigger("TakeDamage");
        
        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = 0;
    }


    public mainBullet bulletPrefab;
    public float aimAngle;

    void shoot()
    {

        if (!dead && Time.timeScale > 0.01)
        {
            if (cooldownHandler.nextBarrage < Time.time)
            {
                //zarabatana.shoot(aimAngle, mouseAngleXY);
                Instantiate(bulletPrefab, enemyTransform.position, Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg));
                cooldownHandler.barrageCooldownStart();
                //Debug.Log(aimAngle);
            }
        }
    }

    void Start()
    {
        enemyAnimator.SetFloat("horizontal", 1);
        enemyAnimator.SetFloat("vertical", 0);
    }

    private void aggroAction()
    {
        aimAngle = Mathf.Atan2(player.playerTransform.position.y + -0.1655711f * 3 - enemyTransform.position.y, player.transform.position.x + 0.005916424f * 2 - enemyTransform.position.x);
        enemyAnimator.SetFloat("horizontal", Mathf.Cos(aimAngle));
        enemyAnimator.SetFloat("vertical", Mathf.Sin(aimAngle));

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (player.isInCombat)
            {
                distanceFromPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);
                if (distanceFromPlayer < aggroDistance)
                {
                    playerIsHostile = true;
                    playerIsClose = true;
                    aggroAction();
                    shoot();
                }
            }
            else
            {
                playerIsHostile = false;
            }
        }
    }
}
