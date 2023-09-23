using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class ResultManager : MonoBehaviour
{
    [Header("フェードイン/アウト用")]
    [SerializeField] Fade fade;
    [Header("スコア類")]
    [SerializeField] int Score;
    [SerializeField] string Difficult;
    [SerializeField] int Just;
    [SerializeField] int Great;
    [SerializeField] int Good;
    [SerializeField] int Bad;
    [SerializeField] int Combo;
    [Header("ベストスコア/コンボ")]
    [SerializeField] int BestScore;
    [SerializeField] int BestCombo;
    [Header("曲情報")]
    [SerializeField] string Name;//曲名
    [SerializeField] int AllCombo;//曲のコンボ数
    [Header("各Image/Text")]
    [SerializeField] Image FullcomboAllJust;//　fullcombo/AllJustどちらかを満たしていれば画像が表示される
    
    [SerializeField] Text Justtxt;
    [SerializeField] Text Greattxt;
    [SerializeField] Text Goodtxt;
    [SerializeField] Text Badtxt;
    [SerializeField] Text MaxCombo;
    [SerializeField] Text Scoretxt;
    [SerializeField] Text BestScoretxt;
    [SerializeField] Text Titletxt;
    [SerializeField] Text Artisttxt;
    [SerializeField] Text ScoreArtisttxt;
    [SerializeField] RawImage Jacket;
    UnityWebRequest unityWebRequest;
    // Start is called before the first frame update
    void Start()
    {
        
        GetScore();
        WriteScore();
        
        StartCoroutine(GetJacket());
    }
    IEnumerator GetJacket()
    {
        if (System.IO.File.Exists(Application.streamingAssetsPath +"/"+ Name +"/"+ Name + ".png"))
        {
            using (unityWebRequest = UnityWebRequestTexture.GetTexture("file://" + Application.streamingAssetsPath + "/" + Name + "/" + Name + ".png"))
            {
                yield return unityWebRequest.SendWebRequest();
                Jacket.texture = ((DownloadHandlerTexture)unityWebRequest.downloadHandler).texture;
                fade.FadeIn(0.5f);
            }
        }
    }
    void GetScore()
    {
        /*--各情報をPlayPrefsで取得する--*/
        Just = PlayerPrefs.GetInt("Just", 0);
        Great = PlayerPrefs.GetInt("Great", 0);
        Good = PlayerPrefs.GetInt("Good", 0);
        Bad = PlayerPrefs.GetInt("Bad", 0);
        Score = PlayerPrefs.GetInt("Score", 0);
        Difficult = PlayerPrefs.GetString("Difficulty", null);
        Combo = PlayerPrefs.GetInt("Combo", 0);
        Name = PlayerPrefs.GetString("Name", "???");
        Titletxt.text = Name;
        AllCombo = PlayerPrefs.GetInt("AllCombo", 0);
        Artisttxt.text = PlayerPrefs.GetString("ArtistName", "???");
        ScoreArtisttxt.text = "ScoreArtist:" + PlayerPrefs.GetString("ScoreArtistName", "???");
        BestScore = PlayerPrefs.GetInt(Name + "_" + Difficult + "_BestScore", 0);
        BestCombo = PlayerPrefs.GetInt(Name + "_" + Difficult + "_BestCombo", 0);
        /*--ベストスコア/コンボを更新する--*/
        if (BestScore < Score)
        {
            BestScoretxt.text = Score.ToString("N0");
            PlayerPrefs.SetInt(Name + "_" + Difficult + "_BestScore", Score);
            PlayerPrefs.Save();
        }
        else
        {
            BestScoretxt.text = BestScore.ToString("N0");
            PlayerPrefs.SetInt(Name + "_" + Difficult + "_BestScore", BestScore);
            PlayerPrefs.Save();
        }
        if (BestCombo < Combo)
        {
            PlayerPrefs.SetInt(Name + "_" + Difficult + "_BestCombo", Combo);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt(Name + "_" + Difficult + "_BestCombo", BestCombo);
            PlayerPrefs.Save();
        }
        if (Combo == AllCombo)
        {
            if (Bad == 0 && Good == 0 && Great == 0)
            {
                Debug.Log("AllJust!!");
                FullcomboAllJust.sprite = Resources.Load<Sprite>("MusicSelect-AllParfect");
                PlayerPrefs.SetString(Difficult + "AllJust", "true");
                PlayerPrefs.Save();
            }
            else if (Bad == 0 && Good == 0)
            {
                Debug.Log("FullCombo");
                FullcomboAllJust.sprite = Resources.Load<Sprite>("MusicSelect-FullCombo");
                PlayerPrefs.SetString(Difficult + "FullCombo", "true");
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString(Difficult + "AllJust", "false");
            PlayerPrefs.SetString(Difficult + "FullCombo", "false");
            PlayerPrefs.Save();
        }
    }
    void WriteScore()
    {
        MaxCombo.text = "MaxCombo    " + Combo.ToString();
        Justtxt.text = "JUST    " + Just.ToString();
        Greattxt.text = "GREAT   " + Great.ToString();
        Goodtxt.text = "GOOD    " + Good.ToString();
        Badtxt.text = "BAD     " + Bad.ToString();
        Scoretxt.text = Score.ToString("N0");
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))//エンターを押すと次のシーンへ進むためフェードアウトする
        {
            fade.FadeOut(0.5f);
            Invoke("NextScene", 0.5f);
           
        }
       
    }
   
    public void OnClickNext()
    {
        fade.FadeOut(0.5f);
        Invoke("NextScene", 0.5f);
    }


    void NextScene()
    {
        SceneManager.LoadScene("SelectMusic");
    }
    // Update is called once per frame
}
