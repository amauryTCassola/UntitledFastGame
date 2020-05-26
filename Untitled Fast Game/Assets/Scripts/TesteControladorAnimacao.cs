using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteControladorAnimacao : MonoBehaviour
{
    IEnumerator corrotinaPlayer;
    public IEnumerator ExecutaAnimacao(ControladorPlayer player, IInimigo inimigo, ResultadoDeAtaque resultadoAtaque)
    {
        

        if (resultadoAtaque == ResultadoDeAtaque.ATAQUE_SUCESSO)
        {
            Debug.Log("Executando animacao de ataque que deu certo... :)");
        }
        else
        {
            Debug.Log("Executando animacao de ataque que deu errado... :(");


        }

        corrotinaPlayer = player.ComecarALerpar(inimigo.PegaPosicao());
        yield return StartCoroutine(corrotinaPlayer);
    }
}
