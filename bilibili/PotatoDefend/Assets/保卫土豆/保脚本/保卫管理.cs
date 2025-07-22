using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 保卫管理 : MonoBehaviour
{

    private static 保卫管理 _实例;  // private static GameManager _instance;

    public bool 是否重置玩家数据;//是否重置游戏  // public bool initPlayerManager;//是否重置游戏

    数据加载 数据加载实例;

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
        数据加载实例.读取数据();
    }


        // Start is called before the first frame update
    void Start()
    {
        //Debug.Log($" **  保存数据 Start {数据加载实例}");
        //Debug.Log($" 数据加载实例 {数据加载实例.关卡数据.名字}");

        if (数据加载实例 != null && 数据加载实例.关卡数据 != null)
        {
            Debug.Log($" 数据加载实例 {数据加载实例.关卡数据.名字}");
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
