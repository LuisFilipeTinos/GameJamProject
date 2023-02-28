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

    [SerializeField] PlayerController playerController;

    [SerializeField] BoxCollider2D fakeFloorCollider;
    BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        sentenceContainer.SetActive(false);
        sentenceText.text = string.Empty;
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isVisible && Input.GetKeyDown(KeyCode.Z))
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
                fakeFloorCollider.enabled = false;
                sentenceText.text = "- Mam�e, porque voc� voltou pra casa? N�o devia ficar no hospital?" + Environment.NewLine +
                                    "- Eu pedi pra voltar pra casa e ficar com voc�, meu amor. Posso fazer meu tratamento daqui.";
            }
            else if (this.gameObject.name == "FourthSentenceTrigger")
            {
                fakeFloorCollider.enabled = false;
                sentenceText.text = "- Nossa, s�o v�rios monstros. � muito perigoso eu pular com eles ali, melhor achar alguma coisa que os acerte de longe.";
            }
            else if (this.gameObject.name == "FifthSentenceTrigger")
            {
                fakeFloorCollider.enabled = false;
                sentenceText.text = "(Som de prato quebrando e de falta de ar)" + Environment.NewLine +
                    "- Mam�e! Mam�e! O que t� acontecendo? Por que voc� t� respirando assim? Fala comigo!" + Environment.NewLine +
                    "(Continua o som de falta de ar)" + Environment.NewLine +
                    "- Mam�e ? Se mexe por favor.";
            }
        }
    }
}
