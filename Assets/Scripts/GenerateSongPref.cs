using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GenerateSongPref : MonoBehaviour
{
    [Header("他スクリプト")]

    [SerializeField] GameObject SongMenuPref;

    [SerializeField] ReadingSongsFolder readingSongsFolder;

    [Header("曲のトグル")]

    public List<GameObject> Songs = new List<GameObject>();

    [Header("親オブジェクト")]


    [SerializeField] GameObject Parent;

    int middlenum;//曲一覧の中央の曲 


    const float Songsizedelta = 72;//曲一覧の間隔
    // Start is called before the first frame update
    void Start()
    {
        //読み込んだデータをトグルを生成しトグルにある曲情報を保管するスクリプトに代入する



        for (int i = 0; i < readingSongsFolder.SongFolderNames.Length; i++)
        {

            Songs.Add(Instantiate(SongMenuPref, new Vector3(0, 0, 0), Quaternion.identity, Parent.transform));

            Songs[i].name = readingSongsFolder.SongFolderNames[i];

            Toggle toggle = Songs[i].GetComponent<Toggle>();

            ToggleGroup toggleGroup = Parent.GetComponent<ToggleGroup>();

            toggle.group = toggleGroup;

            MusicDataBase musicDataBase = Songs[i].GetComponent<MusicDataBase>();

            musicDataBase.Path = readingSongsFolder.SongsFilePath[i];

            musicDataBase.Texture = readingSongsFolder.Jackets[i];

            musicDataBase.Bpm = readingSongsFolder.inputjson[i].BPM;

            Debug.Log("BPMを追加");
            musicDataBase.Name = readingSongsFolder.inputjson[i].name;

            musicDataBase.ArtistName = readingSongsFolder.Artists[i];

            musicDataBase.ScoreArtistName = readingSongsFolder.ScoreArtists[i];

            musicDataBase.Music = readingSongsFolder.Audios[i];

            Debug.Log("ジャケットを追加" + musicDataBase.Texture.name);
            MusicTitleToggle musicTitleToggle = Songs[i].GetComponent<MusicTitleToggle>();

            musicTitleToggle.Label.text = readingSongsFolder.SongFolderNames[i];

        }


        //トグルの初期選択を曲一覧の真ん中の曲に割り当てる

        RectTransform rectTransform = Parent.GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(710, Songsizedelta * Songs.Count);

        for (int a = 1; a <= Songs.Count; a++)
        {
            middlenum += a;

        }

        middlenum /= Songs.Count;

        middlenum -= 1;

        Debug.Log("中央の値は" + middlenum + "です");


        Toggle CurrentToggle = Songs[middlenum].GetComponent<Toggle>();


        CurrentToggle.isOn = true;
    }

}