using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainBullet : MonoBehaviour
{


    private float speed;
    private float damage;
    private float distanceTravelled;

    private float disappearDistance = 20;

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
            print("sdofgbiudfgbu");
            Destroy(gameObject);

            Debug.Log(player.Health);

            //playerController.cooldownHandler.combatTimerCooldownStart();
            //print(playerController.Health);
            player.cooldownHandler.combatTimerCooldownStart();
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
        if (bulletDirection == Vector3.zero)
        {
            bulletDirection.x = Mathf.Cos(bulletTransform.rotation.eulerAngles.z * Mathf.Deg2Rad);
            bulletDirection.y = Mathf.Sin(bulletTransform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        }
        rb.MovePosition(bulletTransform.position + bulletDirection * speed * Time.fixedDeltaTime);
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
