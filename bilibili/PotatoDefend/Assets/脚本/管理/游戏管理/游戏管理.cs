using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 游戏管理 : MonoBehaviour
{

    public 玩家管理器 玩家管理;
    public 工厂管理器 工厂管理;
    public 音频源管理器 音频源管理;
    public 界面管理器 界面管理;


    private static 游戏管理 _实例;

    public static 游戏管理 实例
    {
        get
        {
            return _实例;
        }
    }

    public 关卡 当前关卡;

    public bool 是否重置玩家数据;//是否重置游戏


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
