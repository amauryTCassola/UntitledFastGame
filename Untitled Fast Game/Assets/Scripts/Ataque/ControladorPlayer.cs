using System.Collections;
using UnityEngine;

public class ControladorPlayer:MonoBehaviour
{
    int danoAtual = 1;
    public bool temQLerpar = false;

    /*aqui ele vai checar se a hora que o jogador
     * comecou o lerp e quanto tempo ele lerpou
     */
    public float comecouALerpar;
    public float tempoLerpando;
    
    /*
     * aqui ele pega o local que termina o lerp e o
     * que comeca
     */
    public Vector2 ondeTermina;
    public Vector2 ondeComeca;

    public int Dano(){
        return danoAtual;
    }

    public Vector3 GetPlayerPosition(){
        return gameObject.transform.position;
    }


    public IEnumerator ComecarALerpar(Vector2 recebeTermino)
    {
        ondeComeca = transform.position;
        comecouALerpar = 0f;
        tempoLerpando = 0.2f;
        
        while (comecouALerpar <= tempoLerpando)
        {
            transform.position = Vector3.Lerp(ondeComeca,recebeTermino, (comecouALerpar/tempoLerpando));
            comecouALerpar += Time.deltaTime;
            yield return null;
        }
    }

}