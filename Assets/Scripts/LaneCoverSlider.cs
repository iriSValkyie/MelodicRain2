using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LaneCoverSlider : MonoBehaviour
{
    [SerializeField] Text ChangePosNum;

    [SerializeField] Image LaneCovorImage;

    [SerializeField] RectTransform lanecover;

    [SerializeField]Vector2 laneinitpos = new Vector2(0,540);

    [SerializeField] Vector2 laneMaxpos = new Vector2(0, -243);


    [SerializeField] float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        lanecover.anchoredPosition = laneinitpos;
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();

        ChangePositionNum();

    }
    void Scroll()
    {
        float scroll = Input.mouseScrollDelta.y;

        lanecover.anchoredPosition += new Vector2(lanecover.anchoredPosition.x, Mathf.Clamp(lanecover.anchoredPosition.y,laneMaxpos.y, laneinitpos.y) * scroll * scrollSpeed * Time.deltaTime);



    }
    void ChangePositionNum()
    {
        float abs = Mathf.Abs(laneMaxpos.y - laneinitpos.y);

        float diff = Mathf.Abs(lanecover.anchoredPosition.y - laneMaxpos.y);

        ChangePosNum.text = (diff / abs).ToString();



    }
}
