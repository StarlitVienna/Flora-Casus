using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCooldownHandler : MonoBehaviour
{


    private float barrageCooldown;
    public float nextBarrage;





    void Start()
    {
        nextBarrage = 0f;
        barrageCooldown = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void barrageCooldownStart()
    {
        nextBarrage = Time.time + barrageCooldown;
    }
}
