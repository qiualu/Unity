using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 所有定义汇总
{

    //玩家管理器: 

    public int 冒险模式解锁地图数; // adventrueModelNum; //冒险模式解锁的地图个数
    public int 隐藏关卡解锁地图数; // burriedLevelNum; //隐藏关卡解锁的地图个数
    public int BOSS模式击败数;// bossModelNum;//boss模式KO的BOSS
    public int 金币总数; // coin;//获得金币的总数
    public int 杀怪总数; // killMonsterNum;//杀怪总数
    public int 击败BOSS总数; // killBossNum;//杀掉BOSS的总数
    public int 清理道具总数; // clearItemNum;//清理道具的总数
    public List<bool> 已解锁普通模式大关卡列表;// unLockedNormalModelBigLevelList;//大关卡
    public List<关卡> 已解锁普通模式关卡列表;// unLockedNormalModelLevelList;//所有的小关卡
    public List<int> 已解锁普通模式关卡数量;// unLockedeNormalModelLevelNum;//解锁小关卡数量


    //怪物窝
    public int 饼干数量; // cookies;
    public int 牛奶数量; // milk;
    public int 巢穴数量; // nest;
    public int 钻石数量; // diamands;
    public List<宠物类> 宠物数据列表;// MonsterPetData; //宠物喂养信息 public MonsterPetData monsterPetData;

    //public void 保存数据()
    //public void 读取数据()

    //宠物类
    public int 怪物等级;  // monsterLevel
    public int 剩余饼干;  // remainCookies
    public int 剩余牛奶;  // remainMilk
    public int 怪物ID;    // monsterID

    //public class 关卡
    public int[] 塔ID列表;  // 原 mTowerIDList;//本关卡可以建的塔种类
    public int 塔ID列表长度;  // 原 mTowerIDListLength;//建塔数组长度
    public bool 全清状态;  // 原 mAllClear;//是否清空此关卡道具
    public int 胡萝卜状态;  // 原 mCarrotState;//萝卜状态
    public int 关卡ID;  // 原 mLevelID;//小关卡ID
    public int 大关卡ID;  // 原 mBigLevelID;//大关卡ID
    public bool 已解锁;  // 原 unLocked;//此关卡是否解锁
    public bool 是否奖励关卡;  // 原 mIsRewardLevel;//是否为奖励关卡
    public int 总回合数;  // 原 mTotalRound;//一共几波怪


 
    public const string 开始加载面板 = "开始加载面板"; // 原：StartLoadPanel
    public const string 主面板 = "主面板"; // 原：MainPanel
    public const string 设置面板 = "设置面板"; // 原：SetPanel
    public const string 游戏加载面板 = "游戏加载面板"; // 原：GameLoadPanel
    public const string 帮助面板 = "帮助面板"; // 原：HelpPanel
    public const string 游戏普通选项面板 = "游戏普通选项面板"; // 原：GameNormalOptionPanel
    public const string 游戏普通大关卡面板 = "游戏普通大关卡面板"; // 原：GameNormalBigLevelPanel
    public const string 游戏普通关卡面板 = "游戏普通关卡面板"; // 原：GameNormalLevelPanel
    public const string 游戏Boss选项面板 = "游戏Boss选项面板"; // 原：GameBossOptionPanel
    public const string 普通模式面板 = "普通模式面板"; // 原：NormalModelPanel
    public const string Boss模式面板 = "Boss模式面板"; // 原：BossModelPanel
    public const string 怪物巢穴面板 = "怪物巢穴面板"; // 原：MonsterNestPanel
 


}

