using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    /// <summary>
    /// STOP MOVING WHILE DASHING
    /// </summary>

    Rigidbody2D rb;
    Animator animator;
    public Transform playerTransform;
    public Canvas joystickCanvas;

    public Vector2 movementInput;

    public MainWeaponScript mainWeapon;
    public zarabatanaScript zarabatana;

    public bl_Joystick joystick;

    public Dash dashHandler;
    public playerCooldownHandler cooldownHandler;
    public Camera cam;

    public Vector2 mousePos;
    public Vector2 mouseAim;
    public Vector2 mouseAngleXY;
    public float aimAngle;
    public float aimAngleDegrees;

    private float speed = 40f;
    private float attackSpeed = 1.5f;

    private float health = 200f;

    public bool dead;
    public bool attacking;
    public bool dashing;


    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            Debug.Log(health);
            if (health <= 0 )
            {
                dead = true;
                
                animator.SetTrigger("Death");
            }
        }
    }


    public enum attackDirections
    {
        left, right, up, down
    }

    public attackDirections attackDirection;
    public Vector2 attackDirectionVector;

    public PlayerInputs controllerInputs;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        playerTransform = gameObject.GetComponent<Transform>();

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            joystickCanvas.enabled = true;
        } else
        {
            joystickCanvas.enabled = false;
            controllerInputs = new PlayerInputs();
            controllerInputs.Player.Enable();
            controllerInputs.Player.Move.performed += movement;
            controllerInputs.Player.Attack.performed += Attack;
            //controllerInputs.Player.DashDirection.performed += dashDirection;
            controllerInputs.Player.SimpleDash.performed += simpleDash;
            controllerInputs.Player.Fire.performed += shoot;

            controllerInputs.Player.CloseGame.performed += closeGame;
        }

        animator.SetFloat("AttackSpeed", attackSpeed);
    }

    void closeGame(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    void movement(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
    }

    void shoot(InputAction.CallbackContext context)
    {

        if (cooldownHandler.nextBarrage < Time.time)
        {
            zarabatana.shoot(aimAngle, mouseAngleXY);
            cooldownHandler.barrageCooldownStart();
            //Debug.Log(aimAngle);
        }
    }

    public void mobileDash()
    {
        if (cooldownHandler.nextDashTimer < Time.time)
        {
            rb.AddForce(dashHandler.dashAction(movementInput), ForceMode2D.Impulse);
            cooldownHandler.dashCooldownStart();
        }
    }

    void simpleDash(InputAction.CallbackContext context)
    {
        if (cooldownHandler.nextDashTimer < Time.time)
        {
            rb.AddForce(dashHandler.dashAction(movementInput), ForceMode2D.Impulse);
            cooldownHandler.dashCooldownStart();
        }
    }

    /*
    void dashDirection(InputAction.CallbackContext context)
    {
        //Debug.Log(dashHandler.dashAction(movementInput));
        mouseAngleXY.y = Mathf.Sin(aimAngle);
        mouseAngleXY.x = Mathf.Cos(aimAngle);
        if (cooldownHandler.nextDashTimer < Time.time)
        {
            rb.AddForce(dashHandler.dashAction(mouseAngleXY), ForceMode2D.Impulse);
            cooldownHandler.dashCooldownStart();
        }
    }
    */


    public void mobileAttack()
    {
        animator.SetTrigger("MainAttack");
    }
    private void Attack(InputAction.CallbackContext context)
    {
        //attackProperty = true;
        //animator.SetBool("Attacking", attackProperty);
        animator.SetTrigger("MainAttack");
    }

    // Update is called once per frame
    void Update()
    {

        if (!dead)
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (joystick.Horizontal > 0.0004f)
                {
                    movementInput.x = 1;
                } else if (joystick.Horizontal < -0.0004f)
                {
                    movementInput.x = -1;
                } else
                {
                    movementInput.x = 0;
                    Debug.Log(joystick.Horizontal);
                }


                if (joystick.Vertical > 0.0004f)
                {
                    movementInput.y = 1;
                } else if (joystick.Vertical < -0.0004f)
                {
                    movementInput.y = -1;
                } else
                {
                    movementInput.y = 0;
                }
            } else
            {
                //Debug.Log(playerTransform.position);
                movementInput = controllerInputs.Player.Move.ReadValue<Vector2>();
            }
            animator.SetFloat("speed", movementInput.magnitude);
            animator.SetFloat("Vertical", movementInput.y);
            animator.SetFloat("Horizontal", movementInput.x);
            //Debug.Log(joystick.Vertical);
        }
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            if (movementInput != Vector2.zero && !attacking)
            {
                //rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
                rb.AddForce(movementInput * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseAim = mousePos - rb.position;
            aimAngle = Mathf.Atan2(mouseAim.y, mouseAim.x);
            aimAngleDegrees = (aimAngle * Mathf.Rad2Deg);
            mouseAngleXY.y = Mathf.Sin(aimAngle);
            mouseAngleXY.x = Mathf.Cos(aimAngle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("dihnaoid");
    }

    public void takeKnockback(Vector2 knockbackVector)
    {
        if (!dead)
        {
            rb.AddForce(knockbackVector, ForceMode2D.Impulse);
        }
    }

    public void takeDamage(float damageTaken)
    {
        if (!dead)
        {
            Health -= damageTaken;
        }
    }

    private void mainAttackEnable()
    {
        setAttackDirection();
        mainWeapon.knockbackVector = attackDirectionVector;
        mainWeapon.attackEnable();
        attacking = true;
    }

    private void mainAttackDisable()
    {
        mainWeapon.attackDisable();
        attacking = false;
    }

    private void setAttackDirection()
    {
        // attacking is only true after this function is called
        if (!attacking)
        {
            if (movementInput.x > 0)
            {
                attackDirectionVector.x = 1;
                attackDirectionVector.y = 0;
                attackDirection = attackDirections.right;
            }
            else if (movementInput.x < 0)
            {
                attackDirectionVector.x = -1;
                attackDirectionVector.y = 0;
                attackDirection = attackDirections.left;
            }
            else if (movementInput.y > 0)
            {
                attackDirectionVector.x = 0;
                attackDirectionVector.y = 1;
                attackDirection = attackDirections.up;
            }
            else if (movementInput.y < 0)
            {
                attackDirectionVector.x = 0;
                attackDirectionVector.y = -1;
                attackDirection = attackDirections.down;
            }
            else
            {
                attackDirectionVector.x = 1;
                attackDirectionVector.y = 0;
                attackDirection = attackDirections.right;
            }
        }
    }
}
