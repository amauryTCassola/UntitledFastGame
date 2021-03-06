﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigosManager : MonoBehaviour
{
    List<GameObject> inimigos;

    public void AddInimigos()
    {
        inimigos = new List<GameObject>(GameObject.FindGameObjectsWithTag("Inimigo"));
    }

    public void LimpaLista()
    {
        inimigos.Clear();
    }

    //Dada uma posição, se houver um inimigo suficientemente próximo a esta posição, retorna este inimigo
    public GameObject SelecionaInimigoPorPosicao(Vector3 posicao){
        foreach(GameObject inimigo in inimigos){
            if(Vector3.Distance(posicao, inimigo.transform.position) <= 0.5f){
                return inimigo;
            }
        }
        return null;
    }

    public List<IInimigo> InimigosNaTela(){
        List<IInimigo> inimigosNaTela = new List<IInimigo>();
        foreach(GameObject inimigoObj in inimigos){
            inimigosNaTela.Add(inimigoObj.GetComponent<IInimigo>());
        }
        return inimigosNaTela;
    }
}
