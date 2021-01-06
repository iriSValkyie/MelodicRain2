using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music
{
    public string Name;

    public AudioClip MusicData;

    public TextAsset NotesFile_easy;
    public TextAsset NotesFile_normal;
    public TextAsset NotesFile_hard;
    public TextAsset NotesFile_expart;


    public int BPM;

    public Dif Difficulty;

    public int Level;

    public Texture2D Jacket;


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

        NotesFile_easy = Resources.Load<TextAsset>(Name+ "_es");
        NotesFile_normal = Resources.Load<TextAsset>(Name + "_nor");
        NotesFile_hard = Resources.Load<TextAsset>(Name + "_har");
        NotesFile_expart = Resources.Load<TextAsset>(Name + "_ex");

        BPM = bpm;

        

        Level = level;

        Jacket = Resources.Load<Texture2D>(Name);

    }

   



}

