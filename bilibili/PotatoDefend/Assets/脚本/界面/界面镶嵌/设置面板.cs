using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // 必须添加这行，导入 Tween 类型
using UnityEngine.UI;


public class 设置面板 : 基于基础界面
{
    private GameObject 选项页面游戏对象;  // 原optionPageGo
    private GameObject 统计页面游戏对象;  // 原statisticsPageGo
    private GameObject 制作人员页面游戏对象;  // 原producerPageGo
    private GameObject 重置面板游戏对象;  // 原panel_ResetGo
    private Tween 设置面板动画;  // 原setPanelTween
    private bool 播放背景音乐 = true;  // 原playBGMusic
    private bool 播放音效 = true;  // 原playEffectMusic
    public Sprite[] 按钮精灵数组;  // 原btnSprites; //0.音效开 1.音效关 2.背景音乐开 3.背景音乐关
    private Image 音效按钮图片;  // 原Img_Btn_EffectAudio
    private Image 背景音乐按钮图片;  // 原Img_Btn_BGAudio
    public Text[] 统计文本数组;  // 原statisticesTexts


    protected override void Awake()
    {
        base.Awake();
        设置面板动画 = transform.DOLocalMoveX(0, 0.5f);  // 原setPanelTween = transform.DOLocalMoveX(0, 0.5f);
        设置面板动画.SetAutoKill(false);  // 原setPanelTween.SetAutoKill(false);
        设置面板动画.Pause();  // 原setPanelTween.Pause();
        选项页面游戏对象 = transform.Find("OptionPage").gameObject;  // 原optionPageGo = transform.Find("OptionPage").gameObject;
        统计页面游戏对象 = transform.Find("StatisticsPage").gameObject;  // 原statisticsPageGo = transform.Find("StatisticsPage").gameObject;
        制作人员页面游戏对象 = transform.Find("ProducerPage").gameObject;  // 原producerPageGo = transform.Find("ProducerPage").gameObject;
        音效按钮图片 = 选项页面游戏对象.transform.Find("Btn_EffectAudio").GetComponent<Image>();  // 原Img_Btn_EffectAudio = optionPageGo.transform.Find("Btn_EffectAudio").GetComponent<Image>();
        背景音乐按钮图片 = 选项页面游戏对象.transform.Find("Btn_BGAudio").GetComponent<Image>();  // 原Img_Btn_BGAudio = optionPageGo.transform.Find("Btn_BGAudio").GetComponent<Image>();
        重置面板游戏对象 = transform.Find("Panel_Reset").gameObject;  // 原panel_ResetGo = transform.Find("Panel_Reset").gameObject;
        //InitPanel();  // 原//InitPanel();
    }




    public override void 初始化面板()  // 原InitPanel()
    {
        transform.localPosition = new Vector3(-1920, 0, 0);  // 原transform.localPosition = new Vector3(-1920,0,0);
        transform.SetSiblingIndex(2);  // 原transform.SetSiblingIndex(2);
    }

    //显示页面的方法  // 原//显示页面的方法
    public void 显示选项页面()  // 原ShowOptionPage()
    {
        if (!选项页面游戏对象.activeSelf)  // 原if (!optionPageGo.activeSelf)
        {
            界面外观实例.播放按钮音效();  // mUIFacade.PlayButtonAudioClip();
            选项页面游戏对象.SetActive(true);  // 原optionPageGo.SetActive(true);
        }
        统计页面游戏对象.SetActive(false);  // 原statisticsPageGo.SetActive(false);
        制作人员页面游戏对象.SetActive(false);  // 原producerPageGo.SetActive(false);
    }

    public void 显示统计页面()  // 原ShowStatisticsPage()
    {
        界面外观实例.播放按钮音效();  // mUIFacade.PlayButtonAudioClip();
        选项页面游戏对象.SetActive(false);  // 原optionPageGo.SetActive(false);
        统计页面游戏对象.SetActive(true);  // 原statisticsPageGo.SetActive(true);
        制作人员页面游戏对象.SetActive(false);  // 原producerPageGo.SetActive(false);
        显示统计数据();  // 原ShowStatistics();
    }

    public void 显示制作人员页面()  // 原ShowProducerPage()
    {
        界面外观实例.播放按钮音效();  // mUIFacade.PlayButtonAudioClip();
        选项页面游戏对象.SetActive(false);  // 原optionPageGo.SetActive(false);
        统计页面游戏对象.SetActive(false);  // 原statisticsPageGo.SetActive(false);
        制作人员页面游戏对象.SetActive(true);  // 原producerPageGo.SetActive(true);
    }

    //进入退出页面的方法  // 原//进入退出页面的方法
    public override void 进入面板()  // 原EnterPanel()
    {
        显示选项页面();  // 原ShowOptionPage();
        移动到中心();  // 原MoveToCenter();
    }

