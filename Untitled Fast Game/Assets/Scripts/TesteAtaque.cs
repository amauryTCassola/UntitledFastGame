using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteAtaque : MonoBehaviour
{
    //cria um array pra colocar os inimigos na tela(acho q deviamos transformar em lista depois)
    public GameObject[] inimigos;
    //cria a lista onde os inimigos vão ser guardados
    List<IInimigo> listaInimigos;
    //o controlador do player, pra poder  chamar no update depois
    ControladorPlayer player;

    //aqui vamos colocar a posição do inimigo e do mouse
    Vector2 posicaoInimigo;
    Vector2 posicaoClique;

    // o bool é pq se eu fizesse um método bool eu não poderia colocar o "await"
    bool ataqueDeuCerto;

    //essa variável vai ser importante pra que o jogador não selecione o mesmo inimigo mais de uma vez
    int testaLista;
    async void Start()
    {
        //cria array com todos os inimigos na tela
        inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
        //cria o controlador
        player = new ControladorPlayer();
        //cria a lista de inimigos
        listaInimigos = new List<IInimigo>();

        // eu não acho q seria bom apagar teu código então só comentei
        /*
        listaInimigos.Add(GameObject.Find("jacare (?)").GetComponent<Jacare>());
        listaInimigos.Add(GameObject.Find("porco (?)").GetComponent<Porco>());
        listaInimigos.Add(GameObject.Find("porco 2 (?)").GetComponent<Porco>());
        listaInimigos.Add(GameObject.Find("jacare (?)").GetComponent<Jacare>());

        bool ataqueDeuCerto = await gameObject.GetComponent<ProcessadorDeAtaque>().ExecutaAtaque(player, listaInimigos);
        if(ataqueDeuCerto) Debug.Log("ataque inteiro deu mto certo bah vsf");
        else Debug.Log("ataque inteiro n deu certo ;-;"); 
        */
    }

    //esse método vai ser chamado no SelecionaInimigos pra executar o ataque, ele faz tudo q fazia no start
    async void ExecutaAtaque()
    {
        ataqueDeuCerto = await gameObject.GetComponent<ProcessadorDeAtaque>().ExecutaAtaque(player, listaInimigos);
        if (ataqueDeuCerto) Debug.Log("ataque inteiro deu mto certo bah vsf");
        else Debug.Log("ataque inteiro n deu certo ;-;");
    }

    //aqui é o método importante q vai ser chamado no update, é aqui q a mágica acontece
    void SelecionaInimigos()
    {

        //checa se o mouse está sendo pressionado
        if (Input.GetMouseButton(0))
        {
            /*como o mouse ta sendo pressionado, ele faz um loop pra checar a posição 
              dele relativa a todos os inimigos, se algum deles estiver perto suficiente
              do mouse, ele vai checar se ele já não foi escolhido*/
            for (int i = 0; i < inimigos.Length; i++)
            {
                //aqui pega a posição do inimigo, q foi declarada la em cima, lembra?
                posicaoInimigo = inimigos[i].transform.position;
                //faz a mesma coisa só que com o clique
                posicaoClique = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                /*  aqui é onde a matemágica acontece, ele vai pegar a posição do x e do y
                   e calcular a distancia entre cada um deles. Mas pra isso os dois precisam ser
                   positivos, por isso o Mathf.Abs. Ai eu multiplico por 100 pq a distancia é
                   muito pequena e fica tipo 0,2333. Depois eu arredondo com Mathf.Round pra ficar
                   bonitinho, e mais uma vez deixamos positivo só pra ser mais facil de usar no if.
                 
                 */
                var xis = Mathf.Abs(Mathf.Round((Mathf.Abs(posicaoInimigo.x) - Mathf.Abs(posicaoClique.x)) * 100));
                var ipi = Mathf.Abs(Mathf.Round((Mathf.Abs(posicaoInimigo.y) - Mathf.Abs(posicaoClique.y)) * 100));

                //aqui ele vai pegar os floats x e y e ver se eles são menores q 5(é meio arbitrário a distancia)
                 if (xis <= 2 && ipi <= 2)
                 {
                    //aqui checa se não existe nenhum inimigo na lista ainda
                    if (listaInimigos.Count == 0)
                    {
                        //adiciona qualquer script da interface q tiver no GameObjcet
                        listaInimigos.Add(inimigos[i].GetComponent<IInimigo>());
                        //aqui é só pra confirmar q foi adicionado
                        Debug.Log(listaInimigos.Count);

                        /* aqui é o crux do código, ele vai setar o testaLista pra 0, o 
                          q significa q no próximo loop o i = 1 e testaLista = 0. Isso 
                          é importante pra checar se o inimigo q tá pra ser adicionado 
                          já não acabou de ser adicionado
                         */
                        testaLista = 0;

                    } 
                    
                    else 
                    {
                        /*aqui ele vai checar se o inimigo q vai ser adicionado não 
                           é mesmo. O testaLista vai ser sempre 1 menor q o i, fazendo
                           com que ele sempre cheque o atual com o anterior.
                         */
                        if (listaInimigos[testaLista] != inimigos[i].GetComponent<IInimigo>())
                        {
                            listaInimigos.Add(inimigos[i].GetComponent<IInimigo>());
                            Debug.Log(listaInimigos.Count);

                            //aqui incrimenta o testaLista toda vez q um inimigo novo for adicionado
                            testaLista ++;
                        }
                        else
                        {
                            /* isso aqui é interessante, não entendi pq mas ele
                             precisa desse break pra n ficar adicionando o mesmo infinitamente*/
                            break;
                        }

                    }

                 }

            }

        }
        //quando o jogador solta o mouse ele processa tudo
        else if(Input.GetMouseButtonUp(0))
        {
            ExecutaAtaque();
        }

    }
    void Update()
    {
        //chama a parte importante(n sei pq fiz separado oh well)
        SelecionaInimigos();
    }
}
