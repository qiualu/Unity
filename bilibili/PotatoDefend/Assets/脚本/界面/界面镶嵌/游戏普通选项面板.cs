using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 关卡选择共用面板
/// </summary>
public class 游戏普通选项面板 : 基于基础界面
{  // 对应原GameNormalOptionPanel : BasePanel

    [HideInInspector]  // 保持Unity特性，不在Inspector显示
    public bool 是否在大关卡面板 = true;  // 对应原isInBigLevelPanel

    /// <summary>
    /// 返回上一级面板
    /// </summary>
    public void 返回上一面板()  // 对应原ReturnToLastPanel()
    {
        if (是否在大关卡面板)
        {
            // 返回主界面（切换到主场景状态）
            界面外观实例.切换场景状态(new 主场景状态(界面外观实例));  // 对应原MainSceneState
        }
        else
        {
            // 返回大关卡选择面板
            界面外观实例.当前场景面板字典[字符串管理.游戏普通关卡面板].退出面板();  // 对应原ExitPanel()
            界面外观实例.当前场景面板字典[字符串管理.游戏普通大关卡面板].进入面板();  // 对应原EnterPanel()
        }

        // 播放按钮音效
        界面外观实例.播放按钮音效();  // 对应原PlayButtonAudioClip()
        是否在大关卡面板 = true;  // 重置状态
    }

    /// <summary>
    /// 跳转到帮助面板
    /// </summary>
    public void 跳转到帮助面板()  // 对应原ToHelpPanel()
    {
        界面外观实例.播放按钮音效();  // 播放按钮音效
        界面外观实例.当前场景面板字典[字符串管理.帮助面板].进入面板();  // 显示帮助面板
    }
}