    public override void 退出面板()  // 原ExitPanel()
    {
        界面外观实例.播放按钮音效();  // mUIFacade.PlayButtonAudioClip();
        设置面板动画.PlayBackwards();  // 原setPanelTween.PlayBackwards();
        界面外观实例.当前场景面板字典[字符串管理.主面板].进入面板();  // mUIFacade.currentScenePanelDict[StringManager.MainPanel].EnterPanel();
        初始化面板();  // 原InitPanel();
    }


    public void 移动到中心()  // 原MoveToCenter()
    {
        设置面板动画.PlayForward();  // 原setPanelTween.PlayForward();
    }

    /// <summary>
    /// 音乐处理  // 原/// <summary>/// 音乐处理/// </summary>
    /// </summary>
    public void 关闭或开启背景音乐()  // 原CloseOrOpenBGMusic()
    {
        界面外观实例.播放按钮音效();  // mUIFacade.PlayButtonAudioClip();
        播放背景音乐 = !播放背景音乐;  // 原playBGMusic = !playBGMusic;
        界面外观实例.切换背景音乐开关();  // mUIFacade.CloseOrOpenBGMusic();
        if (播放背景音乐)  // 原if (playBGMusic)
        {
            背景音乐按钮图片.sprite = 按钮精灵数组[2];  // 原Img_Btn_BGAudio.sprite = btnSprites[2];
        }
        else
        {
            背景音乐按钮图片.sprite = 按钮精灵数组[3];  // 原Img_Btn_BGAudio.sprite = btnSprites[3];
        }
    }

    public void 关闭或开启音效()  // 原CloseOrOpenEffectMusic()
    {
        界面外观实例.播放按钮音效();  // mUIFacade.PlayButtonAudioClip();
        播放音效 = !播放音效;  // 原playEffectMusic = !playEffectMusic;
        界面外观实例.切换特效音开关();  // mUIFacade.CloseOrOpenEffectMusic();
        if (播放音效)  // 原if (playEffectMusic)
        {
            音效按钮图片.sprite = 按钮精灵数组[0];  // 原Img_Btn_EffectAudio.sprite = btnSprites[0];
        }
        else
        {
            音效按钮图片.sprite = 按钮精灵数组[1];  // 原Img_Btn_EffectAudio.sprite = btnSprites[1];
        }
    }

    //数据显示  // 原//数据显示
    public void 显示统计数据()  // 原ShowStatistics()
    {
        玩家管理器 玩家管理器实例 = 界面外观实例.玩家管理器实例;  // 原PlayerManager playerManager = 界面外观实例.mPlayerManager;
        统计文本数组[0].text = 玩家管理器实例.冒险模式解锁地图数.ToString();  // 原statisticesTexts[0].text = playerManager.adventrueModelNum.ToString();
        统计文本数组[1].text = 玩家管理器实例.隐藏关卡解锁地图数.ToString();  // 原statisticesTexts[1].text = playerManager.burriedLevelNum.ToString();
        统计文本数组[2].text = 玩家管理器实例.BOSS模式击败数.ToString();  // 原statisticesTexts[2].text = playerManager.bossModelNum.ToString();
        统计文本数组[3].text = 玩家管理器实例.金币总数.ToString();  // 原statisticesTexts[3].text = playerManager.coin.ToString();
        统计文本数组[4].text = 玩家管理器实例.杀怪总数.ToString();  // 原statisticesTexts[4].text = playerManager.killMonsterNum.ToString();
        统计文本数组[5].text = 玩家管理器实例.击败BOSS总数.ToString();  // 原statisticesTexts[5].text = playerManager.killBossNum.ToString();
        统计文本数组[6].text = 玩家管理器实例.清理道具总数.ToString();  // 原statisticesTexts[6].text = playerManager.clearItemNum.ToString();
    }

    //重置游戏  // 原//重置游戏
    public void 重置游戏()  // 原ResetGame()
    {
        界面外观实例.播放按钮音效();  // mUIFacade.PlayButtonAudioClip();
        GameManager.Instance.initPlayerManager = true;  // 原GameManager.Instance.initPlayerManager = true;
        GameManager.Instance.playerManager.ReadData();  // 原GameManager.Instance.playerManager.ReadData();
        显示统计数据();  // 原ShowStatistics();
        关闭重置面板();  // 原CloseResetPanel();
    }

    public void 显示重置面板()  // 原ShowResetPanel()
    {
        重置面板游戏对象.SetActive(true);  // 原panel_ResetGo.SetActive(true);
    }

    public void 关闭重置面板()  // 原CloseResetPanel()
    {
        界面外观实例.播放按钮音效();  // mUIFacade.PlayButtonAudioClip();
        重置面板游戏对象.SetActive(false);  // 原panel_ResetGo.SetActive(false);
    }

 

}
