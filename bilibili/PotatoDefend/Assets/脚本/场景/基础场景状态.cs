using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface 基础场景状态 
{
    // 进入场景（场景切换进入时执行的逻辑）
    void 进入场景();  // EnterScene()
    // 退出场景（场景切换离开时执行的逻辑）
    void 退出场景();  // ExitScene()
}

 