using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// uGUIをイージングさせて動かす
/// </summary>
public class SwitchEasingScript : MonoBehaviour
{
    /*---           このスクリプトはuGui専用です   
                   
              pivotの位置の数値(anchorPosition)に合わせて動きます  
              のでこのスクリプトをアタッチした後pivotを変更しないで
              ください。位置の数値が変わってしまいます
                                                                    ----*/
    bool isOpened;
    [Header("キーを押したときに表示される位置")]
    [SerializeField] Vector2 OpenPos;
    [Header("非表示の位置")]
    [SerializeField] Vector2 ClosePos;
    [Header("イージング速度")]
    [SerializeField] float easing ;
    [Header("入力キー")]
    [SerializeField] string GetKey;
    [Header("入力キー2(必要ない場合は入力キーと同じキーを必ず入れてください)")]
    [SerializeField] string GetKey2;
    RectTransform rectTransform;
    Vector2 diff;
    Vector2 v;
    [Header("動作タイプ")]
    [SerializeField] PullType pulltype;
    [Header("無操作時間(動作タイプがCoolDownの時のみ)")]
    [SerializeField] float cooldown; //無操作時間
    float nowcooldowntime;
    public enum PullType//動作タイプ
    {
        Hold,
        Toggle,
        CoolDown,
    }
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        rectTransform = gameObject.GetComponent<RectTransform>();
        isOpened = false;
    
        nowcooldowntime = cooldown;
    }
    // Update is called once per frame
    void Update()
    {
        //タイプ
        switch (pulltype)
        {
            case PullType.Hold:
                Hold();
                break;
            case PullType.Toggle:
                Toggle();
                break;
            case PullType.CoolDown:
                CoolDown();
                break;
        }
        //開いた時の動作
        switch (isOpened)
        {
            case true:
                diff = rectTransform.anchoredPosition - OpenPos;
                v = diff * easing;
                rectTransform.anchoredPosition -= v;
                if (diff.magnitude < 0.01f)
                {
                    break;
                }
                break;
            case false:
                diff = ClosePos - rectTransform.anchoredPosition;
                v = diff * easing;
                rectTransform.anchoredPosition += v;
                if (diff.magnitude < 0.01f)
                {
                    break;
                }
                break;
        }
    }
        public void CoolDown()//無操作状態が特定秒数続くと閉じる
        {
            if (Input.GetKeyDown(GetKey) || Input.GetKeyDown(GetKey2))
            {
                isOpened = true;
                nowcooldowntime = cooldown;
            }
            if (isOpened ==true && nowcooldowntime >0)
            {
                nowcooldowntime -= Time.deltaTime;
            }
            else if(nowcooldowntime<0)
            {
                isOpened = false;
            }
        }
       public void Hold()//押し続けてる時間 開く
        {
            if (Input.GetKey(GetKey) || Input.GetKey(GetKey2))
            {
                isOpened = true;
            }
            else
            {
                isOpened = false;
            }
        }
       public void Toggle()//押したら開閉する
        {
            if (Input.GetKeyDown(GetKey) || Input.GetKeyDown(GetKey2))
            {
                isOpened = !isOpened;
            }
        }
    
}
