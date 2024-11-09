using System.Collections;
using System.Collections.Generic;
using MelodicRain2;
using UnityEngine;

public class NotesLoaderTester : MonoBehaviour
{
    async void Start()
    {
        NotesLoader notesLoader = new NotesLoader();
        NotesData notesData = 
            await notesLoader.Load("file://" + Application.streamingAssetsPath + "/Songs/inch/inch_hard.json");
        Debug.Log($"NotesData Loaded!:{notesData.name}, BPM:{notesData.BPM} MaxBlock:{notesData.maxBlock} Offset:{notesData.offset}");
    }
}
