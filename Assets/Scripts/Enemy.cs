using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float v = -1, h;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//上右下左
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;

    //计时器
    private float timeVal = 3;
    private float timeValChangeDirection;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        //攻击的时间间隔
        if (timeVal >= 3)
        {
            timeVal = 0;
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    /// <summary>
    /// 固定物理帧 Update之后执行
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }
    /// <summary>
    /// 坦克的攻击方法
    /// </summary>
    private void Attack()
    {
        //transform.rotation是旋转角度
        //子弹产生的角度：当前坦克的角度+子弹旋转的角度 四元素变量
        Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
        timeVal = 0;
    }
    /// <summary>
    /// 坦克的移动方法
    /// </summary>
    private void Move()
    {
        if (timeValChangeDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 4)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num == 1 || num == 2)
            {
                h = -1;
                v = 0;
            }
            else
            {
                h = 1;
                v = 0;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
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
        if (v != 0)
        {
            return;
        }
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
        //玩家分数加1
        PlayerManager.Instance.playerScore++;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
    /// <summary>
    /// 触发条件总结：
    /// 1 碰撞双方必须是碰撞体
    /// 2 被动体必须是个刚体
    /// </summary>
    /// <param name="collision"></param>
    //private void OnCollisionEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        timeValChangeDirection = 4;
    //    }
    //}
}
