using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorLinha{
    LineRenderer lRend;
    GameObject rendererObject;
    List<Vector3> listaPontos;

    public ControladorLinha()
    {
        rendererObject = (GameObject)Object.Instantiate(Resources.Load("DesenhadorDeLinha"));
        lRend = rendererObject.GetComponent<LineRenderer>();
        listaPontos = new List<Vector3>();

    }

    //Retorna os pontos da lista de pontos do line renderer em forma de List<Vector3>
    List<Vector3> GetListaDePontos(){
        return listaPontos;
    }

    //Dada uma List<Vector3>, transforma a lista em array e a insere no lineRenderer
    void SetListaDePontos(List<Vector3> pontosLista){
        listaPontos = pontosLista;
        Vector3[] arrayDePontosBugFixed = LineRendererBugfix(listaPontos.ToArray());
        lRend.positionCount = arrayDePontosBugFixed.Length;
        lRend.SetPositions(arrayDePontosBugFixed);
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
        listaPontos.Clear();
    }

    //Retorna o último ponto da lista
    public Vector3 GetUltimoPonto(){
        List<Vector3> listaPontos = GetListaDePontos();
        return listaPontos[listaPontos.Count-1];
    }

    Vector3[] LineRendererBugfix(Vector3[] original)
     {
         Vector3[] res = new Vector3[original.Length * 3 - 2];
         for (int i = 0; i < res.Length; i++)
         {
             if (i % 3 == 0)
             {
                 res[i] = original[i / 3];
             }
             else if (i % 3 == 1)
             {
                 res[i] = Vector3.Lerp(original[(i - 1) / 3], original[(i + 2) / 3], 0.1f);
             }
             else if (i % 3 == 2)
             {
                 res[i] = Vector3.Lerp(original[(i + 1) / 3], original[(i - 2) / 3], 0.1f);
             }
         }
         return res;
     }

}
