using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ResultManager : MonoBehaviour
{

    [SerializeField] Image backgroundimage;

    

    float speed;

    [SerializeField] bool isFadein;
    [SerializeField] bool isFadeOut;


    [SerializeField] int Score;

    [SerializeField] string Difficult;


    [SerializeField] int Just;


    [SerializeField] int Great;

    [SerializeField] int Good;

    [SerializeField] int Bad;


    [SerializeField] int Combo;

    [SerializeField] int BestScore;

    [SerializeField] int BestCombo;
    [SerializeField] string Name;

    [SerializeField] int AllCombo;


    [SerializeField] Image FullcomboAllJust;

    

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

    // Start is called before the first frame update
    void Start()
    {
        isFadein = false;
        speed = 1;


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

        ScoreArtisttxt.text = "ScoreArtist:" +PlayerPrefs.GetString("ScoreArtistName", "???");


        BestScore = PlayerPrefs.GetInt(Name + "_" + Difficult + "_BestScore", 0);

        BestCombo = PlayerPrefs.GetInt(Name + "_" + Difficult + "_BestCombo", 0);

        if (BestScore < Score)
        {


            BestScoretxt.text = Score.ToString("N0");
            PlayerPrefs.SetInt(Name + "_" + Difficult + "_BestScore",Score);
            PlayerPrefs.Save();
        }
        else
        {

            BestScoretxt.text = BestScore.ToString("N0");
            PlayerPrefs.SetInt(Name + "_" + Difficult + "_BestScore", BestScore);
            PlayerPrefs.Save();
        }


        if(BestCombo < Combo)
        {

            PlayerPrefs.SetInt(Name + "_" + Difficult + "_BestCombo", Combo);
            PlayerPrefs.Save();


        }
        else
        {
            PlayerPrefs.SetInt(Name + "_" + Difficult + "_BestCombo", BestCombo);
            PlayerPrefs.Save();



        }


        if(Combo == AllCombo)
        {

            if(Bad ==0 && Good == 0 && Great ==0)
            {


                
                Debug.Log("AllJust!!");

                

                FullcomboAllJust.sprite = Resources.Load<Sprite>("MusicSelect-AllParfect");
                PlayerPrefs.SetString(Difficult + "AllJust","true");
                PlayerPrefs.Save();



            }else if (Bad == 0 && Good == 0)
            {
                Debug.Log("FullCombo");
                FullcomboAllJust.sprite = Resources.Load<Sprite>("MusicSelect-FullCombo");
                PlayerPrefs.SetString(Difficult+"FullCombo", "true");
                PlayerPrefs.Save();


            }





        }
        else
        {
            PlayerPrefs.SetString(Difficult + "AllJust", "false");
            PlayerPrefs.SetString(Difficult + "FullCombo", "false");
            PlayerPrefs.Save();


        }

        MaxCombo.text = "MaxCombo    " + Combo.ToString();

        Justtxt.text = "JUST    " + Just.ToString();

        Greattxt.text = "GREAT   " + Great.ToString();

        Goodtxt.text = "GOOD    " + Good.ToString();

        Badtxt.text = "BAD     " + Bad.ToString();

        Scoretxt.text = Score.ToString("N0");


        isFadein = true;

    }


    void Update()
    {

        if (isFadein)
        {
            speed -= Time.deltaTime;



            backgroundimage.color = new Color(0, 0, 0, speed);

            if (backgroundimage.color.a <= 0)
            {

                isFadein = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.Return))
        {

            isFadeOut = true;

        }

        if (isFadeOut)
        {
            

            speed += Time.deltaTime;




            backgroundimage.color = new Color(0, 0, 0, speed);


            if(backgroundimage.color.a >= 1)
            {


                SceneManager.LoadScene("SelectMusic");
            }

        }
    }


    public void OnClickNext()
    {
        isFadeOut = true;

       

    }



    // Update is called once per frame
}
