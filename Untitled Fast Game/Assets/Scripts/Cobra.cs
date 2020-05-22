using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : MonoBehaviour, IInimigo
{
    public int vida;
    public int turnosAteAtivarPassiva ;

    public bool PassivaAtiva()
    {
        if (turnosAteAtivarPassiva == 0)
            return true;
        else
            return false;
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

    public void LevaAtaque(int dano)
    {
        this.vida -= dano;

        if (this.EstaMorto())
            /*Animação de morte aqui*/
            Destroy(gameObject);
        
    }

    public bool EstaMorto()
    {
        return vida <= 0;
    }

    public void AtualizaTurno()
    {
        turnosAteAtivarPassiva --;
        //prompt só pra saber
        if (turnosAteAtivarPassiva == 0)
            Debug.Log("Cobra te comeu");
    }
}
