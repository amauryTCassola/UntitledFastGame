using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //a tela de loading
    public GameObject telaCarregar;
    // a progress bar
    public Slider progresso;

    //smp carrega o menu ao iniciar
    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    //lista das telas q vao ser carregadas pra mostrar na barra
    List<AsyncOperation> telasCarregando = new List<AsyncOperation>();

    /* 
        As telas serão carregadas, adicionadas a lista que estão sendo carregadas
        e a tela de loading vai aparecer. O método recebe a cena para ser desligada e 
        a cena para ser aberta
         */
    public void CarregaCena(int daonde, int destino) 
    {
        telasCarregando.Clear();
        telaCarregar.SetActive(true);
        telasCarregando.Add(SceneManager.UnloadSceneAsync(daonde));
        telasCarregando.Add(SceneManager.LoadSceneAsync(destino, LoadSceneMode.Additive));

        StartCoroutine(PegaProgressoTelasCarregando());

    }

    // o progresso da barra de progresso 
    float quantoProgresso;
    
    /*
     * corrotina q recebe a lista das telas a serem carregadas e
     * atualiza a barra de progresso, alem de fechar a tela de loading
     */
    public IEnumerator PegaProgressoTelasCarregando()
    {
        for (int i = 0; i < telasCarregando.Count; i++)
        {
            while (!telasCarregando[i].isDone)
            {
                quantoProgresso = 0;

                foreach(AsyncOperation processo in telasCarregando)
                {
                    quantoProgresso += processo.progress;
                }

                quantoProgresso = (quantoProgresso / telasCarregando.Count) * 100f;

                progresso.value = Mathf.Round(quantoProgresso);

                yield return null;
            }
        }

        telaCarregar.SetActive(false);
    }

    int qualNivel;
    public void Salvar()
    {
        qualNivel = SceneManager.GetSceneAt(1).buildIndex;
        PlayerPrefs.SetInt("nivel", qualNivel);
    }

    public int RetornaSave()
    {
        return PlayerPrefs.GetInt("nivel");
    }

    public int RetornaCena()
    {
        return SceneManager.GetSceneAt(1).buildIndex;
    }
}
