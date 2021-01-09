using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{

    bool isOpened;

    [SerializeField] Vector2 OpenPos;

    [SerializeField]Vector2 ClosePos;


   [SerializeField] float easing = 0.1f;//いーじんぐ速度
   [SerializeField] string GetKey;//入力キー 


    RectTransform rectTransform;
    Vector2 diff;
    Vector2 v;

    [SerializeField] PullType pulltype;

    
    public enum PullType
    {
        Hold,
        Toggle,


    }
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();

        isOpened = false;
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
    }

    void Hold()
    {

        if (Input.GetKey(GetKey))
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

        if (Input.GetKeyDown(GetKey))
        {
            isOpened = !isOpened;

        }
    }
}
