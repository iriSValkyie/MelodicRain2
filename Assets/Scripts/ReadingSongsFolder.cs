using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ReadingSongsFolder : MonoBehaviour
{
   [Header("Path関係")]
    [SerializeField] List<string> SongsFileNamePath = new List<string>();//各フォルダのパス
    public string[] SongsFilePath;//UnityWebRequestで取得するために必要なパス (拡張子含まない)
    public string[] SongFolderNames = new string[0];//譜面データが保存されているフォルダ名
    [Header("Debug用")]
    [SerializeField] AudioClip testaud;
    [SerializeField] AudioSource audio;
    
    UnityWebRequest unityWebRequest;
 
    [Header("Jacket")]
    public Texture2D[] Jackets;
    public Fumen[] inputjson = new Fumen[0];
    string DefaultJacketPath;
    [Header("曲データ")]
    public AudioClip[] Audios;
    [Header("情報")]
    public string[] Artists;
    public string[] ScoreArtists;
    
    [Header("スクリプト")]
    [SerializeField] GameObject SelectManager;
    [SerializeField] GameObject LaneToggle;
    // Start is called before the first frame update
    [Header("UI関係")]
        
        public string UIPath;
        public Texture2D[] RaneCovorIcon = new Texture2D[5]; 
    IEnumerator Start()
    {
        SelectManager.SetActive(false);
        LaneToggle.SetActive(false);
        DefaultJacketPath = Application.streamingAssetsPath + "/" + "UI" + "/" + "DefaultJacket" + "/" + "Default" + ".png";
        ReadSongsFolder(Application.streamingAssetsPath);
        Debug.Log("デフォルトパスの設定が完了");
                            /*-----各楽曲のデータを取得-----*/
        for (int i = 0; i < SongsFilePath.Length; i++)
        {                   /*ジャケット画像の取得*/
            if (System.IO.File.Exists(SongsFilePath[i].Replace("file://", "") + ".png"))
            {
                Debug.Log("ジャケットが存在します" + SongsFilePath[i].Replace("file://", "") + ".png");
                using (unityWebRequest = UnityWebRequestTexture.GetTexture(SongsFilePath[i] + ".png"))
                {
                    yield return unityWebRequest.SendWebRequest();
                    Jackets[i] = ((DownloadHandlerTexture)unityWebRequest.downloadHandler).texture;
                    Debug.Log("ジャケットを追加");
                }
            }
            else
            {
                Debug.Log("ジャケットが存在しません" + SongsFilePath[i].Replace("file://", "") + ".png");
                if (System.IO.File.Exists(DefaultJacketPath))
                {
                    using (unityWebRequest = UnityWebRequestTexture.GetTexture("file://" + DefaultJacketPath))
                    {
                        yield return unityWebRequest.SendWebRequest();
                        Jackets[i] = ((DownloadHandlerTexture)unityWebRequest.downloadHandler).texture;
                        Debug.Log("デフォルトジャケットを追加");
                    }
                }
                else
                {
                    Debug.LogError("初期ジャケットが存在しません" + "ディレクトリを確認してください");
                }
            }
                                    /*楽曲譜面の取得*/
            if (System.IO.File.Exists(SongsFilePath[i].Replace("file://", "") + "_expart.json"))
            {
                Debug.Log("譜面データが存在します");
                using (unityWebRequest = UnityWebRequest.Get(SongsFilePath[i] + "_expart.json"))
                {
                    yield return unityWebRequest.SendWebRequest();
                    Debug.Log(unityWebRequest.downloadHandler.text);
                    string json = System.Text.Encoding.UTF8.GetString(unityWebRequest.downloadHandler.data, 3, unityWebRequest.downloadHandler.data.Length - 3);
                    inputjson[i] = JsonUtility.FromJson<Fumen>(json);
                    Debug.Log("譜面を追加" + SongsFilePath[i] + "_expart.json");
                }
            }
            else
            {
                Debug.Log("譜面データが存在しませんディレクトリを確認してください" + SongsFilePath[i] + "_expart.json");
            }
                                            /*楽曲の取得*/
            if (System.IO.File.Exists(SongsFilePath[i].Replace("file://", "") + ".wav"))
            {
                Debug.Log("音楽データが存在します");
                using (unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(SongsFilePath[i] + ".wav",AudioType.WAV))
                {
                    yield return unityWebRequest.SendWebRequest();
                    Audios[i] = ((DownloadHandlerAudioClip)unityWebRequest.downloadHandler).audioClip;
                   
                    Debug.Log("音楽を追加" + SongsFilePath[i] + ".wav");
                }
            }
            else
            {
                Debug.Log("音楽データが存在しませんディレクトリを確認してください" + SongsFilePath[i] + ".wav");
            }
            /*楽曲譜面の情報を取得*/
            if (System.IO.File.Exists(SongsFilePath[i].Replace("file://", "") + ".ini"))
            {
                Debug.Log("情報データが存在します");
                INIParser ini = new INIParser();
                ini.Open(SongsFilePath[i].Replace("file://", "") + ".ini");
                Artists[i] = ini.ReadValue("Song", "Artist", "???");
                
                ScoreArtists[i] = ini.ReadValue("MusicScore", "Artist", "???");
                ini.Close();
            }
            else
            {
                Debug.Log("情報データが存在しません、アーティスト名と譜面制作者を「???」にします");
                Artists[i] = "???";
                ScoreArtists[i] = "???";
            }
        }
        /*-----UI関係を取得-----*/
        Debug.Log(UIPath + "/"+"LaneCover" +"/"+ "LaneCover" + "_icon.png");
        for (int a = 1;a < 5; a++)
        {           /*レーンカバーの取得*/
            if (System.IO.File.Exists(UIPath +"/"+ "LaneCover" +"/"+ "LaneCover" + a + "_icon.png"))
            {
                
                using (unityWebRequest = UnityWebRequestTexture.GetTexture("file://" + UIPath +"/"+ "LaneCover" + "/" + "LaneCover" + a +"_icon.png"))
                {
                    yield return unityWebRequest.SendWebRequest();
                    RaneCovorIcon[a] = ((DownloadHandlerTexture)unityWebRequest.downloadHandler).texture;
                    Debug.Log("レーンカバーを追加" + a  );
                }
            }
            else
            {
                Debug.Log("自作レーンカバー" +a+ "は検出されませんでした");
            }
        } 
        SelectManager.SetActive(true);
        LaneToggle.SetActive(true);
    }
        void ReadSongsFolder(string path)
        {
            SongsFileNamePath.AddRange(System.IO.Directory.GetDirectories(path, "*", System.IO.SearchOption.AllDirectories));
            SongsFileNamePath.RemoveAll(s => s.Contains("UI"));
            
            System.Array.Resize(ref inputjson, SongsFileNamePath.Count);
            System.Array.Resize(ref Artists, SongsFileNamePath.Count);
            System.Array.Resize(ref ScoreArtists, SongsFileNamePath.Count);
            System.Array.Resize(ref Audios, SongsFileNamePath.Count);    
            System.Array.Resize(ref SongsFilePath, SongsFileNamePath.Count);
            System.Array.Resize(ref Jackets, SongsFileNamePath.Count);
            System.Array.Resize(ref SongFolderNames, SongsFileNamePath.Count);
            UIPath = path + "/" + "UI";
            for (int i = 0; i < SongsFileNamePath.Count; i++)
            {
                SongFolderNames[i] = System.IO.Path.GetFileName(SongsFileNamePath[i]);
                SongsFilePath[i] = "file://" + Application.streamingAssetsPath + "/" + SongFolderNames[i] + "/" + SongFolderNames[i];
            }
            Debug.Log("フォルダ読み込み完了");
        }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
