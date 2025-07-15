using UnityEngine;

public class 刀片类 : MonoBehaviour
{


    // 切割时施加的力大小（可公开调整）sliceForce
    public float 切割时施加的力大小 = 5f;

    // 触发切割的最小速度阈值（低于此值不触发切割）minSliceVelocity
    public float 触发切割的最小速度阈值 = 0.01f;

    // 主摄像机引用（用于屏幕坐标转换） mainCamera
    private Camera 主摄像机引用;

    // 切割碰撞体（用于检测切割交互）sliceCollider
    private Collider 切割碰撞体;

    // 切割轨迹渲染器（显示切割路径的拖尾效果）sliceTrail
    private TrailRenderer 切割轨迹渲染器;

    // 公开属性：获取切割方向向量（私有设置）公开属性  direction
    public Vector3 公开属性 { get; private set; }  // direction

    // 公开属性：获取当前是否正在进行切割（私有设置）slicing
    public bool 切割是否 { get; private set; }


    public void 左键切割()
    {
        Debug.Log("执行左键切割动作");
        // 实际切割逻辑...
    }

    public void 右键切割()
    {
        Debug.Log("执行右键切割动作");
        // 实际切割逻辑...
    }

    public void 中键切割()
    {
        Debug.Log("执行中键切割动作");
        // 实际切割逻辑...
    }


    // Unity 生命周期函数：脚本加载时调用（即使脚本未启用）
    private void Awake()
    {
        Debug.Log("初始化 刀片类");
        主摄像机引用 = Camera.main;
        切割碰撞体 = GetComponent<Collider>();
        切割轨迹渲染器 = GetComponentInChildren<TrailRenderer>();

    }

    // Unity 生命周期函数：脚本启用时调用（每次激活时）
    private void OnEnable()
    {
        结束刀片();
        //开始刀片();
    }

    // Unity 生命周期函数：脚本禁用时调用（每次失活时）
    private void OnDisable()
    {
        结束刀片();
        //开始刀片();
    }

    // Unity 核心循环函数：每帧调用（约每秒60次）
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            开始刀片();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            结束刀片();
        }
        else if (切割是否)
        {
            连续刀片();
        }
    }

    // 自定义函数
    private void 开始刀片()
    {
        // 将鼠标/触摸位置转换为世界坐标（Z轴归零用于2D场景）
        Vector3 position = 主摄像机引用.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        // 将切割器移动到起始位置   // 直接使用transform，无需声明
        transform.position = position;

        // 设置切割状态标志
        切割是否 = true;

        // 启用碰撞检测
        切割碰撞体.enabled = true;

        // 显示切割轨迹
        切割轨迹渲染器.enabled = true;

        // 清除之前的轨迹残留
        切割轨迹渲染器.Clear();
    }

    private void 结束刀片()
    {
        切割是否 = false;
        切割碰撞体.enabled = false;
        切割轨迹渲染器.enabled = false;
    }

    private void 连续刀片()
    {
        //Debug.Log($"物体目标位置: {Input.mousePosition}");

        Vector3 newPosition = 主摄像机引用.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        公开属性 = newPosition - transform.position;

        float velocity = 公开属性.magnitude / Time.deltaTime;
        切割碰撞体.enabled = velocity > 触发切割的最小速度阈值;

        transform.position = newPosition;
    }



    //void Start()
    //{ 
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}


}
