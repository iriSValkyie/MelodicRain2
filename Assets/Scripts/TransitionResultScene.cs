using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TransitionResultScene : MonoBehaviour
{

    [SerializeField] GameController gameController;

    [SerializeField] Judge judge;


    [SerializeField] AudioSource audio;

    [SerializeField] Image background;


    [SerializeField] float fadeinTime;

    float speed;

     bool isFadeOut;

    public bool isPlaying;
     // Start is called before the first frame update
    void Start()
    {
        isFadeOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeOut)
        {

             speed = speed + fadeinTime * Time.deltaTime;

                background.color = new Color(0f, 0f, 0f, speed);
            if(background.color.a >= 1)
            {
                Finish();


            }
        }
    }

    private void LateUpdate()
    {

        if (audio.time == 0 && !audio.isPlaying && isPlaying)
        {
            isFadeOut = true;


        }

    }


    void Finish()
    {


       


        

        PlayerPrefs.SetInt("Just", judge.score.Just);

        PlayerPrefs.SetInt("Great", judge.score.Great);

        PlayerPrefs.SetInt("Good", judge.score.Good);

        PlayerPrefs.SetInt("Bad", judge.score.Bad);

        PlayerPrefs.SetInt("Score", judge.Score);

        PlayerPrefs.SetInt("Combo", judge.Maxcombo);

        PlayerPrefs.Save();

        fadeinTime = 1f * fadeinTime / 10f;


        SceneManager.LoadScene("ResultScene");

        //  StartCoroutine(FadeOut());

    }



    
    
}
