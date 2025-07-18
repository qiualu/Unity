using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 基于基础场景状态 : 基础场景状态
{
    protected 界面外观 界面外观实例;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="界面外观">界面外观类实例</param>
    public 基于基础场景状态(界面外观 界面外观)
    {
        界面外观实例 = 界面外观;
    }

    /// <summary>
    /// 进入场景（虚方法，可被子类重写）
    /// </summary>
    public virtual void 进入场景()
    {
        界面外观实例.初始化字典();
    }

    /// <summary>
    /// 退出场景（虚方法，可被子类重写）
    /// </summary>
    public virtual void 退出场景()
    {
        界面外观实例.清空字典();
    }

     

}





 







