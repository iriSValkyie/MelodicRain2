using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaneToggle : MonoBehaviour
{

    [SerializeField] ReadingSongsFolder readingSongsFolder;

    public string CurrentToggle;

    [Header("トグル")]

    [SerializeField] Toggle Toggle1;
    [SerializeField] Toggle Toggle2;
    [SerializeField] Toggle Toggle3;
    [SerializeField] Toggle Toggle4;


    [Header("レーンカバーアイコン")]

    [SerializeField] RawImage toggle1;
    [SerializeField] RawImage toggle1Unselect;
    [SerializeField] RawImage toggle2;
    [SerializeField] RawImage toggle2Unselect;
    [SerializeField] RawImage toggle3;
    [SerializeField] RawImage toggle3Unselect;
    [SerializeField] RawImage toggle4;
    [SerializeField] RawImage toggle4Unselect;
    // Start is called before the first frame update
    void Start()
    {



        LaneCovor1();
        LaneCovor2();
        LaneCovor3();
        LaneCovor4();



        if (readingSongsFolder.RaneCovorIcon[1] != null)
        {
            toggle1.texture = readingSongsFolder.RaneCovorIcon[1];
            toggle1Unselect.texture = readingSongsFolder.RaneCovorIcon[1];
        }

        if (readingSongsFolder.RaneCovorIcon[2] != null)
        {
            toggle2.texture = readingSongsFolder.RaneCovorIcon[2];
            toggle2Unselect.texture = readingSongsFolder.RaneCovorIcon[2];
        }
        if (readingSongsFolder.RaneCovorIcon[3] != null)
        {
            toggle3.texture = readingSongsFolder.RaneCovorIcon[3];
            toggle3Unselect.texture = readingSongsFolder.RaneCovorIcon[3];
        }
        if (readingSongsFolder.RaneCovorIcon[4] != null)
        {
            toggle4.texture = readingSongsFolder.RaneCovorIcon[4];
            toggle4Unselect.texture = readingSongsFolder.RaneCovorIcon[4];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LaneCovor1()
    {
        if (Toggle1.isOn)
        {

            CurrentToggle = "LaneCover1";


        }
    }
    public void LaneCovor2()
    {

        if (Toggle2.isOn)
        {

            CurrentToggle = "LaneCover2";


        }
    }
    public void LaneCovor3()
    {

        if (Toggle3.isOn)
        {


            CurrentToggle = "LaneCover3";

        }
    }
    public void LaneCovor4()
    {

        if (Toggle4.isOn)
        {


            CurrentToggle = "LaneCover4";

        }


    }


}
