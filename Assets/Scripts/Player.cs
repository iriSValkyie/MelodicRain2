using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player : MonoBehaviour
{

    /// <summary>
    /// プレイヤーの操作やプレイヤーのオプションの情報の補完場所
    /// </summary>


    //判定//

    GameObject[] Notes1;
    GameObject[] Notes2;
    GameObject[] Notes3;
    GameObject[] Notes4;


    GameController jsoncontroller;



/*
    float tappos = 982.5f;

    float JUST = 0.025f;

    float GREAT = 0.0417f;


    float GOOD = 0.0583f;

    */

    GameObject MostNearNote1;
    GameObject MostNearNote2;
    GameObject MostNearNote3;
    GameObject MostNearNote4;


    int combo;

    [SerializeField] Text Combo;//コンボテキスト
    //----//



    //譜面速度関連//
    public float PlayerSpeed;
    [SerializeField] Text SpeedTxt;
    [SerializeField] Slider SpeedSlider;

    //---------//


    //タップ関連//
    [SerializeField] AudioSource SE;
    public string Rane1Key;
    public string Rane2Key;
    public string Rane3Key;
    public string Rane4Key;
    [SerializeField] RectTransform rane1;
    [SerializeField] RectTransform rane2;
    [SerializeField] RectTransform rane3;
    [SerializeField] RectTransform rane4;

    Vector2 Tappos = new Vector2(140,200);
    Vector2 UnTapPos = new Vector2(140, 0);


    Vector2 diff1;
    Vector2 diff2;
    Vector2 diff3;
    Vector2 diff4;

    Vector2 v1;
    Vector2 v2;
    Vector2 v3;
    Vector2 v4;

   [SerializeField] float easing = 0.2f;



    bool isTap1;
    bool isTap2;
    bool isTap3;
    bool isTap4;

    //--------------------//



    // Start is called before the first frame update
    void Start()
    {
        isTap1 = false;
        isTap2 = false;
        isTap3 = false;
        isTap4 = false;



        rane1.sizeDelta = new Vector2(140, 0);
        rane2.sizeDelta = new Vector2(140, 0);
        rane3.sizeDelta = new Vector2(140, 0);
        rane4.sizeDelta = new Vector2(140, 0);
        SpeedTxt.text = PlayerSpeed.ToString();
    }

    // Update is called once per frame


    private void Update()
    {
        KeySE();
    }
    void LateUpdate()
    {
        SpeedChange();


        KeyControll();


        KeyImageEasi1(rane1);
        KeyImageEasi2(rane2);
        KeyImageEasi3(rane3);
        KeyImageEasi4(rane4);

       
        //Debug.Log(diff1);
       // Debug.Log(isTap1);
    }



    void SpeedChange()
    {
        if(Input.GetKeyDown("page up"))
        {
            SpeedSlider.value += 1;
            PlayerSpeed = (float)SpeedSlider.value / 2;

            SpeedTxt.text = PlayerSpeed.ToString();

        }
        if(Input.GetKeyDown("page down"))
        {
            SpeedSlider.value -= 1;
            PlayerSpeed = (float)SpeedSlider.value / 2;
            SpeedTxt.text = PlayerSpeed.ToString();
        }
    }

    void KeyControll()
    {

        if (Input.GetKey(Rane1Key))
        {
            isTap1 = true;
            
        }
        else
        {

            isTap1 = false;

            
        }
        if (Input.GetKey(Rane2Key))
        {
            isTap2 = true;
        }
        else
        {

            isTap2 = false;
        }
        if (Input.GetKey(Rane3Key))
        {
            isTap3 = true;
        }
        else
        {

            isTap3 = false;
        }
        if (Input.GetKey(Rane4Key))
        {
            isTap4 = true;
        }
        else
        {

            isTap4 = false;
        }
    }

    void KeySE()
    {
        if (Input.GetKeyDown(Rane1Key))
        {

            SE.PlayOneShot(SE.clip);
          //  JudgeLane1();
        }
        if (Input.GetKeyDown(Rane2Key))
        {

            SE.PlayOneShot(SE.clip);
          //  JudgeLane2();
        }
        if (Input.GetKeyDown(Rane3Key))
        {

            SE.PlayOneShot(SE.clip);
          //  JudgeLane3();
        }
        if (Input.GetKeyDown(Rane4Key))
        {

            SE.PlayOneShot(SE.clip);
           // JudgeLane4();
        }
    }
    void KeyImageEasi1(RectTransform rect)
    {

        switch (isTap1)
        {



            case true:


                diff1 = rect.sizeDelta - Tappos ;

                 v1 =  diff1* easing;

                rect.sizeDelta -= v1;

                if (diff1.magnitude < 0.01)
                {

                    break;
                }
                
                break;




            case false:

                diff1 = UnTapPos - rect.sizeDelta ;

                v1 = diff1 * easing;

                rect.sizeDelta += v1;

                if (diff1.magnitude <  0.01)
                {
                    break;

                }


                break;



        }
        
    }
    void KeyImageEasi2(RectTransform rect)
    {

        switch (isTap2)
        {



            case true:


                diff2 = rect.sizeDelta - Tappos;

                v2 = diff2 * easing;

                rect.sizeDelta -= v2;

                if (diff2.magnitude < 0.01)
                {

                    break;
                }

                break;




            case false:

                diff2 = UnTapPos - rect.sizeDelta;

                v2 = diff2 * easing;

                rect.sizeDelta += v2;

                if (diff2.magnitude < 0.01)
                {
                    break;

                }


                break;



        }

    }
    void KeyImageEasi3(RectTransform rect)
    {

        switch (isTap3)
        {



            case true:


                diff3 = rect.sizeDelta - Tappos;

                v3 = diff3 * easing;

                rect.sizeDelta -= v3;

                if (diff3.magnitude < 0.01)
                {

                    break;
                }

                break;




            case false:

                diff3 = UnTapPos - rect.sizeDelta;

                v3 = diff3 * easing;

                rect.sizeDelta += v3;

                if (diff3.magnitude < 0.01)
                {
                    break;

                }


                break;



        }

    }
    void KeyImageEasi4(RectTransform rect)
    {

        switch (isTap4)
        {



            case true:


                diff4 = rect.sizeDelta - Tappos;

                v4 = diff4 * easing;

                rect.sizeDelta -= v4;

                if (diff4.magnitude < 0.01)
                {

                    break;
                }

                break;




            case false:

                diff4 = UnTapPos - rect.sizeDelta;

                v4 = diff4 * easing;

                rect.sizeDelta += v4;

                if (diff4.magnitude < 0.01)
                {
                    break;

                }


                break;



        }

    }

    /*

    void JudgeLane1()
    {
        Notes1 = GameObject.FindGameObjectsWithTag("Lane0");

        float Taptiming = jsoncontroller.nowtime;


        foreach(GameObject Note in Notes1)
        {

           
            if(Mathf.Abs(Note.transform.position.y -tappos) < Mathf.Abs(MostNearNote1.transform.position.y -tappos))
            {

                MostNearNote1 = Note;
                Debug.Log("距離が近いものを検出");
            }







        }

        Note note = MostNearNote1.GetComponent<Note>();

        float taprag = Mathf.Abs(Taptiming - (note.notes.timing + jsoncontroller.CoolDownTime));

        if (taprag < JUST) {

            Debug.Log("JUST");

            combo++;
            Combo.text = combo.ToString();
        
        }else if(taprag >JUST && taprag< GREAT)
        {

            Debug.Log("GREAT");
            combo++;
            Combo.text = combo.ToString();
        }
        else if(taprag > GREAT && taprag < GOOD)
        {

            Debug.Log("GOOD");
            combo = 0;
            Combo.text = combo.ToString();
        }
        else if(taprag > GOOD)
        {


            Debug.Log("BAD");
            combo = 0;
            Combo.text = combo.ToString();

        }
    }

    void JudgeLane2()
    {


        Notes2 = GameObject.FindGameObjectsWithTag("Lane1");

        float Taptiming = jsoncontroller.nowtime;


        foreach (GameObject Note in Notes2)
        {


            if (Mathf.Abs(Note.transform.position.y - tappos) < Mathf.Abs(MostNearNote2.transform.position.y - tappos))
            {

                MostNearNote2 = Note;
                Debug.Log("距離が近いものを検出");
            }







        }

        Note note = MostNearNote2.GetComponent<Note>();

        float taprag = Mathf.Abs(Taptiming - (note.notes.timing + jsoncontroller.CoolDownTime));

        if (taprag < JUST)
        {

            Debug.Log("JUST");
            combo++;
            Combo.text = combo.ToString();
        }
        else if (taprag > JUST && taprag < GREAT)
        {

            Debug.Log("GREAT");
            combo++;
            Combo.text = combo.ToString();
        }
        else if (taprag > GREAT && taprag < GOOD)
        {

            Debug.Log("GOOD");
            combo = 0;
            Combo.text = combo.ToString();
        }
        else if (taprag > GOOD)
        {


            Debug.Log("BAD");
            combo = 0;
            Combo.text = combo.ToString();

        }


    }

    void JudgeLane3()
    {

        Notes3 = GameObject.FindGameObjectsWithTag("Lane2");

        float Taptiming = jsoncontroller.nowtime;


        foreach (GameObject Note in Notes3)
        {


            if (Mathf.Abs(Note.transform.position.y - tappos) < Mathf.Abs(MostNearNote3.transform.position.y - tappos))
            {

                MostNearNote3 = Note;
                Debug.Log("距離が近いものを検出");
            }







        }

        Note note = MostNearNote3.GetComponent<Note>();

        float taprag = Mathf.Abs(Taptiming - (note.notes.timing + jsoncontroller.CoolDownTime));

        if (taprag < JUST)
        {

            Debug.Log("JUST");
            combo++;
            Combo.text = combo.ToString();
        }
        else if (taprag > JUST && taprag < GREAT)
        {

            Debug.Log("GREAT");
            combo++;
            Combo.text = combo.ToString();
        }
        else if (taprag > GREAT && taprag < GOOD)
        {

            Debug.Log("GOOD");
            combo = 0;
            Combo.text = combo.ToString();
        }
        else if (taprag > GOOD)
        {


            Debug.Log("BAD");
            combo = 0;
            Combo.text = combo.ToString();

        }
    }

    void JudgeLane4()
    {


        Notes4 = GameObject.FindGameObjectsWithTag("Lane3");

        float Taptiming = jsoncontroller.nowtime;


        foreach (GameObject Note in Notes4)
        {


            if (Mathf.Abs(Note.transform.position.y - tappos) < Mathf.Abs(MostNearNote4.transform.position.y - tappos))
            {

                MostNearNote4= Note;
                Debug.Log("距離が近いものを検出");
            }







        }

        Note note = MostNearNote4.GetComponent<Note>();

        float taprag = Mathf.Abs(Taptiming - (note.notes.timing + jsoncontroller.CoolDownTime));

        if (taprag < JUST)
        {

            Debug.Log("JUST");
            combo++;
            Combo.text = combo.ToString();

        }
        else if (taprag > JUST && taprag < GREAT)
        {

            Debug.Log("GREAT");
            combo++;
            Combo.text = combo.ToString();
        }
        else if (taprag > GREAT && taprag < GOOD)
        {

            Debug.Log("GOOD");
            combo = 0;
            Combo.text = combo.ToString();
        }
        else if (taprag > GOOD)
        {


            Debug.Log("BAD");
            combo = 0;
            Combo.text = combo.ToString();


        }




    }
    */
}


    