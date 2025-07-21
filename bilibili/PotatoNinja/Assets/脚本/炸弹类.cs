using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class 炸弹类 : MonoBehaviour
{
    [Header("动画相关")]
    public Animator 炸弹动画组件;   // Awake 时隐藏，播放时显示
    public GameObject 额外要隐藏的对象;   // Awake 时显示，播放时隐藏
    public Renderer 炸弹渲染器;      // 动画组件的渲染器（如果需要）
    [Tooltip("提前结束的帧索引（24帧动画建议设为20）")]
    public int 提前结束帧 = 20;
    [Tooltip("动画帧率（通常为30）")]
    public int 动画帧率 = 30;

    [Header("设置")]
    public int 分数 = 1;
    private bool 鼠标判定状态 = false;
    private bool 触发状态 = true;
    private bool 已播放动画 = false;
    private Quaternion 原始旋转;
    private float 提前结束时间 => (float)提前结束帧 / 动画帧率;

    // 物理和碰撞组件
    private Rigidbody 炸弹物理组件;
    private SphereCollider 炸弹碰撞体;
    private Camera 主摄像机引用;


    [Header("分数提示预制体")]
    public GameObject 分数预制体; // 拖拽 Canvas 预制体到这里
    private Canvas 主Canvas; // 场景中的主Canvas（用于放置预制体）
    public int 爆炸分数 = -5; // 炸弹爆炸的分数（负数表示扣分）




    ///// 实例化分数预制体并设置位置和文本
    ///// </summary>
    //private void 显示分数提示()
    //{
    //    生成分数预制体跟随鼠标();
    //}

    /// <summary>
    /// 实例化分数预制体并设置位置和文本
    /// </summary>
    private void 显示分数提示()
    {
        if (分数预制体 == null || 主Canvas == null)
        {
            Debug.LogWarning("未设置分数预制体或主Canvas！", this);
            return;
        }

        // 1. 实例化分数预制体（作为主Canvas的子对象）
        GameObject 分数实例 = Instantiate(分数预制体, 主Canvas.transform);

        // 2. 设置分数文本（假设使用 TextMeshProUGUI）
        TextMeshProUGUI 分数文本 = 分数实例.GetComponent<TextMeshProUGUI>();
        if (分数文本 != null)
        {
            // 显示 "+分数" 或 "-分数"
            分数文本.text = (爆炸分数 >= 0 ? "+" : "") + 爆炸分数.ToString();
            // 可选：根据正负分设置颜色（正数绿色，负数红色）
            分数文本.color = 爆炸分数 >= 0 ? Color.green : Color.red;
        }

        // 3. 将预制体定位到炸弹爆炸的屏幕位置
        定位分数预制体(分数实例);
    }

    /// <summary>
    /// 将分数预制体定位到炸弹所在的屏幕位置
    /// </summary>
    private void 定位分数预制体(GameObject 分数实例)
    {
        // a. 获取炸弹在世界空间中的位置
        Vector3 炸弹世界位置 = transform.position;

        // b. 将世界位置转换为屏幕坐标（相对于主摄像机）
        Vector2 屏幕坐标 = 主摄像机引用.WorldToScreenPoint(炸弹世界位置);

        // c. 将屏幕坐标转换为 Canvas 局部坐标
        RectTransform canvasRect = 主Canvas.GetComponent<RectTransform>();
        Vector2 ui坐标;

        // 转换屏幕坐标到UI坐标
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            屏幕坐标,
            主Canvas.worldCamera, // Overlay模式可传null
            out ui坐标
        ))
        {
            // d. 设置分数实例的位置
            RectTransform 分数Rect = 分数实例.GetComponent<RectTransform>();
            分数Rect.anchoredPosition = ui坐标;
        }
 
    }

 



    private void Awake()
    {
        自动获取组件();
        原始旋转 = transform.rotation;
        // 初始状态设置（关键修正）
        初始状态设置();


        // 新增：按固定名称"Canvas"查找主Canvas
        主Canvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();



    }

    /// <summary>
    /// 初始状态配置：
    /// - 炸弹动画组件：隐藏
    /// - 额外要隐藏的对象：显示
    /// </summary>
    private void 初始状态设置()
    {
        // 炸弹动画组件初始隐藏
        if (炸弹动画组件 != null)
        {
            炸弹动画组件.enabled = false;
            炸弹动画组件.Rebind();
        }

        // 动画渲染器初始隐藏（如果需要）
        if (炸弹渲染器 != null)
            炸弹渲染器.enabled = false;

        // 额外对象初始显示
        if (额外要隐藏的对象 != null)
            额外要隐藏的对象.SetActive(true);
    }

    private void 自动获取组件()
    {
        if (炸弹物理组件 == null)
            炸弹物理组件 = GetComponent<Rigidbody>();

        if (炸弹碰撞体 == null)
            炸弹碰撞体 = GetComponent<SphereCollider>();

        if (主摄像机引用 == null)
            主摄像机引用 = Camera.main;

        if (炸弹渲染器 == null && 炸弹动画组件 != null)
            炸弹渲染器 = 炸弹动画组件.GetComponent<Renderer>();
    }

    private void 结束游戏()
    {
        if (已播放动画) return;
        已播放动画 = true;

        // 复位朝向
        transform.rotation = 原始旋转;

        // 禁用碰撞和物理
        if (炸弹碰撞体 != null)
            炸弹碰撞体.enabled = false;

        if (炸弹物理组件 != null)
            炸弹物理组件.isKinematic = true;

        显示分数提示();

        // 切换显示状态并播放动画
        切换显示状态并播放动画();
        土豆忍者管理类.土豆忍者管理.计分函数(爆炸分数);
    }

    /// <summary>
    /// 切换状态：
    /// - 炸弹动画组件：显示并播放
    /// - 额外要隐藏的对象：隐藏
    /// </summary>
    private void 切换显示状态并播放动画()
    {
        // 显示动画组件并播放
        if (炸弹动画组件 != null)
        {
            炸弹动画组件.enabled = true;
            炸弹动画组件.SetTrigger("Explode");
        }

        // 显示动画渲染器
        if (炸弹渲染器 != null)
            炸弹渲染器.enabled = true;

        // 隐藏额外对象
        if (额外要隐藏的对象 != null)
            额外要隐藏的对象.SetActive(false);

        // 启动提前结束协程
        StartCoroutine(等待动画提前结束());
    }

    private IEnumerator 等待动画提前结束()
    {
        yield return new WaitForSeconds(提前结束时间);

        // 动画提前结束后隐藏所有元素
        if (炸弹渲染器 != null)
            炸弹渲染器.enabled = false;

        if (炸弹动画组件 != null)
            炸弹动画组件.enabled = false;

        // 通知游戏结束
        土豆忍者管理类.土豆忍者管理.爆炸结束游戏();
        Destroy(gameObject, 0.1f);
    }

    // 以下判定逻辑保持不变
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !已播放动画)
        {
            Debug.Log("和鼠标碰撞了！");
            结束游戏();
        }
    }

    public void 开始判定() => 鼠标判定状态 = true;
    public void 结束判定() => 鼠标判定状态 = false;

    public void 连续判定()
    {
        if (已播放动画) return;

        Vector3 鼠标世界坐标 = 主摄像机引用.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 主摄像机引用.nearClipPlane));
        Vector2 鼠标2D坐标 = new Vector2(鼠标世界坐标.x, 鼠标世界坐标.y);

        if (炸弹碰撞体 == null) return;

        Vector3 实际中心3D = transform.TransformPoint(炸弹碰撞体.center);
        Vector2 碰撞体中心 = new Vector2(实际中心3D.x, 实际中心3D.y);
        float 实际半径 = 炸弹碰撞体.radius *
            Mathf.Max(Mathf.Abs(transform.lossyScale.x),
                     Mathf.Abs(transform.lossyScale.y),
                     Mathf.Abs(transform.lossyScale.z));

        if (Vector2.Distance(鼠标2D坐标, 碰撞体中心) <= 实际半径)
        {
            触发状态 = false;
            结束游戏();
        }
    }

    private void Update()
    {
        if (触发状态 && !已播放动画)
        {
            if (Input.GetMouseButtonDown(0))
                开始判定();
            else if (Input.GetMouseButtonUp(0))
                结束判定();
            else if (鼠标判定状态)
                连续判定();
            else if (Input.GetMouseButton(0))
                开始判定();
        }
    }
}



