using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private bool dead;

    public Rigidbody2D rb;

    private float health = 50f;
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
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        Health -= damage;
    }

    public void takeKnockback(Vector2 knockbackVector)
    {
        rb.AddForce(knockbackVector, ForceMode2D.Impulse);
    }
}
