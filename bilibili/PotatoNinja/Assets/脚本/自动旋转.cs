using UnityEngine;

/// <summary>
/// 自动旋转脚本
/// 绑定到任何游戏对象上，使其按照设定的轴和速度持续旋转
/// </summary>
public class 自动旋转 : MonoBehaviour
{
    [Header("旋转设置")]
    [Tooltip("旋转轴 (X, Y, Z)，1表示围绕该轴旋转，0表示不旋转")]
    public Vector3 旋转轴 = Vector3.forward; // 默认绕Z轴旋转（forward对应Z轴）

    [Tooltip("旋转速度（度/秒）")]
    [Range(1f, 360f)]
    public float 旋转速度 = 30f; // 默认每秒旋转30度

    [Tooltip("是否使用局部坐标系旋转（否则使用世界坐标系）")]
    public bool 使用局部旋转 = true;

    void Update()
    {
        // 计算每帧旋转的角度
        float 每帧角度 = 旋转速度 * Time.deltaTime;

        // 计算旋转向量
        Vector3 旋转量 = 旋转轴.normalized * 每帧角度;

        // 应用旋转
        if (使用局部旋转)
        {
            // 局部坐标系旋转
            transform.Rotate(旋转量, Space.Self);
        }
        else
        {
            // 世界坐标系旋转
            transform.Rotate(旋转量, Space.World);
        }
    }
}
