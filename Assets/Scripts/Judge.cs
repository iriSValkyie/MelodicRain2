using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ノーツをタップした判定処理
/// </summary>
public class Judge : MonoBehaviour
{
    [Header("他スクリプト")]

    [SerializeField] GameController gcSC;
    [SerializeField] Player player;



    /*キー*/
    string tapkey1 = "d";
    string tapkey2 = "f";
    string tapkey3 = "h";
    string tapkey4 = "j";

    [Header("Combo")]

   [SerializeField] Text Combo;

    public int combo;

    public int Maxcombo;//最大コンボ数

    [Header("Score")]

     [SerializeField] Text Scoretxt;



    
    //各レーンで検出された譜面情報
    Note notes1 = new Note();
    Note notes2= new Note();
    Note notes3= new Note();
    Note notes4= new Note();
    //レーン上にあるゲームオブジェクト
    GameObject[] note = new GameObject[30];
    GameObject[] note2 = new GameObject[30];
    GameObject[] note3 = new GameObject[30];
    GameObject[] note4 = new GameObject[30];

    //レーン事の最小タイミングのノーツの時間
    float mintiming1 = 100; 
    float mintiming2 = 100;
    float mintiming3 = 100;
    float mintiming4 = 100;



    //長押しし続けていなければいけないか
    bool islongtap1;
    bool islongtap2;
    bool islongtap3;
    bool islongtap4;
    


   public int nowFrameRate;

  public  float oneframe;

   public int JustScore;

   public int GreatScore;

   public int GoodScore;

    

  public  int Score;

    float JUST;

    float justmul = 3f;

    float GREAT;

    float greatmul = 7f;

    float GOOD;

    float goodmul = 11f;

    float BAD;

    float badmul = 15f;

    public Score score = new Score();

    [Header("Judge Image")]

    public CanvasRenderer bad;


    [SerializeField]CanvasRenderer good;

    [SerializeField]CanvasRenderer great;

    [SerializeField]CanvasRenderer Just;


