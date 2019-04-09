using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float timeVal;
    private float defendedTimeVal = 3;//保护玩家的时间
    private bool isDefended = true;//是否保护玩家
    
    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//上右下左
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    //控制音效的播放
    public AudioSource moveAudio;
    //拿到音效的资源
    public AudioClip[] tankAudio;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //是否处于无敌状态
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendedTimeVal -= Time.deltaTime;
            if (defendedTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 固定物理帧 Update之后执行
    /// </summary>
    private void FixedUpdate()
    {
        //游戏失败不能攻击了，禁止玩家的一切行为
        if (PlayerManager.Instance.isDefeat)
            return;
        Move();
        //计时攻击 攻击的CD
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    /// <summary>
    /// 坦克的攻击方法
    /// </summary>
    private void Attack()
    {
        //按下空格实例化一个子弹
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //transform.rotation是旋转角度
            //子弹产生的角度：当前坦克的角度+子弹旋转的角度 四元素变量
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
            timeVal = 0;
        }
    }
    /// <summary>
    /// 坦克的移动方法
    /// </summary>
    private void Move()
    {
        //玩家Y轴输入
        float v = Input.GetAxisRaw("Vertical");
        //玩家水平轴输入
        float h = Input.GetAxisRaw("Horizontal");
        //控制玩家在Y轴上的移动
        transform.Translate(Vector3.up * v * moveSpeed * Time.deltaTime, Space.World);
        if (v < 0)//朝向下边
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }
        if (v > 0)//朝向上边
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }
        if (Mathf.Abs(v) > 0.05f || Mathf.Abs(h) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];
        }
        else
        {
            moveAudio.clip = tankAudio[0];
        }
        if (!moveAudio.isPlaying)//判断是否正在播放
        {
            moveAudio.Play();
        }
        if (v != 0)
            return;
        //控制玩家在水平轴上的移动 参数1：移动方向的速度 参数2：方向轴 Space.World为事件方向轴
        //Time.deltaTime按照时间移动
        transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime, Space.World);
        if (h < 0)//朝向左边
        {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        if (h > 0)//朝向右边
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }
    }
    /// <summary>
    /// 坦克的死亡方法
    /// </summary>
    public void Die()
    {
        if (isDefended)
            return;
        PlayerManager.Instance.isDead = true;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
}
