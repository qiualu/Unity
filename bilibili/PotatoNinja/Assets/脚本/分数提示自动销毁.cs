using UnityEngine;
using TMPro;

public class 分数提示自动销毁 : MonoBehaviour
{
    [Tooltip("显示持续时间（秒）")]
    public float 持续时间 = 1f;
    [Tooltip("淡出开始时间（秒）")]
    public float 淡出开始时间 = 0.5f;
    [Tooltip("上浮距离（像素）")]
    public float 上浮距离 = 50f;

    private RectTransform rectTransform;
    private TextMeshProUGUI 文本组件;
    private Vector2 初始位置; // 正确的初始位置（炸弹所在位置）
    private float 存活时间;

    private void Awake()
    {
        // 只获取组件，不记录位置
        rectTransform = GetComponent<RectTransform>();
        文本组件 = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // 关键：在Start中记录位置（此时定位代码已执行完毕）
        if (rectTransform != null)
        {
            初始位置 = rectTransform.anchoredPosition;
            Debug.Log($"记录正确初始位置：{初始位置}", this);
        }
    }

    private void Update()
    {
        存活时间 += Time.deltaTime;

        // 计算进度（0到1）
        float 总进度 = 存活时间 / 持续时间;
        float 移动进度 = Mathf.Min(总进度, 1f);
        float 淡出进度 = Mathf.Max(0, 存活时间 - 淡出开始时间) / (持续时间 - 淡出开始时间);

        // 实现上浮效果（基于正确的初始位置）
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector2(
                初始位置.x,
                初始位置.y + 上浮距离 * 移动进度 // 从初始位置向上移动
            );
        }

        // 实现淡出效果
        if (文本组件 != null)
        {
            Color 文本颜色 = 文本组件.color;
            文本颜色.a = 1f - 淡出进度;
            文本组件.color = 文本颜色;
        }

        // 时间到则销毁
        if (存活时间 >= 持续时间)
        {
            Destroy(gameObject);
        }
    }
}
