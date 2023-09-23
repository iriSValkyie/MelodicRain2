using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Fumen
{
    public string name;//曲名
    public int maxBlock;//最大レーン数(今回4レーンだが読み込まないので関係なし)
    public int BPM;//BPM
    public int offset;//曲と譜面のズレ
    public Notes[] notes;//ノーツのタイミングなどのノーツ情報
   
}