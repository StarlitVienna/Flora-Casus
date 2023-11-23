using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

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

    //public MainWeaponScript mainWeapon;
    public zarabatanaScript zarabatana;

    public bl_Joystick joystick;

    public spawnScript spawner;

    public Dash dashHandler;
    public playerCooldownHandler cooldownHandler;
    public Camera cam;

    //public pauseMenu pausing;

    public Vector2 mousePos;
    public Vector2 mouseAim;
    public Vector2 mouseAngleXY;
    public float aimAngle;
    public float aimAngleDegrees;

    private float speed = 60f;
    private float attackSpeed = 1.5f;

    private float health = 40f;

    // KILLS
    private float score = 0;

    public bool dead;
    public bool attacking;
    public bool dashing;


    public bool isZarabatanaUnlocked;

    public bool isDiaryUnlocked;

    public bool isDashUnlocked;

    public bool isCapeUnlocked;


    public bool isCapeOn = false;


    public bool gameIsPaused;

    public bool diaryIsOpen;



    public bool isInCombat;

    // ifx the enemy detect the player is in combat, it will attack it



    public mainDiaryScript diary;
    public mortisCanvas mortCanva;

    public flamenguistaScript flamenguista;
    public ribeiroScript ribeiro;


    public mainHealthBar healthBar;

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            mortCanva.forceUpdate();
            Debug.Log(health);
            if (health <= 0 )
            {
                dead = true;
                
                animator.SetTrigger("Death");
                //pausing.pauseGame();
                mortCanva.forceUpdate();
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

    public bool rachaduraInRange;


    //public Scene scene = SceneManager.GetActiveScene();

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        dead = false;
    }
    void Start()
    {

        diaryIsOpen = false;
        isInCombat = false;
        isZarabatanaUnlocked = false;

        healthBar.setMaxHealth(health);

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
            //controllerInputs.Player.Attack.performed += Attack;
            //controllerInputs.Player.DashDirection.performed += dashDirection;
            controllerInputs.Player.SimpleDash.performed += simpleDash;
            controllerInputs.Player.Fire.performed += shoot;
            controllerInputs.Player.OpenDiary.performed += openCloseDiary;

            controllerInputs.Player.Interact.performed += startADialogue;
            controllerInputs.Player.Destroy.performed += destroyDam;

            //controllerInputs.Player.CloseGame.performed += closeGame;
        }

        //animator.SetBool("Cape", true);
        animator.SetFloat("AttackSpeed", attackSpeed);
    }


    public int npcIndex = -1;


    public bool playerActionsStopped = false;



    public void setNPCIndex(int indextoset)
    {
        npcIndex = indextoset;
    }


    void destroyDam(InputAction.CallbackContext context)
    {
        if (rachaduraInRange)
        {
            print("DESTRUIÇÃO");
            animator.SetTrigger("destruirBarragem");
            lastDestroyedIndex = lastContactDamIndex;
            print(lastContactDamIndex);
            sendDestroyedSignal();
        } else
        {
            print("LONGE");
        }
    }

    public int lastContactDamIndex = -1;
    public int lastDestroyedIndex = -1;

    public barragemScript barragemUM;
    public barragemScript barragemDOIS;
    public barragemScript barragemTRES;

    void sendDestroyedSignal()
    {
        if (lastDestroyedIndex == 1)
        {
            barragemUM.destroyedState();
        } else if (lastDestroyedIndex == 2)
        {
            barragemDOIS.destroyedState();
        } else if (lastDestroyedIndex == 3)
        {
            barragemTRES.destroyedState();
        }
    }

    public void stopActions()
    {
        playerActionsStopped = true;
    }

    public void resumeActions()
    {
        playerActionsStopped = false;
    }


    void startADialogue(InputAction.CallbackContext context)
    {
        flamenguista.startDialogue();
        ribeiro.startDialogue();
    }

    public void mobileStartADialogue()
    {
        flamenguista.startDialogue();
        ribeiro.startDialogue();
    }

    void openCloseDiary(InputAction.CallbackContext context)
    {
        if (isDiaryUnlocked)
        {
            if (diaryIsOpen)
            {
                diaryIsOpen = false;
                gameIsPaused = false;
                // need to call update for it because its disabled
                diary.forceUpdate();

            }
            else
            {
                gameIsPaused = true;
                diaryIsOpen = true;
                diary.forceUpdate();
            }
        }
    }

    public void addKill()
    {
        ++score;
    }

    public float getScore()
    {
        return score;
    }

    public void putCapeOn()
    {

        if (isCapeUnlocked)
        {
            if (isCapeOn)
            {
                isCapeOn = false;
                animator.SetBool("Cape", false);
            } else
            {
                isCapeOn = true;
                animator.SetBool("Cape", true);
            }
        }
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
        if (!dead && Time.timeScale > 0.01 && !playerActionsStopped && isZarabatanaUnlocked)
        {
            if (cooldownHandler.nextBarrage < Time.time)
            {
                //ff (movementInput.x)
                print(movementInput.x);
                /*
                if (movementInput.x > 0)
                {
                    aimAngle = 0 * Mathf.Deg2Rad;
                    //attackDirection = attackDirections.right;
                }
                else if (movementInput.x < 0)
                {
                    aimAngle = 180 * Mathf.Deg2Rad;
                }
                else if (movementInput.y > 0)
                {
                    aimAngle = 90 * Mathf.Deg2Rad;
                }
                else if (movementInput.y < 0)
                {
                    aimAngle = 270 * Mathf.Deg2Rad;
                }
                else
                {
                    aimAngle = 270 * Mathf.Deg2Rad;
                }
                */
                aimAngle = Mathf.Atan2(movementInput.y, movementInput.x);
                //aimAngle = 1.5708f; //90 degrees
                zarabatana.shoot(aimAngle);
                cooldownHandler.barrageCooldownStart();
                animator.SetFloat("Horizontal", Mathf.Cos(aimAngle));
                animator.SetFloat("Vertical", Mathf.Sin(aimAngle));
                animator.SetTrigger("shoot");
                //Debug.Log(aimAngle);
            }
        }
    }

    public void mobileShoot()
    {
        if (!dead && Time.timeScale > 0.01 && !playerActionsStopped && isZarabatanaUnlocked)
        {
            if (cooldownHandler.nextBarrage < Time.time)
            {
                //ff (movementInput.x)
                print(movementInput.x);

                if (movementInput.x > 0)
                {
                    aimAngle = 0 * Mathf.Deg2Rad;
                    //attackDirection = attackDirections.right;
                }
                else if (movementInput.x < 0)
                {
                    aimAngle = 180 * Mathf.Deg2Rad;
                }
                else if (movementInput.y > 0)
                {
                    aimAngle = 90 * Mathf.Deg2Rad;
                }
                else if (movementInput.y < 0)
                {
                    aimAngle = 270 * Mathf.Deg2Rad;
                }
                else
                {
                    aimAngle = 270 * Mathf.Deg2Rad;
                }
                //aimAngle = 1.5708f; //90 degrees
                zarabatana.shoot(aimAngle);
                cooldownHandler.barrageCooldownStart();
                animator.SetFloat("Horizontal", Mathf.Cos(aimAngle));
                animator.SetFloat("Vertical", Mathf.Sin(aimAngle));
                animator.SetTrigger("shoot");
                //Debug.Log(aimAngle);
            }
        }
    }

    public void mobileDash()
    {
        if (!dead && Time.timeScale > 0.01 && isDashUnlocked && !playerActionsStopped)
        {
            if (cooldownHandler.nextDashTimer < Time.time)
            {
                animator.SetTrigger("dash");
                rb.AddForce(dashHandler.dashAction(movementInput), ForceMode2D.Impulse);
                cooldownHandler.dashCooldownStart();
            }
        }
    }

    void simpleDash(InputAction.CallbackContext context)
    {
        if (!dead && Time.timeScale > 0.01 && isDashUnlocked && !playerActionsStopped)
        {
            if (cooldownHandler.nextDashTimer < Time.time)
            {
                animator.SetTrigger("Dash");
                rb.AddForce(dashHandler.dashAction(movementInput), ForceMode2D.Impulse);
                cooldownHandler.dashCooldownStart();
            }
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
        if (!dead && Time.timeScale > 0.01 && !playerActionsStopped)
        {
            animator.SetTrigger("MainAttack");
        }
    }
    private void Attack(InputAction.CallbackContext context)
    {
        if (!dead && Time.timeScale > 0.01 && !playerActionsStopped)
        {
            //attackProperty = true;
            //animator.SetBool("Attacking", attackProperty);
            animator.SetTrigger("MainAttack");
        }
    }


    public bool joystickMode;

    public void switchInputMode()
    {

        if (joystickMode == true)
        {
            joystickMode = false;
            joystickCanvas.enabled = false;
        } else
        {
            joystickMode = true;
            joystickCanvas.enabled = true;
        }
    }



    // Update is called once per frame
    void Update()
    {

        if (spawner.spawned)
        {
            animator.SetTrigger("spawned");
        }

        if (!dead && Time.timeScale > 0.01 && !playerActionsStopped)
        {
            healthBar.setHealth(health);
            if (joystickMode == true)
            {
                if (joystick.Horizontal > 0.0004f)
                {
                    movementInput.x =  (joystick.Horizontal * speed) / 333.333333333f;
                    print(movementInput.x);
                } else if (joystick.Horizontal < -0.0004f)
                {
                    movementInput.x = (joystick.Horizontal * speed) / 333.333333333f;
                    print(movementInput.x);
                } else
                {
                    movementInput.x = 0;
                    Debug.Log(joystick.Horizontal);
                }


                if (joystick.Vertical > 0.0004f)
                {
                    movementInput.y = joystick.Vertical * speed / 333.333333333f;
                    print(movementInput.x);
                } else if (joystick.Vertical < -0.0004f)
                {
                    movementInput.y = joystick.Vertical * speed / 333.333333333f;
                    print(movementInput.x);
                } else
                {
                    movementInput.y = 0;
                }
            } else
            {
                //Debug.Log(playerTransform.position);
                if (!playerActionsStopped)
                {
                    movementInput = controllerInputs.Player.Move.ReadValue<Vector2>();
                    //print(movementInput.x);
                    //print(movementInput.y);
                } else
                {
                    print("STOPED");
                }
            }
            animator.SetFloat("speed", movementInput.magnitude);
            animator.SetFloat("Vertical", movementInput.y);
            animator.SetFloat("Horizontal", movementInput.x);
            //Debug.Log(joystick.Vertical);
        }
    }

    private void FixedUpdate()
    {
        if (!dead && Time.timeScale > 0.01)
        {
            if (!playerActionsStopped)
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

                cooldownHandler.combatTimerCooldownChecker();
                //isInCombat = cooldownHandler.outOfCombat;
                if (cooldownHandler.outOfCombat)
                {
                    isInCombat = false;
                }
                else
                {
                    isInCombat = true;
                }
            }

        }
    }


    void handleCollisions(Collider2D collision)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("dihnaoid");
    }

    public void takeKnockback(Vector2 knockbackVector)
    {
        if (!dead && Time.timeScale > 0.01 && !playerActionsStopped)
        {
            rb.AddForce(knockbackVector, ForceMode2D.Impulse);
        }
    }

    public void takeDamage(float damageTaken)
    {
        if (!dead && Time.timeScale > 0.01 && !playerActionsStopped)
        {
            Health -= damageTaken;
        }
    }

    private void mainAttackEnable()
    {
        if (!dead && Time.timeScale > 0.01 && !playerActionsStopped)
        {
            setAttackDirection();
           // mainWeapon.knockbackVector = attackDirectionVector;
            //mainWeapon.attackEnable();
            attacking = true;
        }
    }

    private void mainAttackDisable()
    {
        //mainWeapon.attackDisable();
        attacking = false;
    }

    private void setAttackDirection()
    {
        // attacking is only true after this function is called
        if (!attacking && !dead && Time.timeScale > 0.01 && !playerActionsStopped)
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
