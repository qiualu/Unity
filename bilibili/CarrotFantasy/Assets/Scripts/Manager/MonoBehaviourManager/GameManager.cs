using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏总管理类，采用单例模式设计
/// 负责统一管理游戏中所有的子系统管理者，是各模块交互的核心枢纽
/// </summary>
public class GameManager : MonoBehaviour
{

    // 玩家管理器引用，负责玩家数据和状态管理
    public PlayerManager playerManager;
    // 工厂管理器引用，负责资源创建和对象池管理
    public FactoryManager factoryManager;
    // 音频管理器引用，负责游戏音效和音乐的播放控制
    public AudioSourceManager audioSourceManager;
    // UI管理器引用，负责界面切换和UI元素控制
    public UIManager uiManager;

    // 单例实例私有变量
    private static GameManager _instance;

    /// <summary>
    /// 单例访问属性
    /// 提供全局访问点，确保整个游戏中只有一个GameManager实例
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    // 当前游戏关卡/场景引用
    public Stage currentStage;

    // 标记是否需要重置玩家管理器数据
    public bool initPlayerManager;//是否重置游戏

    /// <summary>
    /// 唤醒方法，在游戏对象初始化时调用
    /// 负责初始化单例和各个管理器
    /// </summary>
    private void Awake()
    {
        // 使当前对象在场景切换时不被销毁
        DontDestroyOnLoad(gameObject);
        // 初始化单例实例
        _instance = this;
        // 创建玩家管理器实例
        playerManager = new PlayerManager();
        // 读取玩家存档数据（注释掉的代码为保存数据功能）
        //playerManager.SaveData();  
        playerManager.ReadData();
        // 创建工厂管理器实例
        factoryManager = new FactoryManager();
        // 创建音频管理器实例
        audioSourceManager = new AudioSourceManager();
        // 创建UI管理器实例
        uiManager = new UIManager();
        // 通知当前场景状态进入场景（初始化场景）
        uiManager.mUIFacade.currentSceneState.EnterScene();
    }

    /// <summary>
    /// 创建物品游戏对象
    /// </summary>
    /// <param name="itemGo">要实例化的物品预制体</param>
    /// <returns>实例化后的物品游戏对象</returns>
    public GameObject CreateItem(GameObject itemGo)
    {
        GameObject go = Instantiate(itemGo);
        return go;
    }

    /// <summary>
    /// 获取精灵图资源
    /// </summary>
    /// <param name="resourcePath">资源路径</param>
    /// <returns>指定路径的Sprite资源</returns>
    public Sprite GetSprite(string resourcePath)
    {
        return factoryManager.spriteFactory.GetSingleResources(resourcePath);
    }

    /// <summary>
    /// 获取音频片段资源
    /// </summary>
    /// <param name="resourcePath">资源路径</param>
    /// <returns>指定路径的AudioClip资源</returns>
    public AudioClip GetAudioClip(string resourcePath)
    {
        return factoryManager.audioClipFactory.GetSingleResources(resourcePath);
    }

    /// <summary>
    /// 获取运行时动画控制器资源
    /// </summary>
    /// <param name="resourcePath">资源路径</param>
    /// <returns>指定路径的RuntimeAnimatorController资源</returns>
    public RuntimeAnimatorController GetRunTimeAnimatorController(string resourcePath)
    {
        return factoryManager.runtimeAnimatorControllerFactory.GetSingleResources(resourcePath);
    }

    /// <summary>
    /// 从对象池获取游戏物体资源
    /// </summary>
    /// <param name="factoryType">工厂类型</param>
    /// <param name="resourcePath">资源路径</param>
    /// <returns>获取到的游戏物体</returns>
    public GameObject GetGameObjectResource(FactoryType factoryType, string resourcePath)
    {
        return factoryManager.factoryDict[factoryType].GetItem(resourcePath);
    }

    /// <summary>
    /// 将游戏物体放回对应的对象池
    /// </summary>
    /// <param name="factoryType">工厂类型</param>
    /// <param name="resourcePath">资源路径</param>
    /// <param name="itemGo">要回收的游戏物体</param>
    public void PushGameObjectToFactory(FactoryType factoryType, string resourcePath, GameObject itemGo)
    {
        factoryManager.factoryDict[factoryType].PushItem(resourcePath, itemGo);
    }

}