    CanvasRenderer OldJudge;


  
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.targetFrameRate);
        nowFrameRate = Application.targetFrameRate;

        oneframe = 1 / (float)nowFrameRate;

        if (60/nowFrameRate == 1)
        {
            Debug.Log("60フレームです");

           JUST = oneframe* justmul;
            GREAT = oneframe * greatmul;
            GOOD = oneframe * goodmul;
            BAD = oneframe * badmul;

            Debug.Log("JUST" + JUST);
            Debug.Log("GREAT" + GREAT);
            Debug.Log("GOOD" + GOOD);
            Debug.Log("BAD" + BAD);
        }
        else
        {
            Debug.Log("60フレーム以外のリフレッシュレートでプレイしています");
            float multipleFrame = nowFrameRate / 60;

            Debug.Log(multipleFrame);
            JUST = oneframe * justmul * multipleFrame;
            GREAT = oneframe * greatmul * multipleFrame;
            GOOD = oneframe * goodmul * multipleFrame;
            BAD = oneframe * badmul * multipleFrame;

            Debug.Log("JUST" + JUST);
            Debug.Log("GREAT" + GREAT);
            Debug.Log("GOOD" + GOOD);
            Debug.Log("BAD" + BAD);

        }

        ResetAlpha();


        

        

        
    }

    public void ResetAlpha()
    {
        bad.SetAlpha(0);
        good.SetAlpha(0);
        great.SetAlpha(0);
        Just.SetAlpha(0);

    }


    // Update is called once per frame
    void Update()
    {
        /*タップしたレーンにあるすべてのノーツを取得し
        そのノーツのタップする時間とタップした時間の差が
        一番小さいノーツと判定を行う*/
            Tap1();
           
      
            Tap2();
            
       
            Tap3();
         
        
            Tap4();
         

       
            LongUnTap1();
         
        
            LongUnTap2();
      
      
            LongUnTap3();
          
        
            LongUnTap4();
          
        LongTap1();
        LongTap2();
        LongTap3();
        LongTap4();
       
        Scoretxt.text = Score.ToString("N0");

        
    }

    private void LateUpdate()
    {
        if(combo > Maxcombo)
        {

            Maxcombo = combo;



        }

    }

    void Tap1()
    {
        
        if (Input.GetKeyDown(tapkey1) )
        {
           
            bool quit = false;
            float nowTime = gcSC.nowtime;
            note = GameObject.FindGameObjectsWithTag("Lane0");

            mintiming1 = 100;
            Debug.Log("１タップ");
            foreach (GameObject tmp in note)
            {
                if (note == null)
                {
                    quit = true;
                    Debug.LogError("ノーツが検出されていません" + "入力をキャンセルします");
                    break;
                }

                if (quit)
                {

                    break;
                }
                Note notes = tmp.GetComponent<Note>();
                Debug.Log("tag" + tmp.tag + "timing" + notes.notes.timing);
                float timing = notes.notes.timing + gcSC.CoolDownTime;
                float notetiming = Mathf.Abs(timing - nowTime);
                Debug.Log("時差" + notetiming);
                // Debug.Log("NoteTiming" + notetiming);

                if (notetiming < mintiming1)
                {
                    mintiming1 = notetiming;
                    notes1 = notes;

                    Debug.Log("mintimingの中身を" + notetiming + "に変更");
                }

                

            }

            if(notes1.notes.type == 2)
            {

                islongtap1 = true;

            }
            Debug.Log("最速" + mintiming1);

            if (mintiming1 < JUST)
            {


                Debug.Log("Lane0 is Just");
                combo+=1;
                Debug.Log(combo);
                Combo.text = combo.ToString();
                score.Just+=1;
                Score += JustScore;
                notes1.isTap = true;
                ResetAlpha();
                Just.SetAlpha(1);
                
                // TapNote(notes1.gameObject);
            }
            else if (mintiming1 > JUST && mintiming1 < GREAT)
            {

                Debug.Log("Lane0 is Great");
                combo+=1;
                Debug.Log(combo);
                Combo.text = combo.ToString();
                score.Great+=1;
                Score += GreatScore;
                notes1.isTap = true;
                ResetAlpha();
                great.SetAlpha(1);
                //  TapNote(notes1.gameObject);

            }
            else if (mintiming1 > GREAT && mintiming1 < GOOD)
            {

                Debug.Log("Lane0 is Good");
                combo = 0;
                Debug.Log(combo);
                Combo.text = combo.ToString();
                score.Good+=1;
                Score += GoodScore;
                notes1.isTap = true;
                ResetAlpha();
                good.SetAlpha(1);
                // TapNote(notes1.gameObject);
            }
            else if(GOOD < mintiming1 && BAD>mintiming1)
            {
                Debug.Log("Lane0 is BAD");
                combo = 0;
                Debug.Log(combo);
                Combo.text = combo.ToString();
                notes1.isTap = true;
                score.Bad+=1;
                
                ResetAlpha();
                bad.SetAlpha(1);
                // notes1.TapNote();
            }
            else
            {


                Debug.Log("適切なタップ位置まで到達していません");
            }


           
        }


    }
    void Tap2()
    {
        if (Input.GetKeyDown(tapkey2))
        {
            
            Debug.Log("レーン２タップしています");
            bool quit = false;
            float nowTime = gcSC.nowtime;
            note2 = GameObject.FindGameObjectsWithTag("Lane1");
            Debug.Log("2タップ");
            mintiming2 = 100;
            foreach (GameObject tmp in note2)
            {

                if (note2 == null)
                {
                    quit = true;
                    Debug.LogError("ノーツが検出されていません" + "入力をキャンセルします");
                    break;
                }

                if (quit)
                {

                    break;
                }
                Note notes = tmp.GetComponent<Note>();
                float timing = notes.notes.timing + gcSC.CoolDownTime;
                Debug.Log("tag" + tmp.tag + "timing" + notes.notes.timing);
                float notetiming = Mathf.Abs(timing - nowTime);
                Debug.Log("時差" + notetiming);
                if (notetiming < mintiming2)
                {

                    mintiming2 = notetiming;
                    notes2 = notes;
                    Debug.Log("mintimingの中身を" + notetiming + "に変更");
                }

            }

            if (notes2.notes.type == 2)
            {

                islongtap2 = true;

            }
            Debug.Log("最速" + mintiming2);
            // Debug.Log("最速" + mintiming2);
            if (mintiming2 < JUST)
            {


                Debug.Log("Lane1 is Just");

                combo+=1;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                score.Just+=1;
                Score += JustScore;
                notes2.isTap = true;
                // TapNote(notes2.gameObject);
                ResetAlpha();
                Just.SetAlpha(1);
            }
            else if (mintiming2 > JUST && mintiming2 < GREAT)
            {

                Debug.Log("Lane1 is Great");

                combo+=1;
                Combo.text = combo.ToString();

                Debug.Log(combo);
                score.Great+=1;
                Score += GreatScore;
                notes2.isTap = true;
                // TapNote(notes2.gameObject);
                ResetAlpha();
                great.SetAlpha(1);
            }
            else if (mintiming2 > GREAT && mintiming2 < GOOD)
            {


                Debug.Log("Lane1 is Good");
                combo = 0;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                score.Good+=1;
                Score += GoodScore;
                notes2.isTap = true;
                //  TapNote(notes2.gameObject);
                ResetAlpha();
                good.SetAlpha(1);
            }
            else if(GOOD < mintiming2 && BAD > mintiming2)
            {


                Debug.Log("Lane1 is BAD");

                combo = 0;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                notes2.isTap = true;
                score.Bad+=1;
                
                //   notes2.TapNote();
                ResetAlpha();
                bad.SetAlpha(1);
            }
            else
            {


                Debug.Log("適切なタップ位置まで到達していません");
            }

           
        }


    }

    void Tap3()
    {
        if (Input.GetKeyDown(tapkey3))
        {
            
            bool quit = false;
            float nowTime = gcSC.nowtime;
            note3 = GameObject.FindGameObjectsWithTag("Lane2");
            Debug.Log("3タップ");
            mintiming3 = 100;
            foreach (GameObject tmp in note3)
            {

                if (note3 == null)
                {
                    quit = true;
                    Debug.LogError("ノーツが検出されていません" + "入力をキャンセルします");
                    break;
                }

                if (quit)
                {

                    break;
                }
                Note notes = tmp.GetComponent<Note>();
                float timing = notes.notes.timing + gcSC.CoolDownTime;
                Debug.Log("tag" + tmp.tag + "timing" + notes.notes.timing);
                float notetiming = Mathf.Abs(timing - nowTime);
                Debug.Log("時差" + notetiming);
                if (notetiming < mintiming3)
                {

                    mintiming3 = notetiming;
                    notes3 = notes;
                    Debug.Log("mintimingの中身を" + notetiming + "に変更");
                }


            }
            if (notes3.notes.type == 2)
            {

                islongtap3 = true;

            }
            Debug.Log("最速" + mintiming3);
            // Debug.Log("最速" + mintiming3);
            if (mintiming3 < JUST)
            {


                Debug.Log("Lane2 is Just");
                combo+=1;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                score.Just+=1;
                Score += JustScore;
                notes3.isTap = true;
                // TapNote(notes3.gameObject);
                ResetAlpha();
                Just.SetAlpha(1);
            }
            else if (mintiming3 > JUST && mintiming3 < GREAT)
            {

                Debug.Log("Lane2 is Great");

                combo+=1;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                score.Great+=1;
                Score += GreatScore;
                notes3.isTap = true;
                // TapNote(notes3.gameObject);
                ResetAlpha();
                great.SetAlpha(1);
            }
            else if (mintiming3 > GREAT && mintiming3 < GOOD)
            {


                Debug.Log("Lane2 is Good");
                combo = 0;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                score.Good+=1;
                Score += GoodScore;
                notes3.isTap = true;
                // TapNote(notes3.gameObject);
                ResetAlpha();
                good.SetAlpha(1);
            }
            else if (GOOD < mintiming3 && BAD > mintiming2)
            {
                Debug.Log("Lane2 is BAD");

                combo = 0;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                score.Bad+=1;
                
                notes3.isTap = true;
                // notes3.TapNote();
                ResetAlpha();
                bad.SetAlpha(1);
            }
            else
            {


                Debug.Log("適切なタップ位置まで到達していません");
            }

           
        }

    }

    void Tap4()
    {

        if (Input.GetKeyDown(tapkey4))
        {
           
            bool quit = false;
            float nowTime = gcSC.nowtime;
            note4 = GameObject.FindGameObjectsWithTag("Lane3");
            Debug.Log("4タップ");
            mintiming4 = 100;
            foreach (GameObject tmp in note4)
            {

                if (note4 == null)
                {
                    quit = true;
                    Debug.LogError("ノーツが検出されていません" + "入力をキャンセルします");
                    break;
                }

                if (quit)
                {

                    break;
                }
                Note notes = tmp.GetComponent<Note>();
                   Debug.Log( "tag" + tmp.tag + "timing" + notes.notes.timing);
                float timing = notes.notes.timing + gcSC.CoolDownTime;

                float notetiming = Mathf.Abs(nowTime - timing);
                Debug.Log("時差" + notetiming);
                if (notetiming < mintiming4)
                {


                    mintiming4 = notetiming;

                    notes4 = notes;
                }

            }
            if (notes4.notes.type == 2)
            {

                islongtap4 = true;

            }
            Debug.Log("最速" + mintiming4);
            // Debug.Log("最速" + mintiming4);

            if (mintiming4 < JUST)
            {


                Debug.Log("Lane3 is Just");
                combo+=1;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                score.Just+=1;
                Score += JustScore;
                notes4.isTap = true;
                // TapNote(notes4.gameObject);
                ResetAlpha();
                Just.SetAlpha(1);
               
            }
            else if (mintiming4 > JUST && mintiming4 < GREAT)
            {

                Debug.Log("Lane3 is Great");

                combo+=1;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                score.Great+=1;
                Score += GreatScore;
                notes4.isTap = true;
                //  TapNote(notes4.gameObject);
                ResetAlpha();
                great.SetAlpha(1);
               
            }
            else if (mintiming4 > GREAT && mintiming4 < GOOD)
            {

                Debug.Log("Lane3 is Good");

                combo = 0;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                score.Good+=1;
                Score += GoodScore;
                notes4.isTap = true;
                // TapNote(notes4.gameObject);
                ResetAlpha();
                good.SetAlpha(1);
               
            }
            else if (GOOD < mintiming4 && BAD > mintiming1)
            {

                Debug.Log("Lane3 is BAD");
                combo = 0;
                Combo.text = combo.ToString();
                Debug.Log(combo);
                notes4.isTap = true;
                score.Bad+=1;
                //  notes4.TapNote();
                ResetAlpha();
                bad.SetAlpha(1);
               
            }
            else
            {


                Debug.Log("適切なタップ位置まで到達していません");
            }

           
        }


    }

    void LongTap1()
    {
        if (islongtap1)
        {
            if (Input.GetKey(tapkey1))
            {
                Debug.Log(Input.GetKey(tapkey1));





            }
            else
            {


                combo = 0;
                Combo.text = combo.ToString();
                score.Bad+=1;
                ResetAlpha();
                bad.SetAlpha(1);
            }




        }



    }
    void LongTap2()
    {
        if (islongtap2)
        {
            if (Input.GetKey(tapkey2))
            {
                Debug.Log(Input.GetKey(tapkey2));





            }
            else
            {


                combo = 0;
                Combo.text = combo.ToString();
                score.Bad+=1;
                ResetAlpha();
                bad.SetAlpha(1);
            }




        }



    }
    void LongTap3()
    {
        if (islongtap3)
        {
            if (Input.GetKey(tapkey3))
            {
                Debug.Log(Input.GetKey(tapkey3));





            }
            else
            {


                combo = 0;
                Combo.text = combo.ToString();
                score.Bad+=1;
                ResetAlpha();
                bad.SetAlpha(1);
            }




        }



    }
    void LongTap4()
    {
        if (islongtap4)
        {
            if (Input.GetKey(tapkey4))
            {
                Debug.Log(Input.GetKey(tapkey4));





            }
            else
            {


                combo = 0;
                Combo.text = combo.ToString();
                score.Bad+=1;
                ResetAlpha();
                bad.SetAlpha(1);
            }




        }



    }

    void LongUnTap1()
    {
        if (islongtap1)
        {

            if (Input.GetKeyUp(tapkey1) )
            {
               
                bool quit = false;
                float nowTime = gcSC.nowtime;
                 note = GameObject.FindGameObjectsWithTag("Lane0");

                mintiming1 = 100;
                Debug.Log("１タップ");
                foreach (GameObject tmp in note)
                {
                    if (note == null)
                    {
                        quit = true;
                        Debug.LogError("ノーツが検出されていません" + "入力をキャンセルします");
                        break;
                    }

                    if (quit)
                    {

                        break;
                    }
                    Note notes = tmp.GetComponent<Note>();
                    Debug.Log("tag" + tmp.tag + "timing" + notes.notes.timing);
                    float timing = notes.notes.timing + gcSC.CoolDownTime;
                    float notetiming = Mathf.Abs(timing - nowTime);
                    Debug.Log("時差" + notetiming);
                    // Debug.Log("NoteTiming" + notetiming);

                    if (notetiming < mintiming1)
                    {
                        mintiming1 = notetiming;
                        notes1 = notes;

                        Debug.Log("mintimingの中身を" + notetiming + "に変更");
                    }



                }

                

             

                
                Debug.Log("最速" + mintiming1);

                if (mintiming1 < JUST)
                {


                    Debug.Log("Lane0 is Just");
                    combo+=1;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Just+=1;
                    Score += JustScore;
                    notes1.isTap = true;
                    ResetAlpha();
                    Just.SetAlpha(1);

                    // TapNote(notes1.gameObject);
                }
                else if (mintiming1 > JUST && mintiming1 < GREAT)
                {

                    Debug.Log("Lane0 is Great");
                    combo+=1;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Great+=1;
                    Score += GreatScore;
                    notes1.isTap = true;
                    ResetAlpha();
                    great.SetAlpha(1);
                    //  TapNote(notes1.gameObject);

                }
                else if (mintiming1 > GREAT && mintiming1 < GOOD)
                {

                    Debug.Log("Lane0 is Good");
                    combo = 0;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Good+=1;
                    Score += GoodScore;
                    notes1.isTap = true;
                    ResetAlpha();
                    good.SetAlpha(1);
                    // TapNote(notes1.gameObject);
                }
                else if (GOOD < mintiming1 && BAD > mintiming1)
                {
                    Debug.Log("Lane0 is BAD");
                    combo = 0;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    notes1.isTap = true;
                    score.Bad+=1;
                    ResetAlpha();
                    bad.SetAlpha(1);
                    // notes1.TapNote();
                }
                else
                {


                    Debug.Log("適切なタップ位置まで到達していません");
                }

                player.SE.PlayOneShot(player.SE.clip);
                islongtap1 = false;

            }









            







        }


    }

    void LongUnTap2()
    {
        if (islongtap2)
        {
            if (Input.GetKeyUp(tapkey2))
            {
                
                bool quit = false;
                float nowTime = gcSC.nowtime;
                note2 = GameObject.FindGameObjectsWithTag("Lane1");
                Debug.Log("2タップ");
                mintiming2 = 100;
                foreach (GameObject tmp in note2)
                {

                    if (note2 == null)
                    {
                        quit = true;
                        Debug.LogError("ノーツが検出されていません" + "入力をキャンセルします");
                        break;
                    }

                    if (quit)
                    {

                        break;
                    }
                    Note notes = tmp.GetComponent<Note>();
                    float timing = notes.notes.timing + gcSC.CoolDownTime;
                    Debug.Log("tag" + tmp.tag + "timing" + notes.notes.timing);
                    float notetiming = Mathf.Abs(timing - nowTime);
                    Debug.Log("時差" + notetiming);
                    if (notetiming < mintiming2)
                    {

                        mintiming2 = notetiming;
                        notes2 = notes;
                        Debug.Log("mintimingの中身を" + notetiming + "に変更");
                    }

                }






                Debug.Log("最速" + mintiming2);
                // Debug.Log("最速" + mintiming2);
                if (mintiming2 < JUST)
                {


                    Debug.Log("Lane1 is Just");

                    combo+=1;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Just+=1;
                    Score += JustScore;
                    notes2.isTap = true;
                    // TapNote(notes2.gameObject);
                    ResetAlpha();
                    Just.SetAlpha(1);
                }
                else if (mintiming2 > JUST && mintiming2 < GREAT)
                {

                    Debug.Log("Lane1 is Great");

                    combo+=1;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Great+=1;
                    Score += GreatScore;
                    notes2.isTap = true;
                    // TapNote(notes2.gameObject);
                    ResetAlpha();
                    great.SetAlpha(1);
                }
                else if (mintiming2 > GREAT && mintiming2 < GOOD)
                {


                    Debug.Log("Lane1 is Good");
                    combo = 0;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Good+=1;
                    Score += GoodScore;
                    notes2.isTap = true;
                    //  TapNote(notes2.gameObject);
                    ResetAlpha();
                    good.SetAlpha(1);
                }
                else if (GOOD < mintiming2 && BAD > mintiming2)
                {


                    Debug.Log("Lane1 is BAD");

                    combo = 0;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    notes2.isTap = true;
                    score.Bad+=1;
                    //   notes2.TapNote();
                    ResetAlpha();
                    bad.SetAlpha(1);
                }
                else
                {


                    Debug.Log("適切なタップ位置まで到達していません");
                }

                player.SE.PlayOneShot(player.SE.clip);

                islongtap2 = false;
            }
            

        }
    }

    void LongUnTap3()
    {
        if (islongtap3)
        {
            if (Input.GetKeyUp(tapkey3) )
            {
               
                bool quit = false;
                float nowTime = gcSC.nowtime;
                note3 = GameObject.FindGameObjectsWithTag("Lane2");
                Debug.Log("3タップ");
                mintiming3 = 100;
                foreach (GameObject tmp in note3)
                {

                    if (note3 == null)
                    {
                        quit = true;
                        Debug.LogError("ノーツが検出されていません" + "入力をキャンセルします");
                        break;
                    }

                    if (quit)
                    {

                        break;
                    }
                    Note notes = tmp.GetComponent<Note>();
                    float timing = notes.notes.timing + gcSC.CoolDownTime;
                    Debug.Log("tag" + tmp.tag + "timing" + notes.notes.timing);
                    float notetiming = Mathf.Abs(timing - nowTime);
                    Debug.Log("時差" + notetiming);
                    if (notetiming < mintiming3)
                    {

                        mintiming3 = notetiming;
                        notes3 = notes;
                        Debug.Log("mintimingの中身を" + notetiming + "に変更");
                    }


                }
               

                    

                
                Debug.Log("最速" + mintiming3);
                // Debug.Log("最速" + mintiming3);
                if (mintiming3 < JUST)
                {


                    Debug.Log("Lane2 is Just");
                    combo+=1;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Just+=1;
                    Score += JustScore;
                    notes3.isTap = true;
                    // TapNote(notes3.gameObject);
                    ResetAlpha();
                    Just.SetAlpha(1);
                }
                else if (mintiming3 > JUST && mintiming3 < GREAT)
                {

                    Debug.Log("Lane2 is Great");

                    combo+=1;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Great+=1;
                    Score += GreatScore;
                    notes3.isTap = true;
                    // TapNote(notes3.gameObject);
                    ResetAlpha();
                    great.SetAlpha(1);
                }
                else if (mintiming3 > GREAT && mintiming3 < GOOD)
                {


                    Debug.Log("Lane2 is Good");
                    combo = 0;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Good+=1;
                    Score += GoodScore;
                    notes3.isTap = true;
                    // TapNote(notes3.gameObject);
                    ResetAlpha();
                    good.SetAlpha(1);
                }
                else if (GOOD < mintiming3 && BAD > mintiming2)
                {
                    Debug.Log("Lane2 is BAD");

                    combo = 0;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Bad+=1;
                    notes3.isTap = true;
                    // notes3.TapNote();
                    ResetAlpha();
                    bad.SetAlpha(1);
                }
                else
                {


                    Debug.Log("適切なタップ位置まで到達していません");
                }
                player.SE.PlayOneShot(player.SE.clip);

                islongtap3 = false;
            }
           
        }
    }

    void LongUnTap4()
    {
        if (islongtap4)
        {
            if (Input.GetKeyUp(tapkey4) )
            {
               
                bool quit = false;
                float nowTime = gcSC.nowtime;
                note4 = GameObject.FindGameObjectsWithTag("Lane3");
                Debug.Log("4タップ");
                mintiming4 = 100;
                foreach (GameObject tmp in note4)
                {

                    if (note4 == null)
                    {
                        quit = true;
                        Debug.LogError("ノーツが検出されていません" + "入力をキャンセルします");
                        break;
                    }

                    if (quit)
                    {

                        break;
                    }
                    Note notes = tmp.GetComponent<Note>();
                    Debug.Log("tag" + tmp.tag + "timing" + notes.notes.timing);
                    float timing = notes.notes.timing + gcSC.CoolDownTime;

                    float notetiming = Mathf.Abs(nowTime - timing);
                    Debug.Log("時差" + notetiming);
                    if (notetiming < mintiming4)
                    {


                        mintiming4 = notetiming;

                        notes4 = notes;
                    }

                }




                Debug.Log("最速" + mintiming4);
                // Debug.Log("最速" + mintiming4);

                if (mintiming4 < JUST)
                {


                    Debug.Log("Lane3 is Just");
                    combo+=1;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Just+=1;
                    Score += JustScore;
                    notes4.isTap = true;
                    // TapNote(notes4.gameObject);
                    ResetAlpha();
                    Just.SetAlpha(1);
                }
                else if (mintiming4 > JUST && mintiming4 < GREAT)
                {

                    Debug.Log("Lane3 is Great");

                    combo+=1;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Great+=1;
                    Score += GreatScore;
                    notes4.isTap = true;
                    //  TapNote(notes4.gameObject);
                    ResetAlpha();
                    great.SetAlpha(1);
                }
                else if (mintiming4 > GREAT && mintiming4 < GOOD)
                {

                    Debug.Log("Lane3 is Good");

                    combo = 0;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    score.Good+=1;
                    Score += GoodScore;
                    notes4.isTap = true;
                    // TapNote(notes4.gameObject);
                    ResetAlpha();
                    good.SetAlpha(1);
                }
                else if (GOOD < mintiming4 && BAD > mintiming1)
                {

                    Debug.Log("Lane3 is BAD");
                    combo = 0;
                    Combo.text = combo.ToString();
                    Debug.Log(combo);
                    notes4.isTap = true;
                    score.Bad+=1;
                    //  notes4.TapNote();
                    ResetAlpha();
                    bad.SetAlpha(1);
                }
                else
                {


                    Debug.Log("適切なタップ位置まで到達していません");
                }
                player.SE.PlayOneShot(player.SE.clip);


                islongtap4 = false;
            }

           
        }
    }
}
