using System;
using System.Collections;
using System.Collections.Generic; /*Para usar List*/
using UnityEngine;
using UnityEngine.Events;   /*Para usar UnityEvent*/
using System.Linq;  /*Para usar .Distinct()*/
using System.Threading.Tasks;

public class TesteProcessadorDeAtaque : MonoBehaviour
{
    TesteControladorAnimacao animacao;

    /*
     O script agora eh uma corrotina, eu soh n pensei em um modo pra fazer
     ele retornar um bool e sinceramente to co sono
         */

    public void AtaqueFuncionou()
    {
        Debug.Log("Suceso no ataque");
    }

    public void AtaqueDeuErrado()
    {
        Debug.Log("Falha no ataque");
    }

    public IEnumerator ExecutaAtaque(ControladorPlayer player, List<IInimigo> listaAtaque)
    {
        List<IInimigo> listaInimigos = listaAtaque.Distinct().ToList(); /*Lista de inimigos sendo atacados sem duplicatas*/
        UnityEvent eventoNovoTurno = new UnityEvent(); /*Evento que é ativado cada vez que um turno termina*/
        ResultadoDeAtaque resultadoAtaqueAtual; /*o resultado de um ataque pontual contra um inimigo, 
        usado para decidir qual animação deve ser reproduzida*/
        bool estaoTodosMortos = true; /*Resultado da checagem final do ataque*/


        // TO-DO arrumar essa porra
        animacao = GameObject.FindGameObjectWithTag("Animador").GetComponent<TesteControladorAnimacao>();
        

        /*Vincula todos os callbacks de atualização de turno*/
        foreach (IInimigo inimigo in listaInimigos)
        {
            eventoNovoTurno.AddListener(inimigo.AtualizaTurno);
        }

        /*Loop principal do ataque*/
        foreach (IInimigo inimigoAtaque in listaAtaque.ToArray())
        {
            /*checa se tem passiva pra ativar*/
            foreach (IInimigo inimigoPassiva in listaInimigos)
            {
                /*se tiver, executa e retorna false*/
                if (inimigoPassiva.PassivaAtiva() && !inimigoPassiva.EstaMorto())
                {
                   yield return StartCoroutine(animacao.ExecutaAnimacao(player, inimigoAtaque, ResultadoDeAtaque.ATAQUE_PASSIVA));
                    AtaqueDeuErrado();
                    yield break;
                }
            }

            /*tenta atacar o inimigo atual*/
            resultadoAtaqueAtual = inimigoAtaque.TesteAtaque();
            /*passa o resultado do ataque pro script de animação*/
            yield return StartCoroutine(animacao.ExecutaAnimacao(player, inimigoAtaque, resultadoAtaqueAtual));



            /*com o resultado do ataque, decide se deve continuar ou terminar*/
            if (resultadoAtaqueAtual != ResultadoDeAtaque.ATAQUE_SUCESSO)
            {
                AtaqueDeuErrado();
                yield break;
            }
            else
            {

                /*se deu certo, notifica o inimigo sendo atacado*/
                inimigoAtaque.LevaAtaque(player.Dano());
                /*chama os atualizaTurno de todos (através do evento) e continua*/
                eventoNovoTurno.Invoke();
                continue; //completamente redundante, só pra legibilidade msm
            }
        }

        /*Se chegou até aqui, loopa por todos os monstros checando se estão todos mortos*/
        foreach (IInimigo inimigo in listaInimigos)
        {
            if (!inimigo.EstaMorto())
            {
                estaoTodosMortos = false;
            }
        }

        /*Se estiverem, retorna true*/
        if (estaoTodosMortos) AtaqueFuncionou();
        /*Se não estiverem, retorna false*/
        else AtaqueDeuErrado();

        
    }


}
