using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 普通游戏小关卡选择面板
/// 负责显示指定大关卡下的所有小关卡（通常为5个），包括关卡状态、可建造的塔防、关卡波数等信息
/// 支持小关卡的切换和进入游戏的功能
/// </summary>
public class 普通游戏小关卡面板 : BasePanel
{

    /// <summary>
    /// 图片资源加载的根路径（小关卡相关UI资源存放路径）
    /// </summary>
    private string filePath;
    /// <summary>
    /// 当前所在的大关卡ID（例如1、2、3）
    /// </summary>
    public int currentBigLevelID;
    /// <summary>
    /// 当前选中的小关卡ID（例如1-5）
    /// </summary>
    public int currentLevelID;
    /// <summary>
    /// 当前大关卡的资源路径（结合filePath和currentBigLevelID生成）
    /// </summary>
    private string theSpritePath;

    /// <summary>
    /// 小关卡滚动视图的content组件（用于动态生成小关卡卡片）
    /// </summary>
    private Transform levelContentTrans;
    /// <summary>
    /// 未解锁关卡的遮挡板（显示"锁"图标或奖励关卡标识）
    /// </summary>
    private GameObject img_LockBtnGo;
    /// <summary>
    /// 可建造塔防的容器（显示当前关卡允许使用的塔防类型）
    /// </summary>
    private Transform emp_TowerTrans;
    /// <summary>
    /// 左侧背景图（关卡场景背景左半部分）
    /// </summary>
    private Image img_BGLeft;
    /// <summary>
    /// 右侧背景图（关卡场景背景右半部分）
    /// </summary>
    private Image img_BGRight;
    /// <summary>
    /// 胡萝卜图标（显示关卡获得的胡萝卜数量状态）
    /// </summary>
    private Image img_Carrot;
    /// <summary>
    /// 全清图标（显示关卡是否已全清）
    /// </summary>
    private Image img_AllClear;
    /// <summary>
    /// 总波数文本（显示当前关卡的怪物总波数）
    /// </summary>
    private Text tex_TotalWaves;

    /// <summary>
    /// 玩家数据管理器（存储关卡解锁状态、通关信息等）
    /// </summary>
    private PlayerManager playerManager;
    /// <summary>
    /// 滑动控制器（处理小关卡卡片的左右滑动翻页）
    /// </summary>
    private 翻页控制类 翻页控制实例;
    //翻页控制类 翻页控制实例; SlideScrollView slideScrollView;
    /// <summary>
    /// 存储动态生成的小关卡卡片UI（用于后续回收）
    /// </summary>
    private List<GameObject> levelContentImageGos;
    /// <summary>
    /// 存储动态生成的塔防图标UI（用于后续回收）
    /// </summary>
    private List<GameObject> towerContentImageGos;

    protected override void Awake()
    {
        base.Awake();
        // 初始化资源路径（小关卡UI资源存放的根目录）
        filePath = "GameOption/Normal/Level/";
        // 获取玩家管理器实例（用于读取关卡解锁状态）
        playerManager = mUIFacade.mPlayerManager;
        // 初始化列表（用于管理动态生成的UI对象）
        levelContentImageGos = new List<GameObject>();
        towerContentImageGos = new List<GameObject>();
        // 查找滚动视图的content组件（小关卡卡片的父容器）
        levelContentTrans = transform.Find("Scroll View").Find("Viewport").Find("Content");
        // 查找未解锁关卡的遮挡板
        img_LockBtnGo = transform.Find("Img_LockBtn").gameObject;
        // 查找塔防容器
        emp_TowerTrans = transform.Find("Emp_Tower");
        // 查找背景图和文本组件
        img_BGLeft = transform.Find("Img_BGLeft").GetComponent<Image>();
        img_BGRight = transform.Find("Img_BGRight").GetComponent<Image>();
        tex_TotalWaves = transform.Find("Img_TotalWaves").Find("Text").GetComponent<Text>();
        // 获取滑动控制器组件（处理小关卡滑动）
        翻页控制实例 = transform.Find("Scroll View").GetComponent<翻页控制类>();
        // 初始化关卡ID（默认从第1大关卡、第1小关卡开始）
        currentBigLevelID = 1;
        currentLevelID = 1;
    }



