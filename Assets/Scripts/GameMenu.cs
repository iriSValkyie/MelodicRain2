using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour
{

    [Header("メニュー")]
    [SerializeField] GameObject MenuCanvas;//リスタートや曲選択画面に戻ることができる
    [SerializeField] GameController gameController;

    bool isOpen;
    // Start is called before the first frame update
    void Awake()
    {
        MenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OpenMenu();
        CloseMenu();
    }


    void OpenMenu()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            isOpen = true;
            gameController.music.Pause();
            MenuCanvas.SetActive(true);

            Debug.Log("粋");

            Time.timeScale = 0;

        }

    }

    void CloseMenu()
    {
        if (isOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (MenuCanvas.activeSelf == true)
                {
                    gameController.music.UnPause();
                    MenuCanvas.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }


    }

    public void OnRestart()//メニューからリスタートする
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);




    }


    public void OnBack()//メニューから曲選択に戻る
    {

        SceneManager.LoadScene("SelectScene");


    }


    public void OnResume()//演奏に戻る
    {

        MenuCanvas.SetActive(false);
        Time.timeScale = 1;


    }
}
