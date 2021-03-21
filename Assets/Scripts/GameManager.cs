using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    [Header("フレームレート制限")]
    
    [SerializeField] Text FrameRatetxt;//フレームレートテキスト






  
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;//フレームレートを設定
    }

    // Update is called once per frame
    void Update()
    {
        //フレームレートを計算


        double rate = 1 / Time.deltaTime;



        FrameRatetxt.text = "FPS:" + (int)rate;
    }
}
