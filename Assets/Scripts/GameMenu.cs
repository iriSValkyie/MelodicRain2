using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour
{

    [Header("メニュー")]
    [SerializeField] GameObject MenuCanvas;//リスタートや曲選択画面に戻ることができる
    [SerializeField] GameController gameController;

    bool isOpen;//メニューを開いているか


    public bool MusicStarted;//音楽が再生されて演奏が始まっているか

   
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    private void Start()
    {
        isOpen =false;
        MusicStarted =false;
    }
    // Update is called once per frame
    void Update()
    {

        if (MusicStarted)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                isOpen = !isOpen;

            }

            switch (isOpen)
            {

                case true:

                    OpenMenu();

                    break;


                case false:

                    CloseMenu();

                    break;


            }

        }

    }

    void OpenMenu()
    {

     
           
            isOpen = true;
            MenuCanvas.SetActive(true);
            gameController.music.Pause();
            

           

            Time.timeScale = 0;

     

    }

    void CloseMenu()
    {
        
            gameController.music.UnPause();
            MenuCanvas.SetActive(false);
            Time.timeScale = 1;
            
      
    }

    

    public void OnRestart()//メニューからリスタートする
    {
        //  ReloadSong.isReload = true;

        isOpen = false;
        Time.timeScale = 1;
       
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        



    }


    public void OnBack()//メニューから曲選択に戻る
    {

        isOpen = false; 
        Time.timeScale = 1;
       
        SceneManager.LoadScene("SelectMusic");
        

    }


    public void OnResume()//演奏に戻る
    {
        isOpen = false; 
        gameController.music.UnPause();
        MenuCanvas.SetActive(false);
        Time.timeScale = 1;


    }
}
