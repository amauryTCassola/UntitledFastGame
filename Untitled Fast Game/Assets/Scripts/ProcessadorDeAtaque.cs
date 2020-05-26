using System;
using System.Collections;
using System.Collections.Generic; /*Para usar List*/
using UnityEngine;
using UnityEngine.Events;   /*Para usar UnityEvent*/
using System.Linq;  /*Para usar .Distinct()*/
using System.Threading.Tasks;

public class ProcessadorDeAtaque : MonoBehaviour
{


    /*Dada uma lista de inimigos representando o movimento de ataque e o player que está atacando,
    itera por essa lista, a cada turno decidindo se o ataque foi com sucesso ou não e fazendo
    requisições ao script de animação conforme segue.
    Retorna:    true, se o ataque foi um sucesso
                false, se o ataque não foi um sucesso*/
    public async Task<bool> ExecutaAtaque(ControladorPlayer player, List<IInimigo> listaAtaque){

        List <IInimigo> listaInimigos = listaAtaque.Distinct().ToList(); /*Lista de inimigos sendo atacados sem duplicatas*/
        UnityEvent eventoNovoTurno = new UnityEvent(); /*Evento que é ativado cada vez que um turno termina*/
        ResultadoDeAtaque resultadoAtaqueAtual; /*o resultado de um ataque pontual contra um inimigo, 
        usado para decidir qual animação deve ser reproduzida*/
        bool estaoTodosMortos = true; /*Resultado da checagem final do ataque*/

        /*Vincula todos os callbacks de atualização de turno*/
        foreach(IInimigo inimigo in listaInimigos){
            eventoNovoTurno.AddListener(inimigo.AtualizaTurno);
        }

        /*Loop principal do ataque*/
        foreach(IInimigo inimigoAtaque in listaAtaque){

            /*checa se tem passiva pra ativar*/
            foreach(IInimigo inimigoPassiva in listaInimigos){
                /*se tiver, executa e retorna false*/
                if(inimigoPassiva.PassivaAtiva() && !inimigoPassiva.EstaMorto()){
                    /*passa as infos pro controlador de animação*/
                   // await ControladorAnimacaoDeAtaque.ExecutaAnimacaoAtaque(player, inimigoPassiva, ResultadoDeAtaque.ATAQUE_PASSIVA);
                    /*Como teve uma passiva ativada, o ataque não deu certo e o jogador perdeu, então retorna false*/
                    return false;
                }
            }

            /*tenta atacar o inimigo atual*/
            resultadoAtaqueAtual = inimigoAtaque.TesteAtaque();
            /*passa o resultado do ataque pro script de animação*/
           // await ControladorAnimacaoDeAtaque.ExecutaAnimacaoAtaque(player, inimigoAtaque, resultadoAtaqueAtual);

            

            /*com o resultado do ataque, decide se deve continuar ou terminar*/
            if (resultadoAtaqueAtual != ResultadoDeAtaque.ATAQUE_SUCESSO){
                return false;
            } else {

                /*se deu certo, notifica o inimigo sendo atacado*/
                inimigoAtaque.LevaAtaque(player.Dano());
                /*chama os atualizaTurno de todos (através do evento) e continua*/
                eventoNovoTurno.Invoke();
                continue; //completamente redundante, só pra legibilidade msm
            }
        }

        /*Se chegou até aqui, loopa por todos os monstros checando se estão todos mortos*/
        foreach(IInimigo inimigo in listaInimigos){
            if(!inimigo.EstaMorto()){
                estaoTodosMortos = false;
            }
        }

        /*Se estiverem, retorna true*/
        if(estaoTodosMortos) return true;
        /*Se não estiverem, retorna false*/
        else return false;
    }

}
