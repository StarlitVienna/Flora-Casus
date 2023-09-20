using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCooldownHandler : MonoBehaviour
{
    private float dashCooldown;
    public float nextDashTimer;

    private float zarabatanaCooldown;
    public float nextBarrage;

    private void Start()
    {
        nextDashTimer = 0f;
        dashCooldown = 0.6f;

        nextBarrage = 0f;
        zarabatanaCooldown = 0.5f;
    }

    private void Update()
    {
        
    }

    public void barrageCooldownStart()
    {
        nextBarrage = Time.time + zarabatanaCooldown;
    }

    public void dashCooldownStart()
    {
        nextDashTimer = Time.time + dashCooldown;
    }
}
