using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabList;
    public bool createPlayer;//默认false
    // Start is called before the first frame update
    void Start()
    {
        //延时调用方法
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 产生玩家
    /// </summary>
    private void BornTank()
    {
        //产生玩家
        if (createPlayer)
        {
            Instantiate(playerPrefab, transform.position, transform.rotation);
        }
        else//产生敌人
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, transform.rotation);
        }
    }
}
