using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject canvasMenu;
    public GameObject pause;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
        pause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
            Time.timeScale = 0;

        }
    }

    public void StartGame()
    {
        //Time.timeScale = 1;
        canvasMenu.SetActive(false);
        Variaveis.podeMover = true;
        Variaveis.podeAtacar = true;
        Variaveis.podePular = true;
    }

    void Resume()
    {
        pause.SetActive(false);
        Time.timeScale= 1;
    }

    void Menu()
    {
        canvasMenu.SetActive(true);
        
    }
}
