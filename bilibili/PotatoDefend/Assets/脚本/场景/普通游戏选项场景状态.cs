using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 普通游戏选项场景状态（对应原NormalGameOptionSceneState）
/// </summary>
public class 普通游戏选项场景状态 : 基于基础场景状态  // 继承自基础场景状态（对应原BaseSceneState）
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="界面外观实例">界面外观实例（对应原uiFacade）</param>
    public 普通游戏选项场景状态(界面外观 界面外观实例) : base(界面外观实例)
    {
    }

    /// <summary>
    /// 进入场景（重写父类方法）
    /// </summary>
    public override void 进入场景()
    {
        // 向字典添加各面板（对应原AddPanelToDict）
        界面外观实例.向字典添加面板(字符串管理.游戏普通选项面板);  // 对应原GameNormalOptionPanel
        界面外观实例.向字典添加面板(字符串管理.游戏普通大关卡面板);  // 对应原GameNormalBigLevelPanel
        界面外观实例.向字典添加面板(字符串管理.游戏普通关卡面板);  // 对应原GameNormalLevelPanel
        界面外观实例.向字典添加面板(字符串管理.帮助面板);  // 对应原HelpPanel
        界面外观实例.向字典添加面板(字符串管理.游戏加载面板);  // 对应原GameLoadPanel

        // 调用父类进入场景方法
        base.进入场景();
    }

    /// <summary>
    /// 退出场景（重写父类方法）
    /// </summary>
    public override void 退出场景()
    {
        // 获取普通游戏选项面板实例（对应原GameNormalOptionPanel）
        游戏普通选项面板 游戏普通选项面板实例 = 界面外观实例.当前场景面板字典[字符串管理.游戏普通选项面板] as 游戏普通选项面板;

        // 根据面板状态判断加载哪个场景
        if (游戏普通选项面板实例.是否在大关卡面板)  // 对应原isInBigLevelPanel
        {
            SceneManager.LoadScene(1);  // 加载场景索引1
        }
        else
        {
            SceneManager.LoadScene(3);  // 加载场景索引3
        }

        // 重置面板状态
        游戏普通选项面板实例.是否在大关卡面板 = true;

        // 调用父类退出场景方法
        base.退出场景();
    }
}