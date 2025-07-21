using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

[RequireComponent(typeof(Transform))]
public class 自动缩放 : MonoBehaviour
{
    [Header("缩放设置")]
    [Tooltip("最小缩放比例")]
    public Vector3 最小缩放 = new Vector3(0.8f, 0.8f, 0.8f);

    [Tooltip("最大缩放比例")]
    public Vector3 最大缩放 = new Vector3(1.2f, 1.2f, 1.2f);

    [Tooltip("动画速度")]
    public float 速度 = 1f;

    [Tooltip("是否使用平滑曲线")]
    public bool 使用平滑曲线 = true;

    [Tooltip("是否在开始时随机延迟，避免多个对象同步")]
    public bool 随机开始延迟 = false;

    [Tooltip("随机延迟的最大时间")]
    public float 最大延迟时间 = 1f;

    private float 当前插值时间;
    private float 延迟计时器;
    private bool 正在放大 = true;
    private bool 延迟完成 = false;

    private Vector3 初始缩放;

    void Start()
    {
        // 记录初始缩放比例，作为基准
        初始缩放 = transform.localScale;

        // 如果需要随机延迟
        if (随机开始延迟)
        {
            延迟计时器 = Random.Range(0f, 最大延迟时间);
            延迟完成 = false;
        }
        else
        {
            延迟完成 = true;
        }
    }

    void Update()
    {
        // 处理延迟
        if (!延迟完成)
        {
            延迟计时器 -= Time.deltaTime;
            if (延迟计时器 <= 0)
            {
                延迟完成 = true;
            }
            return;
        }

        // 计算插值时间
        当前插值时间 += Time.deltaTime * 速度;

        // 确定当前应该使用的缩放方向
        Vector3 目标缩放;
        if (正在放大)
        {
            目标缩放 = Vector3.Scale(初始缩放, 最大缩放);
            if (当前插值时间 >= 1f)
            {
                当前插值时间 = 0f;
                正在放大 = false;
            }
        }
        else
        {
            目标缩放 = Vector3.Scale(初始缩放, 最小缩放);
            if (当前插值时间 >= 1f)
            {
                当前插值时间 = 0f;
                正在放大 = true;
            }
        }

        // 计算插值比例，使用平滑曲线或线性
        float 插值比例 = 使用平滑曲线 ? Mathf.SmoothStep(0f, 1f, 当前插值时间) : 当前插值时间;

        // 应用缩放
        if (正在放大)
        {
            transform.localScale = Vector3.Lerp(
                Vector3.Scale(初始缩放, 最小缩放),
                目标缩放,
                插值比例
            );
        }
        else
        {
            transform.localScale = Vector3.Lerp(
                Vector3.Scale(初始缩放, 最大缩放),
                目标缩放,
                插值比例
            );
        }
    }

    // 重置动画
    public void 重置动画()
    {
        当前插值时间 = 0f;
        正在放大 = true;
        transform.localScale = Vector3.Scale(初始缩放, 最小缩放);
    }
}
