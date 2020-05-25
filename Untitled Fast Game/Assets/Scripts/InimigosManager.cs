using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigosManager : MonoBehaviour
{
    public List<GameObject> inimigos;

    // Start is called before the first frame update
    void Start()
    {
        inimigos = new List<GameObject>(GameObject.FindGameObjectsWithTag("Inimigo"));
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
