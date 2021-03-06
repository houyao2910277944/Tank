﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    /// <summary>
    /// 用来装饰初始化地图所需物体的数组
    /// 0.家 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    /// </summary>
    public GameObject[] item;
    /// <summary>
    /// 已经有东西的位置列表
    /// </summary>
    private List<Vector3> itemPositionList = new List<Vector3>();
    //单例方法 Ctrl+R+E
    private static MapCreation instance;

    public static MapCreation Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;//单例初始化为当前函数
        InitMap();
    }
    private void InitMap()
    {
        //实例化家
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //用墙把老家围起来
        CreateItem(item[1], new Vector3(0, -7, 0), Quaternion.identity);
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                CreateItem(item[1], new Vector3(-1 + i * 2, -8 + j, 0), Quaternion.identity);
            }
        }
        for (int i = -11; i <= 11; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -9; i <= 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }
        //初始化玩家
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;
        //产生敌人
        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        //间隔4秒调用一次CreateEnemy函数创建敌人，共调用5次
        InvokeRepeating("CreateEnemy", 4, 5);

        //实例化地图map
        for (int i = 0; i <= 20; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// 放在MapCreation游戏对象中
    /// </summary>
    /// <param name="createGameObject"></param>
    /// <param name="createPosition"></param>
    /// <param name="createRotation"></param>
    private void CreateItem(GameObject createGameObject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
        
    }
    /// <summary>
    /// 产生随机位置的方法
    /// </summary>
    private Vector3 CreateRandomPosition()
    {
        //不生成x=-10,10的两列，y=-8，8的两行的位置
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }
    /// <summary>
    /// 用来判断位置列表中是否有这个位置
    /// </summary>
    private bool HasThePosition(Vector3 createPos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 产生敌人的方法
    /// </summary>
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if (num == 0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPos = new Vector3(10, 8, 0);
        }
        //while (true)
        //{
        //    if (num == 0)
        //    {
        //        EnemyPos = new Vector3(-10, 8, 0);
        //    }
        //    else if (num == 1)
        //    {
        //        EnemyPos = new Vector3(0, 8, 0);
        //    }
        //    else
        //    {
        //        EnemyPos = new Vector3(10, 8, 0);
        //    }
        //    if (HasThePosition(EnemyPos))
        //    {
        //        num = Random.Range(0, 3);
        //    }
        //    else
        //    {
        //        break;
        //    }
        //}
        CreateItem(item[3], EnemyPos, Quaternion.identity);
    }
}
