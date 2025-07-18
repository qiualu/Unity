using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责控制音乐的播放和停止以及游戏中各种音效的播放
/// </summary>
public class 音频源管理器  // AudioSourceManager
{
    private AudioSource[] 音频源数组;  // audioSource;//0.播放BGMusic 1.播放特效音
    private bool 允许播放特效音 = true;  // 原: playEffectMusic（修改字段名，避免与方法重名）
    private bool 允许播放背景音乐 = true;  // 原: playBGMusic（同步修改，保持命名风格一致）

    // 构造函数
    public 音频源管理器()  // AudioSourceManager()
    {
        音频源数组 = 游戏管理.实例.GetComponents<AudioSource>();  // audioSource = GameManager.Instance.GetComponents<AudioSource>();
    }

    // 播放背景音乐
    public void 播放背景音乐(AudioClip 音频片段)  // PlayBGMusic(AudioClip audioClip)
    {
        if (!音频源数组[0].isPlaying || 音频源数组[0].clip != 音频片段)
        {
            音频源数组[0].clip = 音频片段;
            音频源数组[0].Play();
        }
    }

    // 播放音效（方法名保留，与字段区分）
    public void 播放特效音(AudioClip 音频片段)  // PlayEffectMusic(AudioClip audioClip)
    {
        if (允许播放特效音)  // 字段名修改后，这里同步更新
        {
            音频源数组[1].PlayOneShot(音频片段);
        }
    }

    public void 关闭背景音乐()  // CloseBGMusic()
    {
        音频源数组[0].Stop();
    }

    public void 开启背景音乐()  // OpenBGMusic()
    {
        音频源数组[0].Play();
    }

    public void 切换背景音乐开关()  // CloseOrOpenBGMusic()
    {
        允许播放背景音乐 = !允许播放背景音乐;  // 字段名同步修改
        if (允许播放背景音乐)
        {
            开启背景音乐();
        }
        else
        {
            关闭背景音乐();
        }
    }

    public void 切换特效音开关()  // CloseOrOpenEffectMusic()
    {
        允许播放特效音 = !允许播放特效音;  // 字段名同步修改
    }

    // 按钮音效播放
    public void 播放按钮音效()  // PlayButtonAudioClip()
    {
        播放特效音(游戏管理.实例.工厂管理.音频片段工厂实例.获取单个资源("Main/Button"));
    }

    // 翻书音效播放
    public void 播放翻书音效()  // PlayPagingAudioClip()
    {
        播放特效音(游戏管理.实例.工厂管理.音频片段工厂实例.获取单个资源("Main/Paging"));
    }
}