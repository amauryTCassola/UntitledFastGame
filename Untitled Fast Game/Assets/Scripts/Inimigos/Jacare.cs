﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jacare : MonoBehaviour, IInimigo
{
    public int vida = 2;
    public bool levouAtaque = false;
    public bool bocaAberta = false;
    public int turnosAteFecharBoca;
    public Vector2 posicao;
    public Sprite SpriteBocaAberta;
    public Sprite SpriteBocaFechada;
    public bool PassivaAtiva(){
        return false; //o jacare nao tem passiva
    }
     
    public Vector2 PegaPosicao()
    {
        return gameObject.transform.position;
    }
    public ResultadoDeAtaque TesteAtaque(){
        if(bocaAberta)
            return ResultadoDeAtaque.ATAQUE_FALHA; //o player nao pode atacar enquanto ele tiver a boca aberta
        else{
            return ResultadoDeAtaque.ATAQUE_SUCESSO;
        }
    }

    public void LevaAtaque(int dano){
        this.vida -= dano;

        if(this.EstaMorto())
            /*Animação de morte aqui*/
            Destroy(gameObject);
        else{
            this.levouAtaque = true;
        }
    }

    public bool EstaMorto(){
        return vida <= 0;
    }

    public void AtualizaTurno(){
        if(bocaAberta){
            this.turnosAteFecharBoca--;
            if (this.turnosAteFecharBoca <= 0){

                gameObject.GetComponent<SpriteRenderer>().sprite = SpriteBocaFechada;
                
                bocaAberta = false;
            }
        }
        else if(levouAtaque)
        {
            this.bocaAberta = true;
            this.levouAtaque = false;
            this.turnosAteFecharBoca = 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = SpriteBocaAberta;
           
        }
    }
}