using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingNextScene : MonoBehaviour
{

    AsyncOperation async;

    [SerializeField] GameObject LoadCanvas;

    [SerializeField] Slider slider;
    // Start is called before the first frame update
   

    public void NextScene()
    {

        LoadCanvas.SetActive(true);


        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync("2Dmusic");


        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;

        }


       
    }
}
