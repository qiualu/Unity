using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础面板（对应原BasePanel）
/// </summary>
public class 基于基础界面 : MonoBehaviour, 基础界面  // 继承MonoBehaviour，实现基础界面接口（对应原IBasePanel）
{
    // 界面外观实例（对应原mUIFacade）
    protected 界面外观 界面外观实例;

    /// <summary>
    /// 进入面板（对应原EnterPanel）
    /// </summary>
    public virtual void 进入面板()
    {

    }

    /// <summary>
    /// 退出面板（对应原ExitPanel）
    /// </summary>
    public virtual void 退出面板()
    {

    }

    /// <summary>
    /// 初始化面板（对应原InitPanel）
    /// </summary>
    public virtual void 初始化面板()
    {

    }

    /// <summary>
    /// 更新面板（对应原UpdatePanel）
    /// </summary>
    public virtual void 更新面板()
    {

    }

    /// <summary>
    /// 唤醒时初始化（对应原Awake）
    /// </summary>
    protected virtual void Awake()
    {
        // 获取界面外观实例（对应原mUIFacade = GameManager.Instance.uiManager.mUIFacade）
        界面外观实例 = 游戏管理.实例.界面管理.界面外观实例;
    }
}

