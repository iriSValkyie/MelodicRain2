using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{


    public int Combo;
    [SerializeField] Text FrameRatetxt;






    //コンボ関係//
    [SerializeField] CanvasRenderer[] Combo001 = new CanvasRenderer[10];
    [SerializeField] CanvasRenderer[] Combo010 = new CanvasRenderer[10];
    [SerializeField] CanvasRenderer[] Combo100 = new CanvasRenderer[10];
    //---------//

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Update()
    {
        double rate = 1 / Time.deltaTime;



        FrameRatetxt.text = "FPS:" + (int)rate;
    }
}
