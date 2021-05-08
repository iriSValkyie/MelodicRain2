using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


/// <summary>
/// 
/// 選択した曲のデータロード
/// 
/// 
/// </summary>
public class LoadingSelectData : MonoBehaviour
{
    [Header("フェードイン")]

    [SerializeField] float fadeinTime; //フェード時間

    [SerializeField] Image backgroundimage;

    [SerializeField] GameObject Panel;//フェード用パネル




    [Header("Readyアニメーション")]

    [SerializeField] Animator animator;


    [Header("音楽")]

    public AudioClip music;


    [Header("ファイルパス")]

    [SerializeField] string MusicFilePath;//音楽フォルダのパス
    [SerializeField] string LaneCovorFilePath;//レーンカバーのファイルパス


    [Header("曲名")]

    [SerializeField] string Name;


    [Header("作曲者/譜面制作者")]

    [SerializeField] string ArtistName;

    [SerializeField] string ScoreArtistName;


    [Header("譜面テキスト")]

    public string JsonText;




    string Difficult;//難易度

    
    [Header("背景明るさ")]

    public  float Brightness;




    UnityWebRequest unityWebRequest;//ローカルファイルを取得するWebRequest

    [Header("外部スクリプト")]


    [SerializeField] GameObject GameManager;

    [SerializeField] GameController gameController;

    [SerializeField] Player player;

    [SerializeField] Fade fade;

    [Header("各コンポーネント")]


    [SerializeField] Image BrightnessImage;//背景明るさを調整するImage

    [SerializeField] RawImage Jacket;//ジャケット
    [SerializeField] RawImage JacketBackGrond;//背景ジャケット

    [SerializeField] RawImage LaneCover;//レーンカバー

    [SerializeField] Text TitleTxt;//曲名

    [SerializeField] Text ArtistNameTxt;//作曲者名

    [SerializeField] Text ScoreArtistNameTxt;//譜面制作者名
    // Start is called before the first frame update
    IEnumerator Start()
    {
        fadeinTime = 1f * fadeinTime / 10f;


        /*--選択した曲の情報とオプションを取得する--*/


        /*パスの取得*/
        MusicFilePath = PlayerPrefs.GetString("SongFilePath", null);
        Difficult = PlayerPrefs.GetString("Difficulty", null);
        /*オプションの取得*/
        LaneCovorFilePath =  PlayerPrefs.GetString("LaneCoverPath", null);
        Brightness = PlayerPrefs.GetFloat("Brightness", 0);


        /*曲情報の取得*/
        Name = PlayerPrefs.GetString("Name", null);
        ArtistName = PlayerPrefs.GetString("ArtistName", null);
        ScoreArtistName = PlayerPrefs.GetString("ScoreArtistName", null);


        /*代入*/
        BrightnessImage.color = new Color(0, 0, 0, Brightness);

        TitleTxt.text = Name;

        ArtistNameTxt.text = "Music:"+ ArtistName;

        ScoreArtistNameTxt.text = "Score:"+ ScoreArtistName;

        Debug.Log(MusicFilePath);


        /*音楽の取得*/
        if (System.IO.File.Exists(MusicFilePath.Replace("file://", "") + ".wav"))//ファイルが存在しているかの確認
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

            JacketBackGrond.texture = Resources.Load<Texture2D>("Default");//初期ジャケットを取得
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

        GameManager.SetActive(true);//譜面を読み込んだらGameManagerをオンにする

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



        Debug.Log("スタートこるーちン終了");

        fade.FadeIn(0.5f);
        animator.SetTrigger("isReady");//readyのアニメーションを再生
                                       //  StartCoroutine("FadeIn");
    }

    /*IEnumerator FadeIn()
    {

                Debug.Log("フェードインコルーチン開始" + fadeinTime);

                for(float i = 1f; i >= 0; i -= 0.1f)
                {
                    backgroundimage.color = new Color(0f, 0f, 0f, i);

                    yield return new WaitForSeconds(fadeinTime);

                }

                yield return null;



                Panel.SetActive(false);

                animator.SetTrigger("isReady");//readyのアニメーションを再生



   }
      */


    public void OnStart()
    {


        gameController.OnClickStartButton();//演奏を開始する


    }
}
