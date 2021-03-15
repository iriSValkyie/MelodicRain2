using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{


    public int Combo;
    [SerializeField] Text FrameRatetxt;






  
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
