using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    public Notes StartNote;


    public Notes EndNote;


    [SerializeField]Note note;

    public RectTransform startnote;

    public RectTransform endnote;


    RectTransform longnote;

    float StartPos;

    float EndPos;

   [SerializeField] float distance;

    float mid;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {

        


        longnote = this.gameObject.GetComponent<RectTransform>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {



        distance = endnote.anchoredPosition.y - startnote.anchoredPosition.y;






        longnote.sizeDelta = new Vector2(100, distance);

      


           

        
        

    }




   
}
