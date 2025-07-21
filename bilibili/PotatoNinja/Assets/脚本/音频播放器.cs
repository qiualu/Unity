using System.Collections;  // 音频播放器
using System.Collections.Generic;
using UnityEngine;


//音频管理.播放("背景音乐");
//音频管理.播放("发射土豆");
//音频管理.播放("切水果");
//音频管理.播放("水果切开");
//音频管理.播放("爆炸音响");
//土豆忍者管理类.土豆忍者管理.音频管理.播放("切水果");
//土豆忍者管理类.土豆忍者管理.音频管理.播放("爆炸音响");
//土豆忍者管理类.土豆忍者管理.音频管理.播放("发射土豆");


// 音频信息类：用bool类型区分音频类型
public class 音频信息
{
    public bool 是长音频;     // true=长音频，false=片段音频
    public string 路径;       // Resources下的路径
    public bool 循环;         // 是否循环播放
    public bool 播放状态;     // 当前播放状态
    public AudioSource 音频源; // 音频源组件
    public AudioClip 音频片段; // 加载后的音频片段  

    // 构造函数：参数改为bool类型
    public 音频信息(bool 是长音频, string 路径, bool 循环)
    {
        this.是长音频 = 是长音频;
        this.路径 = 路径;
        this.循环 = 循环;
        this.播放状态 = false; // 默认未播放
        this.音频源 = null;
        this.音频片段 = null;
    }
}

public class 音频播放器
{
    // 核心字典：音频名称 -> 音频信息对象
    private Dictionary<string, 音频信息> 音频字典 = new Dictionary<string, 音频信息>()
    {
        { "发射土豆", new 音频信息(false, "音效/发射土豆", false) },  // false=片段音频
        { "背景音乐", new 音频信息(true, "音效/欢快游戏背景音乐_爱给网_aigei_com", true) },  // true=长音频
        { "切水果", new 音频信息(false, "音效/切水果的声音音效_爱给网_aigei_com", false) },
        { "水果切开", new 音频信息(false, "音效/水果切开音效", false) },
        { "爆炸音响", new 音频信息(false, "音效/爆炸音响", false) }
    };

    // 音量属性
    public float 长音频音量 { get; set; }
    public float 片段音频音量 { get; set; }


    private GameObject 音频父对象;


    // 构造函数：接收父容器和音量参数
    public 音频播放器(GameObject 父容器, float 长音频初始音量 = 0.2f, float 片段音频初始音量 = 0.8f)
    {
        // 初始化音量（使用传入的参数或默认值）
        长音频音量 = 长音频初始音量;
        片段音频音量 = 片段音频初始音量;

        // 初始化父对象
        音频父对象 = new GameObject("音频播放器");
        音频父对象.transform.parent = 父容器.transform;
        GameObject.DontDestroyOnLoad(音频父对象);

        // 初始化资源
        初始化音频资源();
    }

    /// <summary>
    /// 初始化音频资源
    /// </summary>
    private void 初始化音频资源()
    {
        foreach (var 键值对 in 音频字典)
        {
            string 名称 = 键值对.Key;
            音频信息 信息 = 键值对.Value;

            // 创建音频源
            GameObject 源对象 = new GameObject($"音频源_{名称}");
            源对象.transform.parent = 音频父对象.transform;
            AudioSource 源 = 源对象.AddComponent<AudioSource>();

            // 配置音频源（用bool类型判断）
            源.loop = 信息.循环;
            源.volume = 信息.是长音频 ? 长音频音量 : 片段音频音量; // bool判断更简洁
            信息.音频源 = 源;

            // 加载音频片段
            AudioClip 片段 = Resources.Load<AudioClip>(信息.路径);
            if (片段 != null)
            {
                信息.音频片段 = 片段;
            }
            else
            {
                Debug.LogError($"音频加载失败：{信息.路径}");
            }
        }
    }


    /// <summary>
    /// 播放音频（用bool类型区分逻辑）
    /// </summary>
    public void 播放(string 名称)
    {
        if (!音频字典.TryGetValue(名称, out 音频信息 信息) ||
            信息.音频源 == null || 信息.音频片段 == null)
        {
            Debug.LogError($"播放失败：未找到音频 {名称}");
            return;
        }

        信息.播放状态 = true;

        // 长音频逻辑（true）
        if (信息.是长音频)
        {
            if (!信息.音频源.isPlaying)
            {
                信息.音频源.clip = 信息.音频片段;
                信息.音频源.Play();
            }
        }
        // 片段音频逻辑（false）
        else
        {
            信息.音频源.PlayOneShot(信息.音频片段);
        }
    }


    /// <summary>
    /// 停止音频
    /// </summary>
    public void 停止(string 名称)
    {
        if (音频字典.TryGetValue(名称, out 音频信息 信息) && 信息.音频源 != null)
        {
            信息.音频源.Stop();
            信息.播放状态 = false;
        }
    }
}


