using UnityEngine;

[AddComponentMenu("动画/自动缩放管理器")]
public class 自动缩放管理器 : MonoBehaviour
{
    [Header("缩放范围设置")]
    [Tooltip("动画开始时的缩放比例（相对于原始大小）")]
    [Range(0.1f, 2f)] public float 初始缩放 = 1f;

    [Tooltip("最大缩放比例（相对于原始大小）")]
    [Range(0.5f, 3f)] public float 最大缩放 = 1.2f;

    [Tooltip("最小缩放比例（相对于原始大小）")]
    [Range(0.1f, 1f)] public float 最小缩放 = 0.8f;

    [Header("动画速度设置")]
    [Tooltip("缩放变化的速度（值越大动画越快）")]
    [Range(0.1f, 2f)] public float 缩放速度 = 0.3f;

    [Tooltip("开始动画前的延迟时间（秒）")]
    public float 延迟时间 = 0f;

    [Header("动画模式设置")]
    [Tooltip("是否循环播放缩放动画")]
    public bool 循环播放 = true;

    [Tooltip("是否先缩小后放大（默认先放大后缩小）")]
    public bool 从缩小开始 = false;

    [Tooltip("禁用后重新启用时是否重置动画")]
    public bool 启用时重置 = true;

    // 私有变量
    private Vector3 原始缩放;
    private float 当前缩放系数;
    private int 缩放方向 = 1;
    private float 延迟计时器;
    private bool 正在动画 = false;

    private void Awake()
    {
        原始缩放 = transform.localScale;
        //Debug.Log($"[{gameObject.name}] 初始化原始缩放: {原始缩放}", this);
    }

    private void OnEnable()
    {
        //Debug.Log($"[{gameObject.name}] 脚本启用，开始准备动画", this);
        if (启用时重置)
        {
            重置动画();
        }
        延迟计时器 = 0;
        正在动画 = false;
    }

    private void Update()
    {
        // 检查是否暂停或禁用
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        // 延迟逻辑
        if (延迟计时器 < 延迟时间)
        {
            延迟计时器 += Time.deltaTime;
            if (延迟计时器 >= 延迟时间 - 0.1f) // 接近延迟结束时提示
            {
                //Debug.Log($"[{gameObject.name}] 延迟结束，即将开始动画", this);
            }
            return;
        }

        // 开始动画
        if (!正在动画)
        {
            正在动画 = true;
            当前缩放系数 = 初始缩放;
            缩放方向 = 从缩小开始 ? -1 : 1;
            //Debug.Log($"[{gameObject.name}] 开始动画，初始缩放系数: {当前缩放系数}", this);
        }

        更新缩放动画();
    }

    private void 更新缩放动画()
    {
        当前缩放系数 += 缩放速度 * Time.deltaTime * 缩放方向;
        transform.localScale = 原始缩放 * 当前缩放系数;

        // 边界检测
        if (当前缩放系数 >= 最大缩放)
        {
            当前缩放系数 = 最大缩放;
            缩放方向 = -1;
            //Debug.Log($"[{gameObject.name}] 达到最大缩放，开始缩小", this);
        }
        else if (当前缩放系数 <= 最小缩放)
        {
            当前缩放系数 = 最小缩放;
            if (!循环播放)
            {
                正在动画 = false;
                //Debug.Log($"[{gameObject.name}] 达到最小缩放，动画结束", this);
                return;
            }
            缩放方向 = 1;
            //Debug.Log($"[{gameObject.name}] 达到最小缩放，开始放大", this);
        }
    }

    // 公共方法（保持不变）
    public void 重置动画()
    {
        当前缩放系数 = 初始缩放;
        transform.localScale = 原始缩放 * 当前缩放系数;
        缩放方向 = 从缩小开始 ? -1 : 1;
        延迟计时器 = 0;
        正在动画 = false;
        //Debug.Log($"[{gameObject.name}] 动画已重置", this);
    }

    public void 触发一次动画()
    {
        循环播放 = false;
        重置动画();
        //Debug.Log($"[{gameObject.name}] 触发单次动画", this);
    }

    public void 暂停动画()
    {
        正在动画 = false;
        //Debug.Log($"[{gameObject.name}] 动画已暂停", this);
    }

    public void 继续动画()
    {
        if (延迟计时器 >= 延迟时间)
        {
            正在动画 = true;
            //Debug.Log($"[{gameObject.name}] 动画继续", this);
        }
    }
}
