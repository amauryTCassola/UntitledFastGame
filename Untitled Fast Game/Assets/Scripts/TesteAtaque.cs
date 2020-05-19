using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteAtaque : MonoBehaviour
{

    // Start is called before the first frame update
    async void Start()
    {
        ControladorPlayer player = new ControladorPlayer();
        List<IInimigo> listaInimigos = new List<IInimigo>();

        listaInimigos.Add(GameObject.Find("jacare (?)").GetComponent<Jacare>());
        listaInimigos.Add(GameObject.Find("porco (?)").GetComponent<Porco>());
        listaInimigos.Add(GameObject.Find("jacare (?)").GetComponent<Jacare>());

        bool ataqueDeuCerto = await gameObject.GetComponent<ProcessadorDeAtaque>().ExecutaAtaque(player, listaInimigos);
        if(ataqueDeuCerto) Debug.Log("ataque inteiro deu mto certo bah vsf");
        else Debug.Log("ataque inteiro n deu certo ;-;");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
