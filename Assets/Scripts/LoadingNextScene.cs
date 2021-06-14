using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// シーンをロードするスクリプト
/// </summary>
public class LoadingNextScene : MonoBehaviour
{
    AsyncOperation async;//非同期動作
    [SerializeField] GameObject LoadCanvas;//背景画面
    [SerializeField] Slider slider;//ロードゲージ
    // Start is called before the first frame update
   
     public void NextScene()
    {
        LoadCanvas.SetActive(true);
        StartCoroutine("LoadScene");
    }
    IEnumerator LoadScene()//ロードする
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
