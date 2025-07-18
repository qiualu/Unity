using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface 基础界面 // IBasePanel
{
    // 初始化面板
    void 初始化面板();  // InitPanel()

    // 进入面板（显示面板时调用）
    void 进入面板();  // EnterPanel()

    // 退出面板（隐藏/关闭面板时调用）
    void 退出面板();  // ExitPanel()

    // 更新面板（每帧刷新逻辑）
    void 更新面板();  // UpdatePanel()
}



