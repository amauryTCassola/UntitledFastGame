using UnityEngine;

public class ControladorPlayer:MonoBehaviour
{
    int danoAtual = 1;
    public bool temQLerpar = false;

    /*aqui ele vai checar se a hora que o jogador
     * começou o lerp e quanto tempo ele lerpou
     */
    public float comecouALerpar;
    public float tempoLerpando;
    
    /*
     * aqui ele pega o local que termina o lerp e o
     * que começa
     */
    public Vector2 ondeTermina;
    public Vector2 ondeComeca;

    public int Dano(){
        return danoAtual;
    }

    /* aqui ele vai setar todos os valores pra quando for a hora de 
     * fazer o lerp ele colocar eles ali
     */
    public void comecarALerpar(Vector2 recebeTermina)
    {
        ondeTermina = recebeTermina;
        ondeComeca = transform.position;
        comecouALerpar = Time.time;

        temQLerpar = true;
    }

    /* aqui é onde a magica acontece, ele vai fazer o lerp de acordo com
     * a porcentagem de tempo q já passou, garantindo q smp seja feito em 
     * um segundo e tals
         */
    public Vector2 Lerpar(Vector2 comeca, Vector2 termina, float quandoComecou, float tempoDeLerp = 1)
    {
        float tempoDesdeComeco = Time.time - quandoComecou;

        float porcentagemCompleta = tempoDesdeComeco / tempoDeLerp;

        Vector2 resultado = Vector2.Lerp(comeca, termina, porcentagemCompleta);
        
        return resultado;
    }


    void Update()
    {
        //aqui ele chama o lerpar e coloca os parametros no lugar
        if (temQLerpar)
        {
            transform.position = Lerpar(ondeComeca, ondeTermina, comecouALerpar, tempoLerpando);
            
        }
            
    }

}