using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltaIniciar : MonoBehaviour
{
    public Carregar carregaMenuIniciar;

    public void VoltaProMenu()
    {
        carregaMenuIniciar.CarregaNivel(0);
    }
}