    /// <summary>
    /// 更新小关卡卡片UI（动态生成5个小关卡卡片，显示解锁状态、通关信息等）
    /// </summary>
    /// <param name="spritePath">当前大关卡的资源路径</param>
    public void UpdateMapUI(string spritePath)
    {
        // 更新左右背景图
        img_BGLeft.sprite = mUIFacade.GetSprite(spritePath + "BG_Left");
        img_BGRight.sprite = mUIFacade.GetSprite(spritePath + "BG_Right");

        // 动态生成5个小关卡卡片（每个大关卡包含5个小关卡）
        for (int i = 0; i < 5; i++)
        {
            // 创建小关卡卡片并添加到content中
            levelContentImageGos.Add(CreateUIAndSetUIPosition("Img_Level", levelContentTrans));
            // 设置卡片的关卡图片（例如Level_1、Level_2）
            levelContentImageGos[i].GetComponent<Image>().sprite = mUIFacade.GetSprite(spritePath + "Level_" + (i + 1).ToString());

            // 获取当前小关卡的状态数据（从玩家管理器中读取）
            Stage stage = playerManager.unLockedNormalModelLevelList[(currentBigLevelID - 1) * 5 + i];

            // 初始化卡片状态（隐藏胡萝卜和全清图标，后续根据状态显示）
            levelContentImageGos[i].transform.Find("Img_Carrot").gameObject.SetActive(false);
            levelContentImageGos[i].transform.Find("Img_AllClear").gameObject.SetActive(false);

            if (stage.unLocked)// 关卡已解锁
            {
                // 显示全清图标（如果关卡已全清）
                if (stage.mAllClear)
                {
                    levelContentImageGos[i].transform.Find("Img_AllClear").gameObject.SetActive(true);
                }

                // 显示胡萝卜图标（根据获得的胡萝卜数量显示不同状态）
                if (stage.mCarrotState != 0)
                {
                    Image carrotImageGo = levelContentImageGos[i].transform.Find("Img_Carrot").GetComponent<Image>();
                    carrotImageGo.gameObject.SetActive(true);
                    carrotImageGo.sprite = mUIFacade.GetSprite(filePath + "Carrot_" + stage.mCarrotState);
                }

                // 隐藏锁图标和背景遮罩
                levelContentImageGos[i].transform.Find("Img_Lock").gameObject.SetActive(false);
                levelContentImageGos[i].transform.Find("Img_BG").gameObject.SetActive(false);
            }
            else// 关卡未解锁
            {
                if (stage.mIsRewardLevel)// 是奖励关卡（显示怪物图标而非锁）
                {
                    levelContentImageGos[i].transform.Find("Img_Lock").gameObject.SetActive(false);
                    levelContentImageGos[i].transform.Find("Img_BG").gameObject.SetActive(true);

                    // 设置奖励关卡的怪物图标
                    Image monsterPetImage = levelContentImageGos[i].transform.Find("Img_BG").Find("Img_Monster").GetComponent<Image>();
                    monsterPetImage.sprite = mUIFacade.GetSprite("MonsterNest/Monster/Baby/" + currentBigLevelID.ToString());
                    monsterPetImage.SetNativeSize();// 自适应图片大小
                    monsterPetImage.transform.localScale = new Vector3(2, 2, 1);// 放大显示
                }
                else// 普通未解锁关卡（显示锁图标）
                {
                    levelContentImageGos[i].transform.Find("Img_Lock").gameObject.SetActive(true);
                    levelContentImageGos[i].transform.Find("Img_BG").gameObject.SetActive(false);
                }
            }
        }

        // 根据小关卡数量设置滚动视图content的长度
        翻页控制实例.SetContentLength(5);
    }

    /// <summary>
    /// 销毁当前生成的小关卡卡片，回收至对象池
    /// </summary>
    private void DestoryMapUI()
    {
        if (levelContentImageGos.Count > 0)
        {
            // 遍历所有小关卡卡片，放回对象工厂
            for (int i = 0; i < 5; i++)
            {
                mUIFacade.PushGameObjectToFactory(FactoryType.UIFactory, "Img_Level", levelContentImageGos[i]);
            }
            // 重置滚动视图content的长度
            翻页控制实例.InitScrollLength();
            // 清空列表（避免下次生成时重复回收）
            levelContentImageGos.Clear();
        }
    }

    /// <summary>
    /// 更新静态UI（塔防列表、总波数、锁状态等）
    /// </summary>
    /// <param name="SpritePath">当前大关卡的资源路径</param>
    public void UpdateLevelUI(string SpritePath)
    {
        // 先回收已生成的塔防图标（避免重复生成）
        if (towerContentImageGos.Count != 0)
        {
            for (int i = 0; i < towerContentImageGos.Count; i++)
            {
                mUIFacade.PushGameObjectToFactory(FactoryType.UIFactory, "Img_Tower", towerContentImageGos[i]);
            }
            towerContentImageGos.Clear();
        }

        // 获取当前小关卡的状态数据
        Stage stage = playerManager.unLockedNormalModelLevelList[(currentBigLevelID - 1) * 5 + currentLevelID - 1];

        // 控制关卡入口按钮的锁状态（未解锁则显示锁）
        if (stage.unLocked)
        {
            img_LockBtnGo.SetActive(false);
        }
        else
        {
            img_LockBtnGo.SetActive(true);
        }

        // 显示当前关卡的总波数
        tex_TotalWaves.text = stage.mTotalRound.ToString();

        // 生成当前关卡允许建造的塔防图标
        for (int i = 0; i < stage.mTowerIDListLength; i++)
        {
            towerContentImageGos.Add(CreateUIAndSetUIPosition("Img_Tower", emp_TowerTrans));
            towerContentImageGos[i].GetComponent<Image>().sprite = mUIFacade.
                GetSprite(filePath + "Tower" + "/Tower_" + stage.mTowerIDList[i].ToString());
        }
    }


