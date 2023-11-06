using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortisCanvas : MonoBehaviour
{

    public GameObject mortisUI;
    public PlayerController controllerAccess;


    private void Start()
    {
        mortisUI.SetActive(false);
    }

    void Update()
    {

        if (controllerAccess.dead)
        {
            mortisUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            mortisUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }


    public void forceUpdate()
    {
        Update();
    }
}
