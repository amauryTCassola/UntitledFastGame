using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porco : MonoBehaviour, IInimigo
{
    public int vida;
    Animator animator; // pra pegar o animator

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public Vector2 PegaPosicao()
    {
        return gameObject.transform.position;
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

    public bool EstaMorto(){
        return vida <= 0;
    }

    public void AtualizaTurno(){
        if(!EstaMorto())
        Debug.Log("Porco: oinc oinc novo turno");
    }

}