//public class 炸弹类 : MonoBehaviour
//{


//    private Rigidbody 炸弹物理组件;    // fruitRigidbody 
//    private SphereCollider 炸弹碰撞体;


//    private Camera 主摄像机引用;

//    private Animator 炸弹动画组件; // 动画组件引用


//    public int 分数 = 1;

//    private bool 鼠标判定状态 = false;
//    private bool 触发状态 = true;
//    private bool 已播放动画 = false;
//    private Quaternion 原始旋转;



//    private void Awake()
//    {
//        炸弹物理组件 = GetComponent<Rigidbody>();
//        //土豆碰撞体 = GetComponent<Collider>();
//        炸弹碰撞体 = GetComponent<SphereCollider>();
//        主摄像机引用 = Camera.main;

//        // 获取动画组件（确保炸弹对象上已添加Animator）
//        炸弹动画组件 = GetComponent<Animator>();

//        // 记录初始旋转作为"原始朝向"
//        原始旋转 = transform.rotation;

//        禁用初始动画();

//    }


//    private void 结束游戏()
//    {
//        //土豆忍者管理类.土豆忍者管理.爆炸结束游戏();
//        if (已播放动画) return;
//        已播放动画 = true;
//        // 复位朝向
//        transform.rotation = 原始旋转;
//        // 隐藏炸弹模型
//        Renderer 炸弹渲染器 = GetComponent<Renderer>();
//        if (炸弹渲染器 != null)
//        {
//            炸弹渲染器.enabled = false;
//        }


