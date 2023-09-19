using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    /// <summary>
    /// STOP MOVING WHILE DASHING
    /// </summary>

    Rigidbody2D rb;
    Animator animator;
    public Transform playerTransform;
    public Vector2 movementInput;

    public MainWeaponScript mainWeapon;
    public zarabatanaScript zarabatana;

    public Dash dashHandler;
    public playerCooldownHandler cooldownHandler;
    public Camera cam;

    public Vector2 mousePos;
    public Vector2 mouseAim;
    public Vector2 mouseAngleXY;
    public float aimAngle;
    public float aimAngleDegrees;

    private float speed = 4f;
    private float attackSpeed = 1.25f;

    private float health = 20f;

    public bool attacking;
    public bool dashing;


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
        controllerInputs = new PlayerInputs();
        controllerInputs.Player.Enable();
        controllerInputs.Player.Move.performed += movement;
        controllerInputs.Player.Attack.performed += Attack;
        //controllerInputs.Player.DashDirection.performed += dashDirection;
        controllerInputs.Player.SimpleDash.performed += simpleDash;
        controllerInputs.Player.Fire.performed += shoot;

        controllerInputs.Player.CloseGame.performed += closeGame;

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

    void Attack(InputAction.CallbackContext context)
    {
        //attackProperty = true;
        //animator.SetBool("Attacking", attackProperty);
        animator.SetTrigger("MainAttack");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerTransform.position);
        movementInput = controllerInputs.Player.Move.ReadValue<Vector2>();
        animator.SetFloat("speed", movementInput.magnitude);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Horizontal", movementInput.x);
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero && !attacking)
        {
            //rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
            rb.AddForce(movementInput * speed * 500 * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseAim = mousePos - rb.position;
        aimAngle = Mathf.Atan2(mouseAim.y, mouseAim.x);
        aimAngleDegrees = (aimAngle * Mathf.Rad2Deg);
        mouseAngleXY.y = Mathf.Sin(aimAngle);
        mouseAngleXY.x = Mathf.Cos(aimAngle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("dihnaoid");
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
