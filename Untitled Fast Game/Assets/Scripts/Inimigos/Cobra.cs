﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : MonoBehaviour, IInimigo
{
    public int vida;
    public int turnosAteAtivarPassiva ;
    public Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public bool PassivaAtiva()
    {
        if (turnosAteAtivarPassiva == 0)
            return true;
        else
            return false;
    }
    public Vector2 PegaPosicao()
    {
        return gameObject.transform.position;
    }


    public ResultadoDeAtaque TesteAtaque()
    {
        if (vida > 0)
        {
            return ResultadoDeAtaque.ATAQUE_SUCESSO; 
        }
            
        else
        {
            return ResultadoDeAtaque.ATAQUE_FALHA;
        }
    }

    public IEnumerator LevaAtaque(int dano)
    {
        this.vida -= dano;

        animator.SetTrigger("LevaDano");

        if (this.EstaMorto())
        {
            animator.SetTrigger("Morreu");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject);
        }
        yield break;
    }

    public bool EstaMorto()
    {
        return vida <= 0;
    }

    public void AtualizaTurno()
    {
        turnosAteAtivarPassiva --;
        //prompt só pra saber
    }
}
