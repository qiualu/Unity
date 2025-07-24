using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 简化版炮塔类：检测碰撞并旋转指向目标
/// </summary>
public class 炮塔类 : MonoBehaviour
{
    [Header("基础设置")]
    public float 旋转速度 = 5f; // 旋转指向目标的速度

    [Header("攻击范围")]
    public float 攻击范围 = 5f; // 检测目标的范围

    private CircleCollider2D 攻击范围碰撞体; // 用于检测目标的碰撞体
    private Transform 当前目标; // 当前检测到的目标

    // 初始化
    private void Start()
    {
        初始化碰撞体();
    }

    // 重新激活时重置
    private void OnEnable()
    {
        初始化碰撞体();
        当前目标 = null;
    }

    // 每帧更新
    private void Update()
    {
        // 如果有目标，旋转指向目标
        if (当前目标 != null)
        {
            旋转指向目标();
        }
    }

    /// <summary>
    /// 初始化碰撞体组件
    /// </summary>
    private void 初始化碰撞体()
    {
        // 获取或添加圆形碰撞体作为触发器
        攻击范围碰撞体 = GetComponent<CircleCollider2D>();
        if (攻击范围碰撞体 == null)
        {
            攻击范围碰撞体 = gameObject.AddComponent<CircleCollider2D>();
        }
        攻击范围碰撞体.isTrigger = true; // 设为触发器以检测碰撞
        攻击范围碰撞体.radius = 攻击范围; // 设置检测范围
    }

    /// <summary>
    /// 旋转炮塔指向目标
    /// </summary>
    private void 旋转指向目标()
    {
        // 计算目标方向
        Vector3 目标方向 = 当前目标.position - transform.position;
        // 忽略Z轴（2D游戏）
        目标方向.z = 0;

        // 如果目标在有效范围内
        if (目标方向.magnitude > 0.1f)
        {
            // 计算需要旋转的角度
            Quaternion 目标旋转 = Quaternion.LookRotation(Vector3.forward, 目标方向);
            // 平滑旋转到目标角度
            transform.rotation = Quaternion.Lerp(transform.rotation, 目标旋转, 旋转速度 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 输出所有碰撞信息（中文）
        Debug.Log($"碰撞触发了！物体名称：{collision.gameObject.name}，标签：{collision.tag}，图层：{LayerMask.LayerToName(collision.gameObject.layer)}");

        // 检查是否是目标标签
        if (collision.tag == "怪物" || collision.tag == "道具")
        {
            Debug.Log($"找到有效目标：{collision.gameObject.name}，位置：{collision.transform.position}");
            当前目标 = collision.transform;
        }
        else
        {
            Debug.Log($"碰撞体标签不符：当前标签是{collision.tag}，需要“怪物”或“道具”");
        }
    }
    /// <summary>
    /// 目标在攻击范围内持续触发
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {

        Debug.Log($"目标在攻击范围内持续触发");

        // 确保目标依然有效
        if ((collision.CompareTag("怪物") || collision.CompareTag("道具")) && 当前目标 == null)
        {
            当前目标 = collision.transform;
        }
    }

    /// <summary>
    /// 目标离开攻击范围时触发
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"目标离开攻击范围时触发");


        // 如果离开的是当前目标，清除目标
        if (collision.transform == 当前目标)
        {
            Debug.Log($"目标离开范围：{collision.name}");
            当前目标 = null;
        }
    }

    // 绘制攻击范围 gizmos（编辑器中可见）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 攻击范围);
    }
}
