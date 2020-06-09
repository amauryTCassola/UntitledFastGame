using System.Collections;
using UnityEngine;

public interface IInimigo
{
    bool PassivaAtiva();

    ResultadoDeAtaque TesteAtaque();

    IEnumerator LevaAtaque(int dano);

    bool EstaMorto();

    void AtualizaTurno();

    Vector2 PegaPosicao();
}
