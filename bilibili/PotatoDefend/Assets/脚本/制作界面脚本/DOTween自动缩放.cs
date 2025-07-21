using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Transform))]
public class DOTween自动缩放 : MonoBehaviour
{
    [Header("缩放参数")]
    [Tooltip("最小缩放比例（相对于初始大小）")]
    public Vector3 最小缩放比例 = new Vector3(0.8f, 0.8f, 0.8f);

    [Tooltip("最大缩放比例（相对于初始大小）")]
    public Vector3 最大缩放比例 = new Vector3(1.2f, 1.2f, 1.2f);

    [Tooltip("单次缩放动画时长（秒）")]
    public float 动画时长 = 1f;

    [Tooltip("缩放动画的缓动类型")]
    public Ease 缓动类型 = Ease.InOutSine;

    [Tooltip("是否随机开始延迟")]
    public bool 启用随机延迟 = false;

    [Tooltip("最大延迟时间（秒）")]
    public float 最大延迟 = 1f;

    private Vector3 初始缩放;
    private Tweener 缩放动画;
    private bool 动画是否暂停 = false; // 手动记录动画状态

    void Start()
    {
        初始缩放 = transform.localScale;
        开始缩放动画();
    }

    /// <summary>
    /// 开始缩放动画序列
    /// </summary>
    private void 开始缩放动画()
    {
        // 计算目标缩放值
        Vector3 最大目标缩放 = Vector3.Scale(初始缩放, 最大缩放比例);
        Vector3 最小目标缩放 = Vector3.Scale(初始缩放, 最小缩放比例);

        // 计算延迟时间
        float 延迟时间 = 启用随机延迟 ? Random.Range(0, 最大延迟) : 0;

        // 创建循环动画
        缩放动画 = transform.DOScale(最大目标缩放, 动画时长)
            .SetEase(缓动类型)
            .SetDelay(延迟时间)
            .OnComplete(() =>
            {
                // 完成后反向缩放
                transform.DOScale(最小目标缩放, 动画时长)
                    .SetEase(缓动类型)
                    .OnComplete(开始缩放动画); // 循环
            });

        动画是否暂停 = false;
    }

    /// <summary>
    /// 暂停动画
    /// </summary>
    public void 暂停动画()
    {
        if (缩放动画 != null && !动画是否暂停)
        {
            缩放动画.Pause();
            动画是否暂停 = true;
        }
    }

    /// <summary>
    /// 继续动画
    /// </summary>
    public void 继续动画()
    {
        if (缩放动画 != null && 动画是否暂停)
        {
            缩放动画.Play();
            动画是否暂停 = false;
        }
    }

    /// <summary>
    /// 停止并重置动画
    /// </summary>
    public void 停止动画()
    {
        if (缩放动画 != null)
        {
            缩放动画.Kill();
            transform.localScale = 初始缩放;
            动画是否暂停 = false;
        }
    }

    void OnDestroy()
    {
        // 销毁时清理动画，防止内存泄漏
        if (缩放动画 != null)
        {
            缩放动画.Kill();
        }
        DOTween.Clear();
    }
}
