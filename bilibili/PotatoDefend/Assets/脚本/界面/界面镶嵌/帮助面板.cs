using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

// 帮助面板（原HelpPanel）：用于显示游戏帮助、怪物说明、塔楼说明等页面
public class 帮助面板 : 基于基础界面  // 原BasePanel（基类统一为“基础面板”）
{
    private GameObject 帮助页面游戏对象;  // 原helpPageGo：帮助页面的根对象
    private GameObject 怪物页面游戏对象;  // 原monsterPageGo：怪物说明页面的根对象
    private GameObject 塔楼页面游戏对象;  // 原towerPageGo：塔楼说明页面的根对象
    private 滑动视图 滑动视图组件;  // 原slideScrollView：塔楼页面的滑动视图组件
    private 可覆盖滑动视图 可覆盖滑动视图组件;  // 原slideCanCoverScrollView：帮助页面的滑动视图组件
    private Tween 帮助面板动画;  // 原helpPanelTween：面板移动到中心的动画


    protected override void Awake()
    {
        base.Awake();
        帮助页面游戏对象 = transform.Find("HelpPage").gameObject;  // 查找帮助页面对象
        怪物页面游戏对象 = transform.Find("MonsterPage").gameObject;  // 查找怪物页面对象
        塔楼页面游戏对象 = transform.Find("TowerPage").gameObject;  // 查找塔楼页面对象
        可覆盖滑动视图组件 = transform.Find("HelpPage").Find("Scroll View").GetComponent<可覆盖滑动视图>();  // 获取帮助页面滑动组件
        滑动视图组件 = transform.Find("TowerPage").Find("Scroll View").GetComponent<滑动视图>();  // 获取塔楼页面滑动组件
        帮助面板动画 = transform.DOLocalMoveX(0, 0.5f);  // 创建面板移动到中心的动画（0.5秒）
        帮助面板动画.SetAutoKill(false);  // 动画不自动销毁（可重复使用）
        帮助面板动画.Pause();  // 暂停动画（等待需要时播放）
    }

    //显示页面的方法  // 原//显示页面的方法
    public void 显示帮助页面()  // 原ShowHelpPage()
    {
        if (!帮助页面游戏对象.activeSelf)  // 如果帮助页面未激活
        {
            界面外观实例.播放按钮音效();  // 原mUIFacade.PlayButtonAudioClip()：播放按钮点击音效
            帮助页面游戏对象.SetActive(true);  // 激活帮助页面
        }
        怪物页面游戏对象.SetActive(false);  // 隐藏怪物页面
        塔楼页面游戏对象.SetActive(false);  // 隐藏塔楼页面
    }

    public void 显示怪物页面()  // 原ShowMonsterPage()
    {
        界面外观实例.播放按钮音效();  // 播放按钮音效
        帮助页面游戏对象.SetActive(false);  // 隐藏帮助页面
        怪物页面游戏对象.SetActive(true);  // 激活怪物页面
        塔楼页面游戏对象.SetActive(false);  // 隐藏塔楼页面
    }

    public void 显示塔楼页面()  // 原ShowTowerPage()
    {
        界面外观实例.播放按钮音效();  // 播放按钮音效
        帮助页面游戏对象.SetActive(false);  // 隐藏帮助页面
        怪物页面游戏对象.SetActive(false);  // 隐藏怪物页面
        塔楼页面游戏对象.SetActive(true);  // 激活塔楼页面
    }

    //处理面板的方法  // 原//处理面板的方法
    public override void 初始化面板()  // 原InitPanel()
    {
        base.初始化面板();  // 调用基类初始化方法

        transform.SetSiblingIndex(5);  // 设置面板层级（确保显示在正确层级）
        滑动视图组件.初始化();  // 原slideScrollView.Init()：初始化塔楼页面滑动组件
        可覆盖滑动视图组件.初始化();  // 原slideCanCoverScrollView.Init()：初始化帮助页面滑动组件
        显示帮助页面();  // 默认显示帮助页面

        //其他处理  // 原//其他处理
        if (transform.localPosition == Vector3.zero)  // 如果面板在中心位置
        {
            gameObject.SetActive(false);  // 隐藏面板
            帮助面板动画.PlayBackwards();  // 播放动画反向（移回初始位置）
        }
        transform.localPosition = new Vector3(1920, 0, 0);  // 初始位置设置在右侧（屏幕外）
    }

    public override void 进入面板()  // 原EnterPanel()
    {
        base.进入面板();  // 调用基类进入方法
        gameObject.SetActive(true);  // 显示面板
        滑动视图组件.初始化();  // 重新初始化滑动组件
        可覆盖滑动视图组件.初始化();  // 重新初始化滑动组件
        移动到中心();  // 播放动画移到屏幕中心
    }

    public override void 退出面板()  // 原ExitPanel()
    {
        base.退出面板();  // 调用基类退出方法
        界面外观实例.播放按钮音效();  // 播放按钮音效

        //在冒险模式选择场景  // 原//在冒险模式选择场景
        if (界面外观实例.当前场景状态.GetType() == typeof(普通游戏选项场景状态))  // 原NormalGameOptionSceneState
        {
            界面外观实例.切换场景状态(new 主场景状态(界面外观实例));  // 原MainSceneState：切换到主场景状态
            SceneManager.LoadScene(1);  // 加载索引1的场景（主场景）
        }
        else  //如果是在主场景  // 原//如果是在主场景
        {
            帮助面板动画.PlayBackwards();  // 播放动画反向（移回右侧）
            界面外观实例.当前场景面板字典[字符串管理.主面板].进入面板();  // 原EnterPanel()：激活主面板
        }
    }

    public void 移动到中心()  // 原MoveToCenter()
    {
        帮助面板动画.PlayForward();  // 播放动画正向（移到屏幕中心）
    }
}