using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    Animator animator;
    public Button botaoContinuar;
    GameManager GM;

    private void Awake()
    {
        GM = GameManager.instance;
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        //PlayerPrefs.DeleteKey("nivel");

        //seta o botão continuar pra inativo caso o jogador nunca tenha salvado
        if (!PlayerPrefs.HasKey("nivel"))
            botaoContinuar.interactable = false;
        else
            botaoContinuar.interactable = true;
    }

    //todas essas fazem a animacao do menu saindo e chamam a corrotina 
    //vai passar o nivel pro game manager
    public void JogarJogo()
    {
        animator.SetTrigger("MenuSai");
        StartCoroutine(EntraJogo(2));
    }

    public void ContinuaJogo()
    {
        animator.SetTrigger("MenuSai");
        StartCoroutine(EntraJogo(GM.RetornaSave()));
    }

    public void SairJogo()
    {
        Debug.Log("SAIU!");
        Application.Quit();
    }

   
    public void EntrarOpcoes()
    {
        animator.SetTrigger("MenuSai");
        StartCoroutine(SaiMenu());
        
    }

    IEnumerator SaiMenu()
    {
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        gameObject.SetActive(false);

    }

    IEnumerator EntraJogo(int nivel)
    {

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        GM.CarregaCena(1, nivel);

    }

}