/*****
 * 







BasePanel
IBasePanel 基础界面



界面管理器 :   执行代码
        界面管理 = new 界面管理器();  // uiManager = new UIManager();
        界面管理.界面外观实例.当前场景状态.进入场景();  // uiManager.mUIFacade.currentSceneState.EnterScene();

public 界面外观 界面外观实例;  // UIFacade mUIFacade;



下面都是定义

public class 开始加载场景状态 : 基于基础场景状态
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="界面外观">界面外观实例</param>
    public 开始加载场景状态(界面外观 界面外观实例) : base(界面外观实例)
    {

    }

    /// <summary>
    /// 进入场景（重写父类方法）
    /// </summary>
    public override void 进入场景()
    {
        界面外观实例.向字典添加面板(字符串管理.开始加载面板);
        base.进入场景();
    }

    /// <summary>
    /// 退出场景（重写父类方法）
    /// </summary>
    public override void 退出场景()
    {
        base.退出场景();
        SceneManager.LoadScene(1);
    }

     

}

public class 界面外观  // UIFacade
{
    // 管理者实例
    private 界面管理器 界面管理器实例;  // UIManager mUIManager
    private 游戏管理 游戏管理器实例;  // GameManager mGameManager
    private 音频源管理器 音频源管理器实例;  // AudioSourceManager mAudioSourceManager
    public 玩家管理器 玩家管理器实例;  // PlayerManager mPlayerManager

    // UI面板字典（存储当前场景的面板接口实例）
    public Dictionary<string, 基础界面> 当前场景面板字典 = new Dictionary<string, 基础界面>();  // Dictionary<string, IBasePanel> currentScenePanelDict

    // 其他成员变量
    private GameObject 遮罩;  // GameObject mask
    private Image 遮罩图片;  // Image maskImage
    public Transform 画布变换;  // Transform canvasTransform

    // 场景状态（接口实例）
    public 基础场景状态 当前场景状态;  // IBaseSceneState currentSceneState
    public 基础场景状态 上一场景状态;  // IBaseSceneState lastSceneState

    // 构造函数（接收界面管理器实例）
    public 界面外观(界面管理器 界面管理器)  // UIFacade(UIManager uiManager)
    {
        游戏管理器实例 = 游戏管理.实例;  // mGameManager = GameManager.Instance
        玩家管理器实例 = 游戏管理.实例.玩家管理;  // mPlayerManager = mGameManager.playerManager
        界面管理器实例 = 界面管理器;  // mUIManager = uiManager
        音频源管理器实例 = 游戏管理器实例.音频源管理;  // mAudioSourceManager = mGameManager.audioSourceManager
        初始化遮罩();  // InitMask()
    }

    // 初始化遮罩
    public void 初始化遮罩()  // InitMask()
    {
        画布变换 = GameObject.Find("Canvas").transform;  // canvasTransform = GameObject.Find("Canvas").transform
        // 遮罩 = 游戏管理器实例.工厂管理器.工厂字典[工厂类型.界面工厂].获取物品("Img_Mask");  // 原注释逻辑保留
        遮罩 = 创建UI并设置位置("黑屏遮罩");  // mask = CreateUIAndSetUIPosition("Img_Mask")
        遮罩图片 = 遮罩.GetComponent<Image>();  // maskImage = mask.GetComponent<Image>()
    }

    public void 向字典添加面板(string 界面面板名称)  // AddPanelToDict(string uiPanelName)
    {
        Debug.Log($"向字典添加面板  { 界面面板名称 } ");  // 原日志信息翻译
        界面管理器实例.当前场景面板字典.Add(界面面板名称, 获取游戏物体资源(工厂类型类.界面面板工厂, 界面面板名称));  // 原逻辑翻译
    }

    // 初始化当前场景所有面板并存入字典
    public void 初始化字典()  // InitDict()
    {
        foreach (var 项 in 界面管理器实例.当前场景面板字典)  // foreach (var item in mUIManager.currentScenePanelDict)
        {
            项.Value.transform.SetParent(画布变换);  // item.Value.transform.SetParent(canvasTransform)
            项.Value.transform.localPosition = Vector3.zero;  // 原逻辑保留
            项.Value.transform.localScale = Vector3.one;  // 原逻辑保留
            基础界面 基础面板 = 项.Value.GetComponent<基础界面>();  // IBasePanel basePanel = item.Value.GetComponent<IBasePanel>()
            if (基础面板 == null)  // if (basePanel == null)
            {
                Debug.Log("获取面板上I基础面板脚本失败");  // 原日志信息翻译
            }
            基础面板.初始化面板();  // basePanel.InitPanel()
            当前场景面板字典.Add(项.Key, 基础面板);  // currentScenePanelDict.Add(item.Key, basePanel)
        }
    }

    // 清空UI面板字典
    public void 清空字典()  // ClearDict()
    {
        当前场景面板字典.Clear();  // currentScenePanelDict.Clear()
        界面管理器实例.清空字典();  // mUIManager.ClearDict()

    }

}

public interface 基础场景状态 
{
    // 进入场景（场景切换进入时执行的逻辑）
    void 进入场景();  // EnterScene()
    // 退出场景（场景切换离开时执行的逻辑）
    void 退出场景();  // ExitScene()
}

 




 * 
 * *****/


