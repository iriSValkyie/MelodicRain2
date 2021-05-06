using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class SelectMusic : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("他スクリプト")]
    [SerializeField] GenerateSongPref generateSongPref;
    [SerializeField] ReadingSongsFolder readingSongsFolder;

    [SerializeField] LaneToggle laneToggle;

    [SerializeField] DifficultToggle difficultToggle;

    [SerializeField] SpeedSelect speedSelect;



    bool isUp;
    bool isDown;
    bool isLeft;
    bool isRight;

    [Header("曲トグル")]

    [SerializeField] ToggleGroup MusictoggleGroup;

    [Header("背景明るさ")]
    [SerializeField] Slider BrightnessSelecter;

    [Header("難易度トグル")]
    [SerializeField] Toggle[] DifficultyToggle = new Toggle[4];




    [SerializeField] Texture2D[] texture2D;

    [SerializeField] RawImage Jacket;

    public RawImage AnimationImage;

    public Text AnimationTitleText;


    [SerializeField] Animator nextSceneAnimation;

    Toggle oldtoggle;
    void Start()
    {
        texture2D = readingSongsFolder.Jackets;
    }


    private void OnEnable()
    {
        texture2D = readingSongsFolder.Jackets;
    }

    // Update is called once per frame
    void Update()
    {
        KeyUp();
        KeyDown();
        KeyLeft();
        KeyRight();

        EntryMusic();


       
    }

    void KeyUp()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isUp = false;
            for (int i = 0; i < generateSongPref.Songs.Count; i++)
            {
                Toggle toggle = generateSongPref.Songs[i].GetComponent<Toggle>();

                if (toggle.isOn && isUp==false)
                {

                    if (!(i < 0))
                    {
                        isUp = true;
                        Toggle NextCurrentToggle = generateSongPref.Songs[i - 1].GetComponent<Toggle>();

                        NextCurrentToggle.isOn = true;

                        
                    }




                }



            }



        }




    }

    void KeyDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isDown = false;
            
            for (int a = 0; a < generateSongPref.Songs.Count; a++)
            {

                Toggle toggle = generateSongPref.Songs[a].GetComponent<Toggle>();

                if (toggle.isOn && isDown == false)
                {
                    
                    if (!(a > generateSongPref.Songs.Count))
                    {
                        isDown = true;
                        Toggle NextCurrentToggle = generateSongPref.Songs[a + 1].GetComponent<Toggle>();


                        NextCurrentToggle.isOn = true;

                        
                    }




                }



            }



        }




    }

    void KeyLeft()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isLeft = false;
            for (int i = 0; i < DifficultyToggle.Length; i++)
            {


                if (DifficultyToggle[i].isOn && isLeft == false)
                {

                    if (!(i < 0))
                    {
                        isLeft = true;


                        DifficultyToggle[i - 1].isOn = true;

                    }




                }




            }

        }
    }

    void KeyRight()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                isRight = false;

                for (int a = 0; a < DifficultyToggle.Length; a++)
                {



                    if (DifficultyToggle[a].isOn && isRight == false)
                    {


                        if (!(a > DifficultyToggle.Length))
                        {
                            isRight = true;



                            DifficultyToggle[a + 1].isOn = true;

                        }




                    }



                }



            }



        }

    

    void EntryMusic()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("この曲で遊ぶドン");

            Toggle togle = MusictoggleGroup.ActiveToggles().FirstOrDefault();

            MusicDataBase musicDataBase = togle.gameObject.GetComponent<MusicDataBase>();

            Debug.Log(togle.gameObject.name);

            AnimationTitleText.text = musicDataBase.Name;

            Debug.Log(musicDataBase.Jacket.texture);

            AnimationImage.texture = musicDataBase.Texture;




            PlayerPrefs.SetString("SongFilePath", musicDataBase.Path);


            

            PlayerPrefs.SetString("Difficulty", difficultToggle.CurrentDifficulty);

            PlayerPrefs.SetString("LaneCoverPath", "file://" + readingSongsFolder.UIPath + "/" + "LaneCover" + "/" + laneToggle.CurrentToggle + ".png");

            PlayerPrefs.SetFloat("NoteSpeed", float.Parse(speedSelect.Speedtxt.text));

            PlayerPrefs.SetFloat("Brightness", BrightnessSelecter.value);

            PlayerPrefs.SetString("Name", musicDataBase.Name);


            PlayerPrefs.SetString("ArtistName", musicDataBase.ArtistName);
            PlayerPrefs.SetString("ScoreArtistName", musicDataBase.ScoreArtistName);

            PlayerPrefs.Save();


            Debug.Log("SongFilePath:" + PlayerPrefs.GetString("SongFilePath", null));
            Debug.Log("Difficult:" + PlayerPrefs.GetString("Difficulty", null));
            Debug.Log("LaneCovorPath:"+PlayerPrefs.GetString("LaneCoverPath", null));
            Debug.Log("NoteSpeed:"+PlayerPrefs.GetFloat("NoteSpeed", 6));
            Debug.Log("Brightness" + PlayerPrefs.GetFloat("Brightness", 0));
            Debug.Log("Name" + PlayerPrefs.GetString("Name", null));
            Debug.Log("ArtistName " + PlayerPrefs.GetString("ArtistName",null));
            Debug.Log("ScoreArtistName " + PlayerPrefs.GetString("ScoreArtistName", null));

            nextSceneAnimation.SetTrigger("isPlayMusic");


            
        }




    }
}
