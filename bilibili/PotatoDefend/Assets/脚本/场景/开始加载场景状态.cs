using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///    StartLoadSceneState  BaseSceneState
/// </summary>
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
