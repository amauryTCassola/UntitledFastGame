using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecionadorDeInimigos : MonoBehaviour
{

    ControladorLinha lineController;
    List<IInimigo> inimigosSelecionados;

    ControladorPlayer player; //TO-DO: pegar o player de algum lugar sei la

    public InimigosManager inimigosManager; //tem que botar no inspector, TO-DO: mudar isso, se pá fazer como eu fiz no ControladorLinha

    int inimigoAnterior;

    public AtaqueManager ataqueManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorPlayer>();//PÉSSIMO HORRÍVEL TEM QUE SER MUDADO MTO RUIM NO-GOOD VERY BAD
        lineController = new ControladorLinha();
        inimigosSelecionados = new List<IInimigo>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            
            LimpaSelecao(); //redundante, mas só por precaução
            lineController.AdicionaPonto(player.GetPlayerPosition());
            lineController.AdicionaPonto(GetMouseWorldPosition());

        } else if(Input.GetMouseButton(0)){
            
            lineController.SubstituiPonto(lineController.GetUltimoPonto(), GetMouseWorldPosition());
            SelecionaInimigo();
        
        } else if(Input.GetMouseButtonUp(0)){
            
            if(TodosInimigosSelecionados()){
                StartCoroutine(ataqueManager.FazendoAtaque(player, inimigosSelecionados));
            } else {
                Debug.Log("Nem todos os inimigos foram selecionados, seu bobo >:^(");
            }
            LimpaSelecao();
        
        }
    }

    void LimpaSelecao(){
        inimigosSelecionados.Clear();
        lineController.ApagaTodosOsPontos();
    }

    bool TodosInimigosSelecionados(){
        foreach(IInimigo inimigo in inimigosManager.InimigosNaTela()){
            if(!inimigosSelecionados.Contains(inimigo)){
                return false;
            }
        }
        return true;
    }

    Vector3 GetMouseWorldPosition(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPosition;
    }

    void AdicionaInimigo(GameObject inimigoProximo)
    {
        inimigosSelecionados.Add(inimigoProximo.GetComponent<IInimigo>());
        lineController.SubstituiPonto(lineController.GetUltimoPonto(), inimigoProximo.transform.position);
        lineController.AdicionaPonto(GetMouseWorldPosition());
    }

    void SelecionaInimigo(){
        GameObject inimigoProximo = inimigosManager.SelecionaInimigoPorPosicao(GetMouseWorldPosition());
        if(inimigoProximo != null){
            if(inimigosSelecionados.Count == 0) 
            {
                AdicionaInimigo(inimigoProximo);
                inimigoAnterior = 0;
                Debug.Log(inimigosSelecionados.Count);
            }
            if (inimigosSelecionados[inimigoAnterior] != inimigoProximo.GetComponent<IInimigo>())
            {
                AdicionaInimigo(inimigoProximo);
                inimigoAnterior ++;
                Debug.Log(inimigosSelecionados.Count);

            }
        } 
    }

    
}
