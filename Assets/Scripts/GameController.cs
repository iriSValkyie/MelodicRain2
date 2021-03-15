using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{


    [SerializeField] LoadingSelectData LoadingSelectData;
   

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
    
    



    public Text Combo;

    public int combo;



    public Score score;


    void Start()
    {



        InitialNotes();

        mintiming1 = 100;
        mintiming2 = 100;
        mintiming3 = 100;
        mintiming4 = 100;



        key1 = player.Rane1Key;

        key2 = player.Rane2Key;

        key3 = player.Rane3Key;

        key4 = player.Rane4Key;


    }


   
    void InitialNotes()
    {
        score = new Score();




        string inputString = LoadingSelectData.JsonText;

        Debug.Log(inputString);

        music.clip = LoadingSelectData.music;



        inputJson = JsonUtility.FromJson<Fumen>(inputString);
        Debug.Log("サンプリング周波数は" + music.clip.frequency + "です");
        offsetTime = (float)inputJson.offset / (float)music.clip.frequency;

        
        
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
                    NotesChild = Instantiate(notePrefab, new Vector2(CurrentX,0), Quaternion.identity, notesParent.transform);

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
                    NotesRect.anchoredPosition3D = new Vector3(CurrentX, 0,0);//アンカーポジションを修正
                        
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
                    LongNotesRect2.anchoredPosition3D = new Vector3(CurrentX,0,0);//アンカーポジションを修正


                    RectTransform LongNotesRect = LongNotesChild.GetComponent<RectTransform>();//インスタンス化したオブジェクトのRectTransformを取得
                    LongNotesRect.anchoredPosition3D = new Vector3(CurrentX,0,0);//アンカーポジションを修正


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



 
    
}



