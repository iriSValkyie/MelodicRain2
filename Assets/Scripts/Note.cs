using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Note : MonoBehaviour
{

    const float TapPos= 982.5f;//叩く位置

    [SerializeField] float TapTiming;//タップする実際の秒数
   
   const float InactivePos = -1219f;//ノーツが消える位置



    public float TapTime;//ノーツが動き初めてからタップするまでの時間

    [SerializeField] float firetiming;//ノーツが動き出す時間

    GameObject GameManager;


    


    [SerializeField] GameController JsonCnotroller;//譜面を生成しタイマーを再生するスクリプト


   

    public float UserSpeed;//プレイヤーが設定した数値をTapTimeの時間を短くするために変換した数値

    public Notes notes;//ノーツのデータ


    public bool isfire;//発射されたかどうｋ

    public bool isTap;

    RectTransform pos;//ノーツの位置
   [SerializeField] float speed;//譜面の移動時間とタップする位置を計算したもの

   

    [SerializeField] Player player;//プレイヤースクリプト


    bool isBad;
    // Start is called before the first frame update

    void Start()
    {
        isTap = false;
        isBad = false;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");

        player = GameManager.GetComponent<Player>();



        UserSpeed = player.PlayerSpeed;


       // InvokeRepeating("CheckSpeed", 0, 0.3f);
       
       
        isfire = false;
       

        JsonCnotroller = GameManager.GetComponent<GameController>();
        

      pos= this.gameObject.GetComponent<RectTransform>();

        TapTiming = JsonCnotroller.CoolDownTime + notes.timing;//スタートするタイミングから譜面が再生される

        firetiming = TapTiming - TapTime;//発射される時間を タップする実際の時間からノーツが動く時間を引き求める

        speed = TapPos / TapTime;
       // Debug.Log(notes.num +"番のノーツの速度は" + speed + "です");


        
    }
   

    // Update is called once per frame
    void FixedUpdate()
    {
     
    }

     void Update()
    {

        if (JsonCnotroller.nowtime >= firetiming)
        {
            isfire = true;
            Move();
        }


        if (pos.anchoredPosition.y <= InactivePos )
        {
            isfire = false;

            JsonCnotroller.combo = 0;
            JsonCnotroller.Combo.text = 0.ToString();


        }
        if (isTap)
        {


            this.gameObject.SetActive(false);
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
            JsonCnotroller.combo = 0;
            JsonCnotroller.Combo.text = 0.ToString();

            isBad = true;
        }
        Debug.Log("オブジェクト削除");
        Destroy(gameObject);


    }


   void CheckSpeed()
    {
        UserSpeed = player.PlayerSpeed;

    }



    
    
}
