using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 曲選択のトグルが変更された時曲名のテキストの色を変えるスクリプト
/// </summary>
public class MusicTitleToggle : MonoBehaviour
{
   [Header("テキスト")]
   public Text Label;

    [Header("トグル")]
   public Toggle toggle;
    [Header("非選択時の色")]
    [SerializeField] Color Unintaractive;
    [Header("選択時の色")]
    [SerializeField] Color intaractive;



    float colorBalance = 0;//カラーバランス

    float Speed = 3f;
    bool isBlack;
    bool isWhite;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //黒にする時
        if (isBlack)
        {

            Label.color = Color.Lerp(intaractive,Unintaractive,colorBalance);

            colorBalance += Time.deltaTime * Speed;

            if(colorBalance >= 1)
            {
                isBlack = false;

                colorBalance = 0;

            }

        }

        //白にする時
        if (isWhite)
        {

            Label.color = Color.Lerp(Unintaractive,intaractive,colorBalance);

            colorBalance += Time.deltaTime * Speed;

            if (colorBalance >= 1)
            {
                isWhite = false;

                colorBalance = 0;

            }


        }
    }

    public void OnValuechange()//トグルが変更された時に色を変える
    {
        if (toggle.isOn)
        {
            

            isWhite = true;





        }else
        {
            isBlack = true;



        }




    }
   
   
}
