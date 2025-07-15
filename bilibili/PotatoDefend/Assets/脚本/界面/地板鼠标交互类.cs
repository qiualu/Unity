using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class 地板鼠标交互类 : MonoBehaviour
//using UnityEngine;
//using System.Collections;

public class 地板鼠标交互类 : MonoBehaviour
{

    public GameObject 内部元素;  // 缩短名称
    public GameObject 升级菜单;  // 更明确的名称

    // 原始缩放比例（初始状态）
    private Vector3 原始缩放;
    // 悬停时的目标缩放倍数
    public float 悬停缩放 = 1.2f;
    // 点击时的目标缩放倍数
    public float 点击缩放 = 1.5f;
    // 缩放过渡速度（数值越大越快）
    public float 过渡速度 = 10f;

    private bool 正在悬停 = false;
    private bool 正在点击 = false;
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
    }

    // 鼠标进入地板范围时
    private void OnMouseEnter()
    {
        正在悬停 = true;
        // 计算目标缩放（原始缩放 * 悬停倍数），传递Vector3类型
        开始缩放协程(原始缩放 * 悬停缩放);
    }

    // 鼠标离开地板范围时
    private void OnMouseExit()
    {
        正在悬停 = false;
        if (!正在点击)
        {
            // 恢复原始缩放
            开始缩放协程(原始缩放);
        }
    }

    // 鼠标按下时
    private void OnMouseDown()
    {
        正在点击 = true;
        // 立即设置为点击缩放（无过渡）
        transform.localScale = 原始缩放 * 点击缩放;
        原始缩放 = 原始缩放 * 1.5f;
        关卡加载类.关卡加载实例.记录点击位置(this);
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
    }
}


