using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AtaqueManager : MonoBehaviour
{
    public ProcessaAtaque processador;
    public ControladorAnimacao animador;


    private void Start()
    {
        processador = gameObject.GetComponent<ProcessaAtaque>();
        animador = gameObject.GetComponent<ControladorAnimacao>();
    }

    public IEnumerator FazendoAtaque(ControladorPlayer player, List<IInimigo> lista)
    {
        List<IInimigo> listaInimigos = lista.Distinct().ToList();
        yield return StartCoroutine(processador.ExecutaAtaque(player, lista, animador));

        if (processador.ChecagemFinal(listaInimigos))
        {
            Debug.Log("Ganhastes");
        }
        else
        {
            Debug.Log("Perdestes");
        }
    }

    
}
