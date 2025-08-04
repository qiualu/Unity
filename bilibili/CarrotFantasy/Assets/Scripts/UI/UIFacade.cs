using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// UI中介，上层与管理者们做交互，下层与UI面板进行交互
/// </summary>
public class UIFacade
{
    //管理者
    private UIManager mUIManager;
    private GameManager mGameManager;
    private AudioSourceManager mAudioSourceManager;
    public PlayerManager mPlayerManager;
    //UI面板
    public Dictionary<string, IBasePanel> currentScenePanelDict = new Dictionary<string, IBasePanel>();
    //其他成员变量
    private GameObject mask;
    private Image maskImage;
    public Transform canvasTransform;
    //场景状态
    public IBaseSceneState currentSceneState;
    public IBaseSceneState lastSceneState;

    public UIFacade(UIManager uiManager)
    {
        mGameManager = GameManager.Instance;
        mPlayerManager = mGameManager.playerManager;
        mUIManager = uiManager;
        mAudioSourceManager = mGameManager.audioSourceManager;
        InitMask();
    }

    //初始化遮罩
    public void InitMask()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        //mask = mGameManager.factoryManager.factoryDict[FactoryType.UIFactory].GetItem("Img_Mask");
        mask = CreateUIAndSetUIPosition("Img_Mask");
        maskImage = mask.GetComponent<Image>();
    }


    //改变当前场景的状态
    public void ChangeSceneState(IBaseSceneState baseSceneState)
    {
        lastSceneState = currentSceneState;
        ShowMask();
        currentSceneState = baseSceneState;
    }

    //显示遮罩
    public void ShowMask()
    {
        mask.transform.SetSiblingIndex(10);
        Tween t = DOTween.To(()=>maskImage.color,toColor=>maskImage.color=toColor,new Color(0,0,0,1),2f);
        t.OnComplete(ExitSceneComplete);
    }

    //离开当前场景
    private void ExitSceneComplete()
    {
        lastSceneState.ExitScene();
        currentSceneState.EnterScene();
        HideMask();
    }

    //隐藏遮罩
    public void HideMask()
    {
        mask.transform.SetSiblingIndex(10);
        DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 0), 2f);
    }



    //实例化当前场景所有面板，并存入字典
    public void InitDict()
    {
        foreach (var item in mUIManager.currentScenePanelDict)
        {
            item.Value.transform.SetParent(canvasTransform);
            item.Value.transform.localPosition = Vector3.zero;
            item.Value.transform.localScale = Vector3.one;
            IBasePanel basePanel = item.Value.GetComponent<IBasePanel>();
            if (basePanel==null)
            {
                Debug.Log("获取面板上IBasePanel脚本失败");
            }
            basePanel.InitPanel();
            currentScenePanelDict.Add(item.Key,basePanel);
        }
    }

    //清空UIPanel字典
    public void ClearDict()
    {
        currentScenePanelDict.Clear();
        mUIManager.ClearDict();
    }


    //添加UIPanel到UIManager字典
    public void AddPanelToDict(string uiPanelName)
    {
        mUIManager.currentScenePanelDict.Add(uiPanelName, GetGameObjectResource(FactoryType.UIPanelFactory, uiPanelName));
    }


    //实例化UI
    public GameObject CreateUIAndSetUIPosition(string uiName)
    {
        GameObject itemGo=GetGameObjectResource(FactoryType.UIFactory,uiName);
        itemGo.transform.SetParent(canvasTransform);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;
        return itemGo;
    }

    //获取资源
    public Sprite GetSprite(string resourcePath)
    {
        return mGameManager.GetSprite(resourcePath);
    }

    public AudioClip GetAudioSource(string resourcePath)
    {
        return mGameManager.GetAudioClip(resourcePath);
    }

    public RuntimeAnimatorController GetRuntimeAnimatorController(string resourcePath)
    {
        return mGameManager.GetRunTimeAnimatorController(resourcePath);
    }

    //获取游戏物体
    public GameObject GetGameObjectResource(FactoryType factoryType, string resourcePath)
    {
        return mGameManager.GetGameObjectResource(factoryType,resourcePath);
    }
    //将游戏物体放回对象池
    public void PushGameObjectToFactory(FactoryType factoryType, string resourcePath, GameObject itemGo)
    {
        mGameManager.PushGameObjectToFactory(factoryType, resourcePath, itemGo);
    }



    /// <summary>
    /// 音乐播放的有关方法
    /// </summary>
    
    //开关音乐
    public void CloseOrOpenBGMusic()
    {
        mAudioSourceManager.CloseOrOpenBGMusic();
    }

    public void CloseOrOpenEffectMusic()
    {
        mAudioSourceManager.CloseOrOpenEffectMusic();
    }

    //播放按钮音效
    public void PlayButtonAudioClip()
    {
        mAudioSourceManager.PlayButtonAudioClip();
    }

    //播放翻书音效
    public void PlayPagingAudioClip()
    {
        mAudioSourceManager.PlayPagingAudioClip();
    }





    /// <summary>
    /// 打印当前场景面板字典中的所有面板信息（键名和对应对象）
    /// </summary>
    public void 打印场景面板字典列表()
    {
        if (currentScenePanelDict == null)
        {
            Debug.LogWarning("currentScenePanelDict 尚未初始化（为null）");
            return;
        }

        if (currentScenePanelDict.Count == 0)
        {
            Debug.Log("currentScenePanelDict 为空，没有任何面板");
            return;
        }

        Debug.Log($"===== 当前面板字典包含 {currentScenePanelDict.Count} 个面板 =====");
        foreach (var panelPair in currentScenePanelDict)
        {
            // 输出键名、对应对象的类型和是否为null
            string panelInfo = $"键名: {panelPair.Key}\n" +
                              $"对象类型: {panelPair.Value?.GetType().Name ?? "null"}\n" +
                              $"是否为空: {panelPair.Value == null}";
            Debug.Log(panelInfo);
        }
        Debug.Log("======================================");
    }






}
