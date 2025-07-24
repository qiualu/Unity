using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物类 : MonoBehaviour
{
    [Tooltip("移动速度倍率，值越大移动越灵敏")]
    public float 移动速度 = 1f;

    // 用于存储鼠标在世界空间中的位置
    private Vector3 目标位置;

    // Update每帧执行，处理鼠标跟随逻辑
    void Update()
    {
        // 获取鼠标在屏幕上的位置，并转换为世界空间坐标（2D游戏用z=0，3D游戏可调整z值）
        目标位置 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        目标位置.z = transform.position.z; // 保持原有z轴位置，避免对象前后移动

        // 让对象平滑移动到目标位置（鼠标位置）
        transform.position = Vector3.Lerp(
            transform.position,       // 当前位置
            目标位置,                 // 鼠标位置
            移动速度 * Time.deltaTime // 移动速度（乘以Time.deltaTime使速度不受帧率影响）
        );
    }
}