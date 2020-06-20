using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltaIniciar : MonoBehaviour
{
    GameManager GM;
    private void Awake()
    {
        GM = GameManager.instance;
    }

    public void VoltaProMenu()
    {
        GM.Salvar();
        GM.CarregaCena(GM.RetornaSave(), 1);
    }
}
