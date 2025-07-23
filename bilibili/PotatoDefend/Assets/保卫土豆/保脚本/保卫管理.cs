using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 保卫管理 : MonoBehaviour
{

    private static 保卫管理 _实例;  // private static GameManager _instance;

    public bool 是否重置玩家数据;//是否重置游戏  // public bool initPlayerManager;//是否重置游戏

    public 数据加载 数据加载实例;

    public 开始界面管理 界面实例;
 


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

        //界面实例 = new 开始界面管理();
        // 重要：不要用new创建MonoBehaviour对象
        // 方法1：在Inspector面板中拖拽场景中的开始界面管理对象到这个字段
        // 方法2：如果场景中只有一个开始界面管理，可以这样查找
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
        StartCoroutine(延迟复原(3));


    }

    // 延迟复原
    IEnumerator 延迟复原(float 延迟时间)
    {
        //Debug.Log($"开始 触发，延迟{延迟时间}秒");
        yield return new WaitForSeconds(延迟时间);

        //Debug.Log($"  | 延迟复原 | 当前游戏状态 ***  触发，  {界面实例.游戏状态} ");

        //if (界面实例.游戏状态 == 1)
        //{
        //    界面实例.显示第二关();
        //}
        //else if (界面实例.游戏状态 == 2)
        //{
        //    界面实例.显示第三关();
        //}
        //else if(界面实例.游戏状态 == 3)
        //{
        //    界面实例.恢复开始界面();
        //}

        //Debug.Log($"  | 延迟复原 End | ");
    }


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log($" **  保存数据 Start {数据加载实例}");
        //Debug.Log($" 数据加载实例 {数据加载实例.关卡数据.名字}");

        //if (数据加载实例 != null && 数据加载实例.当前关卡数据 != null)
        //{
        //    Debug.Log($" 数据加载实例 {数据加载实例.当前关卡数据.名字}");

        //    数据加载实例.切换到关卡("关卡1");
        //    Debug.Log($" 数据加载实例 {数据加载实例.当前关卡数据.名字}");
        //    数据加载实例.切换到关卡("关卡2");
        //    Debug.Log($" 数据加载实例 {数据加载实例.当前关卡数据.名字}");
        //    数据加载实例.切换到关卡("关卡3");
        //    Debug.Log($" 数据加载实例 {数据加载实例.当前关卡数据.名字}");
 
        //}
        //else
        //{
        //    Debug.LogError("数据加载实例或关卡数据为空");
        //}

         

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
