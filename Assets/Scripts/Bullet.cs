using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;
    public bool isPlayerBullect;//默认为false

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Space.World代表的方向是指沿着事件坐标系方向移动
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }
    /// <summary>
    /// 刚体触发自动调用此方法OnTriggerEnter2D
    /// collision是触发器碰到的返回物体
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullect)//敌人的子弹销毁玩家和敌人的子弹
                {
                    //触发Player中的Die方法
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                //调用家的脚本中的Die方法
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullect)//玩家的子弹销毁敌人和自己的子弹
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                //销毁墙
                Destroy(collision.gameObject);
                //销毁子弹
                Destroy(gameObject);
                break;
            case "Barrier":
                collision.SendMessage("PlayerAudio");
                Destroy(gameObject);//销毁子弹
                break;
            default:
                break;
        }
    }
}
