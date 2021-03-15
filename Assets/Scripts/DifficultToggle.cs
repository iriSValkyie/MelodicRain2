using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DifficultToggle : MonoBehaviour
{

    public string CurrentDifficulty;


    [SerializeField] Toggle easy;
    [SerializeField] Toggle normal;
    [SerializeField] Toggle hard;
    [SerializeField] Toggle Expart;

    // Start is called before the first frame update
    void Start()
    {
        CheckisOn();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void OnClick_easy()
    {
        if (easy.isOn)
        {

            CurrentDifficulty = "easy";
        }
    }

    public void OnClick_Normal()
    {
        if (normal.isOn)
        {
            CurrentDifficulty = "normal";
        }
    }

    public void OnClick_Hard()
    {
        if (hard.isOn)
        {
            CurrentDifficulty = "hard";
        }
    }

    public void OnClick_Expart()
    {
        if (Expart.isOn)
        {
            CurrentDifficulty = "expart";
        }
    }


    void CheckisOn()
    {
        if (easy.isOn)
        {

            CurrentDifficulty = "easy";
        }
        if (normal.isOn)
        {
            CurrentDifficulty = "normal";
        }
        if (hard.isOn)
        {
            CurrentDifficulty = "hard";
        }

        if (Expart.isOn)
        {
            CurrentDifficulty = "expart";
        }

    }
}
