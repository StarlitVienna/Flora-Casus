using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookNcape : MonoBehaviour
{

    public GameObject bookNcapeObject;
    public PlayerController playerController;
    void OnTriggerEnter2D(Collider2D collision)
    {


        print("TRIGGERED"); 
        if (collision.tag == "Player")
        {
            playerController.isDashUnlocked = true;
            playerController.isCapeUnlocked = true;
            playerController.isDiaryUnlocked = true;
            playerController.isZarabatanaUnlocked = true;

            //put the cape on
            print("CapeIsOn");
            playerController.putCapeOn();
            print("CapeIsOn");
            //put the cape on

            bookNcapeObject.SetActive(false);
        }
    }

    void Update()
    {

    }
}
