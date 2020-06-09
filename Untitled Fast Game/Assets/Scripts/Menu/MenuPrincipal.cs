using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    Animator animator;
    SaveManager saveManager;
    public Button botaoContinuar;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        //PlayerPrefs.DeleteKey("nivel");

        if (!PlayerPrefs.HasKey("nivel"))
            botaoContinuar.interactable = false;
        else
            botaoContinuar.interactable = true;
    }
    public void JogarJogo()
    {
        animator.SetTrigger("MenuSai");
        StartCoroutine(EntraJogo());
    }

    public void SairJogo()
    {
        Debug.Log("SAIU!");
        Application.Quit();
    }

    public void ContinuaJogo()
    {
        SceneManager.LoadScene(saveManager.RetornaSave());
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

    IEnumerator EntraJogo()
    {

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
