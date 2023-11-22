using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class spiral : MonoBehaviour
{


    //public Rigidbody2D rb;

    public Transform spiralTransform;
    public Transform playerTransform;


    public PlayerController player;


    public spiralCooldownHandler cooldownHandler;

    public Collider2D spiralCollider;

    //public Vector3 distanceFromPlayer;

    private float health = 6f;



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
                //enemyAnimator.SetTrigger("Death");
                spiralCollider.isTrigger = true;
                player.addKill();
                destroyEntity();
            }
        }
    }

    private float aggroDistance = 210;
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
        //enemyAnimator.SetTrigger("TakeDamage");

        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = 0;
    }


    public mainBullet bulletPrefab;
    public slowdownBullet slowdownBulletPrefab;
    public float aimAngle;
    public int pattern;
    [SerializeField] float spiralAngle1;
    [SerializeField] float spiralAngle2;
    [SerializeField] float spiralAngle3;

    private float angle3Mult;


    void SingleSpiralPattern()
    {
        Instantiate(bulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg));
        cooldownHandler.setBarrageCoooldown(0.06f);
        cooldownHandler.barrageCooldownStart();
        spiralAngle1 += 0.0174533f * 25; //about 2 degrees
    }

    void DoubleSpiralPattern()
    {
        Instantiate(bulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg));
        Instantiate(bulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg));
        cooldownHandler.setBarrageCoooldown(0.06f);
        cooldownHandler.barrageCooldownStart();
        spiralAngle1 += 0.0174533f * 20; //about 2 degrees
        spiralAngle2 -= 0.0174533f * 20;

        if ( spiralAngle1 >= 360)
        {
            spiralAngle1 = 0;
        }
        if ( spiralAngle2 >= 360)
        {
            spiralAngle2 = 0;
        }
    }

    void TriplePattern()
    {
        cooldownHandler.setBarrageCoooldown(0.1f);
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg));
        cooldownHandler.barrageCooldownStart();
        spiralAngle1 += 0.0174533f * 20; //about 20 degrees
        spiralAngle2 += 0.0174533f * 20;
        spiralAngle3 += 0.0174533f * 20;
        if (spiralAngle1 >= 360)
        {
            spiralAngle1 = 0;
        }
        if (spiralAngle2 >= 360)
        {
            spiralAngle2 = 0;
        }
        if (spiralAngle3 >= 360)
        {
            spiralAngle3 = 0;
        }
    }
    void tripleQuasarPattern()
    {
        cooldownHandler.setBarrageCoooldown(0.01f);
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg + 10));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg + 20));


        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg + 10));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg + 20));

        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg + 10));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg + 20));

        cooldownHandler.barrageCooldownStart();
        spiralAngle1 += 0.0174533f * 20; //about 20 degrees
        spiralAngle2 += 0.0174533f * 20;
        spiralAngle3 += 0.0174533f * 20;
        if (spiralAngle1 >= 360)
        {
            spiralAngle1 = 0;
        }
        if (spiralAngle2 >= 360)
        {
            spiralAngle2 = 0;
        }
        if (spiralAngle3 >= 360)
        {
            spiralAngle3 = 0;
        }
    }

    void QuasarPattern()
    {
        cooldownHandler.setBarrageCoooldown(0.04f);
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg + 10));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg + 20));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg + 30));

        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg + 10));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg + 20));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg + 30));

        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg + 10));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg + 20));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg + 30));

        cooldownHandler.barrageCooldownStart();
        spiralAngle1 += 0.0174533f * 15; //about 20 degrees
        spiralAngle2 += 0.0174533f * 15;
        spiralAngle3 += 0.0174533f * 15;
        if (spiralAngle1 >= 360)
        {
            spiralAngle1 = 0;
        }
        if (spiralAngle2 >= 360)
        {
            spiralAngle2 = 0;
        }
        if (spiralAngle3 >= 360)
        {
            spiralAngle3 = 0;
        }
    }

    void simpleQuasarPattern()
    {
        cooldownHandler.setBarrageCoooldown(0.15f);
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle1 * Mathf.Rad2Deg + 20));


        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle2 * Mathf.Rad2Deg + 20));


        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg));
        Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, spiralAngle3 * Mathf.Rad2Deg + 20));


        cooldownHandler.barrageCooldownStart();
        spiralAngle1 += 0.0174533f * 15; //about 20 degrees
        spiralAngle2 += 0.0174533f * 15;
        spiralAngle3 += 0.0174533f * 15;
        if (spiralAngle1 >= 360)
        {
            spiralAngle1 = 0;
        }
        if (spiralAngle2 >= 360)
        {
            spiralAngle2 = 0;
        }
        if (spiralAngle3 >= 360)
        {
            spiralAngle3 = 0;
        }
    }
    void Pulse()
    {
        for (int i = 0; i < 360; i += 10)
        {
            Instantiate(slowdownBulletPrefab, spiralTransform.position, Quaternion.Euler(0, 0, i));
            cooldownHandler.setBarrageCoooldown(0.4f);
            cooldownHandler.barrageCooldownStart();
        }
    }
    void shoot()
    {

        if (!dead && Time.timeScale > 0.01)
        {
            if (cooldownHandler.nextBarrage < Time.time)
            {
                if (pattern == 1)
                {
                    SingleSpiralPattern();
                } else if (pattern == 2)
                {
                    DoubleSpiralPattern();
                } else if (pattern == 3)
                {
                    Pulse();
                } else if (pattern == 4)
                {
                    QuasarPattern();
                } else if (pattern == 41)
                {
                    simpleQuasarPattern();
                }
            }
        }
    }

    void Start()
    {
        //enemyAnimator.SetFloat("horizontal", 1);
        //enemyAnimator.SetFloat("vertical", 0);
        spiralAngle1 = spiralAngle1 * Mathf.Deg2Rad;
        spiralAngle2 = spiralAngle2 * Mathf.Deg2Rad;
        spiralAngle3 = spiralAngle3 * Mathf.Deg2Rad;
    }

    private void aggroAction()
    {
        aimAngle = Mathf.Atan2(player.playerTransform.position.y + -0.1655711f * 3 - spiralTransform.position.y, player.transform.position.x + 0.005916424f * 2 - spiralTransform.position.x);
        //enemyAnimator.SetFloat("horizontal", Mathf.Cos(aimAngle));
        //enemyAnimator.SetFloat("vertical", Mathf.Sin(aimAngle));

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (player.isInCombat)
            {
                distanceFromPlayer = Vector3.Distance(spiralTransform.position, playerTransform.position);
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
