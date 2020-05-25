using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorLinha : MonoBehaviour
{
    //o testalinha serve pra que a quantidade de pontos seja calculada corretamente
    int testaLinha = 1;
    //criando o lineRenderer
    LineRenderer lRend;

    public void CriaGeradorDeLinha(Vector3 posicaoAtual, int qualInimigo)
    {
        //instanciando o lineRenderer
        lRend = gameObject.GetComponent<LineRenderer>();

        //colocando o primeiro ponto smp no player
        lRend.SetPosition(0, gameObject.transform.position);
        //adicionando uma posição a lista
        lRend.positionCount = testaLinha + qualInimigo;
        //colocando o próximo ponto onde o inimigo está
        lRend.SetPosition(qualInimigo, posicaoAtual);
        
    }
    /*
        Não sei pq não ta funcionando, mas esse método é chamado no PorcessadorDeAtaque
        logo quando o ataque da certo
         */
    public void ApagaLinha(int qual, int paraQual)
    {
        /*aqui ele deveria colocar o ponto atual no próximo ponto, 
          mas só faz isso com o primeiro(vini big burro)*/
        lRend.SetPosition(qual, lRend.GetPosition(paraQual));
        
    }

}
