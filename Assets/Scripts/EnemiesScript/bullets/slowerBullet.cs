using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class slowdownBullet : MonoBehaviour
{


    private float speed;
    private float damage;
    private float distanceTravelled;
    public float deltaMult = 1f;
    public float deltaAdd = 0f;
    public float deltaSub = 0f;
    public float deltaDiv = 1f;

    public float deltaAngleAdd = 0f;
    public float deltaAngleSub = 0f;
    public float deltaAngleDiv = 1f;
    public float deltaAngleMult = 1f;

    private float disappearDistance = 200;

    Vector3 bulletDirection;
    Transform bulletTransform;
    Rigidbody2D rb;

    public void setDirection(Vector3 direction)
    {
        bulletDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Enemy enemy = collision.GetComponent<Enemy>();
            //enemy.takeDamage(damage);

            PlayerController player = collision.GetComponent<PlayerController>();
            player.takeDamage(damage);
            //print("sdofgbiudfgbu");
            Destroy(gameObject);

            Debug.Log(player.Health);

            //playerController.cooldownHandler.combatTimerCooldownStart();
            //print(playerController.Health);
            player.cooldownHandler.combatTimerCooldownStart();
        } else if (collision.tag == "parede")
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        speed = 10f;
        damage = 1;

        //playerController = gameObject.GetComponent<PlayerController>();
        bulletTransform = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        speed *= deltaMult;
        speed += deltaAdd;
        speed -= deltaSub;
        speed /= deltaDiv;

        if (speed <= 0.01)
        {
            Destroy(gameObject);
        }

        if (bulletDirection == Vector3.zero)
        {
            bulletDirection.x = Mathf.Cos((bulletTransform.rotation.eulerAngles.z * Mathf.Deg2Rad));
            bulletDirection.y = Mathf.Sin((bulletTransform.rotation.eulerAngles.z * Mathf.Deg2Rad));
        }

        //bulletDirection.x = Mathf.Cos((bulletTransform.rotation.eulerAngles.z * Mathf.Deg2Rad + deltaAngleAdd - deltaAngleSub) * deltaAngleMult / deltaAngleDiv);
        //bulletDirection.y = Mathf.Sin((bulletTransform.rotation.eulerAngles.z * Mathf.Deg2Rad + deltaAngleAdd - deltaAngleSub) * deltaAngleMult / deltaAngleDiv);

        rb.MovePosition(bulletTransform.position + bulletDirection * speed * Time.fixedDeltaTime);
        //rb.AddForce(bulletDirection * speed*20 * Time.fixedDeltaTime, ForceMode2D.Force);
        distanceTravelled += Mathf.Sqrt(Mathf.Pow(bulletDirection.x * speed * Time.fixedDeltaTime, 2) + Mathf.Pow(bulletDirection.y * speed * Time.fixedDeltaTime, 2));

        if (distanceTravelled >= disappearDistance)
        {
            Destroy(gameObject);
        }
        //Debug.Log(bulletDirection);
        //rb.velocity = new Vector2(speed, speed);
        //bulletTransform.position.x = bulletDirection * speed;
    }
}
