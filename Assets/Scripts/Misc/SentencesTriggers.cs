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
    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        sentenceContainer.SetActive(false);
        sentenceText.text = string.Empty;
    }

    private void Update()
    {
        if (isVisible && Input.GetKeyDown(KeyCode.Z))
        {
            sentenceContainer.SetActive(false);
            playerController.canMove = true;
            playerController.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
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
            playerController.rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            if (this.gameObject.name == "FirstSentenceTrigger")
            {
                sentenceText.text = "- Você vai ficar bem mamãe?" + Environment.NewLine +
                                    "- Vou sim meu amor, o médico disse que eu preciso fazer o tratamento e descansar.";
            }
            else if (this.gameObject.name == "SecondSentenceTrigger")
            {
                sentenceText.text = "- Não estou com um bom pressentimento. Acho melhor achar alguma coisa pra me defender.";
            }
        }
    }
}
