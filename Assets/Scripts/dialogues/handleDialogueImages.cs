using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handleDialogueImages : MonoBehaviour
{
    public Image flamenguistaSprite;
    public Image ribeirinhoSprite;

    private bool isFlamenguista;

    public void switchBool(int indexType)
    {
        if (indexType == 0)
        {
            isFlamenguista = false;
        } else
        {
            isFlamenguista = true;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlamenguista)
        {
            flamenguistaSprite.enabled = true;
            ribeirinhoSprite.enabled = false;
        }
        else
        {
            flamenguistaSprite.enabled = false;
            ribeirinhoSprite.enabled = true;
        }
    }
}
