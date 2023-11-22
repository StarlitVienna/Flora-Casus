using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    

    public Animator animator;

    public bool spawned = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned == true)
        {
            animator.SetTrigger("spawned");
        }
    }

    void endSpawning()
    {
        spawned = true;
    }
}
