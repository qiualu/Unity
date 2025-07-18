using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// UI中介，上层与管理者们做交互，下层与UI面板进行交互
/// </summary>
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
        遮罩 = 创建UI并设置位置("Img_Mask");  // mask = CreateUIAndSetUIPosition("Img_Mask")
        遮罩图片 = 遮罩.GetComponent<Image>();  // maskImage = mask.GetComponent<Image>()
    }

    // 切换当前场景的状态
    public void 切换场景状态(基础场景状态 基础场景状态)  // ChangeSceneState(IBaseSceneState baseSceneState)
    {
        上一场景状态 = 当前场景状态;  // lastSceneState = currentSceneState
        显示遮罩();  // ShowMask()
        当前场景状态 = 基础场景状态;  // currentSceneState = baseSceneState
    }

    // 显示遮罩
    public void 显示遮罩()  // ShowMask()
    {
        遮罩.transform.SetSiblingIndex(10);  // mask.transform.SetSiblingIndex(10)
        Tween 动画 = DOTween.To(() => 遮罩图片.color, toColor => 遮罩图片.color = toColor, new Color(0, 0, 0, 1), 2f);  // Tween t = DOTween.To(...)
        动画.OnComplete(离开场景完成);  // t.OnComplete(ExitSceneComplete)
    }

    // 离开当前场景完成（遮罩显示完毕后执行）
    private void 离开场景完成()  // ExitSceneComplete()
    {
        上一场景状态.退出场景();  // lastSceneState.ExitScene()
        当前场景状态.进入场景();  // currentSceneState.EnterScene()
        隐藏遮罩();  // HideMask()
    }

    // 隐藏遮罩
    public void 隐藏遮罩()  // HideMask()
    {
        遮罩.transform.SetSiblingIndex(10);  // mask.transform.SetSiblingIndex(10)
        DOTween.To(() => 遮罩图片.color, toColor => 遮罩图片.color = toColor, new Color(0, 0, 0, 0), 2f);  // 原逻辑保留
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

    // 向界面管理器字典添加UI面板
    public void 向字典添加面板(string 界面面板名称)  // AddPanelToDict(string uiPanelName)
    {
        界面管理器实例.当前场景面板字典.Add(界面面板名称, 获取游戏物体资源(工厂类型类.界面面板工厂, 界面面板名称));  // 原逻辑翻译
    }

    // 实例化UI并设置位置
    public GameObject 创建UI并设置位置(string UI名称)  // CreateUIAndSetUIPosition(string uiName)
    {
        GameObject 物品对象 = 获取游戏物体资源(工厂类型类.界面工厂, UI名称);  // GameObject itemGo = GetGameObjectResource(...)
        物品对象.transform.SetParent(画布变换);  // 原逻辑保留
        物品对象.transform.localPosition = Vector3.zero;  // 原逻辑保留
        物品对象.transform.localScale = Vector3.one;  // 原逻辑保留
        return 物品对象;  // return itemGo
    }

    // 获取精灵资源
    public Sprite 获取精灵(string 资源路径)  // GetSprite(string resourcePath)
    {
        return 游戏管理器实例.获取精灵(资源路径);  // return mGameManager.GetSprite(resourcePath)
    }

    // 获取音频片段资源（修正原方法名疑似笔误）
    public AudioClip 获取音频片段(string 资源路径)  // GetAudioSource（实际应为GetAudioClip）
    {
        return 游戏管理器实例.获取音频片段(资源路径);  // return mGameManager.GetAudioClip(resourcePath)
    }

    // 获取运行时动画控制器资源
    public RuntimeAnimatorController 获取运行时动画控制器(string 资源路径)  // GetRuntimeAnimatorController(...)
    {
        return 游戏管理器实例.获取运行时动画控制器(资源路径);  // 原逻辑翻译
    }

    // 获取游戏对象资源
    public GameObject 获取游戏物体资源(工厂类型类 工厂类型, string 资源路径)  // GetGameObjectResource(...)
    {
        return 游戏管理器实例.获取游戏物体资源(工厂类型, 资源路径);  // 原逻辑翻译
    }

    // 将游戏对象放回工厂（对象池）
    public void 将游戏物体放回对象池(工厂类型类 工厂类型, string 资源路径, GameObject 物品对象)  // PushGameObjectToFactory(...)
    {
        游戏管理器实例.将游戏物体放回对象池(工厂类型, 资源路径, 物品对象);  // 原逻辑翻译
    }

    /// <summary>
    /// 音乐播放相关方法
    /// </summary>

    // 切换背景音乐开关
    public void 切换背景音乐开关()  // CloseOrOpenBGMusic()
    {
        音频源管理器实例.切换背景音乐开关();  // mAudioSourceManager.CloseOrOpenBGMusic()
    }

    // 切换特效音开关
    public void 切换特效音开关()  // CloseOrOpenEffectMusic()
    {
        音频源管理器实例.切换特效音开关();  // mAudioSourceManager.CloseOrOpenEffectMusic()
    }

    // 播放按钮音效
    public void 播放按钮音效()  // PlayButtonAudioClip()
    {
        音频源管理器实例.播放按钮音效();  // mAudioSourceManager.PlayButtonAudioClip()
    }

    // 播放翻书音效
    public void 播放翻书音效()  // PlayPagingAudioClip()
    {
        音频源管理器实例.播放翻书音效();  // mAudioSourceManager.PlayPagingAudioClip()
    }
}