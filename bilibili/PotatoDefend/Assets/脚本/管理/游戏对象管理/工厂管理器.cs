using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 工厂管理器  // FactoryManager
{

    //public Dictionary<FactoryType, IBaseFacotry> factoryDict = new Dictionary<FactoryType, IBaseFacotry>();
    public Dictionary<工厂类型类, 基础工厂基类> 工厂字典 = new Dictionary<工厂类型类, 基础工厂基类>();
    //public AudioClipFactory audioClipFactory;
    public 音频片段工厂 音频片段工厂实例;  // AudioClipFactory audioClipFactory;
    //public SpriteFactory spriteFactory;
    public 精灵工厂 精灵工厂实例;  // SpriteFactory spriteFactory;

    //public RuntimeAnimatorControllerFactory runtimeAnimatorControllerFactory;
    public 运行时动画控制器工厂 运行时动画控制器工厂实例;  // RuntimeAnimatorControllerFactory runtimeAnimatorControllerFactory;


    //public FactoryManager()
    //{
    //    factoryDict.Add(FactoryType.UIPanelFactory, new UIPanelFactory());
    //    factoryDict.Add(FactoryType.UIFactory, new UIFactory());
    //    factoryDict.Add(FactoryType.GameFactory, new GameFactory());
    //    audioClipFactory = new AudioClipFactory();
    //    spriteFactory = new SpriteFactory();
    //    runtimeAnimatorControllerFactory = new RuntimeAnimatorControllerFactory();
    //}
     
 
    public 工厂管理器()  // FactoryManager()
    {
        工厂字典.Add(工厂类型类.界面面板工厂, new 界面面板工厂());  // factoryDict.Add(FactoryType.UIPanelFactory,new UIPanelFactory());
        工厂字典.Add(工厂类型类.界面工厂, new 界面工厂());  // factoryDict.Add(FactoryType.UIFactory, new UIFactory());
        工厂字典.Add(工厂类型类.游戏工厂, new 游戏工厂());  // factoryDict.Add(FactoryType.GameFactory, new GameFactory());
        音频片段工厂实例 = new 音频片段工厂();  // audioClipFactory = new AudioClipFactory();
        精灵工厂实例 = new 精灵工厂();  // spriteFactory = new SpriteFactory();
        运行时动画控制器工厂实例 = new 运行时动画控制器工厂();  // runtimeAnimatorControllerFactory = new RuntimeAnimatorControllerFactory();
    }


}
