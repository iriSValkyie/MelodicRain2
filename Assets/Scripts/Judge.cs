using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{
    [SerializeField] GameController gcSC;


    string tapkey1 = "d";
    string tapkey2 = "f";
    string tapkey3 = "h";
    string tapkey4 = "j";

   [SerializeField] Text Combo;

    int combo;

    Note notes1 = new Note();

    Note notes2= new Note();
    Note notes3= new Note();
    Note notes4= new Note();


    GameObject[] note = new GameObject[30];
    GameObject[] note2 = new GameObject[30];
    GameObject[] note3 = new GameObject[30];
    GameObject[] note4 = new GameObject[30];

    float mintiming1 = 100;
    float mintiming2 = 100;
    float mintiming3 = 100;
    float mintiming4 = 100;


    float JUST = 0.025f;

    float GREAT = 0.0417f;


    float GOOD = 0.0583f;

    public Score score = new Score();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Tap1();
        Tap2();
        Tap3();
        Tap4();
        
       
        
    }

    void Tap1()
    {

        if (Input.GetKeyDown(tapkey1))
        {
            bool quit = false;

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
                float notetiming = Mathf.Abs(timing - gcSC.nowtime);
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
                combo++;
                Combo.text = combo.ToString();
                score.Just++;
                notes1.isTap = true;
                // TapNote(notes1.gameObject);
            }
            else if (mintiming1 > JUST && mintiming1 < GREAT)
            {

                Debug.Log("Lane0 is Great");
                combo++;
                Combo.text = combo.ToString();
                score.Great++;
                notes1.isTap = true;
                //  TapNote(notes1.gameObject);

            }
            else if (mintiming1 > GREAT && mintiming1 < GOOD)
            {

                Debug.Log("Lane0 is Good");
                combo = 0;
                Combo.text = combo.ToString();
                score.Good++;
                notes1.isTap = true;
                // TapNote(notes1.gameObject);
            }
            else if(GOOD < mintiming1)
            {
                Debug.Log("Lane0 is BAD");
                combo = 0;
                Combo.text = combo.ToString();
                notes1.isTap = true;
                score.BAD++;
                // notes1.TapNote();
            }



        }


    }
    void Tap2()
    {
        if (Input.GetKeyDown(tapkey2))
        {
            bool quit = false;
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
                float notetiming = Mathf.Abs(timing - gcSC.nowtime);
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

                combo++;
                Combo.text = combo.ToString();

                score.Just++;
                notes2.isTap = true;
                // TapNote(notes2.gameObject);

            }
            else if (mintiming2 > JUST && mintiming2 < GREAT)
            {

                Debug.Log("Lane1 is Great");

                combo++;
                Combo.text = combo.ToString();
                score.Great++;
                notes2.isTap = true;
                // TapNote(notes2.gameObject);

            }
            else if (mintiming2 > GREAT && mintiming2 < GOOD)
            {


                Debug.Log("Lane1 is Good");
                combo = 0;
                Combo.text = combo.ToString();
                score.Good++;
                notes2.isTap = true;
                //  TapNote(notes2.gameObject);

            }
            else if(GOOD < mintiming2)
            {


                Debug.Log("Lane1 is BAD");

                combo = 0;
                Combo.text = combo.ToString();
                notes2.isTap = true;
                score.BAD++;
                //   notes2.TapNote();

            }

        }


    }

    void Tap3()
    {
        if (Input.GetKeyDown(tapkey3))
        {
            bool quit = false;
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
                float notetiming = Mathf.Abs(timing - gcSC.nowtime);
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
                combo++;
                Combo.text = combo.ToString();
                score.Just++;
                notes3.isTap = true;
                // TapNote(notes3.gameObject);

            }
            else if (mintiming3 > JUST && mintiming3 < GREAT)
            {

                Debug.Log("Lane2 is Great");

                combo++;
                Combo.text = combo.ToString();
                score.Great++;
                notes3.isTap = true;
                // TapNote(notes3.gameObject);
            }
            else if (mintiming3 > GREAT && mintiming3 < GOOD)
            {


                Debug.Log("Lane2 is Good");
                combo = 0;
                Combo.text = combo.ToString();
                score.Good++;
                notes3.isTap = true;
                // TapNote(notes3.gameObject);
            }
            else if (GOOD < mintiming3)
            {
                Debug.Log("Lane2 is BAD");

                combo = 0;
                Combo.text = combo.ToString();
                score.BAD++;
                notes3.isTap = true;
                // notes3.TapNote();
            }


        }

    }

    void Tap4()
    {

        if (Input.GetKeyDown(tapkey4))
        {
            bool quit = false;
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

                float notetiming = Mathf.Abs(gcSC.nowtime - timing);
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
                combo++;
                Combo.text = combo.ToString();
                score.Just++;
                notes4.isTap = true;
                // TapNote(notes4.gameObject);
            }
            else if (mintiming4 > JUST && mintiming4 < GREAT)
            {

                Debug.Log("Lane3 is Great");

                combo++;
                Combo.text = combo.ToString();
                score.Great++;
                notes4.isTap = true;
                //  TapNote(notes4.gameObject);
            }
            else if (mintiming4 > GREAT && mintiming4 < GOOD)
            {

                Debug.Log("Lane3 is Good");

                combo = 0;
                Combo.text = combo.ToString();
                score.Good++;
                notes4.isTap = true;
                // TapNote(notes4.gameObject);
            }
            else if (GOOD < mintiming4)
            {

                Debug.Log("Lane3 is BAD");
                combo = 0;
                Combo.text = combo.ToString();
                notes4.isTap = true;
                score.BAD++;
                //  notes4.TapNote();
            }


        }


    }
}
