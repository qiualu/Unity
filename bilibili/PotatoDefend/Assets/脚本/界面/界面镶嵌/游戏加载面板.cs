using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 游戏加载面板（原GameLoadPanel）
public class 游戏加载面板 : 基于基础界面  // 原BasePanel
{
    // 进入面板（原EnterPanel）
    public override void 进入面板()
    {
        gameObject.SetActive(true);
        transform.SetSiblingIndex(8);
    }

    // 初始化面板（原InitPanel）
    public override void 初始化面板()
    {
        base.初始化面板();
        gameObject.SetActive(false);
    }
}
