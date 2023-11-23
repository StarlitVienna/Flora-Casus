using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barragemScript : MonoBehaviour
{


    [SerializeField] int myDamIndex;
    private bool isPlayerInRange;

    public SpriteRenderer ownImage;
    public Collider2D ownCollider;
    public Collider2D blockCollider;

    public PlayerController playerController;

    private bool destroyed;

    public void destroyedState()
    {
        destroyed = true;
        print("GOT DESTROYED");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInRange = true;
            playerController.rachaduraInRange = true;
            print("�");
            print("�");
            print("�");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }


    void Start()
    {
        isPlayerInRange = false;   
    }

    // Update is called once per frame
    void Update()
    {

        if (!destroyed)
        {
            if (isPlayerInRange)
            {
                playerController.rachaduraInRange = true;
                playerController.lastContactDamIndex = myDamIndex;
            }
            else
            {
                playerController.rachaduraInRange = false;
            }
        } else
        {
            ownImage.enabled = false;
            ownCollider.enabled = false;
            blockCollider.enabled = false;
        }
    }
}
