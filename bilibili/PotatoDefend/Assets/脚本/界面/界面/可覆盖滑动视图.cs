using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

//  SlideCanCoverScrollView
public class 可覆盖滑动视图 : MonoBehaviour, IBeginDragHandler, IEndDragHandler // SlideCanCoverScrollView
{

    private float 容器长度; // contentLength（容器长度）
    private float 开始鼠标位置X; // beginMousePostionX（注意原变量拼写：Postion应为Position）
    private float 结束鼠标位置X; // endMousePositionX
    private ScrollRect 滚动矩形; // scrollRect
    private float 上一位置比例; // lastProportion（上一个位置比例）

    public int 单元格长度; // cellLength（每个单元格长度）
    public int 间隙; // spacing（间隙）
    public int 左偏移量; // leftOffset（左偏移量）
    private float 上限值; // upperLimit（上限值）
    private float 下限值; // lowerLimit（下限值）
    private float 首个项目长度; // firstItemLength（移动第一个单元格的距离）
    private float 单个项目长度; // oneItemLength（滑动一个单元格需要的距离）
    private float 单个项目比例; // oneItemProportion（滑动一个单元格所占比例）

    public int 总项目数量; // totalItemNum（共有几个单元格）
    private int 当前索引; // currentIndex（当前单元格索引）

    public Text 页码文本; // pageText

    private void Awake()
    {
        滚动矩形 = GetComponent<ScrollRect>(); // scrollRect = GetComponent<ScrollRect>();
        容器长度 = 滚动矩形.content.rect.xMax - 2 * 左偏移量 - 单元格长度; // contentLength = scrollRect.content.rect.xMax - 2 * leftOffset - cellLength;
        首个项目长度 = 单元格长度 / 2 + 左偏移量; // firstItemLength = cellLength / 2 + leftOffset;
        单个项目长度 = 单元格长度 + 间隙; // oneItemLength = cellLength + spacing;
        单个项目比例 = 单个项目长度 / 容器长度; // oneItemProportion = oneItemLength / contentLength;
        上限值 = 1 - 首个项目长度 / 容器长度; // upperLimit=1- firstItemLength / contentLength;
        下限值 = 首个项目长度 / 容器长度; // lowerLimit = firstItemLength / contentLength;
        当前索引 = 1; // currentIndex = 1;
        滚动矩形.horizontalNormalizedPosition = 0; // scrollRect.horizontalNormalizedPosition = 0;
        if (页码文本 != null) // if (pageText != null)
        {
            页码文本.text = 当前索引.ToString() + "/" + 总项目数量; // pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
    }

    public void 初始化() // Init
    {
        上一位置比例 = 0; // lastProportion = 0;
        当前索引 = 1; // currentIndex = 1;
        if (滚动矩形 != null) // if (scrollRect != null)
        {
            滚动矩形.horizontalNormalizedPosition = 0; // scrollRect.horizontalNormalizedPosition = 0;
            页码文本.text = 当前索引.ToString() + "/" + 总项目数量; // pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
    }

    public void OnEndDrag(PointerEventData 事件数据) // OnEndDrag(PointerEventData eventData)
    {
        float 偏移X = 0; // offSetX
        结束鼠标位置X = Input.mousePosition.x; // endMousePositionX = Input.mousePosition.x;
        偏移X = (开始鼠标位置X - 结束鼠标位置X) * 2; // offSetX = (beginMousePostionX - endMousePositionX)*2;
        //Debug.Log("offSetX:" + offSetX);
        //Debug.Log("首个项目长度:" + 首个项目长度); // Debug.Log("firstItemLength:" + firstItemLength);
        if (Mathf.Abs(偏移X) > 首个项目长度) // 执行滑动动作的前提是要大于第一个需要滑动的距离（if (Mathf.Abs(offSetX)>firstItemLength)）
        {
            if (偏移X > 0) // 右滑（if (offSetX>0)）
            {
                if (当前索引 >= 总项目数量) // if (currentIndex>=totalItemNum)
                {
                    return;
                }
                int 移动数量 =
                    (int)((偏移X - 首个项目长度) / 单个项目长度) + 1; // 当次可以移动的格子数目（(int)((offSetX - firstItemLength) / oneItemLength) + 1）
                当前索引 += 移动数量; // currentIndex += moveCount;
                if (当前索引 >= 总项目数量) // if (currentIndex>=totalItemNum)
                {
                    当前索引 = 总项目数量; // currentIndex = totalItemNum;
                }
                // 当次需要移动的比例:上一次已经存在的单元格位置
                // 的比例加上这一次需要去移动的比例
                上一位置比例 += 单个项目比例 * 移动数量; // lastProportion += oneItemProportion * moveCount;
                if (上一位置比例 >= 上限值) // if (lastProportion>=upperLimit)
                {
                    上一位置比例 = 1; // lastProportion = 1;
                }
            }
            else // 左滑（else）
            {
                if (当前索引 <= 1) // if (currentIndex <=1)
                {
                    return;
                }
                int 移动数量 =
                    (int)((偏移X + 首个项目长度) / 单个项目长度) - 1; // 当次可以移动的格子数目（(int)((offSetX + firstItemLength) / oneItemLength) - 1）
                当前索引 += 移动数量; // currentIndex += moveCount;
                if (当前索引 <= 1) // if (currentIndex <=1)
                {
                    当前索引 = 1; // currentIndex = 1;
                }
                // 当次需要移动的比例:上一次已经存在的单元格位置
                // 的比例加上这一次需要去移动的比例
                上一位置比例 += 单个项目比例 * 移动数量; // lastProportion += oneItemProportion * moveCount;
                if (上一位置比例 <= 下限值) // if (lastProportion <= lowerLimit)
                {
                    上一位置比例 = 0; // lastProportion = 0;
                }
            }
            if (页码文本 != null) // if (pageText!=null)
            {
                页码文本.text = 当前索引.ToString() + "/" + 总项目数量; // pageText.text = currentIndex.ToString() + "/" + totalItemNum;
            }

        }

        DOTween.To(() => 滚动矩形.horizontalNormalizedPosition, 插值结果 => 滚动矩形.horizontalNormalizedPosition = 插值结果, 上一位置比例, 0.5f).SetEase(Ease.OutQuint); // DOTween.To(() => scrollRect.horizontalNormalizedPosition, lerpValue => scrollRect.horizontalNormalizedPosition = lerpValue, lastProportion, 0.5f).SetEase(Ease.OutQuint);
        游戏管理.实例.音频源管理.播放翻书音效(); // GameManager.Instance.audioSourceManager.PlayPagingAudioClip();
    }

    public void OnBeginDrag(PointerEventData 事件数据) // OnBeginDrag(PointerEventData eventData)
    {
        开始鼠标位置X = Input.mousePosition.x; // beginMousePostionX = Input.mousePosition.x;
    }
}