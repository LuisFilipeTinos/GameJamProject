using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SentencesTriggers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sentenceText;
    [SerializeField] GameObject sentenceContainer;
    bool isVisible;
    bool dadSpeaks;

    [SerializeField] PlayerController playerController;

    [SerializeField] BoxCollider2D fakeFloorCollider;
    BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        dadSpeaks = false;
        isVisible = false;
        sentenceContainer.SetActive(false);
        sentenceText.text = string.Empty;
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isVisible && Input.GetKeyDown(KeyCode.Mouse0) && !dadSpeaks)
        {
            if (this.gameObject.name == "FirstSentenceTrigger" ||
                this.gameObject.name == "ThirdSentenceTrigger")
            {
                if (this.gameObject.name == "FirstSentenceTrigger")
                    sentenceText.text = "- Essas vozes foram lembran�as?";
                else if (this.gameObject.name == "ThirdSentenceTrigger")
                    sentenceText.text = "- Ela n�o contou que o tratamento n�o estava funcionando, n�o quis preocupar a Sara.";

                dadSpeaks = true;
            }
            else
            {
                sentenceContainer.SetActive(false);
                playerController.canMove = true;
                playerController.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                coll.enabled = false;
            }
        }
        else if (isVisible && Input.GetKeyDown(KeyCode.Mouse0) && dadSpeaks)
        {
            sentenceContainer.SetActive(false);
            playerController.canMove = true;
            playerController.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            coll.enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sentenceContainer.SetActive(true);
            isVisible = true;
            playerController.canMove = false;
            playerController.rb2d.velocity = Vector2.zero;
            playerController.rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;

            if (this.gameObject.name == "FirstSentenceTrigger")
            {
                sentenceText.text = "- Voc� vai ficar bem mam�e?" + Environment.NewLine +
                                    "- Vou sim meu amor, o m�dico disse que eu preciso fazer o tratamento e descansar.";
            }
            else if (this.gameObject.name == "SecondSentenceTrigger")
            {
                fakeFloorCollider.enabled = false;
                sentenceText.text = "- N�o estou com um bom pressentimento. Acho melhor achar alguma coisa pra me defender.";
            }
            else if (this.gameObject.name == "ThirdSentenceTrigger")
            {
                sentenceText.text = "- Mam�e, porque voc� voltou pra casa? N�o devia ficar no hospital?" + Environment.NewLine +
                                    "- Eu pedi pra voltar pra casa e ficar com voc�, meu amor. Posso fazer meu tratamento daqui.";
            }
            else if (this.gameObject.name == "FourthSentenceTrigger")
            {
                sentenceText.text = "- Nossa, s�o v�rios monstros. � muito perigoso eu pular com eles ali, melhor achar alguma coisa que os acerte de longe.";
            }
            else if (this.gameObject.name == "FifthSentenceTrigger")
            {
                sentenceText.text = "(Som de prato quebrando e de falta de ar)" + Environment.NewLine +
                    "- Mam�e! Mam�e! O que t� acontecendo? Por que voc� t� respirando assim? Fala comigo!" + Environment.NewLine +
                    "(Continua o som de falta de ar)" + Environment.NewLine +
                    "- Mam�e ? Se mexe por favor.";
            }
        }
    }
}
