using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    private int choice = 0;
    public Transform posOne;
    public Transform posTwo;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            choice = 1;
            transform.position = posOne.position;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            choice = 2;
            transform.position = posTwo.position;
        }
        //加载场景
        if (choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
