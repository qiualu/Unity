using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class 地板鼠标交互类 : MonoBehaviour
//using UnityEngine;
//using System.Collections;

public class 地板鼠标交互类 : MonoBehaviour
{

    public GameObject 地板元素;  // 缩短名称
    public GameObject 升级菜单;  // 更明确的名称

    // 调试模式 
    //public bool 调试模式 = false;
    public bool 调试模式 = true;

    // 原始缩放比例（初始状态）
    private Vector3 原始缩放;
    private Vector3 原始缩放保持;

    // 悬停时的目标缩放倍数
    public float 悬停缩放 = 1.2f;
    // 点击时的目标缩放倍数
    public float 点击缩放 = 1.5f;
    // 缩放过渡速度（数值越大越快）
    public float 过渡速度 = 10f;

    private bool 正在悬停 = false;
    private bool 正在点击 = false;

    private Material 地板材质实例;  // 材质实例，避免影响其他对象

    public float 淡入时间 = 0.5f;  // 淡入动画时长
    public float 淡出时间 = 0.5f;  // 淡出动画时长

    public float 过渡颜色速度 = 1.0f;  // 过渡速度，值越大变化越快
    public float 自动淡出时间 = 2.0f;  // 过渡速度，值越大变化越快

    private float 自动淡出计时 = 0.0f;  // 过渡速度，值越大变化越快
    public float 淡出值 = 0.0f;  // 淡出动画时长
    public float 淡入值 = 0.5f;  // 淡出动画时长
 
    private float 过渡值 = 0.0f;  // 淡出动画时长 临时计算值   

    private bool 显示状态 = false;

     
    private Color 原始颜色;  // 保存原始颜色
    private Coroutine 当前动画协程;  // 当前运行的动画协程
    private Coroutine 过渡协程;  // 当前运行的动画协程
    private Coroutine 淡出过渡计时协程;  // 当前运行的动画协程
    private Coroutine 缩放协程;

    public int 地板id = -1;

    // 关卡加载类的引用，用于记录点击位置
    private 关卡加载类 关卡加载器;
    // 记录当前地板在网格中的位置
    public Vector2Int 网格坐标;

    private void Awake()
    {
        // 记录初始缩放（确保地板预制体初始缩放正确）
        原始缩放 = transform.localScale;
        原始缩放保持 = 原始缩放 * 1.5f;

        // 确保地板元素已赋值
        if (地板元素 != null)
        {
            // 获取渲染组件
            Renderer renderer = 地板元素.GetComponent<Renderer>();
            if (renderer != null)
            {
                // 创建材质实例，避免影响其他使用相同材质的对象
                地板材质实例 = Instantiate(renderer.material);
                renderer.material = 地板材质实例;

                // 保存原始颜色（包含原始透明度）
                原始颜色 = 地板材质实例.color;
            }
            else
            {
                Debug.LogError("地板元素上没有Renderer组件！");
            }
        }
        else
        {
            Debug.LogError("请赋值地板元素！");
        }
        播放淡出动画();

    }

    // 鼠标进入地板范围时
    private void OnMouseEnter()
    {
        正在悬停 = true;
        // 计算目标缩放（原始缩放 * 悬停倍数），传递Vector3类型
        开始缩放协程(原始缩放 * 悬停缩放);
        //播放淡入动画();

        //if (!显示状态) // 等于false 
        //{
        //    播放淡入动画();
        //}


        Debug.Log("开始 切换显示状态");
        切换显示状态();

    }

    // 鼠标离开地板范围时
    private void OnMouseExit()
    {
        正在悬停 = false;
        //if (!正在点击)
        //{
        //    // 恢复原始缩放
        //    开始缩放协程(原始缩放);
        //}
        //切换隐藏状态();
    }

    // 鼠标按下时
    private void OnMouseDown()
    {
        正在点击 = true;

        Debug.Log($"调试模式 : {调试模式}");

        if (调试模式)
        {

            原始缩放 = 原始缩放保持;
            // 立即设置为点击缩放（无过渡）
            transform.localScale = 原始缩放 * 点击缩放;
            关卡加载类.关卡加载实例.记录点击位置(this);
        }


        //Debug.Log("开始 播放淡出动画");
        //播放淡出动画();
        //Color 目标颜色 = 原始颜色;  // 恢复原始不透明度 
        //目标颜色.a = 0.0f;  // 起始为完全透明
        //地板材质实例.color = 目标颜色;
        Debug.Log("开始 切换显示状态");
        切换显示状态();
    }

    // 鼠标松开时
    private void OnMouseUp()
    {
        正在点击 = false;
        if (正在悬停)
        {
            // 若仍在悬停，过渡到悬停缩放
            开始缩放协程(原始缩放 * 悬停缩放);
        }
        else
        {
            // 若已离开，恢复原始缩放
            开始缩放协程(原始缩放);
        }

        //Debug.Log("开始 播放淡入动画");
        //播放淡入动画();
        //Color 目标颜色 = 原始颜色;  // 恢复原始不透明度 
        //目标颜色.a = 1.0f;  // 起始为完全透明
        //地板材质实例.color = 目标颜色;
        //切换隐藏状态();
    }

    // 启动缩放协程（参数改为Vector3目标缩放）
    private void 开始缩放协程(Vector3 目标缩放)
    {
        // 停止当前协程（避免多个过渡冲突）
        if (缩放协程 != null)
        {
            StopCoroutine(缩放协程);
        }
        // 启动新协程，传递Vector3目标
        缩放协程 = StartCoroutine(平滑缩放至目标(目标缩放));
    }

