using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAnimacao : MonoBehaviour
{
    public IEnumerator ExecutaAnimacao(ControladorPlayer player, IInimigo inimigo, ResultadoDeAtaque resultadoAtaque)
    {


        if (resultadoAtaque == ResultadoDeAtaque.ATAQUE_SUCESSO)
        {
            yield return StartCoroutine(player.ComecarALerpar(inimigo.PegaPosicao())); //espera o player fazer o lerp pra continuar
            StartCoroutine(inimigo.LevaAtaque(player.Dano()));
            
        }
        //quando o jogador morrer por passiva ele n vai ateh o proximo inimigo
        else if (resultadoAtaque == ResultadoDeAtaque.ATAQUE_PASSIVA)
        {
            Debug.Log("Executando animacao de ataque que deu passiva... :(");
            Destroy(player.gameObject);
        }
        else
        {
            yield return StartCoroutine(player.ComecarALerpar(inimigo.PegaPosicao()));//espera o lerp e destroi o player
            Debug.Log("Executando animacao de ataque que deu errado... :(");
            Destroy(player.gameObject);
        }

        yield break;
    }
}
