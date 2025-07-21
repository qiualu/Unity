using UnityEngine;
using TMPro;
using System;

public class 倒计时 : MonoBehaviour
{
    [Header("文本组件")]
    public TextMeshProUGUI 倒计时文本;

    [Header("倒计时设置")]
    public int 总分钟 = 0;   // 分钟数
    public int 总秒数 = 10;  // 秒数（总时长 = 总分钟×60 + 总秒数）

    // 倒计时结束时调用的函数
    public Action 倒计时结束后执行;

    private float 剩余总秒数;
    private bool 正在倒计时 = false;

    // Unity生命周期：初始化（英文命名，确保被调用）
    private void Awake()
    {
        // 初始化文本组件
        if (倒计时文本 == null)
            倒计时文本 = GetComponent<TextMeshProUGUI>();

        if (倒计时文本 == null)
        {
            Debug.LogError("未找到Text (TMP) 组件！", this);
            return;
        }

        // 初始隐藏
        倒计时文本.gameObject.SetActive(false);
    }

    // 游戏开始时自动启动倒计时
    private void Start()
    {
        开始倒计时();
    }

    /// <summary>
    /// 开始倒计时
    /// </summary>
    public void 开始倒计时()
    {
        if (倒计时文本 == null) return;

        // 计算总秒数（分钟转秒 + 剩余秒数）
        剩余总秒数 = 总分钟 * 60 + 总秒数;
        正在倒计时 = true;
        倒计时文本.gameObject.SetActive(true);
        更新显示格式();
    }

    /// <summary>
    /// 强制结束倒计时
    /// </summary>
    public void 强制结束倒计时()
    {
        if (!正在倒计时) return;

        正在倒计时 = false;
        处理结束逻辑();
    }

    // Unity生命周期：每帧更新（必须用英文Update，否则不执行）
    private void Update()
    {
        if (!正在倒计时 || 倒计时文本 == null) return;

        // 减少剩余时间（每帧减Time.deltaTime）
        剩余总秒数 -= Time.deltaTime;

        // 确保时间不小于0
        if (剩余总秒数 < 0)
            剩余总秒数 = 0;

        // 更新显示
        更新显示格式();

        // 检查是否结束
        if (剩余总秒数 <= 0)
        {
            正在倒计时 = false;
            处理结束逻辑();
        }
    }

    /// <summary>
    /// 按 00:00 格式更新显示
    /// </summary>
    private void 更新显示格式()
    {
        // 计算当前分钟和秒数
        int 当前分钟 = (int)剩余总秒数 / 60;
        int 当前秒数 = (int)剩余总秒数 % 60;

        // 格式化为两位数（不足补0）
        倒计时文本.text = $"{当前分钟:D2}:{当前秒数:D2}";
    }

    /// <summary>
    /// 处理倒计时结束逻辑
    /// </summary>
    private void 处理结束逻辑()
    {
        倒计时文本.text = "00:00"; // 结束时显示00:00
        倒计时结束后执行?.Invoke(); // 调用结束函数
        Invoke(nameof(隐藏文本), 1f); // 1秒后隐藏
    }

    private void 隐藏文本()
    {
        倒计时文本.gameObject.SetActive(false);
        Debug.Log("倒计时 结束了 ");
        土豆忍者管理类.土豆忍者管理.爆炸结束游戏_倒计时();
        
    }
}
