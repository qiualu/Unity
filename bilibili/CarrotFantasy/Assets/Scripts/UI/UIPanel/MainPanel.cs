using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // 引入DOTween插件，用于UI动画控制

/// <summary>
/// 主面板控制器，继承自BasePanel
/// 负责主界面的UI显示、动画播放及场景切换逻辑
/// </summary>
public class MainPanel : BasePanel
{

    // 胡萝卜动画组件（用于播放胡萝卜生长动画）
    private Animator carrotAnimator;
    // 怪物图片的Transform（用于控制怪物动画）
    private Transform monsterTrans;
    // 云朵图片的Transform（用于控制云朵动画）
    private Transform cloudTrans;
    // 主面板的动画数组（0：向右移动动画，1：向左移动动画）
    private Tween[] mainPanelTween;
    // 离开主面板时播放的动画
    private Tween ExitTween;

    private PlayerManager playerManager;
    /// <summary>
    /// 唤醒方法，在对象初始化时调用
    /// 初始化UI组件、动画及基础设置
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        playerManager = mUIFacade.mPlayerManager;

        // 设置面板在UI层级中的显示顺序（8表示在较上层，避免被其他UI遮挡）
        transform.SetSiblingIndex(8);
        // 获取胡萝卜动画组件，并播放生长动画
        carrotAnimator = transform.Find("Emp_Carrot").GetComponent<Animator>();
        carrotAnimator.Play("CarrotGrow");
        // 获取怪物和云朵的Transform引用
        monsterTrans = transform.Find("Img_Monster");
        cloudTrans = transform.Find("Img_Cloud");

        // 初始化动画数组
        mainPanelTween = new Tween[2];
        // 创建向右移动的动画（X轴移动到1920，时长0.5秒）
        mainPanelTween[0] = transform.DOLocalMoveX(1920, 0.5f);
        mainPanelTween[0].SetAutoKill(false); // 不自动销毁动画
        mainPanelTween[0].Pause(); // 暂停动画，等待需要时播放
        // 创建向左移动的动画（X轴移动到-1920，时长0.5秒）
        mainPanelTween[1] = transform.DOLocalMoveX(-1920, 0.5f);
        mainPanelTween[1].SetAutoKill(false);
        mainPanelTween[1].Pause();

