using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 刀痕迹类 : MonoBehaviour
{

    // 切割时施加的力大小（可公开调整）sliceForce
    public float 切割力 = 5f;

    // 触发切割的最小速度阈值（低于此值不触发切割）minSliceVelocity
    public float 切割阈值 = 0.01f;

    // 主摄像机引用（用于屏幕坐标转换） mainCamera
    private Camera 主摄像机引用;

    // 切割碰撞体（用于检测切割交互）sliceCollider
    private Collider 碰撞体;

    // 切割轨迹渲染器（显示切割路径的拖尾效果）sliceTrail
    private TrailRenderer 轨迹渲染器;

    // 公开属性：获取切割方向向量（私有设置）公开属性  direction
    public Vector3 公开属性 { get; private set; }  // direction

    // 公开属性：获取当前是否正在进行切割（私有设置）slicing
    public bool 刀状态 { get; private set; }

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

    // Unity 生命周期函数：脚本禁用时调用（每次失活时）
    private void OnDisable()
    {
        结束刀片(); 
    }

    private void 结束刀片()
    {
        刀状态 = false;
        碰撞体.enabled = false;
        轨迹渲染器.enabled = false;
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // 自定义函数
    private void 开始刀片()
    {
        // 将鼠标/触摸位置转换为世界坐标（Z轴归零用于2D场景）
        Vector3 position = 主摄像机引用.ScreenToWorldPoint(Input.mousePosition);
        position.z = -2f;
        // 将切割器移动到起始位置   // 直接使用transform，无需声明
        transform.position = position;

        // 设置切割状态标志
        刀状态 = true;

        // 启用碰撞检测
        碰撞体.enabled = true;

        // 显示切割轨迹
        轨迹渲染器.enabled = true;

        // 清除之前的轨迹残留
        轨迹渲染器.Clear();
    }

    private void 连续刀片()
    {
        //Debug.Log($"物体目标位置: {Input.mousePosition}");

        Vector3 newPosition = 主摄像机引用.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = -2f;
        公开属性 = newPosition - transform.position;

        float velocity = 公开属性.magnitude / Time.deltaTime;
        碰撞体.enabled = velocity > 切割阈值;

        transform.position = newPosition;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            开始刀片();
            //Debug.Log("开始刀片");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            结束刀片();
            //Debug.Log("结束刀片");
        }
        else if (刀状态)
        {
            连续刀片();
        }
    }
}
