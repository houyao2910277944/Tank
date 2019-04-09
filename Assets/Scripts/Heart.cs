using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject explosionPrefab;
    // 音效添加
    public AudioClip dieAudio;

    public Sprite BrokenSprite;//拿到家被破坏的图片
    // Start is called before the first frame update
    void Start()
    {
        //GetCompoment <T>()
        //从当前游戏对象获取组件T，
        //只在当前游戏对象中获取，
        //没得到的就返回null，不会去子物体中去寻找。
        sr = GetComponent<SpriteRenderer>();
    }
   
    /// <summary>
    /// 家没了直接失败
    /// </summary>
    public void Die()
    {
        sr.sprite = BrokenSprite;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
        //播放音效
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