    /// <summary>
    /// 外部调用的进入当前面板的方法（从大关卡面板跳转时使用）
    /// </summary>
    /// <param name="currentBigLevel">当前要进入的大关卡ID</param>
    public void ToThisPanel(int currentBigLevel)
    {
        currentBigLevelID = currentBigLevel;
        currentLevelID = 1; // 默认选中第1个小关卡
        EnterPanel();// 进入面板并刷新UI
    }

    /// <summary>
    /// 初始化面板（隐藏面板，准备进入）
    /// </summary>
    public override void InitPanel()
    {
        base.InitPanel();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 进入面板（显示面板并刷新所有UI）
    /// </summary>
    public override void EnterPanel()
    {
        base.EnterPanel();
        gameObject.SetActive(true);
        // 生成当前大关卡的资源路径
        theSpritePath = filePath + currentBigLevelID.ToString() + "/";
        // 先销毁上一次生成的小关卡卡片
        DestoryMapUI();
        // 生成并更新小关卡卡片UI
        UpdateMapUI(theSpritePath);
        // 更新静态UI（塔防列表、波数等）
        UpdateLevelUI(theSpritePath);
        // 初始化滑动控制器
        翻页控制实例.Init();
    }

    /// <summary>
    /// 更新面板（刷新静态UI，例如切换小关卡后调用）
    /// </summary>
    public override void UpdatePanel()
    {
        base.UpdatePanel();
        UpdateLevelUI(theSpritePath);
    }

    /// <summary>
    /// 退出面板（隐藏面板）
    /// </summary>
    public override void ExitPanel()
    {
        base.ExitPanel();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 进入游戏面板（点击"开始游戏"按钮时调用）
    /// </summary>
    public void ToGamePanel()
    {
        // 播放按钮音效
        mUIFacade.PlayButtonAudioClip();
        // 记录当前选中的关卡数据
        GameManager.Instance.currentStage = playerManager.unLockedNormalModelLevelList[(currentBigLevelID - 1) * 5 + currentLevelID - 1];
        // 显示游戏加载面板
        //mUIFacade.currentScenePanelDict[StringManager.GameLoadPanel].EnterPanel(); // 资源名字.游戏加载面板
        mUIFacade.currentScenePanelDict[资源名字.游戏加载面板].EnterPanel(); // 资源名字.游戏加载面板
        // 切换场景状态为普通游戏模式
        mUIFacade.ChangeSceneState(new 普通模式场景(mUIFacade));
    }


    /// <summary>
    /// 预加载资源（提前加载所有可能用到的图片资源，避免卡顿）
    /// </summary>
    private void LoadResource()
    {
        mUIFacade.GetSprite(filePath + "AllClear");
        mUIFacade.GetSprite(filePath + "Carrot_1");
        mUIFacade.GetSprite(filePath + "Carrot_2");
        mUIFacade.GetSprite(filePath + "Carrot_3");

        // 预加载所有大关卡的背景和小关卡图片
        for (int i = 1; i < 4; i++)
        {
            string spritePath = filePath + i.ToString() + "/";
            mUIFacade.GetSprite(spritePath + "BG_Left");
            mUIFacade.GetSprite(spritePath + "BG_Right");
            for (int j = 1; j < 6; j++)
            {
                mUIFacade.GetSprite(spritePath + "Level_" + j.ToString());
            }
        }

        // 预加载所有塔防图标
        for (int j = 1; j < 13; j++)
        {
            mUIFacade.GetSprite(filePath + "Tower/Tower_" + j.ToString());
        }
    }

    /// <summary>
    /// 动态创建UI并设置位置
    /// </summary>
    /// <param name="uiName">UI资源名称（从对象工厂获取）</param>
    /// <param name="parentTrans">父容器的Transform</param>
    /// <returns>创建的UI游戏对象</returns>
    public GameObject CreateUIAndSetUIPosition(string uiName, Transform parentTrans)
    {
        // 从对象工厂获取UI实例
        GameObject itemGo = mUIFacade.GetGameObjectResource(FactoryType.UIFactory, uiName);
        // 设置父容器
        itemGo.transform.SetParent(parentTrans);
        // 重置位置和缩放
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;
        return itemGo;
    }

    /// <summary>
    /// 切换到下一个小关卡（滑动或按钮触发）
    /// </summary>
    public void ToNextLevel()
    {
        currentLevelID++;
        UpdatePanel();
    }

    /// <summary>
    /// 切换到上一个小关卡（滑动或按钮触发）
    /// </summary>
    public void ToLastLevel()
    {
        currentLevelID--;
        UpdatePanel();
    }
}