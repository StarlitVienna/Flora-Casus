using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spiralCooldownHandler : MonoBehaviour
{


    private float barrageCooldown;
    public float nextBarrage;





    void Start()
    {
        nextBarrage = 0f;
        barrageCooldown = 0.035f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void setBarrageCoooldown(float cooldown)
    {
        barrageCooldown = cooldown;
    }

    public void barrageCooldownStart()
    {
        nextBarrage = Time.time + barrageCooldown;
    }
}
