using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //属性值
    /// <summary>
    /// 玩家生命值
    /// </summary>
    public int lifeValue = 3;
    /// <summary>
    /// 玩家的得分
    /// </summary>
    public int playerScore = 0;
    /// <summary>
    /// 判断玩家是否死亡
    /// </summary>
    public bool isDead;//false
    public bool isDefeat;
    public bool isDestroy;

    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text playerLifeValueText;
    public GameObject isDefeatUI;

    //单例 创建单例方法Ctrl+R+E
    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (isDestroy)
        {
            return;
        }
        if (isDefeat)
        {
            isDestroy = true;
            isDefeatUI.SetActive(true);
            print(isDefeatUI.name + "---" + isDefeatUI.transform.position);
            //三秒后调用ReturnToTheMainMenu方法
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }
        if (isDead)
        {
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        playerLifeValueText.text = lifeValue.ToString();

    }
    /// <summary>
    /// 复活的方法
    /// </summary>
    private void Recover()
    {
        //游戏失败，返回主界面
        if (lifeValue < 1)
        {
            isDefeat = true;
        }
        else//玩家复活
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }
    /// <summary>
    /// 返回主界面 第一个场景
    /// </summary>
    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);//加载第一个场景
    }
}
