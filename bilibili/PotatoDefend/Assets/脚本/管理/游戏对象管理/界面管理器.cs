using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责管理UI的管理者
/// </summary>
public class 界面管理器  // UIManager
{
    public 界面外观 界面外观实例;  // UIFacade mUIFacade;
    public Dictionary<string, GameObject> 当前场景面板字典;  // Dictionary<string, GameObject> currentScenePanelDict;
    private 游戏管理 游戏管理实例;  // GameManager mGameManager;

    public 界面管理器()  // UIManager()
    {
        游戏管理实例 = 游戏管理.实例;  // mGameManager = GameManager.Instance;
        当前场景面板字典 = new Dictionary<string, GameObject>();  // currentScenePanelDict = new Dictionary<string, GameObject>();
        界面外观实例 = new 界面外观(this);  // mUIFacade = new UIFacade(this);
        //界面外观实例.当前场景状态 = new 开始加载场景状态(界面外观实例);  // mUIFacade.currentSceneState = new StartLoadSceneState(mUIFacade);
    }

    // 将UIPanel放回工厂
    private void 放回界面面板(string 界面面板名称, GameObject 界面面板对象)  // PushUIPanel(string uiPanelName,GameObject uiPanelGo)
    {
        游戏管理实例.将游戏物体放回对象池(工厂类型类.界面面板工厂, 界面面板名称, 界面面板对象);  // mGameManager.PushGameObjectToFactory(FactoryType.UIPanelFactory,uiPanelName, uiPanelGo);
    }

    // 清空字典
    public void 清空字典()  // ClearDict()
    {
        foreach (var 项 in 当前场景面板字典)  // foreach (var item in currentScenePanelDict)
        {
            放回界面面板(项.Value.name.Substring(0, 项.Value.name.Length - 7), 项.Value);  // PushUIPanel(item.Value.name.Substring(0,item.Value.name.Length-7),item.Value);
        }

        当前场景面板字典.Clear();  // currentScenePanelDict.Clear();
    }

}

