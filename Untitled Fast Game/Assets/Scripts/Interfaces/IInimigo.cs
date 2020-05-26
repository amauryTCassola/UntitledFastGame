using UnityEngine;

public interface IInimigo
{
    bool PassivaAtiva();

    ResultadoDeAtaque TesteAtaque();

    void LevaAtaque(int dano);

    bool EstaMorto();

    void AtualizaTurno();

    Vector2 PegaPosicao();
}
