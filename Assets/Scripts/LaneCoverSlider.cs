using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// レーンカバーの動作
/// 
/// </summary>

public class LaneCoverSlider : MonoBehaviour
{
    [SerializeField] Text ChangePosNum; //レーンカバーのテキスト 数値を入れる

    [SerializeField] Image LaneCovorImage; 

    [SerializeField] RectTransform lanecover; //レーンカバーのRectTransform

    [SerializeField]Vector2 laneinitpos = new Vector2(0,540); //初期位置

    [SerializeField] Vector2 laneMaxpos = new Vector2(0, -243); //可動部 最大位置


    [SerializeField] float scrollSpeed;//スクロール感度


    // Start is called before the first frame update
    void Start()
    {
        lanecover.anchoredPosition = laneinitpos;//レーンカバーを初期位置へ
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();

        ChangePositionNum();

    }
    void Scroll()
    {
        float scroll = Input.mouseScrollDelta.y;// マウスホイールの値を入れる

        lanecover.anchoredPosition += new Vector2(lanecover.anchoredPosition.x, Mathf.Clamp(lanecover.anchoredPosition.y,laneMaxpos.y, laneinitpos.y) * scroll * scrollSpeed * Time.deltaTime);//



    }
    void ChangePositionNum()// テキストに表示する情報を計算
    {
        float abs = Mathf.Abs(laneMaxpos.y - laneinitpos.y);

        float diff = Mathf.Abs(lanecover.anchoredPosition.y - laneMaxpos.y);

        ChangePosNum.text = (diff / abs *100).ToString();//可動範囲の何パーセント覆っているか計算してテキストに表示



    }
}
