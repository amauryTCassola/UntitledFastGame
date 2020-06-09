using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public int qualNivel;


    public void Salvar()
    {
        qualNivel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("nivel", qualNivel);
    }

    public int RetornaSave()
    {
       return PlayerPrefs.GetInt("nivel");
    }

}
