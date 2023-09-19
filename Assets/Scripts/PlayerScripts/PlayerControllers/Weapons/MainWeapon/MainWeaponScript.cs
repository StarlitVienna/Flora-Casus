using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeaponScript : MonoBehaviour
{
    private float damage;

    public Vector2 knockbackVector;
    private Vector2 knockbackForce = new Vector2(8, 8);

    private Collider2D weaponCollider;
    private void Start()
    {
        damage = 3;
        weaponCollider = gameObject.GetComponent<Collider2D>();
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.takeDamage(damage);
            enemy.takeKnockback(knockbackVector * knockbackForce * enemy.rb.mass);
            
        }
    }

    public void attackEnable()
    {
        weaponCollider.enabled = true;
    }

    public void attackDisable()
    {
        weaponCollider.enabled = false;
    }
}