    // 平滑缩放的协程（接收Vector3目标缩放）
    private IEnumerator 平滑缩放至目标(Vector3 目标缩放)
    {
        // 逐步过渡到目标缩放
        while (Vector3.Distance(transform.localScale, 目标缩放) > 0.01f)
        {
            // 每帧插值缩放
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                目标缩放,
                过渡速度 * Time.deltaTime
            );
            yield return null; // 等待下一帧
        }
        // 最终精确设置目标缩放
        transform.localScale = 目标缩放;
    }

    // 淡入动画：从透明到不透明
    public void 播放淡入动画()
    {
        // 如果有正在运行的动画，先停止
        if (当前动画协程 != null)
        {
            StopCoroutine(当前动画协程);
        }
        当前动画协程 = StartCoroutine(淡入过程());
    }

    // 淡出动画：从不透明到透明
    public void 播放淡出动画()
    {
        if (当前动画协程 != null)
        {
            StopCoroutine(当前动画协程);
        }
        当前动画协程 = StartCoroutine(淡出过程());
    }

    public void 切换显示状态()
    {
        // 如果状态未变化，不执行任何操作
        显示状态 = true;
        // 如果已有正在运行的协程，先停止它
        if (过渡协程 != null)
        {
            StopCoroutine(过渡协程);
        }
        // 启动新的过渡协程
        过渡协程 = StartCoroutine(执行过渡());
        // 停止计时 
        if (淡出过渡计时协程 != null) // 淡出过渡计时协程
        {
            StopCoroutine(淡出过渡计时协程);
        }

    }
    public void 切换隐藏状态()
    {
        // 如果状态未变化，不执行任何操作
        显示状态 = false;
        // 如果已有正在运行的协程，先停止它
        if (过渡协程 != null) // 淡出过渡计时协程
        {
            StopCoroutine(过渡协程);
        }
        // 启动新的过渡协程
        过渡协程 = StartCoroutine(执行过渡());
    }

    //淡入协程
    private IEnumerator 执行过渡()
    {
        // 根据目标状态确定目标值
        float 目标值 = 显示状态 ? 淡入值 : 淡出值;

        // 循环直到过渡值接近目标值
        while (Mathf.Abs(过渡值 - 目标值) > 0.01f)
        {
            目标值 = 显示状态 ? 淡入值 : 淡出值;

            // 根据当前状态和目标值调整过渡值
            if (显示状态)
            {
                // 淡入：过渡值增加
                过渡值 = Mathf.MoveTowards(过渡值, 目标值, 过渡颜色速度 * Time.deltaTime);
            }
            else
            {
                // 淡出：过渡值减少
                过渡值 = Mathf.MoveTowards(过渡值, 目标值, 过渡颜色速度 * Time.deltaTime);
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
        if (显示状态) // 显示状态 
        {
            
            
            // 如果已有正在运行的协程，先停止它
            if (淡出过渡计时协程 != null) // 淡出过渡计时协程
            {
                StopCoroutine(淡出过渡计时协程);
            }
            // 启动新的过渡协程
            淡出过渡计时协程 = StartCoroutine(淡出过渡计时());
        }
            
    }

    private IEnumerator 淡出过渡计时()
    {
        // 根据目标状态确定目标值
        float 目标值 = 自动淡出时间;
        自动淡出计时 = 0.0f;
        // 循环直到过渡值接近目标值
        while (自动淡出计时 < 目标值 )
        {
            自动淡出计时 += Time.deltaTime;
            // 等待下一帧
            yield return null;
        }
        切换隐藏状态();
        淡出过渡计时协程 = null;
    }


    //淡入协程
    private IEnumerator 淡入过程()
    {
        if (地板材质实例 == null) yield break;

        float 经过时间 = 0;
        Color 目标颜色 = 原始颜色;  // 恢复原始不透明度
        Color 起始颜色 = 原始颜色;
        起始颜色.a = 0;  // 起始为完全透明

        while (经过时间 < 淡入时间)
        {
            // 计算插值比例（0到1）
            float 比例 = 经过时间 / 淡入时间;
            // 平滑插值，使动画更自然
            比例 = Mathf.SmoothStep(0, 1, 比例);
            // 应用颜色插值
            地板材质实例.color = Color.Lerp(起始颜色, 目标颜色, 比例);

            经过时间 += Time.deltaTime;
            yield return null;
        }

        // 确保最终颜色正确
        地板材质实例.color = 目标颜色;
        当前动画协程 = null;
        显示状态 = true;
    }

    // 淡出协程
    private IEnumerator 淡出过程()
    {
        if (地板材质实例 == null) yield break;

        float 经过时间 = 0;
        Color 起始颜色 = 地板材质实例.color;
        Color 目标颜色 = 原始颜色;
        目标颜色.a = 0;  // 目标为完全透明

        while (经过时间 < 淡出时间)
        {
            float 比例 = 经过时间 / 淡出时间;
            比例 = Mathf.SmoothStep(0, 1, 比例);
            地板材质实例.color = Color.Lerp(起始颜色, 目标颜色, 比例);

            经过时间 += Time.deltaTime;
            yield return null;
        }

        地板材质实例.color = 目标颜色;
        当前动画协程 = null;
        显示状态 = false;
    }


    // 从对象池激活时重置状态
    private void OnEnable()
    {
        transform.localScale = 原始缩放;
        正在悬停 = false;
        正在点击 = false;
        if (缩放协程 != null)
        {
            StopCoroutine(缩放协程);
        }
        播放淡出动画();
    }


}


