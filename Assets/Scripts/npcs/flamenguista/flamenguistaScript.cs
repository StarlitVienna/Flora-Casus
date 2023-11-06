using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class flamenguistaScript : MonoBehaviour
{
    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    public handleDialogueImages dialogImageHandler;

    public string[] sentences = new string[10] {
        "Com licença...",
        "Me desculpe a pergunta, mas… o que é você?",
        "Hm, parece que você não é muito de falar. Pelo menos você parece estar entendendo o que eu estou falando.",
        "Apesar de não te conhecer e não saber que tipo de criatura misteriosa você é, eu sinto uma familiaridade com você. É como se eu sentisse que você tem alguma relação com essa natureza que nos cerca.\r\n\r\n",
        "Ah, essa floresta, meu lar, tudo que nos cerca, está tudo sendo levado, tudo sendo destruído por essa empresa, que está buscando mais e mais recursos daqui.",
        "Desde de quando ela chegou, toda a natureza daqui está morrendo aos poucos, nós já pedimos várias vezes para que parassem com essa exploração destrutiva, mas eles nos ignoram por não termos dinheiro e poder de fala o suficiente.",
        "E quando alguém consegue alcance o suficiente... bem, acontece o que aconteceu com o Chico Mendes.",
        "A gente já até tentou falar com alguns institutos como o IBAMA, mas nem assim alguma coisa acontece.",
        "Essa droga de empresa continua derrubando as árvores como se não fosse nada, destruindo habitats para os animais e matando a flora de toda essa floresta que nos cerca.",
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
            //print("é");
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
            dialogImageHandler.switchBool(1);
            dialogueUI.SetActive(false);
            dialogUIisOpen = false;
        }

        dialogueText.text = sentences[sentenceNumber];
    }
}
