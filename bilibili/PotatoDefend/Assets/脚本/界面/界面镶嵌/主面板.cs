using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class 主面板 : 基于基础界面
{
    private Animator 胡萝卜动画器; // carrotAnimator：胡萝卜动画组件
    private Transform 怪物变换; // monsterTrans：怪物的Transform组件
    private Transform 云朵变换; // cloudTrans：云朵的Transform组件
    private Tween[] 主面板动画数组; // mainPanelTween：主面板的动画数组（0.右移动画，1.左移动画）
    private Tween 退出动画; // ExitTween：离开主面板时播放的动画


    protected override void Awake()
    {
        base.Awake();
        // 获取成员变量
        transform.SetSiblingIndex(8); // 设置UI层级索引
        胡萝卜动画器 = transform.Find("Emp_Carrot").GetComponent<Animator>(); // 查找胡萝卜动画组件
        胡萝卜动画器.Play("CarrotGrow"); // 播放胡萝卜生长动画
        怪物变换 = transform.Find("Img_Monster"); // 查找怪物的Transform
        云朵变换 = transform.Find("Img_Cloud"); // 查找云朵的Transform

        主面板动画数组 = new Tween[2]; // 初始化动画数组
        主面板动画数组[0] = transform.DOLocalMoveX(1920, 0.5f); // 右移动画（X轴移到1920，时长0.5秒）
        主面板动画数组[0].SetAutoKill(false); // 禁用自动销毁
        主面板动画数组[0].Pause(); // 暂停动画
        主面板动画数组[1] = transform.DOLocalMoveX(-1920, 0.5f); // 左移动画（X轴移到-1920，时长0.5秒）
        主面板动画数组[1].SetAutoKill(false); // 禁用自动销毁
        主面板动画数组[1].Pause(); // 暂停动画

        播放UI动画(); // 播放UI元素动画
    }

    public override void 进入面板()
    {
        transform.SetSiblingIndex(8); // 设置UI层级索引
        胡萝卜动画器.Play("CarrotGrow"); // 播放胡萝卜生长动画
        if (退出动画 != null)
        {
            退出动画.PlayBackwards(); // 反向播放退出动画（回到初始位置）
        }
        云朵变换.gameObject.SetActive(true); // 显示云朵
    }

    public override void 退出面板()
    {
        退出动画.PlayForward(); // 正向播放退出动画（离开视野）
        云朵变换.gameObject.SetActive(false); // 隐藏云朵
    }

    // UI动画播放
    private void 播放UI动画()
    {
        // 怪物Y轴往复运动（目标Y=600，时长1.5秒，无限循环，往返模式）
        怪物变换.DOLocalMoveY(600, 1.5f).SetLoops(-1, LoopType.Yoyo);
        // 云朵X轴循环移动（目标X=1300，时长8秒，无限循环，重启模式）
        云朵变换.DOLocalMoveX(1300, 8f).SetLoops(-1, LoopType.Restart);
    }

    public void 移动到右侧()
    {
        界面外观实例.播放按钮音效(); // 播放按钮点击音效
        退出动画 = 主面板动画数组[0]; // 赋值右移动画为退出动画
        // 进入设置面板
        界面外观实例.当前场景面板字典[字符串管理.设置面板].进入面板();
    }

    public void 移动到左侧()
    {
        界面外观实例.播放按钮音效(); // 播放按钮点击音效
        退出动画 = 主面板动画数组[1]; // 赋值左移动画为退出动画
        // 进入帮助面板
        界面外观实例.当前场景面板字典[字符串管理.帮助面板].进入面板();
    }

    // 场景状态切换的方法 开始游戏按钮
    public void 到普通模式场景()
    {
        Debug.Log(" 到普通模式场景 ");

          
        //界面外观实例.播放按钮音效(); // 播放按钮点击音效
        //// 进入游戏加载面板
        //界面外观实例.当前场景面板字典[字符串管理.游戏加载面板].进入面板();
        //// 切换到普通游戏选项场景状态
        //界面外观实例.切换场景状态(new 普通游戏选项场景状态(界面外观实例));
    }

    public void 到Boss模式场景()
    {
        界面外观实例.播放按钮音效(); // 播放按钮点击音效
        // 进入游戏加载面板
        界面外观实例.当前场景面板字典[字符串管理.游戏加载面板].进入面板();
        // 切换到Boss游戏选项场景状态
        界面外观实例.切换场景状态(new Boss游戏选项场景状态(界面外观实例));
    }

    public void 到怪物巢穴()
    {
        界面外观实例.播放按钮音效(); // 播放按钮点击音效
        // 进入游戏加载面板
        界面外观实例.当前场景面板字典[字符串管理.游戏加载面板].进入面板();
        // 切换到怪物巢穴场景状态
        界面外观实例.切换场景状态(new 怪物巢穴场景状态(界面外观实例));
    }

    public void 退出游戏()
    {
        界面外观实例.播放按钮音效(); // 播放按钮点击音效
        游戏管理.实例.玩家管理.保存数据(); // 保存玩家数据
        Application.Quit(); // 退出应用
    }
}