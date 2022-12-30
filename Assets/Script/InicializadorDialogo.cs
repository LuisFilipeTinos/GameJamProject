using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicializadorDialogo : MonoBehaviour
{
    [SerializeField]    
    private GerenciarDialogo _gerenciador;
    [SerializeField]
    private Dialogo _dialogo;

    public Animator anim;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Inicializa();
            Variaveis.podeMover = false;
            Variaveis.podeAtacar = false;
            Variaveis.podePular = false;
            anim.SetBool("Walk", false);
            anim.SetBool("Jump", false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gerenciador._caixa.SetActive(false);
        }
    }
    public void Inicializa()
    {
        if (_gerenciador == null)
            return;
        _gerenciador.Inicializa(_dialogo);
    }
}
