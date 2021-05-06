using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    [Header("他スクリプト")]

    [SerializeField] Judge judge;

    [SerializeField] TransitionResultScene transitionResultScene;

    [SerializeField] LoadingSelectData LoadingSelectData;

    [SerializeField] Player player;

    [SerializeField] GameMenu gameMenu;

    [Header("ノーツプレハブ")]

    public GameObject notePrefab;
    public GameObject LongnotePrefab;


    /*各レーン座標*/
    const float lane0X = -203.7f;
    const float lane1X = -68.8f;
    const float lane2X = 68.6f;
    const float lane3X = 204f;

    [Header("クールダウン")]

    public float CoolDownTime = 4;//曲が始まるまでの空き時間です。譜面が0秒から始まるため空きを付けてます




   


    [Header("ノーツの親オブジェクト")]

    [SerializeField] GameObject notesParent;

    [Header("各ノーツの情報")]

    [SerializeField] Note NoteController;
    [SerializeField] Note EndNoteController;//ロングノーツの終点のノーツ
    [SerializeField] LongNote LongNoteController;

    

    float CurrentX; // 各ノーツのレーン座標


    bool isPlay; //曲再生用bool

    bool isstart;//演奏を開始するbool




    bool isHumenFire;//演奏時間を計測するbool

    
    

    

    [Header("演奏時間")]

    public float nowtime;




    float offsetTime;//譜面のオフセット(s)


    int setactive = 0;//Activeにするノーツの要素数
    int setactiveLong = 0;//Activeにするロングノーツの要素数


    [Header("各リスト")]

    [SerializeField] List<GameObject> NotesPref;//ノーツプレハブ
    [SerializeField] List<GameObject> EndNotesPref;//ロングノーツの終点のプレハブ
    [SerializeField] List<GameObject> LongNotePref;//ロングノーツのプレハブ
    [SerializeField] List<float> NotesPrefTiming;//各ノーツのActiveにするタイミング
    [SerializeField] List<float> LongNotesPrefTiming;//各ロングノーツのActiveにするタイミング



    [Header("音楽")]

    public AudioSource music;
    AudioClip Songfile;

    public bool isStopped;

    [Header("タップ時間のリスト")]

    [SerializeField] List<float> NotesPrefTapTiming;//各ノーツのタップする時間
    [SerializeField] List<float> LongNotesPrefTapTiming;//各ロングノーツのタップする時間

    [Header("譜面")]
    [SerializeField] Fumen inputJson;


    GameObject NotesChild;//生成したノーツ

    [Header("ノーツ数")]

    [SerializeField] int NotesNum;






    [Header("各スコアの理論値")]

    public int AllJustScore = 1000000;
    public int AllGreatScore = 900000;
    public int AllGoodScore = 700000;
    


  


    [Header("デバッグ用")]


    [SerializeField] bool isDebug;//Debug情報を取得するか

    [SerializeField] Text debugTimetxt;//演奏時間を表示するテキスト



    [SerializeField] Text FPS;//FPS表示テキスト
    void Start()
    {
        


        InitialNotes();

       
        
    }


   
    void InitialNotes()
    {
       

        if (isDebug)//デバッグモードかを検出する
        {

            debugTimetxt.color=new Color(1, 1, 1, 1);
            FPS.color = new Color(1, 1, 1, 1);

        }
        else
        {
            debugTimetxt.color = new Color(1, 1, 1, 0);
            FPS.color = new Color(1, 1, 1, 0);



        }

        isStopped = false;

        string inputString = LoadingSelectData.JsonText;//譜面データを取得

        Debug.Log(inputString);



        Songfile =  LoadingSelectData.music;//音楽を取得

        music.clip = Songfile;

        inputJson = JsonUtility.FromJson<Fumen>(inputString);
        Debug.Log("サンプリング周波数は" + music.clip.frequency + "です");
        offsetTime = (float)inputJson.offset / (float)music.clip.frequency;

        
        
        Debug.Log("Offset:" + inputJson.offset);
        Debug.Log("OffsetTime is" + offsetTime + "(s)!");
        Debug.Log("QueueMusic:" + inputJson.name);

        Debug.Log("Notes quantity is" + inputJson.notes.Length + "Notes");
           

        for (int a = 0; a < inputJson.notes.Length; a++)
        {
            switch (inputJson.notes[a].block)//レーンの検出
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

            
            switch (inputJson.notes[a].type)//ロングノーツの始点を検出(1.ノーツ2.ロングノーツ)
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
                    switch (inputJson.notes[a].block)//レーンの検出
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
                    EndNotesPref[EndNotesPref.Count - 1].gameObject.SetActive(  false);//終点ノーツを非表示







                    break;


            }


           
        }

        NotesNum = EndNotesPref.Count + NotesPref.Count; //合計コンボを計算


        /*1ノーツに対してのスコア数を計算*/
        judge.JustScore = AllJustScore / NotesNum;

        judge.GreatScore = AllGreatScore / NotesNum;

        judge.GoodScore = AllGoodScore / NotesNum;



        PlayerPrefs.SetInt("AllCombo", NotesNum);//PlayPrefsにセーブ
        PlayerPrefs.Save();

     



        Debug.Log("譜面準備完了");


       


    }

   



    

    void Update()
    {
        OnStart();

        StartMusic();

        Timer();

        ActiveNote();

        ActiveLongNote();


        





    }

    private void LateUpdate()
    {

        
       
    }
   

    void OnStart()
    {

        if (isstart)
        {

            isHumenFire = true;
            if (nowtime > CoolDownTime)//譜面が表示されるタイミングと音楽の再生のタイミングを合わせる
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
            transitionResultScene.isPlaying = true;

            gameMenu.MusicStarted = true;
        }


    }

    void Timer()
    {

        if (isHumenFire)//時間計測が開始
        {

            nowtime += Time.deltaTime;

        }
        debugTimetxt.text = "Time:" + nowtime;



    }

    void ActiveNote()
    {
        if (nowtime >= NotesPrefTiming[setactive])//activeのタイミングの時間が来たらノーツをActiveする
        {
            
            NotesPref[setactive].gameObject.SetActive(true);
            setactive++;
        }
        

    }

    void ActiveLongNote()
    {
        if (nowtime >= LongNotesPrefTiming[setactiveLong])//activeのタイミングの時間が来たらロングノーツをActiveする
        {
            EndNotesPref[setactiveLong].gameObject.SetActive(true);

            setactiveLong++;
        }
        

    }

    public void OnClickStartButton()
    {

        //ロードが終わると発火

        isstart = true;





    }

   



}



