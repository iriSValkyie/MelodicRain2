using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicTitleToggle : MonoBehaviour
{
   public Text Label;
   public Toggle toggle;

    [SerializeField] Color Unintaractive;

    [SerializeField] Color intaractive;
    float colorBalance = 0;

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

    public void OnValuechange()
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
