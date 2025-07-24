using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 保卫管理 : MonoBehaviour
{

    private static 保卫管理 _实例;  // private static GameManager _instance;

    public bool 是否重置玩家数据;//是否重置游戏  // public bool initPlayerManager;//是否重置游戏


    public bool 是否暂停 = false;
    public int 游戏状态 = 1; // 保卫管理.实例.游戏状态
    public float 游戏速度 = 1.0f;
    // 集火目标：存储玩家当前指定的攻击目标（怪物或道具）
    [HideInInspector] public Transform 目标位置;



    public 开始界面管理 界面实例;

    //public 铺设地板 铺设地板实例;  铺设地板.铺设地板实例


    public static 保卫管理 实例     // public static GameManager Instance
    {
        get
        {
            return _实例;  // return _instance;
        }
    }

    private void Awake()
    {
        // ---------------- 测试 ---------
        是否重置玩家数据 = true;
        DontDestroyOnLoad(gameObject);
        _实例 = this;  // _instance = this;

      
        if (界面实例 == null)
        {
            界面实例 = FindObjectOfType<开始界面管理>();
            if (界面实例 == null)
            {
                Debug.LogError("找不到开始界面管理实例！请确保场景中有该组件", this);
            }
        }

 

    }

    public void 开始游戏(int 游戏状态) {
        // 
        Debug.Log($"****保卫管理*******开始游戏: 触发开始游戏 {游戏状态}   {界面实例.游戏状态}    ");
        StartCoroutine(延迟复原(1));


    }

    // 延迟复原
    IEnumerator 延迟复原(float 延迟时间)
    {
        //Debug.Log($"开始 触发，延迟{延迟时间}秒");
        yield return new WaitForSeconds(延迟时间);

 

        Debug.Log($"  | 执行铺设地板 Start | ");
        铺设地板.铺设地板实例.关卡加载();
        铺设地板.铺设地板实例.铺设一个测试版本();

    }


    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


