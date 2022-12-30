using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GerenciarDialogo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nomeNpc;
    [SerializeField]
    private TextMeshProUGUI _texto;
    [SerializeField]
    private TextMeshProUGUI _btnContinua;

    
    public GameObject _caixa;

    private int _contador = 0;
    private Dialogo _dialogoAtual;

    public void Inicializa(Dialogo dialogo)
    {
        _contador = 0;
        _dialogoAtual = dialogo;
        ProximaFase();
    }

    public void ProximaFase()
    {
        if (_dialogoAtual == null)
        {
            return;
        }
        if(_contador == _dialogoAtual.GetFrases().Length)
        {
            Variaveis.podeMover = true;
            Variaveis.podeAtacar = true;
            Variaveis.podePular = true;
            _caixa.gameObject.SetActive(false);
            _dialogoAtual = null;
            _contador = 0;
            return;
        }
        _nomeNpc.text = _dialogoAtual.GetNomeNPC();
        _texto.text = _dialogoAtual.GetFrases()[_contador].GetFrase();
        _btnContinua.text = _dialogoAtual.GetFrases()[_contador].GetBotaoContinuar();
        _caixa.gameObject.SetActive(true);
        _contador++;
    }
}
