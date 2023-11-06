using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCooldownHandler : MonoBehaviour
{
    private float dashCooldown;
    public float nextDashTimer;

    private float zarabatanaCooldown;
    public float nextBarrage;

    private float combatTimer;
    private float combatTimerEnd;
    public bool outOfCombat;

    private void Start()
    {
        nextDashTimer = 0f;
        dashCooldown = 0.6f;

        nextBarrage = 0f;
        zarabatanaCooldown = 0.25f;

        combatTimer = 5f;
    }

    private void Update()
    {
        
    }

    public void barrageCooldownStart()
    {
        nextBarrage = Time.time + zarabatanaCooldown;
    }

    public void combatTimerCooldownChecker()
    {
        if (!outOfCombat && Time.time >= combatTimerEnd)
        {
            outOfCombat = true;
        }
    }

    public void combatTimerCooldownStart()
    {
        combatTimerEnd = Time.time + combatTimer;
        outOfCombat = false;
    }

    public void dashCooldownStart()
    {
        nextDashTimer = Time.time + dashCooldown;
    }
}
