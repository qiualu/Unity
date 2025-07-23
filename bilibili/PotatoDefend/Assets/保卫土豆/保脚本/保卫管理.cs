using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 保卫管理 : MonoBehaviour
{

    private static 保卫管理 _实例;  // private static GameManager _instance;

    public bool 是否重置玩家数据;//是否重置游戏  // public bool initPlayerManager;//是否重置游戏

    public 数据加载 数据加载实例;

    public 界面管理 界面实例;



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

        数据加载实例 = new 数据加载();
        //数据加载实例 = 数据加载();
        数据加载实例.读取数据();

        界面实例 = new 界面管理();

    }

    public void 开始游戏(int 游戏状态) {
        // 
        Debug.Log("开始游戏: 触发开始游戏 ", this);

        界面实例.开始界面实例.游戏状态 = 2;
        界面实例.开始界面实例.游戏状态 = 2;

       
        if (游戏状态 == 1) {
            界面实例.开始界面实例.切换到下一个背景索引(1, 2); //淡出 
            界面实例.开始界面实例.游戏状态 = 2;
        }
        if (游戏状态 == 2)
        {
            界面实例.开始界面实例.切换到下一个背景索引(2, 3); //淡出 
            界面实例.开始界面实例.游戏状态 = 3;
        }
        if (游戏状态 == 3)
        {
            界面实例.开始界面实例.切换到下一个背景索引(3, 1); //淡出 
            界面实例.开始界面实例.游戏状态 = 1;
        }


    }


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log($" **  保存数据 Start {数据加载实例}");
        //Debug.Log($" 数据加载实例 {数据加载实例.关卡数据.名字}");

        if (数据加载实例 != null && 数据加载实例.当前关卡数据 != null)
        {
            Debug.Log($" 数据加载实例 {数据加载实例.当前关卡数据.名字}");

            数据加载实例.切换到关卡("关卡1");
            Debug.Log($" 数据加载实例 {数据加载实例.当前关卡数据.名字}");
            数据加载实例.切换到关卡("关卡2");
            Debug.Log($" 数据加载实例 {数据加载实例.当前关卡数据.名字}");
            数据加载实例.切换到关卡("关卡3");
            Debug.Log($" 数据加载实例 {数据加载实例.当前关卡数据.名字}");


        }
        else
        {
            Debug.LogError("数据加载实例或关卡数据为空");
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
