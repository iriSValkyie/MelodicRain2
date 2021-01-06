using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataBase : MonoBehaviour
{
    public List<Music> musics = new List<Music>();


    // Start is called before the first frame update
    void Awake()
    {
        musics.Add(new Music("inch", 191, 12));



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
