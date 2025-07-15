using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


//public class 防御塔 : MonoBehaviour
//{
//    public GameObject 子弹预制体;
//    public Transform 发射点;

//    private void 发射()
//    {
//        // 从对象池获取子弹
//        GameObject 子弹 = 对象池管理器.实例.获取对象(子弹预制体, 发射点.position, 发射点.rotation);

//        // 设置子弹属性
//        子弹.GetComponent<子弹>().初始化(目标);
//    }
//}

//public class 子弹 : MonoBehaviour
//{
//    private Transform 目标;
//    private float 速度 = 10f;

//    public void 初始化(Transform 目标)
//    {
//        this.目标 = 目标;
//    }

//    private void Update()
//    {
//        if (目标 == null)
//        {
//            回收();
//            return;
//        }

//        // 移动逻辑...

//        if (检测到碰撞())
//        {
//            造成伤害();
//            回收();
//        }
//    }

//    private void 回收()
//    {
//        对象池管理器.实例.回收对象(gameObject);
//    }
//}


/*

   public float 淡入时间 = 0.5f;  // 淡入动画时长
   public float 淡出时间 = 0.5f;  // 淡出动画时长

   public float 淡出值 = 0.0f;  // 淡出动画时长
   public float 淡入值 = 0.5f;  // 淡出动画时长 
   private float 过渡值 = 0.0f;  // 淡出动画时长 临时计算值   

   private bool 显示状态 = false;


   经过时间 / 淡入时间;
 == 
   递增值 /  0.5 

   递增值  = 

public float 淡出值 = 0.0f;  // 完全透明
public float 淡入值 = 0.5f;  // 半透明 (你可以设为1.0f表示完全不透明)
public float 过渡速度 = 1.0f;  // 过渡速度，值越大变化越快
private float 过渡值 = 0.0f;  // 当前透明度值
private bool 显示状态 = false;
private Coroutine 过渡协程;

// 启动淡入/淡出的公共方法
public void 切换显示状态(bool 目标状态)
{
    // 如果状态未变化，不执行任何操作
    if (显示状态 == 目标状态) return;
    
    // 更新目标状态
    显示状态 = 目标状态;
    
    // 如果已有正在运行的协程，先停止它
    if (过渡协程 != null)
    {
        StopCoroutine(过渡协程);
    }
    
    // 启动新的过渡协程
    过渡协程 = StartCoroutine(执行过渡());
}

private IEnumerator 执行过渡()
{
    // 根据目标状态确定目标值
    float 目标值 = 显示状态 ? 淡入值 : 淡出值;
    
    // 循环直到过渡值接近目标值
    while (Mathf.Abs(过渡值 - 目标值) > 0.01f)
    {
        // 根据当前状态和目标值调整过渡值
        if (显示状态)
        {
            // 淡入：过渡值增加
            过渡值 = Mathf.MoveTowards(过渡值, 目标值, 过渡速度 * Time.deltaTime);
        }
        else
        {
            // 淡出：过渡值减少
            过渡值 = Mathf.MoveTowards(过渡值, 目标值, 过渡速度 * Time.deltaTime);
        }
        
        // 应用过渡值到材质
        if (地板材质实例 != null)
        {
            Color 当前颜色 = 地板材质实例.color;
            当前颜色.a = 过渡值;
            地板材质实例.color = 当前颜色;
        }
        
        // 等待下一帧
        yield return null;
    }
    
    // 确保最终值精确
    过渡值 = 目标值;
    if (地板材质实例 != null)
    {
        Color 当前颜色 = 地板材质实例.color;
        当前颜色.a = 过渡值;
        地板材质实例.color = 当前颜色;
    }
    
    过渡协程 = null;
}




*/


