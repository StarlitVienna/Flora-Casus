using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class displayScore : MonoBehaviour
{

    public TMP_Text text;
    public PlayerController controller;
    void Update()
    {
        text.text = controller.getScore().ToString();
    }
}
