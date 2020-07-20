using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    GameObject player;
    public List<Object> inimigosPref;
    Vector2 posicaoPlayer;
    List<GameObject> inimigos;
    List<Vector2> posicoes;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inimigosPref = new List<Object>(Resources.LoadAll("prefabs/inimigos"));
        inimigos = new List<GameObject>(GameObject.FindGameObjectsWithTag("Inimigo"));
        posicoes = new List<Vector2>();
    }

    void Respawnar()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
