using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// スピードスライダーを調整するスクリプト
/// </summary>
public class SpeedSelect : MonoBehaviour
{
    [Header("スピード")]
    public Text Speedtxt;
    [Header("計算前スピード")]
    [SerializeField][Range(1,15)]float value;
    // Start is called before the first frame update
    void Start()
    {
        
        //設定したスピードを表示する(初期値は6)
       Speedtxt.text = PlayerPrefs.GetFloat("NoteSpeed",6).ToString();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UP()//スピードを上げた時の動作
    {
        value = float.Parse(Speedtxt.text);
        value = Mathf.Clamp(value + 0.5f, 1, 15);
        Speedtxt.text = value.ToString();
    }
    public void Down()//スピードを下げた時の動作
    {
         value = float.Parse(Speedtxt.text);
        value = Mathf.Clamp(value - 0.5f,1,15);
        Speedtxt.text = value.ToString();
    }
}
