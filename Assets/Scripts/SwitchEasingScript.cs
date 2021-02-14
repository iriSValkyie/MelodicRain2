using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEasingScript : MonoBehaviour
{
    /*---           このスクリプトはuGui専用です   
                   
              pivotの位置の数値(anchorPosition)に合わせて動きます  
              のでこのスクリプトをアタッチした後pivotを変更しないで
              ください。位置の数値が変わってしまいます
                                                                    ----*/

    bool isOpened;

    [SerializeField] Vector2 OpenPos;

    [SerializeField] Vector2 ClosePos;


    [SerializeField] float easing = 0.1f;//いーじんぐ速度
    [SerializeField] string GetKey;//入力キー 

    [SerializeField] string GetKey2;//入力キー２
    RectTransform rectTransform;
    Vector2 diff;
    Vector2 v;

    [SerializeField] PullType pulltype;


    [SerializeField] float cooldown; //無操作時間


    float nowcooldowntime;

    bool isFinCoolDown;
    public enum PullType
    {
        Hold,
        Toggle,
        CoolDown,

    }



    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();

        isOpened = false;

        isFinCoolDown = false;
        nowcooldowntime = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        switch (pulltype)
        {
            case PullType.Hold:
                Hold();
                break;


            case PullType.Toggle:
                Toggle();
                break;

            case PullType.CoolDown:

                CoolDown();
                break;
        }



        switch (isOpened)
        {

            case true:

                diff = rectTransform.anchoredPosition - OpenPos;

                v = diff * easing;

                rectTransform.anchoredPosition -= v;

                if (diff.magnitude < 0.01f)
                {
                    break;
                }

                break;

            case false:




                diff = ClosePos - rectTransform.anchoredPosition;

                v = diff * easing;

                rectTransform.anchoredPosition += v;

                if (diff.magnitude < 0.01f)
                {

                    break;
                }


                break;

        }








        void CoolDown()
        {
            if (Input.GetKeyDown(GetKey) || Input.GetKeyDown(GetKey2))
            {
                isOpened = true;
                nowcooldowntime = cooldown;

            }

            if (isOpened ==true && nowcooldowntime >0)
            {
                nowcooldowntime -= Time.deltaTime;



            }
            else if(nowcooldowntime<0)
            {

                isOpened = false;

            }

        }
        void Hold()
        {

            if (Input.GetKeyDown(GetKey) || Input.GetKeyDown(GetKey2))
            {
                isOpened = true;

            }
            else
            {

                isOpened = false;
            }





        }

        void Toggle()
        {

            if (Input.GetKeyDown(GetKey) || Input.GetKeyDown(GetKey2))
            {
                isOpened = !isOpened;

            }
        }
    }
}
