
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 刀痕迹类 : MonoBehaviour
{

    // 切割时施加的力大小（可公开调整）
    public float 切割力 = 5f;

    // 触发切割的最小速度阈值（低于此值不触发切割）
    public float 切割阈值 = 0.01f;

    // 判断为直线的角度阈值（角度越小要求越严格）
    public float 直线角度阈值 = 10f;

    // 触发音效所需的最小直线长度
    public float 最小直线长度 = 0.5f;

    // 主摄像机引用（用于屏幕坐标转换）
    private Camera 主摄像机引用;

    // 切割碰撞体（用于检测切割交互）
    private Collider 碰撞体;

    // 切割轨迹渲染器（显示切割路径的拖尾效果）
    private TrailRenderer 轨迹渲染器;

    // 切割方向向量
    public Vector3 公开属性 { get; private set; }

    // 当前是否正在进行切割
    public bool 刀状态 { get; private set; }

    // 上一帧的切割方向
    private Vector3 上一方向;

    // 当前直线移动的累计距离
    private float 直线累计距离;

    // 标记是否已播放过音效（避免短时间内重复播放）
    private bool 已播放音效;

    private void Awake()
    {
        Debug.Log("初始化 刀片类");
        主摄像机引用 = Camera.main;
        碰撞体 = GetComponent<Collider>();
        轨迹渲染器 = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        结束刀片();
    }

    private void OnDisable()
    {
        结束刀片();
    }

    private void 结束刀片()
    {
        刀状态 = false;
        碰撞体.enabled = false;
        轨迹渲染器.enabled = false;
        直线累计距离 = 0;
        已播放音效 = false;
    }

    private void 开始刀片()
    {
        Vector3 position = 主摄像机引用.ScreenToWorldPoint(Input.mousePosition);
        position.z = -2f;
        transform.position = position;

        刀状态 = true;
        碰撞体.enabled = true;
        轨迹渲染器.enabled = true;
        轨迹渲染器.Clear();

        // 初始化直线检测变量
        上一方向 = Vector3.zero;
        直线累计距离 = 0;
        已播放音效 = false;
    }

    private void 连续刀片()
    {
        Vector3 newPosition = 主摄像机引用.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = -2f;
        公开属性 = newPosition - transform.position;

        float velocity = 公开属性.magnitude / Time.deltaTime;
        碰撞体.enabled = velocity > 切割阈值;

        // 检测是否在绘制直线
        if (velocity > 切割阈值)
        {
            检测直线切割(公开属性);
        }

        transform.position = newPosition;
    }

    private void 检测直线切割(Vector3 当前方向)
    {
        // 如果是刚开始切割，记录初始方向
        if (上一方向 == Vector3.zero)
        {
            上一方向 = 当前方向.normalized;
            return;
        }

        // 计算当前方向与上一方向的夹角
        float 角度 = Vector3.Angle(当前方向.normalized, 上一方向);

        // 如果角度小于阈值，视为继续沿直线移动
        if (角度 < 直线角度阈值)
        {
            直线累计距离 += 当前方向.magnitude;

            // 当直线长度足够且未播放过音效时，播放音效
            if (直线累计距离 >= 最小直线长度 && !已播放音效)
            {
                // 播放切割音效
                //土豆忍者管理类.土豆忍者管理.音频管理.播放("切水果");
                //已播放音效 = true;
            }
        }
        else
        {
            // 方向变化过大，重置直线检测
            上一方向 = 当前方向.normalized;
            直线累计距离 = 当前方向.magnitude;
            已播放音效 = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            开始刀片();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            结束刀片();
        }
        else if (刀状态)
        {
            连续刀片();
        }
    }
}




//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class 刀痕迹类 : MonoBehaviour
//{

//    // 切割时施加的力大小（可公开调整）sliceForce
//    public float 切割力 = 5f;

//    // 触发切割的最小速度阈值（低于此值不触发切割）minSliceVelocity
//    public float 切割阈值 = 0.01f;

//    // 主摄像机引用（用于屏幕坐标转换） mainCamera
//    private Camera 主摄像机引用;

//    // 切割碰撞体（用于检测切割交互）sliceCollider
//    private Collider 碰撞体;

//    // 切割轨迹渲染器（显示切割路径的拖尾效果）sliceTrail
//    private TrailRenderer 轨迹渲染器;

//    // 公开属性：获取切割方向向量（私有设置）公开属性  direction
//    public Vector3 公开属性 { get; private set; }  // direction

//    // 公开属性：获取当前是否正在进行切割（私有设置）slicing
//    public bool 刀状态 { get; private set; }

//    private void Awake()
//    {
//        Debug.Log("初始化 刀片类");
//        主摄像机引用 = Camera.main;
//        碰撞体 = GetComponent<Collider>();
//        轨迹渲染器 = GetComponentInChildren<TrailRenderer>();

//    }
//    private void OnEnable()
//    {
//        结束刀片(); 
//    }

//    // Unity 生命周期函数：脚本禁用时调用（每次失活时）
//    private void OnDisable()
//    {
//        结束刀片(); 
//    }

//    private void 结束刀片()
//    {
//        刀状态 = false;
//        碰撞体.enabled = false;
//        轨迹渲染器.enabled = false;
//    }

//    //// Start is called before the first frame update
//    //void Start()
//    //{

//    //}

//    // 自定义函数
//    private void 开始刀片()
//    {
//        // 将鼠标/触摸位置转换为世界坐标（Z轴归零用于2D场景）
//        Vector3 position = 主摄像机引用.ScreenToWorldPoint(Input.mousePosition);
//        position.z = -2f;
//        // 将切割器移动到起始位置   // 直接使用transform，无需声明
//        transform.position = position;

//        // 设置切割状态标志
//        刀状态 = true;

//        // 启用碰撞检测
//        碰撞体.enabled = true;

//        // 显示切割轨迹
//        轨迹渲染器.enabled = true;

//        // 清除之前的轨迹残留
//        轨迹渲染器.Clear();
//    }

//    private void 连续刀片()
//    {
//        //Debug.Log($"物体目标位置: {Input.mousePosition}");

//        Vector3 newPosition = 主摄像机引用.ScreenToWorldPoint(Input.mousePosition);
//        newPosition.z = -2f;
//        公开属性 = newPosition - transform.position;

//        float velocity = 公开属性.magnitude / Time.deltaTime;
//        碰撞体.enabled = velocity > 切割阈值;

//        transform.position = newPosition;
//    }


//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            开始刀片();
//            //Debug.Log("开始刀片");
//        }
//        else if (Input.GetMouseButtonUp(0))
//        {
//            结束刀片();
//            //Debug.Log("结束刀片");
//        }
//        else if (刀状态)
//        {
//            连续刀片();
//        }
//    }
//}
