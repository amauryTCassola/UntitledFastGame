using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ProcessaAtaque : MonoBehaviour
{

    public IEnumerator ExecutaAtaque(ControladorPlayer player, List<IInimigo> listaAtaque, ControladorAnimacao animador)
    {
        List<IInimigo> listaInimigos = listaAtaque.Distinct().ToList(); /*Lista de inimigos sendo atacados sem duplicatas*/
        UnityEvent eventoNovoTurno = new UnityEvent(); /*Evento que é ativado cada vez que um turno termina*/
        ResultadoDeAtaque resultadoAtaqueAtual; /*o resultado de um ataque pontual contra um inimigo, 
        usado para decidir qual animação deve ser reproduzida*/

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
                    yield return StartCoroutine(animador.ExecutaAnimacao(player, inimigoAtaque, ResultadoDeAtaque.ATAQUE_PASSIVA));
                    yield break;
                }
            }

            /*tenta atacar o inimigo atual*/
            resultadoAtaqueAtual = inimigoAtaque.TesteAtaque();
            /*passa o resultado do ataque pro script de animação e espera a animação executar*/
            yield return StartCoroutine(animador.ExecutaAnimacao(player, inimigoAtaque, resultadoAtaqueAtual));


            /*com o resultado do ataque, decide se deve continuar ou terminar*/
            if (resultadoAtaqueAtual != ResultadoDeAtaque.ATAQUE_SUCESSO)
            {
                yield break;
            }
            else
            {
                /*chama os atualizaTurno de todos (através do evento) e continua*/
                eventoNovoTurno.Invoke();
                continue; //completamente redundante, só pra legibilidade msm
            }
        }
    }

    public bool ChecagemFinal(List<IInimigo> listaInimigos)
    {
        bool morreramTodos = true;
        /*Se chegou até aqui, loopa por todos os monstros checando se estão todos mortos*/
        foreach (IInimigo inimigo in listaInimigos)
        {
            if (!inimigo.EstaMorto())
            {
                morreramTodos = false;
            }
        }

        /*Se estiverem, retorna true*/
         if (morreramTodos) return true;
        /*Se não estiverem, retorna false*/
         else return false;
    }

}
