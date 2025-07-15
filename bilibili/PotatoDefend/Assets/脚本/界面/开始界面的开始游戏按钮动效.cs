using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 开始界面的开始游戏按钮动效 : MonoBehaviour
{

    [Header("大小变化设置")]
    public float minScale = 0.9f;    // 最小缩放值
    public float maxScale = 1.1f;    // 最大缩放值
    public float speed = 1f;         // 变化速度

    private RectTransform buttonRect;
    private Vector3 originalScale;

    void Start()
    {
        buttonRect = GetComponent<RectTransform>();
        originalScale = buttonRect.localScale;
    }

    void Update()
    {
        // 计算当前缩放比例（基于正弦波的平滑变化）
        float scaleFactor = Mathf.Lerp(minScale, maxScale,
            (Mathf.Sin(Time.time * speed) + 1) * 0.5f);

        // 应用缩放
        buttonRect.localScale = originalScale * scaleFactor;
    }
}
