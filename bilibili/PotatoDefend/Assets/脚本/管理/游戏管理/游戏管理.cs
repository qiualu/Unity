using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏总管理，负责管理所有的管理者
/// </summary>
public class 游戏管理 : MonoBehaviour  // GameManager : MonoBehaviour
{
    public 玩家管理器 玩家管理;    // PlayerManager playerManager;
    public 工厂管理器 工厂管理;    // FactoryManager factoryManager;
    public 音频源管理器 音频源管理;  // AudioSourceManager audioSourceManager;
    public 界面管理器 界面管理;    // UIManager uiManager;

    private static 游戏管理 _实例;  // private static GameManager _instance;

    public static 游戏管理 实例     // public static GameManager Instance
    {
        get
        {
            return _实例;  // return _instance;
        }
    }


    public 关卡 当前关卡;      // public Stage currentStage;

    public bool 是否重置玩家数据;//是否重置游戏  // public bool initPlayerManager;//是否重置游戏


    // Start is called before the first frame update
    private void Awake()
    {

        // ---------------- 测试 ---------
        是否重置玩家数据 = true;



        DontDestroyOnLoad(gameObject);
        _实例 = this;  // _instance = this;
        玩家管理 = new 玩家管理器();  // playerManager = new PlayerManager();
        //玩家管理.保存数据();  // playerManager.SaveData();  
        玩家管理.读取数据();  // playerManager.ReadData();
        //玩家管理.读取数据测试();

        工厂管理 = new 工厂管理器();  // factoryManager = new FactoryManager();
        音频源管理 = new 音频源管理器();  // audioSourceManager = new AudioSourceManager();
        界面管理 = new 界面管理器();  // uiManager = new UIManager();
        //界面管理.界面外观.当前场景状态.进入场景();  // uiManager.mUIFacade.currentSceneState.EnterScene();


    }

    //获取精灵资源
    public Sprite GetSprite(string resourcePath)
    {
        //return factoryManager.spriteFactory.GetSingleResources(resourcePath);
        return  null;
    }

    ////创建物品
    //public GameObject 创建物品(GameObject 物品对象)  // public GameObject CreateItem(GameObject itemGo)
    //{
    //    GameObject 实例对象 = Instantiate(物品对象);  // GameObject go = Instantiate(itemGo);
    //    return 实例对象;  // return go;
    //}

    //获取精灵资源
    public Sprite 获取精灵(string 资源路径)  // public Sprite GetSprite(string resourcePath)
    {
        //return 工厂管理.精灵工厂.获取单个资源(资源路径);  // return factoryManager.spriteFactory.GetSingleResources(resourcePath);
        return  null;
    }

    ////获取音频片段资源
    //public AudioClip 获取音频片段(string 资源路径)  // public AudioClip GetAudioClip(string resourcePath)
    //{
    //    return 工厂管理.音频片段工厂.获取单个资源(资源路径);  // return factoryManager.audioClipFactory.GetSingleResources(resourcePath);
    //}

    //public RuntimeAnimatorController 获取运行时动画控制器(string 资源路径)  // public RuntimeAnimatorController GetRunTimeAnimatorController(string resourcePath)
    //{
    //    return 工厂管理.运行时动画控制器工厂.获取单个资源(资源路径);  // return factoryManager.runtimeAnimatorControllerFactory.GetSingleResources(resourcePath);
    //}

    ////获取游戏物体
    //public GameObject 获取游戏物体资源(工厂类型 工厂类型, string 资源路径)  // public GameObject GetGameObjectResource(FactoryType factoryType,string resourcePath)
    //{
    //    return 工厂管理.工厂字典[工厂类型].获取物品(资源路径);  // return factoryManager.factoryDict[factoryType].GetItem(resourcePath);
    //}

    ////将游戏物体放回对象池
    //public void 推送游戏物体到工厂(工厂类型 工厂类型, string 资源路径, GameObject 物品对象)  // public void PushGameObjectToFactory(FactoryType factoryType, string resourcePath,GameObject itemGo)
    //{
    //    工厂管理.工厂字典[工厂类型].推送物品(资源路径, 物品对象);  // factoryManager.factoryDict[factoryType].PushItem(resourcePath,itemGo);
    //}

}
