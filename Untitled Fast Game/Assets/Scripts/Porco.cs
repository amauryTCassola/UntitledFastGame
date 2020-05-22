﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porco : MonoBehaviour, IInimigo
{
    public int vida;
   
    public Vector2 PegaPosicao()
    {
        return transform.position;
    }
    
    public bool PassivaAtiva(){
        return false; //o porco nao tem passiva
    }

    public ResultadoDeAtaque TesteAtaque(){
        if (vida <= 0)
        {
            return ResultadoDeAtaque.ATAQUE_FALHA;
        }
        else {
            return ResultadoDeAtaque.ATAQUE_SUCESSO; //o player sempre consegue atacar porcos
            }
    }

    public void LevaAtaque(int dano){
        this.vida -= dano;
        if(this.EstaMorto())
            /*Animação de morte aqui*/
            Destroy(gameObject);
    }

    public bool EstaMorto(){
        return vida <= 0;
    }

    public void AtualizaTurno(){
        if(!EstaMorto())
        Debug.Log("Porco: oinc oinc novo turno");
    }

}