        // 播放UI元素的循环动画
        PlayUITween();
    }

    /// <summary>
    /// 进入面板时的操作（重写自BasePanel）
    /// 用于面板激活时的初始化
    /// </summary>
    public override void EnterPanel()
    {
        // 确保面板显示在正确层级
        transform.SetSiblingIndex(8);
        // 播放胡萝卜生长动画
        carrotAnimator.Play("CarrotGrow");
        // 如果存在离开动画，反向播放（即回到初始位置）
        if (ExitTween != null)
        {
            ExitTween.PlayBackwards();
        }
        // 显示云朵
        cloudTrans.gameObject.SetActive(true);
    }

    /// <summary>
    /// 退出面板时的操作（重写自BasePanel）
    /// 用于面板隐藏时的清理
    /// </summary>
    public override void ExitPanel()
    {
        // 播放离开动画（向前播放）
        ExitTween.PlayForward();
        // 隐藏云朵
        cloudTrans.gameObject.SetActive(false);
    }

    /// <summary>
    /// 播放UI元素的循环动画
    /// 包括怪物上下浮动和云朵左右移动
    /// </summary>
    private void PlayUITween()
    {
        // 怪物沿Y轴移动到600位置，时长1.5秒，无限循环且往返运动（Yoyo）
        monsterTrans.DOLocalMoveY(600, 1.5f).SetLoops(-1, LoopType.Yoyo);
        // 云朵沿X轴移动到1300位置，时长8秒，无限循环且重新开始（Restart）
        cloudTrans.DOLocalMoveX(1300, 8f).SetLoops(-1, LoopType.Restart);
    }

    /// <summary>
    /// 向右移动面板（切换到设置面板）
    /// 按钮点击事件
    /// </summary>
    public void MoveToRight()
    {
        // 播放按钮点击音效
        mUIFacade.PlayButtonAudioClip();
        // 设置离开动画为向右移动
        ExitTween = mainPanelTween[0];
        // 进入设置面板
        mUIFacade.currentScenePanelDict[StringManager.SetPanel].EnterPanel();
    }

    /// <summary>
    /// 向左移动面板（切换到帮助面板）
    /// 按钮点击事件
    /// </summary>
    public void MoveToLeft()
    {
        // 播放按钮点击音效
        mUIFacade.PlayButtonAudioClip();
        // 设置离开动画为向左移动
        ExitTween = mainPanelTween[1];
        // 进入帮助面板
        mUIFacade.currentScenePanelDict[StringManager.HelpPanel].EnterPanel();
    }

    /// <summary>
    /// 以下为场景状态切换的方法（按钮点击事件）
    /// </summary>

    /// <summary>
    /// 进入普通模式场景
    /// </summary>
    public void ToNormalModelScene()
    {

        Debug.Log($"开始游戏- 0.1,ToNormalModelScene --  {GameController.Instance}");

        // 播放按钮点击音效
        mUIFacade.PlayButtonAudioClip();
        // 显示游戏加载面板
        mUIFacade.currentScenePanelDict[StringManager.GameLoadPanel].EnterPanel();
        // 切换场景状态为普通游戏选项场景
        mUIFacade.ChangeSceneState(new NormalGameOptionSceneState(mUIFacade));
    }



    public void 直接开始游戏准备()
    {
        Transform bigLevelPage;//大关卡按钮数组
        bigLevelPage = new Transform;
        //大关卡选择 id  1  4  5   Button(UnityEngine.RectTransform)  1

    }




    /// <summary>
    /// 进入Boss模式场景
    /// </summary>
    public void ToBossModelScene()
    {
        Debug.Log("直接开始游戏");
        ////// 播放按钮点击音效
        //mUIFacade.PlayButtonAudioClip();
        ////// 显示游戏加载面板
        //mUIFacade.currentScenePanelDict[资源名字.游戏加载面板].EnterPanel();
        ////// 切换场景状态为Boss游戏选项场景
        //mUIFacade.ChangeSceneState(new 保卫土豆场景(mUIFacade));

        // 增加空检查，避免原错误掩盖调用栈信息
    


        int currentBigLevelID = 1;  //  大关卡
        int currentLevelID = 1;    // 小关卡
        Debug.Log($"  直接开始游戏 测试版 小关卡选择 id  {currentBigLevelID} {currentLevelID} ");

        mUIFacade.PlayButtonAudioClip();
        GameManager.Instance.currentStage = playerManager.unLockedNormalModelLevelList[(currentBigLevelID - 1) * 5 + currentLevelID - 1];
        mUIFacade.currentScenePanelDict[StringManager.GameLoadPanel].EnterPanel();
        mUIFacade.ChangeSceneState(new NormalModelSceneState(mUIFacade));



    }




    /// <summary>
    /// 进入怪物巢穴场景
    /// </summary>
    public void ToMonsterNest()
    {
        Debug.Log("进入怪物巢穴场景");
        //// 播放按钮点击音效
        //mUIFacade.PlayButtonAudioClip();
        //// 显示游戏加载面板
        //mUIFacade.currentScenePanelDict[StringManager.GameLoadPanel].EnterPanel();
        //// 切换场景状态为怪物巢穴场景
        //mUIFacade.ChangeSceneState(new MonsterNestSceneState(mUIFacade));
    }

    /// <summary>
    /// 退出游戏
    /// 按钮点击事件
    /// </summary>
    public void ExitGame()
    {
        // 播放按钮点击音效
        mUIFacade.PlayButtonAudioClip();
        // 保存玩家数据
        GameManager.Instance.playerManager.SaveData();
        // 退出应用程序
        Application.Quit();
    }
}