using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music
{
    public string Name;//名前

    public AudioClip MusicData;//音楽ファイル

    //難易度別の譜面ファイル
    public string NotesFile_easy;
    public string NotesFile_normal;
    public string NotesFile_hard;
    public string NotesFile_expart;


    public int BPM;//BPM(手動入力)

    public Dif Difficulty;//難易度(enumで選択可能)

    public int Level;//レベル

    public Texture2D Jacket;//ジャケット画像


    public int Score_easy;//最高スコア
    public int Score_normal;
    public int Score_hard;
    public int Score_expart;

    public int Combo_easy;//コンボ
    public int Combo_normal;
    public int Combo_hard;
    public int Combo_expart;


    public bool FullCombo_easy;//フルコンボかどうか
    public bool FullCombo_normal;
    public bool FullCombo_hard;
    public bool FullCombo_expart;

    public bool AllParfect_easy;//APかどうか
    public bool AllParfect_normal;
    public bool AllParfect_hard;
    public bool AllParfect_expart;

 

    public enum Dif
    {

        easy,
        normal,
        hard,
        expart,


    }


    public Music(string name, int bpm, int level)
    {
        Name = name;


        MusicData = Resources.Load<AudioClip>(Name);

        NotesFile_easy = (Name+ "_es");
        NotesFile_normal =(Name + "_nor");
        NotesFile_hard = (Name + "_har");
        NotesFile_expart =(Name + "_ex");

        BPM = bpm;

        

        Level = level;

        Jacket = Resources.Load<Texture2D>(Name);

        Score_easy = 0;
        Score_normal = 0;
        Score_hard = 0;
        Score_expart = 0;

        Combo_easy =0;
        Combo_normal= 0;
        Combo_hard = 0;
        Combo_expart = 0;


        FullCombo_easy = false;
        FullCombo_normal = false;
        FullCombo_hard = false; 
        FullCombo_expart = false;

        AllParfect_easy =false;
        AllParfect_normal = false;
        AllParfect_hard = false;
        AllParfect_expart = false;
    }

    public void AddScoreCombo(int score, int combo,string dif)//スコアとコンボを記載する
    {
        switch (dif)
        {
            case "easy":
                
                
            Score_easy = score;

            Combo_easy = combo;
                            
                            break;


            case "normal":

            Score_normal = score;

            Combo_normal = combo;

                            break;


            case "hard":
            
            Score_hard = score;

            Combo_hard = combo;


                           break;




            case "expart":

            Score_expart = score;

            Combo_expart = combo;

                           break;



        }
       


    }

   



}

