using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorLinha{
    LineRenderer lRend;
    GameObject rendererObject;

    public ControladorLinha()
    {
        rendererObject = (GameObject)Object.Instantiate(Resources.Load("DesenhadorDeLinha"));
        lRend = rendererObject.GetComponent<LineRenderer>();

    }

    //Retorna os pontos da lista de pontos do line renderer em forma de List<Vector3>
    List<Vector3> GetListaDePontos(){
        Vector3[] positions = new Vector3[lRend.positionCount];
        lRend.GetPositions(positions);
        return new List<Vector3>(positions);
    }

    //Dada uma List<Vector3>, transforma a lista em array e a insere no lineRenderer
    void SetListaDePontos(List<Vector3> pontosLista){
        lRend.positionCount = pontosLista.Count;
        lRend.SetPositions(pontosLista.ToArray());
    }

    //Dado um ponto representado por um Vector3, se este ponto faz parte da lista de pontos
    //do line renderer, retira este ponto da lista
    //Se o ponto não está na lista, não faz nada
    public void ApagaPonto(Vector3 pontoASerApagado){
        List<Vector3> pontosLista = GetListaDePontos();
        if(pontosLista.Contains(pontoASerApagado)){
            pontosLista.Remove(pontoASerApagado);
            SetListaDePontos(pontosLista);
        }
    }

    //Dado um ponto representado por um Vector3, adiciona ele ao fim da lista de pontos do line renderer
    public void AdicionaPonto(Vector3 novoPonto){
        List<Vector3> pontosLista = GetListaDePontos();
        pontosLista.Add(novoPonto);
        SetListaDePontos(pontosLista);
    }

    //Dado um Vector3 original e um Vector3 novo, se o original existir na lista,
    //substitui ele pelo novo
    public void SubstituiPonto(Vector3 pontoOriginal, Vector3 pontoNovo){
        List<Vector3> pontosLista = GetListaDePontos();
        if(pontosLista.Contains(pontoOriginal)){
            pontosLista[pontosLista.IndexOf(pontoOriginal)] = pontoNovo;
            SetListaDePontos(pontosLista);
        }
    }

    //Substitui a lista de pontos atual por uma vazia
    public void ApagaTodosOsPontos(){
        lRend.positionCount = 0;
    }

    //Retorna o último ponto da lista
    public Vector3 GetUltimoPonto(){
        List<Vector3> listaPontos = GetListaDePontos();
        return listaPontos[listaPontos.Count-1];
    }

}
