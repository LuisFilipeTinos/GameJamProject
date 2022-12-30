using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject canvasMenu;
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //Time.timeScale = 1;
        canvasMenu.SetActive(false);
        Variaveis.podeMover = true;
        Variaveis.podeAtacar = true;
        Variaveis.podePular = true;
    }
}
