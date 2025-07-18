using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频资源工厂
/// </summary>

public class 音频片段工厂 : 泛型资源工厂接口<AudioClip>
{

    //protected Dictionary<string, AudioClip> factoryDict = new Dictionary<string, AudioClip>();
    protected Dictionary<string, AudioClip> 工厂字典 = new Dictionary<string, AudioClip>();  // factoryDict

    protected string 加载路径;  // loadPath

    public 音频片段工厂()  // AudioClipFactory()
    {
        加载路径 = "AudioClips/";  // loadPath = "AudioClips/";
    }

    public AudioClip 获取单个资源(string 资源路径)  // GetSingleResources(string resourcePath)
    {
        AudioClip 音频资源 = null;  // AudioClip itemGo = null;
        string 完整加载路径 = 加载路径 + 资源路径;  // string itemLoadPath = loadPath + resourcePath;

        if (工厂字典.ContainsKey(资源路径))  // if (factoryDict.ContainsKey(resourcePath))
        {
            音频资源 = 工厂字典[资源路径];  // itemGo = factoryDict[resourcePath];
        }
        else
        {
            音频资源 = Resources.Load<AudioClip>(完整加载路径);  // itemGo = Resources.Load<AudioClip>(itemLoadPath);
            工厂字典.Add(资源路径, 音频资源);  // factoryDict.Add(resourcePath, itemGo);
        }

        if (音频资源 == null)  // if (itemGo==null)
        {
            Debug.Log(资源路径 + "的资源获取失败，失败路径为:" + 完整加载路径);  // Debug.Log(resourcePath + "的资源获取失败，失败路径为:" + itemLoadPath);
        }

        return 音频资源;  // return itemGo;
    }

}


// 泛型资源工厂基类
