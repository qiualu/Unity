using UnityEngine;

/// <summary>
/// 仅控制切片旋转（限制X、Y轴，保留Z轴自由旋转）
/// 不处理销毁逻辑，由根目录脚本负责销毁
/// </summary>
public class 切片旋转限制器 : MonoBehaviour
{
    [Header("旋转限制")]
    public bool 限制XY旋转 = true; // 是否限制X、Y轴旋转
    private float 初始X旋转; // 记录初始X轴角度
    private float 初始Y旋转; // 记录初始Y轴角度


    private void OnEnable()
    {
        // 记录切片激活时的初始X、Y旋转角度（保留切割后的初始姿态）
        初始X旋转 = transform.eulerAngles.x;
        初始Y旋转 = transform.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        // 限制X、Y轴旋转，仅允许Z轴自由旋转
        if (限制XY旋转)
        {
            Vector3 当前旋转 = transform.eulerAngles;
            // 强制X、Y轴保持初始角度，Z轴保持当前旋转
            transform.eulerAngles = new Vector3(
                初始X旋转,
                初始Y旋转,
                当前旋转.z
            );
        }
    }
}
