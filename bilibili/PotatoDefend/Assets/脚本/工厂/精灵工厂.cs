using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 精灵资源工厂
/// </summary>
public class 精灵工厂 : 泛型资源工厂接口<Sprite>  // SpriteFactory : IBaseRescrousFactory<Sprite>
{
    protected Dictionary<string, Sprite> 工厂字典 = new Dictionary<string, Sprite>();  // factoryDict

    protected string 加载路径;  // loadPath

    public 精灵工厂()  // SpriteFactory()
    {
        加载路径 = "Pictures/";  // loadPath = "Pictures/";
    }

    public Sprite 获取单个资源(string 资源路径)  // GetSingleResources(string resourcePath)
    {
        Sprite 精灵资源 = null;  // Sprite itemGo = null;
        string 完整加载路径 = 加载路径 + 资源路径;  // string itemLoadPath = loadPath + resourcePath;

        if (工厂字典.ContainsKey(资源路径))  // if (factoryDict.ContainsKey(resourcePath))
        {
            精灵资源 = 工厂字典[资源路径];  // itemGo = factoryDict[resourcePath];
        }
        else
        {
            精灵资源 = Resources.Load<Sprite>(完整加载路径);  // itemGo = Resources.Load<Sprite>(itemLoadPath);
            工厂字典.Add(资源路径, 精灵资源);  // factoryDict.Add(resourcePath, itemGo);
        }

        if (精灵资源 == null)  // if (itemGo == null)
        {
            Debug.Log(资源路径 + "的资源获取失败，失败路径为:" + 完整加载路径);  // Debug.Log(resourcePath + "的资源获取失败，失败路径为:" + itemLoadPath);
        }

        return 精灵资源;  // return itemGo;
    }
}