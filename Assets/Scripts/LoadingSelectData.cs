using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class LoadingSelectData : MonoBehaviour
{


    [SerializeField] float fadeinTime;

    [SerializeField]Image backgroundimage;

    [SerializeField] GameObject Panel;

    [SerializeField] Animator animator;
   


    public AudioClip music;



    [SerializeField] string MusicFilePath;
    [SerializeField] string LaneCovorFilePath;

    [SerializeField] string Name;

    [SerializeField] string ArtistName;

    [SerializeField] string ScoreArtistName;

    public string JsonText;

    string Difficult;

    
    

    public  float Brightness;


    UnityWebRequest unityWebRequest;

    [Header("外部オブジェクト")]


    [SerializeField] GameObject GameManager;






    [SerializeField] Image BrightnessImage;

    [SerializeField] RawImage Jacket;
    [SerializeField] RawImage JacketBackGrond;

    [SerializeField] RawImage LaneCover;


    [SerializeField] GameController gameController;
   

    [SerializeField] Player player;


    [SerializeField] Text TitleTxt;

    [SerializeField] Text ArtistNameTxt;

    [SerializeField] Text ScoreArtistNameTxt;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        fadeinTime = 1f * fadeinTime / 10f;

        

        MusicFilePath = PlayerPrefs.GetString("SongFilePath", null);
        Difficult = PlayerPrefs.GetString("Difficulty", null);
        LaneCovorFilePath =  PlayerPrefs.GetString("LaneCoverPath", null);
       
        Brightness = PlayerPrefs.GetFloat("Brightness", 0);
        Name = PlayerPrefs.GetString("Name", null);
        ArtistName = PlayerPrefs.GetString("ArtistName", null);
        ScoreArtistName = PlayerPrefs.GetString("ScoreArtistName", null);


        BrightnessImage.color = new Color(0, 0, 0, Brightness);

      

        TitleTxt.text = Name;

        ArtistNameTxt.text = "Music:"+ ArtistName;

        ScoreArtistNameTxt.text = "Score:"+ ScoreArtistName;

        Debug.Log(MusicFilePath);


        /*音楽の取得*/
        if (System.IO.File.Exists(MusicFilePath.Replace("file://", "") + ".wav"))
        {
            using (unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(MusicFilePath + ".wav", AudioType.WAV))
            {
                yield return unityWebRequest.SendWebRequest();


                music = ((DownloadHandlerAudioClip)unityWebRequest.downloadHandler).audioClip;

            }

        }
        else
        {

            Debug.LogError("音楽ファイルのパスが間違っています、PLayPrefsのキーなどを確認してください");


        }
        /*ジャケットの取得*/
        if (System.IO.File.Exists(MusicFilePath.Replace("file://", "") + ".png"))
        {
            using (unityWebRequest = UnityWebRequestTexture.GetTexture(MusicFilePath + ".png"))
            {
                yield return unityWebRequest.SendWebRequest();


                Jacket.texture = ((DownloadHandlerTexture)unityWebRequest.downloadHandler).texture;

                JacketBackGrond.texture = Jacket.texture;
            }

        }
        else
        {
            Debug.LogError("ジャケットファイルのパスが間違っています、PLayPrefsのキーなどを確認してください");

            JacketBackGrond.texture = Resources.Load<Texture2D>("Default");
            Jacket.texture = Resources.Load<Texture2D>("Default");

        }
        /*譜面の取得*/
        if (System.IO.File.Exists(MusicFilePath.Replace("file://", "") + "_" + Difficult + ".json"))
        {
            using (unityWebRequest = UnityWebRequest.Get(MusicFilePath + "_" + Difficult + ".json"))
            {


                yield return unityWebRequest.SendWebRequest();

                JsonText = System.Text.Encoding.UTF8.GetString(unityWebRequest.downloadHandler.data, 3, unityWebRequest.downloadHandler.data.Length - 3);


            }
        }
        else
        {


            Debug.LogError("譜面ファイルのパスが間違っています、PLayPrefsのキーなどを確認してください");
        }

        GameManager.SetActive(true);

        /*レーンカバーの取得*/
        if (System.IO.File.Exists(LaneCovorFilePath.Replace("file://", "")))
        {
            using (unityWebRequest = UnityWebRequestTexture.GetTexture(LaneCovorFilePath))
            {
                yield return unityWebRequest.SendWebRequest();


                LaneCover.texture = ((DownloadHandlerTexture)unityWebRequest.downloadHandler).texture;


            }
        }
        else
        {


            Debug.LogError("レーンカバーファイルのパスが間違っています、PLayPrefsのキーなどを確認してください");
        }




            yield return null;



        

        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        for(var i = 1f; i >= 0; i -= 0.1f)
        {
            backgroundimage.color = new Color(0f, 0f, 0f, i);

            yield return new WaitForSeconds(fadeinTime);

        }


        Panel.SetActive(false);

        animator.SetTrigger("isReady");

        StopCoroutine("FadeIn");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStart()
    {


        gameController.OnClickStartButton();


    }
}
