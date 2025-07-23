using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class 开始界面管理 : MonoBehaviour
{
    [Header("背景对象（UI Image）")]
    public GameObject 背景1;
    public GameObject 背景2;
    public GameObject 背景3;

    [Header("切换按钮")]
    public Button 切换按钮;

    [Header("动画设置")]
    public float 淡入淡出时间 = 0.5f;
    public float 测试复原延迟 = 3f;

    public int 游戏状态 = 0;

     // 当前显示的背景
    private GameObject 当前显示的背景;
    private bool 正在切换 = false;


    private Coroutine 复原协程 = null; // 保存当前运行的复原协程

    private void Start()
    {
        Debug.Log("Start: 开始初始化", this);
        游戏状态 = 0;

        // 检查UI背景是否正确赋值且包含Image组件
        检查UI背景(背景1, "背景1");
        检查UI背景(背景2, "背景2");
        检查UI背景(背景3, "背景3");

        // 检查按钮
        if (切换按钮 == null)
        {
            Debug.LogError("切换按钮未赋值！", this);
            return;
        }

        // 初始化显示（隐藏所有背景）
        初始化显示();

        // 绑定按钮事件
        切换按钮.onClick.RemoveAllListeners();
        切换按钮.onClick.AddListener(开始游戏);
        Debug.Log("Start: 按钮事件绑定完成", this);
    }

    // 检查UI背景是否有效（针对Image组件）
    private void 检查UI背景(GameObject 背景对象, string 名称)
    {
        if (背景对象 == null)
        {
            Debug.LogError($"{名称}未赋值！", this);
            return;
        }

        // 检查是否有Image组件（UI背景必须有）
        if (背景对象.GetComponent<Image>() == null)
        {
            Debug.LogError($"{名称}缺少Image组件！请确保是UI Image对象", 背景对象);
        }
        else
        {
            // 确保UI背景在Canvas下（UI必须的层级）
            if (背景对象.GetComponentInParent<Canvas>() == null)
            {
                Debug.LogError($"{名称}不在Canvas下！UI元素必须放在Canvas中", 背景对象);
            }
        }
    }

    private void 初始化显示()
    {
        //强制隐藏(背景1);
        //强制隐藏(背景2);
        //强制隐藏(背景3);
        当前显示的背景 = null;

        淡入淡出(背景1, false);
        淡入淡出(背景2, false);
        淡入淡出(背景3, false);
    }

    private void 开始游戏()
    {
        Debug.Log("开始游戏：触发背景1淡入", this);
        // 确保从隐藏状态开始淡入
        //强制隐藏(背景2);
        //强制隐藏(背景3);
        淡入淡出(背景1, true);

        游戏状态 = 1;

    }
 
    private void 切换完成()
    {
        Debug.Log("切换完成", this);
        //StartCoroutine(延迟复原(测试复原延迟));
        //终止复原协程();
        // 启动新协程并保存引用
        //复原协程 = StartCoroutine(延迟复原(测试复原延迟));
        保卫管理.实例.开始游戏(游戏状态);

    }

    private void 淡出切换完成()
    {
        Debug.Log("淡出切换完成", this);
        //StartCoroutine(延迟复原(测试复原延迟));
        //终止复原协程();
        // 启动新协程并保存引用
        //复原协程 = StartCoroutine(延迟复原(测试复原延迟));
    }
    private void 淡入切换完成()
    {
        Debug.Log("动画完成，启动延迟复原", this);
        //StartCoroutine(延迟复原(测试复原延迟));
        终止复原协程();
        // 启动新协程并保存引用
        复原协程 = StartCoroutine(延迟复原(测试复原延迟));

        if (游戏状态 == 1) {
            保卫管理.实例.开始游戏(1);
        }else if (游戏状态 == 2)
        {
            保卫管理.实例.开始游戏(2);
        }
        else if (游戏状态 == 3)
        {
            保卫管理.实例.开始游戏(3);
        }


    }


    // 终止当前运行的复原协程（关键：防止多个协程同时运行）
    private void 终止复原协程()
    {
        if (复原协程 != null)
        {
            Debug.Log("终止正在运行的复原协程", this);
            StopCoroutine(复原协程); // 终止协程
            复原协程 = null; // 清空引用
        }
    }



    private void 淡入淡出(GameObject 目标对象, bool 淡入状态)
    {
        // 针对UI Image的空检查
        if (目标对象 == null)
        {
            Debug.LogError("淡入淡出目标为null", this);
            return;
        }

        if (淡入状态)
        {
            淡入(目标对象, 淡入切换完成);
        }
        else
        {
            淡出(目标对象, 淡出切换完成);
        }
    }

    // 切换到下一个背景（针对UI的逻辑）
    public void 切换到下一个背景索引(int 当前索引, int 淡入对象索引)
    {
        GameObject 当前;
        GameObject 淡入对象;

        if (当前索引 == 1) {
            当前 = 背景1;
        }else if (当前索引 == 2)
        {
            当前 = 背景2;
        }
        else if (当前索引 == 3)
        {
            当前 = 背景3;
        }
        if (淡入对象索引 == 1)
        {
            淡入对象 = 背景1;
        }
        else if (淡入对象索引 == 2)
        {
            淡入对象 = 背景2;
        }
        else if (淡入对象索引 == 3)
        {
            淡入对象 = 背景3;
        }


        if (正在切换 || 当前 == null || 淡入对象 == null) return;
        正在切换 = true;

        // 先淡出当前，再淡入下一个
        淡出(当前, () =>
        {
            淡入(淡入对象, () =>
            {
                当前显示的背景 = 淡入对象;
                正在切换 = false;
                切换完成();
            });
        });
    }
    public void 切换到下一个背景(GameObject 当前, GameObject 淡入对象)
    {
        if (正在切换 || 当前 == null || 淡入对象 == null) return;
        正在切换 = true;

        // 先淡出当前，再淡入下一个
        淡出(当前, () =>
        {
            淡入(淡入对象, () =>
            {
                当前显示的背景 = 淡入对象;
                正在切换 = false;
                切换完成();
            });
        });
    }
    // 淡入（针对UI Image优化）
    public void 淡入(GameObject 目标对象, System.Action 完成后执行)
    {
        Image 背景图片 = 目标对象.GetComponent<Image>();
        if (背景图片 == null)
        {
            Debug.LogError("淡入失败：目标没有Image组件", 目标对象);
            完成后执行?.Invoke();
            return;
        }

        // UI背景需要先激活并放在最上层
        目标对象.SetActive(true);
        目标对象.transform.SetAsLastSibling(); // 确保在UI层级最上层显示

        // 淡入逻辑（UI Image专用）
        背景图片.color = new Color(1, 1, 1, 0); // 先透明
        背景图片.DOFade(1, 淡入淡出时间)
                 .SetEase(Ease.Linear) // UI动画建议用线性缓动
                 .OnComplete(() =>
                 {
                     Debug.Log($"{目标对象.name}淡入完成", 目标对象);
                     完成后执行?.Invoke();
                 });
    }

    // 淡出（针对UI Image优化）
    public void 淡出(GameObject 目标对象, System.Action 完成后执行)
    {
        Image 背景图片 = 目标对象.GetComponent<Image>();
        if (背景图片 == null)
        {
            Debug.LogError("淡出失败：目标没有Image组件", 目标对象);
            完成后执行?.Invoke();
            return;
        }

        // 淡出逻辑（UI Image专用）
        背景图片.DOFade(0, 淡入淡出时间)
                 .SetEase(Ease.Linear)
                 .OnComplete(() =>
                 {
                     目标对象.SetActive(false); // 淡出后隐藏
                     Debug.Log($"{目标对象.name}淡出完成", 目标对象);
                     完成后执行?.Invoke();
                 });
    }

    // 延迟复原
    IEnumerator 延迟复原(float 延迟时间)
    {
        Debug.Log($"开始延迟复原，延迟{延迟时间}秒", this);
        yield return new WaitForSeconds(延迟时间);

        // 复原时先淡出当前显示的背景
        if (当前显示的背景 != null)
        {
            淡出(当前显示的背景, () =>
            {
                初始化显示(); // 回到初始状态
            });
        }
        else
        {
            初始化显示();
        }
    }

    // 强制显示UI背景
    private void 强制显示(GameObject 目标对象)
    {
        if (目标对象 == null) return;

        Image 背景图片 = 目标对象.GetComponent<Image>();
        if (背景图片 != null)
        {
            背景图片.color = new Color(1, 1, 1, 1);
        }
        目标对象.SetActive(true);
        目标对象.transform.SetAsLastSibling();
    }

    // 强制隐藏UI背景
    private void 强制隐藏(GameObject 目标对象)
    {
        if (目标对象 == null) return;

        Image 背景图片 = 目标对象.GetComponent<Image>();
        if (背景图片 != null)
        {
            背景图片.color = new Color(1, 1, 1, 0);
        }
        目标对象.SetActive(false);
    }
}