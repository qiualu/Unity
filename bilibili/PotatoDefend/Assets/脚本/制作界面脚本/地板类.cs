using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
using UnityEngine.UI;


public class 地板类 : MonoBehaviour
{

    [Header("引用设置")]
    public GameObject 地板元素;  // 地板的视觉元素
    private CanvasGroup 画布组;   // 用于控制透明度

    [Header("动画设置")]
    public float 淡入时长 = 0.3f;
    public float 淡出时长 = 0.3f;
    public float 自动隐藏延迟 = 3f;  // 无操作自动隐藏延迟

    [Header("状态数据")]
    public int 地板id = -1;        // 唯一标识
    public Vector2Int 网格坐标;    // 网格位置
    private bool 已激活 = false;   // 是否处于激活状态

 
    public int 地板X = -1;
    public int 地板Y = -1;

    // 自动隐藏的协程
    private Coroutine 自动隐藏协程;

    private void Awake()
    {
        初始化画布组();
    }

    private void Start()
    {
        // 开机时自动执行淡出（从可见到透明）
        初始淡出();
    }

    /// <summary>
    /// 初始化画布组组件
    /// </summary>
    private void 初始化画布组()
    {
        if (地板元素 == null) return;

        画布组 = 地板元素.GetComponent<CanvasGroup>();
        if (画布组 == null)
        {
            画布组 = 地板元素.AddComponent<CanvasGroup>();
        }

        // 初始设置
        画布组.alpha = 1;  // 初始可见，准备执行淡出
        画布组.blocksRaycasts = true;
    }


    /// <summary>
    /// 初始淡出效果（开机时执行）
    /// </summary>
    private void 初始淡出()
    {
        if (画布组 != null)
        {
            画布组.DOFade(0, 淡出时长)
                  .SetEase(Ease.InQuad)
                  .OnComplete(() =>
                  {
                      已激活 = false;
                      画布组.blocksRaycasts = false;
                  });
        }
    }

    // 鼠标进入地板范围时
    private void OnMouseEnter() {
        if (!已激活)
        {
            transform.DOScale(Vector3.one * 1.05f, 0.2f).SetEase(Ease.InOutQuad);
        }
    }

    // 鼠标离开地板范围时
    private void OnMouseExit() {
        if (!已激活)
        {
            transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutQuad);
        }
    }

    // 鼠标按下时
    private void OnMouseDown()
    {
        //Debug.Log($"鼠标按下时 地板id:{地板id}  调试模式: {调试模式} ");
        // 向管理类发送点击信号（仅传递信息）
        if (保卫萝卜开始管理.游戏管理 != null)
        {
            //保卫萝卜开始管理.游戏管理.接收地板点击(this);
        }

        // 自身处理显示逻辑
        处理点击();
    }

    /// <summary>
    /// 处理点击事件，控制自身显示状态
    /// </summary>
    private void 处理点击()
    {
        if (已激活)
        {
            // 已激活状态下点击，立即淡出
            执行淡出();
        }
        else
        {
            // 未激活状态下点击，执行淡入并启动自动隐藏
            执行淡入();
        }
    }

    /// <summary>
    /// 执行淡入动画
    /// </summary>
    private void 执行淡入()
    {
        // 停止任何正在进行的动画和协程
        停止所有动画和协程();

        已激活 = true;
        画布组.blocksRaycasts = true;

        // 淡入动画
        画布组.DOFade(1, 淡入时长).SetEase(Ease.OutQuad);
        transform.DOScale(Vector3.one * 1.1f, 淡入时长).SetEase(Ease.OutBack);

        // 启动自动隐藏
        自动隐藏协程 = StartCoroutine(等待自动隐藏());
    }

    /// <summary>
    /// 执行淡出动画
    /// </summary>
    private void 执行淡出()
    {
        停止所有动画和协程();

        // 淡出动画
        画布组.DOFade(0, 淡出时长).SetEase(Ease.InQuad);
        transform.DOScale(Vector3.one, 淡出时长).SetEase(Ease.InBack)
                 .OnComplete(() =>
                 {
                     已激活 = false;
                     画布组.blocksRaycasts = false;
                 });
    }

    /// <summary>
    /// 等待自动隐藏的协程
    /// </summary>
    private IEnumerator 等待自动隐藏()
    {
        yield return new WaitForSeconds(自动隐藏延迟);

        // 如果仍然处于激活状态，则执行淡出
        if (已激活)
        {
            执行淡出();
        }
    }


    /// <summary>
    /// 停止所有动画和协程
    /// </summary>
    private void 停止所有动画和协程()
    {
        // 停止协程
        if (自动隐藏协程 != null)
        {
            StopCoroutine(自动隐藏协程);
            自动隐藏协程 = null;
        }

        // 停止DOTween动画
        transform.DOKill();
        if (画布组 != null)
        {
            画布组.DOKill();
        }
    }


    // 鼠标松开时
    private void OnMouseUp() { }

    // 从对象池激活时重置状态
    private void OnEnable(){}

 
 
}
