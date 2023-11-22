using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ribeiroScript : MonoBehaviour
{
    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    public handleDialogueImages dialogImageHandler;

    public string[] sentences = new string[19] {
        "Com licença.",
        "Me desculpe a pergunta, mas… o que é você?",
        "Hm, parece que você não é muito de falar. Pelo menos você parece estar entendendo o que eu estou falando.",
        "Apesar de não te conhecer e não saber que tipo de criatura misteriosa você é, eu sinto uma familiaridade com você. É como se eu sentisse que você tem alguma relação com essa natureza que nos cerca.",
        "Ah, essa floresta, meu lar, tudo que nos cerca, está tudo sendo levado, tudo sendo destruído por essa empresa, que está buscando mais e mais recursos daqui.",
        "Desde de quando ela chegou, toda a natureza daqui está morrendo aos poucos, nós já pedimos várias vezes para que parassem com essa exploração destrutiva, mas eles nos ignoram por não termos dinheiro e poder de fala o suficiente.",
        "E quando alguém consegue alcance o suficiente... bem, acontece o que aconteceu com o Chico Mendes.",
        "Ah, você não sabe quem foi ele?",
        "Chico Mendes foi um ambientalista que sempre lutou pela proteção do meio ambiente, mas, por ser contra a exploração não sustentável, ele acabou sendo...",
        "*suspiro*",
        "Enfim, o que eu quero dizer é que ninguém consegue mudar os planos dessa empresa.",
        "A gente já até tentou falar com alguns institutos como o IBAMA, mas nem assim alguma coisa acontece.",
        "Essa droga de empresa continua derrubando as árvores como se não fosse nada, destruindo habitats para os animais e matando a flora de toda essa floresta que nos cerca.",
        "Se ao menos tivesse algo para nos ajudar, algo para fazer com que eles param de desmatar a floresta...",
        "Espera um pouco, talvez você consiga!",
        "Eu não sei se você tem algum poder ou algo do tipo, mas se você realmente estiver ligado de alguma forma à natureza, você deve saber o que fazer!",
        "Antes da ponte da cidade, se você subir a estrada e virar à esquerda, vai encontrar o local que a empresa está destruindo dessa vez.",
        "Se você for capaz de fazer algo, lá é o lugar.",
        "Conto com você!"
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
            print("PERTO");
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
            print("STarutedo");
            dialogueUI.SetActive(true);
            dialogUIisOpen = true;
            print("STARTED");
        }
        else
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
