using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 开始加载界面 : 基于基础界面
{


    // 界面外观实例（对应原mUIFacade）
    protected 界面外观 界面外观实例;

    /// <summary>
    /// 唤醒时初始化（对应原Awake()）
    /// </summary>
    protected override void Awake()  // 保留Unity生命周期方法原名，便于识别
    {
        base.Awake();  // 调用父类Awake方法
                       // 延迟2秒调用加载下一场景方法（对应原Invoke("LoadNextScene", 2)）
        Invoke("加载下一场景", 2);
    }

    // 每帧更新（对应原Update()，保留原名）
    void Update()
    {

    }

    /// <summary>
    /// 加载下一场景（对应原LoadNextScene()）
    /// </summary>
    private void 加载下一场景()
    {
        // 切换场景状态为新的主场景状态（对应原mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade))）
        界面外观实例.切换场景状态(new 主场景状态(界面外观实例));  // 主场景状态对应原MainSceneState
    }


}



 