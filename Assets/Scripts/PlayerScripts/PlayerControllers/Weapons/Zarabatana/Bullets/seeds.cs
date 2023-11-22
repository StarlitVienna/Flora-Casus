using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seeds : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField]

    private float speed;
    private float damage;
    private float distanceTravelled;

    Vector3 bulletDirection;
    Transform bulletTransform;
    Rigidbody2D rb;
    PlayerController playerController;


    public void setDirection(Vector3 direction)
    {
        bulletDirection = direction;
    }

    void Start()
    {
        speed = 15f;
        damage = 2;

        //playerController = gameObject.GetComponent<PlayerController>();
        bulletTransform = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "oranges")
        {
            //Enemy enemy = collision.GetComponent<Enemy>();
            //enemy.takeDamage(damage);
            //print("sdofgbiudfgbu");
            //print("sdofgbiudfgbu");

            orangeEnemy enemy = collision.GetComponent<orangeEnemy>();
            if (!enemy.dead)
            {
                enemy.takeDamage(damage);
                //print("sdofgbiudfgbu");
                Destroy(gameObject);

                Debug.Log(enemy.Health);

                //playerController.cooldownHandler.combatTimerCooldownStart();
                //print(playerController.Health);
                enemy.player.cooldownHandler.combatTimerCooldownStart();
            }
        } else if (collision.tag == "spirals")
        {
            spiral enemy = collision.GetComponent<spiral>();
            if (!enemy.dead)
            {
                enemy.takeDamage(damage);
                //print("sdofgbiudfgbu");
                Destroy(gameObject);

                Debug.Log(enemy.Health);

                //playerController.cooldownHandler.combatTimerCooldownStart();
                //print(playerController.Health);
                enemy.player.cooldownHandler.combatTimerCooldownStart();
            }
        } else if (collision.tag == "parede")
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (bulletDirection == Vector3.zero)
        {
            bulletDirection.x = Mathf.Cos(bulletTransform.rotation.eulerAngles.z * Mathf.Deg2Rad);
            bulletDirection.y = Mathf.Sin(bulletTransform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        }
        rb.MovePosition(bulletTransform.position + bulletDirection * speed * Time.fixedDeltaTime);
        distanceTravelled += Mathf.Sqrt(Mathf.Pow(bulletDirection.x * speed * Time.fixedDeltaTime, 2) + Mathf.Pow(bulletDirection.y * speed * Time.fixedDeltaTime, 2));
        
        if (distanceTravelled >= 20)
        {
            Destroy(gameObject);
        }
        //Debug.Log(bulletDirection);
        //rb.velocity = new Vector2(speed, speed);
        //bulletTransform.position.x = bulletDirection * speed;
    }
}
