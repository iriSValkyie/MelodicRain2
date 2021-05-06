using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ノーツの動作
/// </summary>
public class Note : MonoBehaviour
{

    const float TapPos= 982.5f;//叩く位置

    [Header("タップ時間")]

    [SerializeField] float TapTiming;//タップする実際の秒数
   
   const float InactivePos = -1219f;//ノーツが消える位置


   
    public float TapTime;//ノーツが動き初めてからタップするまでの時間



    float oldTapTime;//スピード変更前のTapTime


    [Header("発火時間")]
    [SerializeField] float firetiming;//ノーツが動き出す時間


    float oldfiretiming;




    [Header("他スクリプト関連")]
    GameObject GameManager;


    [SerializeField] Judge judge;

    

    [SerializeField] GameController JsonCnotroller;//譜面を生成しタイマーを再生するスクリプト


    [SerializeField] Player player;//プレイヤースクリプト

    public float UserSpeed;//プレイヤーが設定した数値をTapTimeの時間を短くするために変換した数値


    float oldUserSpeed;



    public Notes notes;//ノーツのデータ


    public bool isfire;//発射されたかどうか

    public bool isTap;//タップされたかどうか

    RectTransform pos;//ノーツの位置

    [Header("スピード関連")]

   [SerializeField] float speed;//譜面の移動時間とタップする位置を計算したもの

    float oldspeed;//スピード変更前のスピード



    


    bool isBad;
    // Start is called before the first frame update

    void Start()
    {
        isTap = false;
        isBad = false;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");

        player = GameManager.GetComponent<Player>();

        judge = GameManager.GetComponent<Judge>();

        UserSpeed = player.PlayerSpeed;

        oldUserSpeed = UserSpeed;


        TapTime = (15.5f - UserSpeed)/10;//15.5 = UserSpeedのMaxの値 画面で移動する時間
        oldTapTime = TapTime;


      //  Debug.Log("TapTime =" + TapTime + "S");
       
       
       
        isfire = false;
       

        JsonCnotroller = GameManager.GetComponent<GameController>();
        

        pos= this.gameObject.GetComponent<RectTransform>();

        TapTiming = JsonCnotroller.CoolDownTime + notes.timing;//スタートするタイミングから譜面が再生される

        firetiming = TapTiming - TapTime;//発射される時間を タップする実際の時間からノーツが動く時間を引き求める
        oldfiretiming = firetiming;

        speed = TapPos / TapTime;
        oldspeed = speed;


       // Debug.Log(notes.num +"番のノーツの速度は" + speed + "です");


        
    }
   

    // Update is called once per frame
    void FixedUpdate()
    {
        if (JsonCnotroller.nowtime >= firetiming)
        {
            isfire = true;
            Move();
        }
    }

     void Update()
    {

        
        if (isTap)// タップされたら
        {


            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

        if (pos.anchoredPosition.y <= InactivePos )//タップされずに特定の座標を過ぎると
        {
            isfire = false;


            NoteDestroy();

        }
       

    }


    private void LateUpdate()
    {
        
        if(oldUserSpeed != player.PlayerSpeed)//スピード変更
        {   
            UserSpeed = player.PlayerSpeed;
            
            TapTime = (15.5f - UserSpeed)/10;

            firetiming = TapTiming - TapTime;

            speed = TapPos / TapTime;

            oldUserSpeed = UserSpeed;

            
            

        }
        
       
    }





    void Move()
    {
         
        pos.anchoredPosition -= new Vector2(0, speed  * Time.deltaTime);

    }

    void NoteDestroy()
    {


        if (isBad == false)
        {
            
           
            judge.ResetAlpha();
            judge.bad.SetAlpha(1);
            Destroy(gameObject);
            isBad = true;

            judge.combo = 0;
        }
        //Debug.Log("オブジェクト削除");
        


    }


 



    
    
}
