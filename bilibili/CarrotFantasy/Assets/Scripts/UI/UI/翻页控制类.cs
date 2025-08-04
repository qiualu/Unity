using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// 滑动滚动视图控制器
/// 实现横向滑动翻页功能，支持拖拽翻页和按钮翻页，常用于关卡选择、图片浏览等场景
/// </summary>
public class 翻页控制类 : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// 内容区域的RectTransform组件（ScrollRect的content）
    /// </summary>
    private RectTransform contentTrans;
    /// <summary>
    /// 拖拽开始时的鼠标X坐标
    /// </summary>
    private float beginMousePositionX;
    /// <summary>
    /// 拖拽结束时的鼠标X坐标
    /// </summary>
    private float endMousePositionX;
    /// <summary>
    /// 滚动视图组件（ScrollRect）
    /// </summary>
    private ScrollRect scrollRect;

    /// <summary>
    /// 单个单元格的长度（宽度）
    /// </summary>
    public int cellLength;
    /// <summary>
    /// 单元格之间的间距
    /// </summary>
    public int spacing;
    /// <summary>
    /// </summary>
    public int leftOffset;
    /// <summary>
    /// 滑动一个项目的总长度（单元格长度 + 间距）
    /// </summary>
    private float moveOneItemLength;

    /// <summary>
    /// 内容区域上一次的位置（用于计算滑动距离）
    /// </summary>
    private Vector3 currentContentLocalPos;
    /// <summary>
    /// 内容区域的初始位置
    /// </summary>
    private Vector3 contentInitPos;
    /// <summary>
    /// 内容区域的初始大小
    /// </summary>
    private Vector2 contentTransSize;

    /// <summary>
    /// 总项目数量（总页数）
    /// </summary>
    public int totalItemNum;
    /// <summary>
    /// 当前所在的索引（当前页码，从1开始）
    /// </summary>
    private int currentIndex;

    /// <summary>
    /// 显示页码的文本组件（如"1/5"）
    /// </summary>
    public Text pageText;

    /// <summary>
    /// 是否需要发送翻页消息给父对象
    /// </summary>
    public bool needSendMessage;

    private void Awake()
    {
        // 获取ScrollRect组件和内容区域引用
        scrollRect = GetComponent<ScrollRect>();
        contentTrans = scrollRect.content;
        // 计算滑动一个项目的总长度（单元格长度 + 间距）
        moveOneItemLength = cellLength + spacing;
        // 记录初始位置和大小
        currentContentLocalPos = contentTrans.localPosition;
        contentTransSize = contentTrans.sizeDelta;
        contentInitPos = contentTrans.localPosition;
        // 初始页码设为1
        currentIndex = 1;
        // 更新页码显示
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
    }

    /// <summary>
    /// 初始化滚动视图，重置到初始状态
    /// </summary>
    public void Init()
    {
        // 重置页码为1
        currentIndex = 1;

        // 重置内容区域位置
        if (contentTrans != null)
        {
            contentTrans.localPosition = contentInitPos;
            currentContentLocalPos = contentInitPos;
            // 更新页码显示
            if (pageText != null)
            {
                pageText.text = currentIndex.ToString() + "/" + totalItemNum;
            }
        }
    }

    /// <summary>
    /// 拖拽结束时触发（实现IEndDragHandler接口）
    /// 根据拖拽方向和距离判断翻页方向，并执行滑动动画
    /// </summary>
    /// <param name="eventData">拖拽事件数据</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        // 记录拖拽结束时的鼠标X坐标
        endMousePositionX = Input.mousePosition.x;
        // 计算拖拽偏移量和需要滑动的距离
        float offSetX = 0;
        float moveDistance = 0;
        offSetX = beginMousePositionX - endMousePositionX;

        if (offSetX > 0) // 鼠标向右滑动（内容向左移动，翻到下一页）
        {
            // 如果已经是最后一页，不执行操作
            if (currentIndex >= totalItemNum)
            {
                return;
            }
            // 如果需要发送消息，通知父对象翻到下一页
            if (needSendMessage)
            {
                UpdatePanel(true);
            }
            // 计算滑动距离（向左滑动一个项目长度）
            moveDistance = -moveOneItemLength;
            // 页码+1
            currentIndex++;
        }
        else // 鼠标向左滑动（内容向右移动，翻到上一页）
        {
            // 如果已经是第一页，不执行操作
            if (currentIndex <= 1)
            {
                return;
            }
            // 如果需要发送消息，通知父对象翻到上一页
            if (needSendMessage)
            {
                UpdatePanel(false);
            }
            // 计算滑动距离（向右滑动一个项目长度）
            moveDistance = moveOneItemLength;
            // 页码-1
            currentIndex--;
        }
        // 更新页码显示
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
        // 执行滑动动画（使用DOTween实现平滑过渡）
        DOTween.To(() => contentTrans.localPosition, lerpValue => contentTrans.localPosition = lerpValue, currentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        // 更新当前位置记录
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
        // 播放翻页音效
        GameManager.Instance.audioSourceManager.PlayPagingAudioClip();
    }

    /// <summary>
    /// 点击按钮翻到下一页
    /// </summary>
    public void ToNextPage()
    {
        float moveDistance = 0;
        // 如果已经是最后一页，不执行操作
        if (currentIndex >= totalItemNum)
        {
            return;
        }
        // 计算滑动距离（向左滑动一个项目长度）
        moveDistance = -moveOneItemLength;
        // 页码+1
        currentIndex++;
        // 更新页码显示
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
        // 如果需要发送消息，通知父对象翻到下一页
        if (needSendMessage)
        {
            UpdatePanel(true);
        }
        // 执行滑动动画
        DOTween.To(() => contentTrans.localPosition, lerpValue => contentTrans.localPosition = lerpValue, currentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        // 更新当前位置记录
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    /// <summary>
    /// 点击按钮翻到上一页
    /// </summary>
    public void ToLastPage()
    {
        float moveDistance = 0;
        // 如果已经是第一页，不执行操作
        if (currentIndex <= 1)
        {
            return;
        }
        // 计算滑动距离（向右滑动一个项目长度）
        moveDistance = moveOneItemLength;
        // 页码-1
        currentIndex--;
        // 更新页码显示
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
        // 如果需要发送消息，通知父对象翻到上一页
        if (needSendMessage)
        {
            UpdatePanel(false);
        }
        // 执行滑动动画
        DOTween.To(() => contentTrans.localPosition, lerpValue => contentTrans.localPosition = lerpValue, currentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        // 更新当前位置记录
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    /// <summary>
    /// 开始拖拽时触发（实现IBeginDragHandler接口）
    /// 记录拖拽开始时的鼠标X坐标
    /// </summary>
    /// <param name="eventData">拖拽事件数据</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        beginMousePositionX = Input.mousePosition.x;
    }

    /// <summary>
    /// 设置内容区域的长度（根据项目数量动态调整）
    /// </summary>
    /// <param name="itemNum">项目总数量</param>
    public void SetContentLength(int itemNum)
    {
        // 计算并设置内容区域的宽度（初始宽度 + 额外项目的总长度）
        contentTrans.sizeDelta = new Vector2(contentTrans.sizeDelta.x + (cellLength + spacing) * (itemNum - 1), contentTrans.sizeDelta.y);
        // 更新总项目数量
        totalItemNum = itemNum;
    }

    /// <summary>
    /// 初始化内容区域的长度（恢复到初始大小）
    /// </summary>
    public void InitScrollLength()
    {
        contentTrans.sizeDelta = contentTransSize;
    }

    /// <summary>
    /// 向上级对象发送翻页消息
    /// </summary>
    /// <param name="toNext">是否翻到下一页（true：下一页；false：上一页）</param>
    public void UpdatePanel(bool toNext)
    {
        if (toNext)
        {
            // 发送"翻到下一关"的消息
            gameObject.SendMessageUpwards("ToNextLevel");
        }
        else
        {
            // 发送"翻到上一关"的消息
            gameObject.SendMessageUpwards("ToLastLevel");
        }
    }
}