//        // 禁用碰撞和物理
//        炸弹碰撞体.enabled = false;
//        炸弹物理组件.isKinematic = true;

//        // 播放爆炸动画
//        if (炸弹动画组件 != null)
//        {
//            炸弹动画组件.enabled = true; // 启用组件
//            炸弹动画组件.SetTrigger("爆炸触发"); // 触发动画
//            StartCoroutine(等待动画结束());
//        }
//        else
//        {
//            土豆忍者管理类.土豆忍者管理.爆炸结束游戏();
//        }


//    }

//    // 其他方法保持不变...
//    private IEnumerator 等待动画结束()
//    {
//        // 获取当前播放的动画长度（更精确的等待方式）
//        AnimatorStateInfo 状态信息 = 炸弹动画组件.GetCurrentAnimatorStateInfo(0);
//        yield return new WaitForSeconds(状态信息.length);

//        土豆忍者管理类.土豆忍者管理.爆炸结束游戏();
//        Destroy(gameObject, 0.1f);
//    }

//    /// <summary>
//    /// 确保动画不会自动播放，只在触发时播放
//    /// </summary>
//    private void 禁用初始动画()
//    {
//        if (炸弹动画组件 != null)
//        {
//            // 1. 停止所有动画播放
//            炸弹动画组件.StopPlayback();
//            炸弹动画组件.enabled = false; // 先禁用组件

//            // 2. 确保动画处于第一帧（初始状态）
//            炸弹动画组件.Rebind(); // 重置动画状态到初始帧

//            // 3. 如果需要循环动画，这里可以提前设置默认参数
//            // 炸弹动画组件.SetBool("是否循环", false);
//        }
//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("和鼠标碰撞了！");
//            结束游戏();
//        }
//    }



//    public void 开始判定()
//    {
//        鼠标判定状态 = true;
//    }
//    public void 结束判定()
//    {
//        鼠标判定状态 = false;
//    }
//    public void 连续判定()
//    {
//        // 获取鼠标世界坐标（仅XY平面）
//        Vector3 鼠标世界坐标 = 主摄像机引用.ScreenToWorldPoint(
//            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 主摄像机引用.nearClipPlane));

//        //Debug.Log($" {gameObject.name} 鼠标世界坐标 ： {鼠标世界坐标} ");
//        // 转换为2D坐标
//        Vector2 鼠标2D坐标 = new Vector2(鼠标世界坐标.x, 鼠标世界坐标.y);
//        //Debug.Log($" {gameObject.name} 鼠标2D坐标 ： {鼠标2D坐标} " );

//        // 确保获取的是SphereCollider组件
//        if (炸弹碰撞体 == null)
//        {
//            炸弹碰撞体 = GetComponent<SphereCollider>();
//            if (炸弹碰撞体 == null) return;
//        }

//        // 计算实际中心点（考虑偏移量）
//        Vector3 实际中心3D = transform.TransformPoint(炸弹碰撞体.center);
//        Vector2 碰撞体中心 = new Vector2(实际中心3D.x, 实际中心3D.y);

//        // 计算缩放后的半径（取最大缩放值）
//        float 实际半径 = 炸弹碰撞体.radius *
//            Mathf.Max(Mathf.Abs(transform.lossyScale.x),
//                     Mathf.Abs(transform.lossyScale.y),
//                     Mathf.Abs(transform.lossyScale.z));

//        // 计算距离
//        float 距离 = Vector2.Distance(鼠标2D坐标, 碰撞体中心);

//        //Debug.Log($" {gameObject.name} 鼠标2D坐标: {鼠标2D坐标} 碰撞体：{碰撞体中心}，缩放半径：{实际半径}，鼠标距离：{距离}");

//        if (距离 <= 实际半径)
//        {
//            触发状态 = false;
//            结束游戏();
//        }

//    }

//    private void Update()
//    {
//        if (触发状态) {
//            if (Input.GetMouseButtonDown(0))
//            {
//                开始判定();
//                //Debug.Log("开始刀片");
//            }
//            else if (Input.GetMouseButtonUp(0))
//            {
//                结束判定();
//                //Debug.Log("结束刀片");
//            }
//            else if (鼠标判定状态)
//            {
//                连续判定();
//            }
//            else if(Input.GetMouseButton(0))
//            {
//                //Debug.Log("鼠标按压状态");
//                // 只要按住鼠标就持续检测（不依赖鼠标移动事件）
//                开始判定();
//            }
//        }
//    }



//}
