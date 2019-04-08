using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //0.167秒后自动销毁
        Destroy(gameObject, 0.167f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
