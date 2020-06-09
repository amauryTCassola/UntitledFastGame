using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpcoes : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void EntrarPrincipal()
    {
        animator.SetTrigger("MenuOpcaoSai");
        StartCoroutine(SaiMenuOpcao());

    }

    IEnumerator SaiMenuOpcao()
    {

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        gameObject.SetActive(false);

    }
}