/********
 
 
    // 当前拥有的gameObject类型的资源(UI,UIPanel,Game) 切记：它存放的是游戏物体资源
    protected Dictionary<string, GameObject> 工厂字典 = new Dictionary<string, GameObject>();  // facotryDict

    // 对象池字典
    protected Dictionary<string, Stack<GameObject>> 对象池字典 = new Dictionary<string, Stack<GameObject>>();  // objectPoolDict

    // 对象池(就是我们具体存贮的游戏物体类型的对象)注：对应的是一个具体的游戏物体对象

    // 加载路径
    protected string 加载路径;  // loadPath

    public 基础工厂()  // BaseFactory()
    {
        加载路径 = "Prefabs/";  // loadPath = "Prefabs/";
    }

    // 放入池子
    public void 回收物品(string 物品名称, GameObject 物品)  // PushItem(string itemName, GameObject item)
    {
        物品.SetActive(false);  // item.SetActive(false);
        物品.transform.SetParent(游戏管理器.实例.transform);  // item.transform.SetParent(GameManager.Instance.transform);
        if (对象池字典.ContainsKey(物品名称))  // if (objectPoolDict.ContainsKey(itemName))
        {
            对象池字典[物品名称].Push(物品);  // objectPoolDict[itemName].Push(item);
        }
        else
        {
            Debug.Log("当前字典没有" + 物品名称 + "的栈");  // Debug.Log("当前字典没有"+itemName+"的栈");
        }
    }

    // 取实例
    public GameObject 获取物品(string 物品名称)  // GetItem(string itemName)
    {
        GameObject 物品对象 = null;  // GameObject itemGo = null;
        if (对象池字典.ContainsKey(物品名称))  // if (objectPoolDict.ContainsKey(itemName))//包含此对象池
        {
            if (对象池字典[物品名称].Count == 0)  // if (objectPoolDict[itemName].Count==0)
            {
                GameObject 资源 = 获取资源(物品名称);  // GameObject go = GetResource(itemName);
                物品对象 = 游戏管理器.实例.创建物品(资源);  // itemGo = GameManager.Instance.CreateItem(go);
            }
            else
            {
                物品对象 = 对象池字典[物品名称].Pop();  // itemGo = objectPoolDict[itemName].Pop();
                物品对象.SetActive(true);  // itemGo.SetActive(true);
            }
        }
        else  // 不包含此对象池
        {
            对象池字典.Add(物品名称, new Stack<GameObject>());  // objectPoolDict.Add(itemName,new Stack<GameObject>());
            GameObject 资源 = 获取资源(物品名称);  // GameObject go = GetResource(itemName);
            物品对象 = 游戏管理器.实例.创建物品(资源);  // itemGo = GameManager.Instance.CreateItem(go);
        }

        if (物品对象 == null)  // if (itemGo==null)
        {
            Debug.Log(物品名称 + "的实例获取失败");  // Debug.Log(itemName+"的实例获取失败");
        }

        return 物品对象;  // return itemGo;
    }

    // 取资源
    private GameObject 获取资源(string 物品名称)  // GetResource(string itemName)
    {
        GameObject 物品资源 = null;  // GameObject itemGo = null;
        string 物品加载路径 = 加载路径 + 物品名称;  // string itemLoadPath = loadPath + itemName;
        if (工厂字典.ContainsKey(物品名称))  // if (facotryDict.ContainsKey(itemName))
        {
            物品资源 = 工厂字典[物品名称];  // itemGo = facotryDict[itemName];
        }
        else
        {
            物品资源 = Resources.Load<GameObject>(物品加载路径);  // itemGo = Resources.Load<GameObject>(itemLoadPath);
            工厂字典.Add(物品名称, 物品资源);  // facotryDict.Add(itemName,itemGo);
        }
        if (物品资源 == null)  // if (itemGo==null)
        {
            Debug.Log(物品名称 + "的资源获取失败");  // Debug.Log(itemName+"的资源获取失败");
            Debug.Log("失败路径：" + 物品加载路径);  // Debug.Log("失败路径："+itemLoadPath);
        }
        return 物品资源;  // return itemGo;
    }
 





Assets\脚本\界面\界面\界面外观.cs(108,38): error CS0103: The name '获取游戏对象资源' does not exist in the current context

报错原因讲解讲解

public class 界面管理器  // UIManager
{
       // 获取游戏对象资源
    public GameObject 获取游戏物体资源(工厂类型类 工厂类型, string 资源路径)  // GetGameObjectResource(...)
    {
        return 游戏管理器实例.获取游戏物体资源(工厂类型, 资源路径);  // 原逻辑翻译
    }
 }


public enum 工厂类型类
{
    界面面板工厂,   // UIPanelFactory
    界面工厂,       // UIFactory
    游戏工厂        // GameFactory
}

public class 游戏管理 : MonoBehaviour  // GameManager : MonoBehaviour
{
        ////获取游戏物体
    public GameObject 获取游戏物体资源(工厂类型类 工厂类型, string 资源路径)  // public GameObject GetGameObjectResource(FactoryType factoryType,string resourcePath)
    {
        return 工厂管理.工厂字典[工厂类型].获取物品(资源路径);  // return factoryManager.factoryDict[factoryType].GetItem(resourcePath);
    }

 }



ArgumentException: The Object you want to instantiate is null.
UnityEngine.Object.Instantiate[T] (T original) (at <b1fe495152fd4f0180f79e56e3bccacc>:0)
游戏管理.创建物品 (UnityEngine.GameObject 物品对象) (at Assets/脚本/管理/游戏管理/游戏管理.cs:60)
基础工厂类.获取物品 (System.String 物品名称) (at Assets/脚本/工厂/基础工厂类.cs:68)
游戏管理.获取游戏物体资源 (工厂类型类 工厂类型, System.String 资源路径) (at Assets/脚本/管理/游戏管理/游戏管理.cs:85)
界面外观.获取游戏物体资源 (工厂类型类 工厂类型, System.String 资源路径) (at Assets/脚本/界面/界面/界面外观.cs:142)
界面外观.创建UI并设置位置 (System.String UI名称) (at Assets/脚本/界面/界面/界面外观.cs:114)
界面外观.初始化遮罩 () (at Assets/脚本/界面/界面/界面外观.cs:45)
界面外观..ctor (界面管理器 界面管理器) (at Assets/脚本/界面/界面/界面外观.cs:37)
界面管理器..ctor () (at Assets/脚本/管理/游戏对象管理/界面管理器.cs:18)
游戏管理.Awake () (at Assets/脚本/管理/游戏管理/游戏管理.cs:49)


public class 基础工厂类 : 基础工厂基类  // IBaseFacotry
{
    // 取实例
    public GameObject 获取物品(string 物品名称)  // GetItem(string itemName)
    {
        GameObject 物品对象 = null;  // GameObject itemGo = null;
        if (对象池字典.ContainsKey(物品名称))  // if (objectPoolDict.ContainsKey(itemName))//包含此对象池
        {
            if (对象池字典[物品名称].Count == 0)  // if (objectPoolDict[itemName].Count==0)
            {
                GameObject 资源 = 获取资源(物品名称);  // GameObject go = GetResource(itemName);
                物品对象 = 游戏管理.实例.创建物品(资源);  // itemGo = GameManager.Instance.CreateItem(go);
            }
            else
            {
                物品对象 = 对象池字典[物品名称].Pop();  // itemGo = objectPoolDict[itemName].Pop();
                物品对象.SetActive(true);  // itemGo.SetActive(true);
            }
        }
        else  // 不包含此对象池
        {
            对象池字典.Add(物品名称, new Stack<GameObject>());  // objectPoolDict.Add(itemName,new Stack<GameObject>());
            GameObject 资源 = 获取资源(物品名称);  // GameObject go = GetResource(itemName);
            物品对象 = 游戏管理.实例.创建物品(资源);  // itemGo = GameManager.Instance.CreateItem(go);
        }
}


public class 游戏管理 : MonoBehaviour  // GameManager : MonoBehaviour
{
    //创建物品
    public GameObject 创建物品(GameObject 物品对象)  // public GameObject CreateItem(GameObject itemGo)
    {
        GameObject 实例对象 = Instantiate(物品对象);  // GameObject go = Instantiate(itemGo);
        return 实例对象;  // return go;
    }
}






********/







