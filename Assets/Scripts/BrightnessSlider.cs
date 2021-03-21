using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BrightnessSlider : MonoBehaviour
{
    [Header("明るさ変更用Image")]
    [SerializeField] Image Brightness;
    [Header("スライダー")]
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Brightness.color = new Color(0, 0, 0, PlayerPrefs.GetFloat("Brightness", 0));//明るさを0にする
        ChangeValue();
    }

    // Update is called once per frame


    public void ChangeValue() 
    {
        /*--明るさをスライダーの数値によって変更する--*/

        Brightness.color = new Color(0,0,0,slider.value);
       

    } 
}
