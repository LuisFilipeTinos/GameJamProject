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
            sentenceContainer.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sentenceContainer.SetActive(true);
            isVisible = true;

            if (this.gameObject.name == "FirstSentenceTrigger")
            {
                sentenceText.text = "- Voc� vai ficar bem mam�e?" + Environment.NewLine +
                                    "- Vou sim meu amor, o m�dico disse que eu preciso fazer o tratamento e descansar.";
            }
        }
    }
}
