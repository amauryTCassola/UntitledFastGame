using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public static class ControladorAnimacaoDeAtaque{
    
    /*Dado um player que está atacando, um inimigo que está sendo atacando e o resultado
    da tentativa de ataque, ativa as animações do player e dp inimigo de acordo com o resultado,
    espera pelo fim delas e retorna*/
    
    //Tem que ser uma coroutine pra poder esperar pelo fim das animações antes de retornar
    public static async Task ExecutaAnimacaoAtaque(ControladorPlayer player, IInimigo inimigo, ResultadoDeAtaque resultadoAtaque){
        
        if(resultadoAtaque == ResultadoDeAtaque.ATAQUE_SUCESSO){
            Debug.Log("Executando animacao de ataque que deu certo... :)");
            await Task.Delay(1000);
        }
        else{
            Debug.Log("Executando animacao de ataque que deu errado... :(");
            await Task.Delay(1000);
        }
    }
}