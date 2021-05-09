using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OperationMethod : MonoBehaviour
{
    [Header("キーテキスト")]
    [SerializeField] List<Text> KeyTexts = new List<Text>();
    [Header("他スクリプト")]
    [SerializeField] Player player;

   
    // Start is called before the first frame update
    void Start()
    {

        //各レーンに割り当てられているキーをテキストで表示
        KeyTexts[0].text = player.Rane1Key.ToUpper();
        KeyTexts[1].text = player.Rane2Key.ToUpper();
        KeyTexts[2].text = player.Rane3Key.ToUpper();
        KeyTexts[3].text = player.Rane4Key.ToUpper();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
