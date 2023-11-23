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

    private string[] sentences = new string[17] {
        "Você realmente chegou…",
        "Após tanto tempo implorando por sua vinda ",
        "Nós precisamos de você.",
        "A floresta está morrendo, já não temos tanta água quanto antes",
        "Ah, essa floresta, meu lar, tudo que nos cerca, está tudo sendo levado, tudo sendo destru�do por essa empresa, que está buscando mais e mais recursos daqui.",
        "Nossa flora já não vive como se vivia em tempos passados",
        "As pessoas que trabalham nessas empresas…",
        "Parecem não se importar com a vida.",
        "Continue neste caminho",
        "Em frente você encontrará uma capa e uma zarabatana",
        "Elas te ajudarão a enfrentar o mal humano",
        "Junto delas há um diário com todo o conhecimento sobre a flora que pudemos reunir",
        "Com ele você poderá entender a vida natural e saber como recuperá-la",
        "Mais abaixo, um pouco depois do rio, há uma região sem árvores",
        "Os agentes que destruíram a vida estão lá.",
        "Ajude-nos a recuperar a vitalidade da floresta",
        "Vá!",
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
            if (sentenceNumber < 16)
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
