using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedSelect : MonoBehaviour
{
    public Text Speedtxt;

    [SerializeField][Range(1,15)]float value;
    // Start is called before the first frame update
    void Start()
    {

        

       Speedtxt.text = PlayerPrefs.GetFloat("NoteSpeed",6).ToString();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UP()
    {

        value = float.Parse(Speedtxt.text);

        value = Mathf.Clamp(value + 0.5f, 1, 15);

        Speedtxt.text = value.ToString();


    }

    public void Down()
    {

         value = float.Parse(Speedtxt.text);

        value = Mathf.Clamp(value - 0.5f,1,15);

        Speedtxt.text = value.ToString();



    }
}
