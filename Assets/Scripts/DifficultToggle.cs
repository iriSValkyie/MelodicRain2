using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DifficultToggle : MonoBehaviour
{
    [Header("選択中の難易度")]
    public string CurrentDifficulty;

    [Header("各トグル")]
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

    /*トグルに変更が来るたびにCurrentDiffecultyを変更する*/
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
