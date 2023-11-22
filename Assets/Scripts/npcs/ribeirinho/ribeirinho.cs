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
        "Com licen�a.",
        "Me desculpe a pergunta, mas� o que � voc�?",
        "Hm, parece que voc� n�o � muito de falar. Pelo menos voc� parece estar entendendo o que eu estou falando.",
        "Apesar de n�o te conhecer e n�o saber que tipo de criatura misteriosa voc� �, eu sinto uma familiaridade com voc�. � como se eu sentisse que voc� tem alguma rela��o com essa natureza que nos cerca.",
        "Ah, essa floresta, meu lar, tudo que nos cerca, est� tudo sendo levado, tudo sendo destru�do por essa empresa, que est� buscando mais e mais recursos daqui.",
        "Desde de quando ela chegou, toda a natureza daqui est� morrendo aos poucos, n�s j� pedimos v�rias vezes para que parassem com essa explora��o destrutiva, mas eles nos ignoram por n�o termos dinheiro e poder de fala o suficiente.",
        "E quando algu�m consegue alcance o suficiente... bem, acontece o que aconteceu com o Chico Mendes.",
        "Ah, voc� n�o sabe quem foi ele?",
        "Chico Mendes foi um ambientalista que sempre lutou pela prote��o do meio ambiente, mas, por ser contra a explora��o n�o sustent�vel, ele acabou sendo...",
        "*suspiro*",
        "Enfim, o que eu quero dizer � que ningu�m consegue mudar os planos dessa empresa.",
        "A gente j� at� tentou falar com alguns institutos como o IBAMA, mas nem assim alguma coisa acontece.",
        "Essa droga de empresa continua derrubando as �rvores como se n�o fosse nada, destruindo habitats para os animais e matando a flora de toda essa floresta que nos cerca.",
        "Se ao menos tivesse algo para nos ajudar, algo para fazer com que eles param de desmatar a floresta...",
        "Espera um pouco, talvez voc� consiga!",
        "Eu n�o sei se voc� tem algum poder ou algo do tipo, mas se voc� realmente estiver ligado de alguma forma � natureza, voc� deve saber o que fazer!",
        "Antes da ponte da cidade, se voc� subir a estrada e virar � esquerda, vai encontrar o local que a empresa est� destruindo dessa vez.",
        "Se voc� for capaz de fazer algo, l� � o lugar.",
        "Conto com voc�!"
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
