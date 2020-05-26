using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSelecao : MonoBehaviour
{
    
    List<GameObject> inimigosNaTela;//cria um array pra colocar os inimigos na tela(acho q deviamos transformar em lista depois)
    List<IInimigo> listaSelecao; //cria a lista onde os inimigos vão ser guardados
    ControladorPlayer player;//o controlador do player, pra poder  chamar no update depois
    ControladorLinha linha; //controlador da linha pra chamar no update depois

    //aqui vamos colocar a posição do inimigo, e do mouse
    Vector2 posicaoInimigo;
    Vector2 posicaoClique;

    bool ataqueDeuCerto;// o bool é pq se eu fizesse um método bool eu não poderia colocar o "await"
    int posLista; //importante pra selecionar qual indice da lista e do array

    void Start()
    {
        /* em ordem: array dinâmico para inimigos, lista da seleção do player,
           controlador do player pro ProcessadorDeAtaque, controlador da linha */
        inimigosNaTela = new List<GameObject>(GameObject.FindGameObjectsWithTag("Inimigo")); ;
        listaSelecao = new List<IInimigo>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorPlayer>();
        linha = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorLinha>();

    }

    void ChecaEAdd()
    {
        for ( int i = 0;  i < inimigosNaTela.Count; i++)
        {
            //pegando posiçao do inimigo e do mouse
            posicaoInimigo = inimigosNaTela[i].transform.position;
            posicaoClique = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // checa o mouse ta perto suficiente e manda adicionar pra lista
            if (Vector2.Distance(posicaoClique, posicaoInimigo) <= 0.6)
            {
                    AddLista(i);
            }
        }
        
    }


    void AddLista(int i)
    {
         if (listaSelecao.Count == 0) //se a lista for nula adiciona sem nem pensar
         {
                listaSelecao.Add(inimigosNaTela[i].GetComponent<IInimigo>());

                posLista = 0; //seta o posLista pra zero
                Debug.Log(listaSelecao.Count);//isso é pra teste

                //linha.CriaGeradorDeLinha(posicaoInimigo, listaSelecao.Count);
         }
         /*aqui usamos o posLista, q vai sempre ser a posição anterior da lista 
           e vemos se ela é igual ao inimigo q quer ser adicionado*/
         else if (listaSelecao[posLista] != inimigosNaTela[i].GetComponent<IInimigo>())
         {
               listaSelecao.Add(inimigosNaTela[i].GetComponent<IInimigo>());

               posLista += 1; // incrementando a posicao
               Debug.Log(listaSelecao.Count);//pra teste

               //linha.CriaGeradorDeLinha(posicaoInimigo, listaSelecao.Count);
         }
        
        
    }


    //método só pra n poluir o update, termina a jogada
    async void FinalizaJogada()
    {
        ataqueDeuCerto = await gameObject.GetComponent<ProcessadorDeAtaque>().ExecutaAtaque(player, listaSelecao);
         if (ataqueDeuCerto) Debug.Log("ataque inteiro deu mto certo bah vsf");
         else
        {
            Debug.Log("ataque inteiro n deu certo ;-;");
            Destroy(player.gameObject);
        }
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ChecaEAdd();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (listaSelecao.Count >= inimigosNaTela.Count)
                FinalizaJogada();
            else
            {
                Debug.Log("Voce nao selecionou todos os inimigos!");
                listaSelecao.Clear();
            }
        }
    }
}