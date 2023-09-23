﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// リザルトシーンへの遷移、スコア受け取り
/// </summary>
public class TransitionResultScene : MonoBehaviour
{
    [Header("他スクリプト")]
    [SerializeField] GameController gameController;
    [SerializeField] Judge judge;
    [Header("音楽")]
    [SerializeField] AudioSource audio;
    [Header("フェードアウト")]
    // [SerializeField] Image background;
    [SerializeField] Fade fade;
  //  [SerializeField] float fadeinTime;
    float speed;
    bool isFadeOut;
    public bool isPlaying;
     // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate()
    {
        if (audio.time == 0 && !audio.isPlaying && isPlaying)//演奏が終了するとリザルトシーンへの遷移のためにフェードアウトを実行
        {
            fade.FadeOut(0.5f);
            Finish();
        }
    }
    void Finish()
    {
       
        
        PlayerPrefs.SetInt("Just", judge.score.Just);
        PlayerPrefs.SetInt("Great", judge.score.Great);
        PlayerPrefs.SetInt("Good", judge.score.Good);
        PlayerPrefs.SetInt("Bad", judge.score.Bad);
        PlayerPrefs.SetInt("Score", judge.Score);
        PlayerPrefs.SetInt("Combo", judge.Maxcombo);
        PlayerPrefs.Save();
        SceneManager.LoadScene("ResultScene");
    
    }
    
    
}
