using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class 保地板类 : MonoBehaviour
{
    public int 地板id = -1; // 地板唯一标识
    public Vector2Int 网格坐标; // 地板在网格中的位置坐标

    // 组件引用（当前预制体自身的SpriteRenderer）
    private SpriteRenderer 精灵渲染器; // 预制体自带的图片渲染组件

    // 地板核心状态
    public 地板状态结构体 地板状态; // 存储地板的各种状态信息


    // 标记是否正在执行协程（避免重复调用）
    private bool 地板延时协程状态 = false;

    // 子原件
    public GameObject 升级小标志; 
    public GameObject 攻击范围显示; 
    public GameObject 建造按钮; 
    
    public GameObject 升级1; 
    public GameObject 升级2; 
    public GameObject 升级金币不足1; 
    public GameObject 升级金币不足2;

    public GameObject 移除1;
    public GameObject 移除2;
    public GameObject 移除3;

    public GameObject 炮台; 
    public GameObject 炮塔1;
    public GameObject 炮塔2; 
    public GameObject 炮塔3;


    /// <summary>
    /// 地板状态结构体
    /// 包含地板的可建造性、路径属性、道具信息等
    /// </summary>
    public struct 地板状态结构体
    {
        public bool 可建造;             // 是否允许建造塔
        public bool 是怪物路径;         // 是否为怪物行走路径
        public bool 有道具;             // 地板上是否存在道具
        public int 道具ID;              // 道具ID（-1表示无道具）
        public bool 已建塔;             // 地板上是否已建造塔
    }

    private void Awake()
    {
        // 获取当前预制体自身的SpriteRenderer组件
        精灵渲染器 = GetComponent<SpriteRenderer>();

        // 初始化地板状态（默认可建造、非怪物路径、无道具）
        地板状态.可建造 = true;
        地板状态.是怪物路径 = false;
        地板状态.有道具 = false;
        地板状态.道具ID = -1;
        地板状态.已建塔 = false;

        隐藏所有子组件();
        图片淡出();
    }



    private void 隐藏所有子组件()
    {
        // 逐个隐藏UI组件
        设置物体状态(升级小标志, false);
        设置物体状态(攻击范围显示, false);
        设置物体状态(建造按钮, false);

        设置物体状态(升级1, false);
        设置物体状态(升级2, false);
        设置物体状态(升级金币不足1, false);
        设置物体状态(升级金币不足2, false);

        设置物体状态(移除1, false);
        设置物体状态(移除2, false);
        设置物体状态(移除3, false);

        // 隐藏炮塔相关物体
        设置物体状态(炮台, false);
        设置物体状态(炮塔1, false);
        设置物体状态(炮塔2, false);
        设置物体状态(炮塔3, false);

        设置物体状态(炮台, true);
        设置物体状态(炮塔1, true);


    }

 

    // 鼠标按下时
    private void OnMouseDown()
    {
        图片淡入();
        执行延迟操作(测试结束);
        //执行延迟操作(() => 图片淡出());

        //设置物体状态(炮台, true);
        //设置物体状态(炮塔1, true); 

    }

    private void 测试结束()
    {
        图片淡出();
        //设置物体状态(炮台, false);
        //设置物体状态(炮塔1, false); 
    }



    /// 执行带2秒延迟的操作，且避免重复调用
    /// </summary>
    /// <param name="action">延迟后要执行的操作</param>
    public void 执行延迟操作(System.Action action)
    {
        // 如果协程正在运行，则直接返回（避免重复调用）
        if (地板延时协程状态)
        {
            //Debug.Log("操作正在冷却中，2秒内不可重复执行");
            return;
        }

        // 启动协程
        StartCoroutine(延迟操作协程(2f, action));
    }
    /// <summary>
    /// 延迟执行的协程
    /// </summary>
    /// <param name="delayTime">延迟时间（秒）</param>
    /// <param name="action">延迟后执行的方法</param>
    private IEnumerator 延迟操作协程(float delayTime, System.Action action)
    {
        // 标记为正在执行
        地板延时协程状态 = true;

        // 等待指定时间（2秒）
        yield return new WaitForSeconds(delayTime);

        // 执行目标操作
        action?.Invoke();

        // 操作完成后，标记为可再次执行
        地板延时协程状态 = false;
    }





    /// <summary>
    /// 图片淡入效果（仅控制自身SpriteRenderer）
    /// 从完全透明过渡到完全不透明
    /// </summary>
    /// <param name="持续时间">淡入动画的时长（秒），默认0.5秒</param>
    public void 图片淡入(float 持续时间 = 0.5f)
    {
        if (精灵渲染器 == null) return; // 保护机制：若组件不存在则不执行

        // 确保渲染器处于启用状态
        精灵渲染器.enabled = true;

        // 记录当前颜色的RGB值，仅修改透明度
        Color 当前颜色 = 精灵渲染器.color;
        当前颜色.a = 0; // 起始透明度设为0（完全透明）
        精灵渲染器.color = 当前颜色;

        // 使用DOTween执行淡入动画（透明度从0→1）
        DOTween.To(
            () => 精灵渲染器.color, // 读取当前颜色
            目标颜色 => 精灵渲染器.color = 目标颜色, // 写入目标颜色
            new Color(当前颜色.r, 当前颜色.g, 当前颜色.b, 1), // 目标透明度1（完全不透明）
            持续时间
        );
    }

    /// <summary>
    /// 图片淡出效果（仅控制自身SpriteRenderer）
    /// 从完全不透明过渡到完全透明，结束后隐藏渲染器
    /// </summary>
    /// <param name="持续时间">淡出动画的时长（秒），默认0.5秒</param>
    public void 图片淡出(float 持续时间 = 0.5f)
    {
        if (精灵渲染器 == null) return; // 保护机制：若组件不存在则不执行

        // 确保渲染器处于启用状态
        精灵渲染器.enabled = true;

        // 记录当前颜色的RGB值，仅修改透明度
        Color 当前颜色 = 精灵渲染器.color;
        当前颜色.a = 1; // 起始透明度设为1（完全不透明）
        精灵渲染器.color = 当前颜色;

        // 使用DOTween执行淡出动画（透明度从1→0）
        DOTween.To(
            () => 精灵渲染器.color, // 读取当前颜色
            目标颜色 => 精灵渲染器.color = 目标颜色, // 写入目标颜色
            new Color(当前颜色.r, 当前颜色.g, 当前颜色.b, 0), // 目标透明度0（完全透明）
            持续时间
        ).OnComplete(() => {
            // 动画结束后隐藏渲染器（节省性能）
            精灵渲染器.enabled = false;
        });
    }




    /// <param name="状态">激活状态</param>
    private void 设置物体状态(GameObject obj, bool 状态)
    {
        if (obj != null) // 检查物体是否存在，避免赋值遗漏导致报错
        {
            obj.SetActive(状态);
        }
        else
        {
            Debug.LogWarning($"未赋值的物体：{obj?.name ?? "未知物体"}，请在Inspector中检查引用");
        }
    }



}