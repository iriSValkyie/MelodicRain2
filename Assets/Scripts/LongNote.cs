using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class LongNote : MonoBehaviour
{
    [Header("ロングノーツの始点のノーツ")]
    public Notes StartNote;
    [Header("ロングノーツの終点のノーツ")]
    public Notes EndNote;
    [Header("他スクリプト")]
    [SerializeField]Note note;
    [Header("始点ノーツの座標")]
    public RectTransform startnote;
    [Header("終点ノーツの座標")]
    public RectTransform endnote;
    RectTransform longnote;//アタッチされているオブジェクトのTransform
    [Header("始点と終点の距離")]
    [SerializeField] float distance;
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
        if (startnote == null)
        {
            
        }
        else
        {
            distance = endnote.anchoredPosition.y - startnote.anchoredPosition.y;//距離を計算
        }
        longnote.sizeDelta = new Vector2(100, distance);//距離によってロングノーツの距離が延びる
      
           
        
        
    }
   
}
