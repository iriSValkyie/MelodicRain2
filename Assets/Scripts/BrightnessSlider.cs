using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BrightnessSlider : MonoBehaviour
{

    [SerializeField] Image Brightness;
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Brightness.color = new Color(0, 0, 0, PlayerPrefs.GetFloat("Brightness", 0));
        ChangeValue();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void ChangeValue() 
    {

        Debug.Log(255 * slider.value);
        Brightness.color = new Color(0,0,0,slider.value);
       

    } 
}
