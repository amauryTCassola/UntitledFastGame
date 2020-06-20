using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* 
    O ataque manager pega todas as partes necessarias para um ataque e
    as executa. Ligando o processador de ataque e mandando as informacoes
    necessarias para ele, passando para o proximo grupo de inimigos e 
    colocando o player no lugar apos um ataque. No futuro ele vai chamar
    o codigo de respawn do player tambem
     */
public class AtaqueManager : MonoBehaviour
{
    public ProcessaAtaque processador;
    public ControladorAnimacao animador;
    public NivelManager nivelManager;
    public ControladorPlayer contPlayer;

    Vector3 posicaoPlayer;

    private void Start()
    {
        processador = gameObject.GetComponent<ProcessaAtaque>();
        animador = gameObject.GetComponent<ControladorAnimacao>();
        contPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorPlayer>();
        posicaoPlayer = contPlayer.GetPlayerPosition();
    }

    public IEnumerator FazendoAtaque(ControladorPlayer player, List<IInimigo> lista)
    {
        List<IInimigo> listaInimigos = lista.Distinct().ToList();
        yield return StartCoroutine(processador.ExecutaAtaque(player, lista, animador));

        if (processador.ChecagemFinal(listaInimigos))
        {
            yield return StartCoroutine(contPlayer.ComecarALerpar(posicaoPlayer));
            nivelManager.PassaProxima();
        }
        else
        {
            Debug.Log("perdeu");
        }
    }
}
