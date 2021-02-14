using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{



    float TimeCount = 0.01f;

    public GameObject notePrefab;
    public GameObject LongnotePrefab;

    const float lane0X = -203.7f;
    const float lane1X = -68.8f;
    const float lane2X = 68.6f;
    const float lane3X = 204f;



    public float CoolDownTime = 4;//曲が始まるまでの空き時間です。譜面が0秒から始まるため空きを付けてます


    const float TapDistance = 980.5f; //画面上部のノーツが出てくるとこから叩く場所までの距離

    [SerializeField] GameObject notesParent;
    [SerializeField] Note NoteController;
    [SerializeField] Note EndNoteController;
    [SerializeField] LongNote LongNoteController;



    float CurrentX;


    bool isPlay;

    bool isstart;


    bool isHumenFire;


    public float nowtime;


    float offsetTime;


    int setactive = 0;
    int setactiveLong = 0;
    [SerializeField] List<GameObject> NotesPref;
    [SerializeField] List<GameObject> EndNotesPref;
    [SerializeField] List<GameObject> LongNotePref;
    [SerializeField] List<float> NotesPrefTiming;
    [SerializeField] List<float> LongNotesPrefTiming;
    [SerializeField] AudioSource music;

    [SerializeField] List<float> NotesPrefTapTiming;
    [SerializeField] List<float> LongNotesPrefTapTiming;

    [SerializeField] Text debugTimetxt;

    [SerializeField] Fumen inputJson;
    GameObject NotesChild;



    [SerializeField] Player player;



    [SerializeField] string key1;

    [SerializeField] string key2;

    [SerializeField] string key3;

    [SerializeField] string key4;

    float mintiming1;
    float mintiming2;
    float mintiming3;
    float mintiming4;
    // float tappos = 982.5f;

    float JUST = 0.025f;

    float GREAT = 0.0417f;


    float GOOD = 0.0583f;



    Note notes1;

    Note notes2;
    Note notes3;
    Note notes4;

    public Text Combo;

    public int combo;



    public Score score;


    void Awake()
    {




        InitialNotes();




    }


    private void Start()
    {
        mintiming1 = 100;
        mintiming2 = 100;
        mintiming3 = 100;
        mintiming4 = 100;
        
    }

    void InitialNotes()
    {
        score = new Score();

        string inputString = Resources.Load<TextAsset>("inch_ex").ToString();

        music.clip = Resources.Load<AudioClip>("inch");



        inputJson = JsonUtility.FromJson<Fumen>(inputString);

        offsetTime = (float)inputJson.offset / (float)44250;

        Debug.Log("Offset:" + inputJson.offset);
        Debug.Log("OffsetTime is" + offsetTime + "(s)!");
        Debug.Log("QueueMusic:" + inputJson.name);

        Debug.Log("Notes quantity is" + inputJson.notes.Length + "Notes");


        for (int a = 0; a < inputJson.notes.Length; a++)
        {
            switch (inputJson.notes[a].block)
            {
                case 0:


                    CurrentX = lane0X;

                    break;
                case 1:

                    CurrentX = lane1X;
                    break;

                case 2:


                    CurrentX = lane2X;
                    break;

                case 3:

                    CurrentX = lane3X;

                    break;

                default:

                    Debug.LogError("存在しないレーンにノーツが打たれています！ NoteEditorで再編集してください");

                    break;



            }

            // Debug.Log("JsonIndex:" + a + "=" + CurrentX);
            switch (inputJson.notes[a].type)
            {

                case 1:
                    Debug.Log("ロングノーツを検出できませんでした");
                    NotesChild = Instantiate(notePrefab, new Vector2(CurrentX, 0), Quaternion.identity, notesParent.transform);

                    switch (inputJson.notes[a].block)
                    {

                        case 0:

                            NotesChild.gameObject.tag = "Lane0";

                            break;
                        case 1:
                            NotesChild.gameObject.tag = "Lane1";



                            break;
                        case 2:


                            NotesChild.gameObject.tag = "Lane2";


                            break;
                        case 3:

                            NotesChild.gameObject.tag = "Lane3";
                            break;

                    }
                    NotesPref.Add(NotesChild);//リストに追加

                    NoteController = NotesChild.GetComponent<Note>();//インスタンス化したオブジェクトのスクリプトを取得
                    NoteController.notes = inputJson.notes[a];//Notesクラスの中身をNoteスクリプトに代入

                    RectTransform NotesRect = NotesChild.GetComponent<RectTransform>();//インスタンス化したオブジェクトのRectTransformを取得
                    NotesRect.anchoredPosition = new Vector2(CurrentX, 0);//アンカーポジションを修正

                    Debug.Log("Num:" + inputJson.notes[a].num + "　Block:" + inputJson.notes[a].block + "　A:" + a);

                    inputJson.notes[a].timing = (60 / (float)inputJson.BPM) / (float)inputJson.notes[a].LPB * inputJson.notes[a].num + offsetTime;//Noteクラスのタイミングにタップするタイミングを代入（秒）

                    Debug.Log("timing" + inputJson.notes[a].timing + "Position" + NotesChild.transform.position);
                    NotesPrefTiming.Add(inputJson.notes[a].timing + (CoolDownTime / 2));//ノーツを表示するタイミングを追加
                    NotesPrefTapTiming.Add(inputJson.notes[a].timing + CoolDownTime);
                    




                    NotesPref[a].gameObject.SetActive(false);//ノーツを非表示にする

                    break;



                case 2:
                    Debug.Log("ロングノーツを検出");
                    GameObject LongNotesChild = Instantiate(notePrefab, new Vector2(CurrentX, 0), Quaternion.identity, notesParent.transform);//ロングノーツ始点をインスタンス化
                    NotesPref.Add(LongNotesChild);//リストに追加


                    GameObject LongNote = Instantiate(LongnotePrefab, new Vector2(CurrentX, 0), Quaternion.identity, notesParent.transform);//ロングノーツをインスタンス化(始点の子に指定)
                    LongNotePref.Add(LongNote);


                    LongNoteController = LongNote.GetComponent<LongNote>();
                    LongNoteController.startnote = LongNotesChild.GetComponent<RectTransform>();


                    GameObject LongNotesChild2 = Instantiate(notePrefab, new Vector2(CurrentX, 0), Quaternion.identity, notesParent.transform);//ロングノーツ終点をインスタンス化
                    EndNotesPref.Add(LongNotesChild2);//リストに追加
                    switch (inputJson.notes[a].block)
                    {

                        case 0:

                            LongNotesChild.gameObject.tag = "Lane0";
                            LongNotesChild2.gameObject.tag = "Lane0";

                            break;
                        case 1:
                            LongNotesChild.gameObject.tag = "Lane1";
                            LongNotesChild2.gameObject.tag = "Lane1";


                            break;
                        case 2:


                            LongNotesChild.gameObject.tag = "Lane2";
                            LongNotesChild2.gameObject.tag = "Lane2";

                            break;
                        case 3:

                            LongNotesChild.gameObject.tag = "Lane3";
                            LongNotesChild2.gameObject.tag = "Lane3";
                            break;

                    }
                    LongNoteController.endnote = LongNotesChild2.GetComponent<RectTransform>();
                    NoteController = LongNotesChild.GetComponent<Note>();//インスタンス化した始点オブジェクトのスクリプトを取得
                    NoteController.notes = inputJson.notes[a];//Notesクラスの中身をNoteスクリプトに代入




                    LongNote.gameObject.transform.SetParent(LongNotesChild2.gameObject.transform);//終点ノーツの子にロングノーツを
                                                                                                  //   LongNotesChild.gameObject.transform.SetParent(LongNotesChild2.gameObject.transform);//終点ノーツの子に始点ノーツを

                    //  LongNote.transform.SetParent(notesParent.transform);//始点との親子関係を解消
                    //int NotesiblingIndex = LongNotesChild.transform.GetSiblingIndex();//始点の順番を変数に代入
                    //LongNote.transform.SetSiblingIndex(NotesiblingIndex - 1);//始点より上にロングノーツの画像を移動



                    EndNoteController = LongNotesChild2.GetComponent<Note>();//インスタンス化した終点オブジェクトのスクリプトを取得
                    EndNoteController.notes = inputJson.notes[a].notes[0];//Notesクラスの中身をNoteスクリプトに代入

                    RectTransform LongNotesRect2 = LongNotesChild2.GetComponent<RectTransform>();//インスタンス化したオブジェクトのRectTransformを取得
                    LongNotesRect2.anchoredPosition = new Vector2(CurrentX, 0);//アンカーポジションを修正


                    RectTransform LongNotesRect = LongNotesChild.GetComponent<RectTransform>();//インスタンス化したオブジェクトのRectTransformを取得
                    LongNotesRect.anchoredPosition = new Vector2(CurrentX, 0);//アンカーポジションを修正


                    Debug.Log("Num:" + inputJson.notes[a].num + "　Block:" + inputJson.notes[a].block + "　A:" + a);
                    Debug.Log("Num:" + inputJson.notes[a].notes[0].num + "　Block:" + inputJson.notes[a].notes[0].block + "　A:" + a);

                    inputJson.notes[a].timing = (60 / (float)inputJson.BPM) / (float)inputJson.notes[a].LPB * inputJson.notes[a].num + offsetTime;//Noteクラスのタイミングにタップするタイミングを代入（秒）

                    inputJson.notes[a].notes[0].timing = (60 / (float)inputJson.BPM) / (float)inputJson.notes[a].notes[0].LPB * inputJson.notes[a].notes[0].num + offsetTime;//Noteクラスのタイミングに離すタイミングを代入（秒）

                    Debug.Log("timing" + inputJson.notes[a].timing + "Position" + LongNotesChild.transform.position);
                    Debug.Log("timing" + inputJson.notes[a].notes[0].timing + "Position" + LongNotesChild2.transform.position);
                    NotesPrefTiming.Add(inputJson.notes[a].timing + (CoolDownTime / 2));//ロングノーツ始点タイミングをリストに追加
                    LongNotesPrefTiming.Add(inputJson.notes[a].notes[0].timing);//ロングノーツ終点タイミングをリストに追加
                    NotesPrefTapTiming.Add(inputJson.notes[a].timing + CoolDownTime);
                    LongNotesPrefTapTiming.Add(inputJson.notes[a].notes[0].timing + CoolDownTime);









                    NotesPref[a].gameObject.SetActive(false);//ノーツを非表示にする
                    EndNotesPref[EndNotesPref.Count - 1].gameObject.SetActive(false);//終点ノーツを非表示









                    break;


            }




        }



        Debug.Log("譜面準備完了");


        key1 = player.Rane1Key;

        key2 = player.Rane2Key;

        key3 = player.Rane3Key;

        key4 = player.Rane4Key;


    }



    void Update()
    {
        OnStart();

        StartMusic();

        Timer();

        ActiveNote();

        ActiveLongNote();


        if (isHumenFire)
        {
            /*    
            Tap1();
            Tap2();
            Tap3();
            Tap4();

            */
        }
    }



    void OnStart()
    {

        if (isstart)
        {

            isHumenFire = true;
            if (nowtime > 5)
            {
                isstart = false;
                isPlay = true;
            }

        }

    }

    void StartMusic()
    {
        if (isPlay && isstart == false)
        {

            music.Play();

            isPlay = false;

        }


    }

    void Timer()
    {

        if (isHumenFire)
        {

            nowtime += Time.deltaTime;

        }
        debugTimetxt.text = "Time:" + nowtime;



    }

    void ActiveNote()
    {
        if (nowtime >= NotesPrefTiming[setactive])
        {

            NotesPref[setactive].gameObject.SetActive(true);
            setactive++;
        }

    }

    void ActiveLongNote()
    {
        if (nowtime >= LongNotesPrefTiming[setactiveLong])
        {
            EndNotesPref[setactiveLong].gameObject.SetActive(true);

            setactiveLong++;
        }

    }

    public void OnClickStartButton()
    {

        isstart = true;





    }


    void TapNote(GameObject tmp)
    {


        tmp.gameObject.SetActive(false);
    }

    void Tap1()
    {

        if (Input.GetKeyDown(key1))
        {
            GameObject[] note = GameObject.FindGameObjectsWithTag("Lane0");

            foreach (GameObject tmp in note)
            {
                Note notes = tmp.GetComponent<Note>();
              //  Debug.Log(nowtime);
                float timing = notes.notes.timing + CoolDownTime;
                float notetiming = Mathf.Abs(timing - nowtime);

                // Debug.Log("NoteTiming" + notetiming);

                if (notetiming < mintiming1)
                {
                    mintiming1 = notetiming;
                    notes1 = notes;

                    //Debug.Log("mintimingの中身を変更");
                }


            }


            //Debug.Log("最速" + mintiming1);

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
            else
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

        if (Input.GetKeyDown(key2))
        {
            GameObject[] note2 = GameObject.FindGameObjectsWithTag("Lane1");

            foreach (GameObject tmp in note2)
            {
                Note notes = tmp.GetComponent<Note>();
                float timing = notes.notes.timing + CoolDownTime;
             //   Debug.Log(nowtime);
                float notetiming = Mathf.Abs(timing - nowtime);

                if (notetiming < mintiming2)
                {

                    mintiming2 = notetiming;
                    notes2 = notes;
                }

            }
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
            else
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
        if (Input.GetKeyDown(key3))
        {
            GameObject[] note3 = GameObject.FindGameObjectsWithTag("Lane2");

            foreach (GameObject tmp in note3)
            {
                Note notes = tmp.GetComponent<Note>();
                float timing = notes.notes.timing + CoolDownTime;
            //    Debug.Log(nowtime);
                float notetiming = Mathf.Abs(timing - nowtime);

                if (notetiming < mintiming3)
                {

                    mintiming3 = notetiming;
                    notes3 = notes;
                }


            }
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
            else
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
        if (Input.GetKeyDown(key4))
          {
           GameObject[] note4 = GameObject.FindGameObjectsWithTag("Lane3");

           foreach (GameObject tmp in note4)
            {
               Note notes = tmp.GetComponent<Note>();
             //   Debug.Log(nowtime);
                float timing  = notes.notes.timing + CoolDownTime;

             float notetiming = Mathf.Abs(nowtime - timing);

                if(notetiming < mintiming4)
                {


                    mintiming4 = notetiming;

                    notes4 = notes;
                }
                 
           }

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
            else
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



