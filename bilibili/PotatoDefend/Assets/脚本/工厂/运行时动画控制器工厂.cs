using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画控制器资源工厂
/// </summary>
public class 运行时动画控制器工厂 : 泛型资源工厂接口<RuntimeAnimatorController>  // RuntimeAnimatorControllerFactory : IBaseRescrousFactory<RuntimeAnimatorController>
{
    protected Dictionary<string, RuntimeAnimatorController> 工厂字典 = new Dictionary<string, RuntimeAnimatorController>();  // factoryDict

    protected string 加载路径;  // loadPath

    public 运行时动画控制器工厂()  // RuntimeAnimatorControllerFactory()
    {
        加载路径 = "Animator/AnimatorController/";  // loadPath = "Animator/AnimatorController/";
    }

    public RuntimeAnimatorController 获取单个资源(string 资源路径)  // GetSingleResources(string resourcePath)
    {
        RuntimeAnimatorController 动画控制器资源 = null;  // RuntimeAnimatorController itemGo = null;
        string 完整加载路径 = 加载路径 + 资源路径;  // string itemLoadPath = loadPath + resourcePath;

        if (工厂字典.ContainsKey(资源路径))  // if (factoryDict.ContainsKey(resourcePath))
        {
            动画控制器资源 = 工厂字典[资源路径];  // itemGo = factoryDict[resourcePath];
        }
        else
        {
            动画控制器资源 = Resources.Load<RuntimeAnimatorController>(完整加载路径);  // itemGo = Resources.Load<RuntimeAnimatorController>(itemLoadPath);
            工厂字典.Add(资源路径, 动画控制器资源);  // factoryDict.Add(resourcePath, itemGo);
        }

        if (动画控制器资源 == null)  // if (itemGo == null)
        {
            Debug.Log(资源路径 + "的资源获取失败，失败路径为:" + 完整加载路径);  // Debug.Log(resourcePath + "的资源获取失败，失败路径为:" + itemLoadPath);
        }

        return 动画控制器资源;  // return itemGo;
    }
}