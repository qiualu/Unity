using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 主场景状态 : 基于基础场景状态
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="界面外观">界面外观实例（对应原uiFacade）</param>
    public 主场景状态(界面外观 界面外观实例) : base(界面外观实例)
    {
    }

    /// <summary>
    /// 进入场景（重写父类方法）
    /// </summary>
    public override void 进入场景()
    {
        // 向字典添加各面板（对应原AddPanelToDict）
        界面外观实例.向字典添加面板(字符串管理.主面板);
        界面外观实例.向字典添加面板(字符串管理.设置面板);
        界面外观实例.向字典添加面板(字符串管理.帮助面板);
        界面外观实例.向字典添加面板(字符串管理.游戏加载面板);

        // 调用父类进入场景方法
        base.进入场景();

        // 播放主场景背景音乐
        游戏管理.实例.音频源管理.播放背景音乐(
            游戏管理.实例.工厂管理.音频片段工厂实例.获取单个资源("Main/BGMusic")
        );
    }

    /// <summary>
    /// 退出场景（重写父类方法）
    /// </summary>
    public override void 退出场景()
    {
        // 调用父类退出场景方法
        base.退出场景();

        // 根据当前场景状态类型决定加载哪个场景
        if (界面外观实例.当前场景状态.GetType() == typeof(普通游戏选项场景状态))
        {
            SceneManager.LoadScene(2);  // 加载普通游戏场景（索引2）
        }
        else if (界面外观实例.当前场景状态.GetType() == typeof(Boss游戏选项场景状态))
        {
            SceneManager.LoadScene(3);  // 加载Boss游戏场景（索引3）
        }
        else
        {
            SceneManager.LoadScene(6);  // 加载默认场景（索引6）
        }
    }
}
