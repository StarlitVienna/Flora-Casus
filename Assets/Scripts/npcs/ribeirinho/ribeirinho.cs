using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ribeirinhoScript : MonoBehaviour
{
    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    public handleDialogueImages dialogImageHandler;

    public string[] sentences = new string[8] {
        "fdh.",
        "dfghdfgdfghdfghd",
        "asdfasdfasdaf",
        "asdfadfasdé, eu sinto uma familiaridade com você. É como se eu sentisse que você tem alguma relação com essa natureza que nos cerca.",
        "asdfasdf está tudo sendo levado, tudo sendo destruído por essa empresa, que está buscando mais e mais recursos daqui.",
        "asdfasdfa nós já pedimos várias vezes para que parassem com essa exploração destrutiva, mas eles nos ignoram por não termos dinheiro e poder de fala o suficiente.",
        "adsfasdfasdfe não fosse nada, destruindo habitats para os animais e matando a flora de toda essa floresta que nos cerca.",
        "Se ao menos tivesse algo para nos ajudar…"
    };

    private int sentenceNumber;

    public PlayerController playerController;

    private bool isPlayerInRange;
    private bool dialogUIisOpen;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInRange = true;
            sentenceNumber = 0;
            print("é");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInRange = false;
            sentenceNumber = 0;
        }
    }

    public void startDialogue()
    {
        if (isPlayerInRange && !dialogUIisOpen)
        {
            dialogueUI.SetActive(true);
            dialogUIisOpen = true;
        } else
        {
            if (sentenceNumber < 7)
            {
                ++sentenceNumber;
            }
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerInRange)
        {
            dialogImageHandler.switchBool(0); // imagem do ribeirinho
            dialogueUI.SetActive(false);
            dialogUIisOpen = false;
        }

        dialogueText.text = sentences[sentenceNumber];
    }
}
