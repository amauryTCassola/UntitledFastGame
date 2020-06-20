ing System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Carregar : MonoBehaviour
{
    public GameObject telaLoading;
    public Slider slider;
   public void CarregaNivel(int indexCena)
    {
        StartCoroutine(CarregarAssincrono(indexCena));

    }

    IEnumerator CarregarAssincrono(int indexCena)
    {
        AsyncOperation operacao = SceneManager.LoadSceneAsync(indexCena);

        while(!operacao.isDone)
        {
            float progresso = Mathf.Clamp01(operacao.progress / .9f);

            telaLoading.SetActive(true);

            slider.value = progresso;

            yield return null;
        }
    }
}
