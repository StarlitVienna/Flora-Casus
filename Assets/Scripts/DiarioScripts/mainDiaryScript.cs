using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainDiaryScript : MonoBehaviour
{
    public PlayerController controllerAccess;

    public GameObject diaryUI;

    void Update()
    {

        if (controllerAccess.diaryIsOpen)
        {
            diaryUI.SetActive(true);
            //Time.timeScale = 0f;
        } else
        {
            diaryUI.SetActive(false);
            //Time.timeScale = 1f;
        }
    }

    public void forceUpdate()
    {
        Update();
    }
}
