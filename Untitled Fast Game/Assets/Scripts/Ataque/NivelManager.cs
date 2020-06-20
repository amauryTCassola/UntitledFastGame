using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NivelManager : MonoBehaviour
{
    List<GameObject> telas;
    GameManager GM;
    public InimigosManager inimigosManager;

    private void Awake()
    {
        GM = GameManager.instance;
    }
    void Start()
    {
        telas = new List<GameObject>(GameObject.FindGameObjectsWithTag("CamadaInimigo"));
        telas = telas.OrderBy(go => go.name).ToList();
        DesligaTelas();
        inimigosManager.AddInimigos();
        Debug.Log(telas.Count);
    }

    void DesligaTelas()
    {
        for (int i = 0; i < telas.Count; i++)
        {
            if (i > 0)
                telas[i].SetActive(false);
        }
    }

    public void PassaProxima()
    {
        for (int i = 0; i < telas.Count; i++)
        {
            if (telas[i].activeInHierarchy == true
                && i < (telas.Count - 1))
            {
                Debug.Log(i);
                telas[i].SetActive(false);
                telas[i + 1].SetActive(true);
                inimigosManager.LimpaLista();
                break;
            }
            else if(i == (telas.Count - 1))
            {
                telas[i].SetActive(false);
                GM.CarregaCena(GM.RetornaCena(), (GM.RetornaCena() + 1));
                GM.Salvar();
            }
        }
        inimigosManager.AddInimigos();
    }
}
