using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ノーツ情報
/// </summary>
[System.Serializable]
public class Notes
{
    public int num;//ライン番号
    public int block;//レーン番号
    public int LPB;//一拍のライン数
    public int type;//普通のノーツ(1)orロングノーツ(2)
    public float timing;//クールダウン含めない発射される時間
    public Notes[] notes;
    public GameObject NotesPrefab;
}
