using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultToggle : MonoBehaviour
{

    public string CurrentDifficulty;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void OnClick_easy()
    {
        CurrentDifficulty = "easy";

    }

    public void OnClick_Normal()
    {

        CurrentDifficulty = "normal";

    }

    public void OnClick_Hard()
    {

        CurrentDifficulty = "hard";

    }

    public void OnClick_Expart()
    {

        CurrentDifficulty = "expart";

    }
}
