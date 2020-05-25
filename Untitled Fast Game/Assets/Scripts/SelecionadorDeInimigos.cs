using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecionadorDeInimigos : MonoBehaviour
{

    ControladorLinha lineController;
    List<IInimigo> inimigosSelecionados;

    ControladorPlayer player; //TO-DO: pegar o player de algum lugar sei la

    public InimigosManager inimigosManager; //tem que botar no inspector, TO-DO: mudar isso

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorPlayer>();//PÉSSIMO HORRÍVEL TEM QUE SER MUDADO MTO RUIM NO-GOOD VERY BAD
        lineController = new ControladorLinha();
        inimigosSelecionados = new List<IInimigo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            
            lineController.ApagaTodosOsPontos();
            lineController.AdicionaPonto(player.GetPlayerPosition());
            lineController.AdicionaPonto(GetMouseWorldPosition());

        } else if(Input.GetMouseButton(0)){
            
            lineController.SubstituiPonto(lineController.GetUltimoPonto(), GetMouseWorldPosition());
            SelecionaInimigo();
        
        } else if(Input.GetMouseButtonUp(0)){
            
            lineController.ApagaTodosOsPontos();
            Debug.Log("INIMIGOS SELECIONADOS!");
            inimigosSelecionados.Clear();
        
        }
    }

    Vector3 GetMouseWorldPosition(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        return worldPosition;
    }

    void SelecionaInimigo(){
        GameObject inimigoProximo = inimigosManager.SelecionaInimigoPorPosicao(GetMouseWorldPosition());
        if(inimigoProximo != null){
            inimigosSelecionados.Add(inimigoProximo.GetComponent<IInimigo>());
            lineController.SubstituiPonto(lineController.GetUltimoPonto(), inimigoProximo.transform.position);
            lineController.AdicionaPonto(GetMouseWorldPosition());
        }
    }
}
