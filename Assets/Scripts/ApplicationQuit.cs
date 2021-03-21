using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuit : MonoBehaviour
{

    [Header("ゲーム終了画面")]
    [SerializeField] GameObject Canvas;
    // Start is called before the first frame update
    void Awake()
    {
        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Canvas.SetActive(true);


        }
    }

    public void OnQuit()
    {//ゲームを終了する
        Application.Quit();
    }



    public void Back()
    {

        //ゲーム選択画面に戻る
        Canvas.SetActive(false);

    }
}
