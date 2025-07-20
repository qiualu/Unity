using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

//滑动视图
//public class 滑动视图 : MonoBehaviour, IBeginDragHandler, IEndDragHandler
 
/// <summary>
/// 单滑
/// </summary>
public class 滑动视图 : MonoBehaviour, IBeginDragHandler, IEndDragHandler // SlideScrollView
{

    private RectTransform 内容变换; // contentTrans
    private float 开始鼠标位置X; // beginMousePositionX
    private float 结束鼠标位置X; // endMousePositionX
    private ScrollRect 滚动矩形; // scrollRect

    public int 单元格长度; // cellLength
    public int 间距; // spacing
    public int 左偏移; // leftOffset
    private float 移动一个项目长度; // moveOneItemLength

    private Vector3 当前内容本地位置; // currentContentLocalPos（上一次的位置）
    private Vector3 内容初始位置; // contentInitPos（Content初始位置）
    private Vector2 内容变换大小; // contentTransSize（Content初始大小）

    public int 总项目数量; // totalItemNum
    private int 当前索引; // currentIndex

    public Text 页码文本; // pageText

    public bool 需要发送消息; // needSendMessage

    private void Awake()
    {
        滚动矩形 = GetComponent<ScrollRect>(); // scrollRect = GetComponent<ScrollRect>();
        内容变换 = 滚动矩形.content; // contentTrans = scrollRect.content;
        移动一个项目长度 = 单元格长度 + 间距; // moveOneItemLength = cellLength + spacing;
        当前内容本地位置 = 内容变换.localPosition; // currentContentLocalPos = contentTrans.localPosition;
        内容变换大小 = 内容变换.sizeDelta; // contentTransSize = contentTrans.sizeDelta;
        内容初始位置 = 内容变换.localPosition; // contentInitPos = contentTrans.localPosition;
        当前索引 = 1; // currentIndex = 1;
        if (页码文本 != null) // if (pageText != null)
        {
            页码文本.text = 当前索引.ToString() + "/" + 总项目数量; // pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
    }

    public void 初始化() // Init
    {
        当前索引 = 1; // currentIndex = 1;

        if (内容变换 != null) // if (contentTrans!=null)
        {
            内容变换.localPosition = 内容初始位置; // contentTrans.localPosition = contentInitPos;
            当前内容本地位置 = 内容初始位置; // currentContentLocalPos = contentInitPos;
            if (页码文本 != null) // if (pageText != null)
            {
                页码文本.text = 当前索引.ToString() + "/" + 总项目数量; // pageText.text = currentIndex.ToString() + "/" + totalItemNum;
            }
        }
    }

    /// <summary>
    /// 通过拖拽与松开来达成翻页效果
    /// </summary>
    /// <param name="事件数据">事件数据（eventData）</param>
    public void OnEndDrag(PointerEventData 事件数据) // OnEndDrag(PointerEventData eventData)
    {
        结束鼠标位置X = Input.mousePosition.x; // endMousePositionX = Input.mousePosition.x;
        float 偏移X = 0; // offSetX
        float 移动距离 = 0; // moveDistance（当次需要滑动的距离）
        偏移X = 开始鼠标位置X - 结束鼠标位置X; // offSetX = beginMousePositionX - endMousePositionX;

        if (偏移X > 0) // 右滑（offSetX>0）
        {
            if (当前索引 >= 总项目数量) // if (currentIndex>=totalItemNum)
            {
                return;
            }
            if (需要发送消息) // if (needSendMessage)
            {
                更新面板(true); // UpdatePanel(true);
            }

            移动距离 = -移动一个项目长度; // moveDistance = -moveOneItemLength;
            当前索引++; // currentIndex++;
        }
        else // 左滑（else）
        {
            if (当前索引 <= 1) // if (currentIndex<=1)
            {
                return;
            }
            if (需要发送消息) // if (needSendMessage)
            {
                更新面板(false); // UpdatePanel(false);
            }
            移动距离 = 移动一个项目长度; // moveDistance = moveOneItemLength;
            当前索引--; // currentIndex--;
        }
        if (页码文本 != null) // if (pageText != null)
        {
            页码文本.text = 当前索引.ToString() + "/" + 总项目数量; // pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
        // 原语句：DOTween.To(()=>contentTrans.localPosition,lerpValue=>contentTrans.localPosition=lerpValue,currentContentLocalPos+new Vector3(moveDistance,0,0),0.5f).SetEase(Ease.OutQuint);
        DOTween.To(() => 内容变换.localPosition, 插值结果 => 内容变换.localPosition = 插值结果, 当前内容本地位置 + new Vector3(移动距离, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        当前内容本地位置 += new Vector3(移动距离, 0, 0); // currentContentLocalPos += new Vector3(moveDistance, 0, 0);
        // 只能存在于此项目
        游戏管理.实例.音频源管理.播放翻书音效(); // GameManager.Instance.audioSourceManager.PlayPagingAudioClip();
    }

    /// <summary>
    /// 按钮来控制翻书效果
    /// </summary>
    public void 到下一页() // ToNextPage
    {
        float 移动距离 = 0; // moveDistance
        if (当前索引 >= 总项目数量) // if (currentIndex>=totalItemNum)
        {
            return;
        }

        移动距离 = -移动一个项目长度; // moveDistance = -moveOneItemLength;
        当前索引++; // currentIndex++;
        if (页码文本 != null) // if (pageText!=null)
        {
            页码文本.text = 当前索引.ToString() + "/" + 总项目数量; // pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
        if (需要发送消息) // if (needSendMessage)
        {
            更新面板(true); // UpdatePanel(true);
        }
        // 原语句：DOTween.To(() => contentTrans.localPosition, lerpValue => contentTrans.localPosition = lerpValue, currentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        DOTween.To(() => 内容变换.localPosition, 插值结果 => 内容变换.localPosition = 插值结果, 当前内容本地位置 + new Vector3(移动距离, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        当前内容本地位置 += new Vector3(移动距离, 0, 0); // currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    public void 到上一页() // ToLastPage
    {
        float 移动距离 = 0; // moveDistance
        if (当前索引 <= 1) // if (currentIndex <=1)
        {
            return;
        }

        移动距离 = 移动一个项目长度; // moveDistance = moveOneItemLength;
        当前索引--; // currentIndex--;
        if (页码文本 != null) // if (pageText != null)
        {
            页码文本.text = 当前索引.ToString() + "/" + 总项目数量; // pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
        if (需要发送消息) // if (needSendMessage)
        {
            更新面板(false); // UpdatePanel(false);
        }
        // 原语句：DOTween.To(() => contentTrans.localPosition, lerpValue => contentTrans.localPosition = lerpValue, currentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        DOTween.To(() => 内容变换.localPosition, 插值结果 => 内容变换.localPosition = 插值结果, 当前内容本地位置 + new Vector3(移动距离, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        当前内容本地位置 += new Vector3(移动距离, 0, 0); // currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    public void OnBeginDrag(PointerEventData 事件数据) // OnBeginDrag(PointerEventData eventData)
    {
        开始鼠标位置X = Input.mousePosition.x; // beginMousePositionX = Input.mousePosition.x;
    }

    // 设置Content的大小
    public void 设置内容长度(int 项目数量) // SetContentLength(int itemNum)
    {
        // 原语句：contentTrans.sizeDelta = new Vector2(contentTrans.sizeDelta.x+(cellLength+spacing)*(itemNum-1),contentTrans.sizeDelta.y);
        内容变换.sizeDelta = new Vector2(内容变换.sizeDelta.x + (单元格长度 + 间距) * (项目数量 - 1), 内容变换.sizeDelta.y);
        总项目数量 = 项目数量; // totalItemNum = itemNum;
    }

    // 初始化Content的大小
    public void 初始化滚动长度() // InitScrollLength
    {
        内容变换.sizeDelta = 内容变换大小; // contentTrans.sizeDelta = contentTransSize;
    }

    // 发送翻页信息的方法
    public void 更新面板(bool 到下一个) // UpdatePanel(bool toNext)
    {
        if (到下一个) // if (toNext)
        {
            gameObject.SendMessageUpwards("ToNextLevel"); // gameObject.SendMessageUpwards("ToNextLevel");
        }
        else // else
        {
            gameObject.SendMessageUpwards("ToLastLevel"); // gameObject.SendMessageUpwards("ToLastLevel");
        }
    }
}