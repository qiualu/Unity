using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹基类：所有子弹的父类，统一管理子弹的移动、碰撞和回收逻辑
/// </summary>
public class 子弹基类 : MonoBehaviour
{
    [HideInInspector]
    public Transform 目标位置; // 子弹要攻击的目标位置
    public int 移动速度; // 子弹飞行速度
    public int 攻击数值; // 子弹造成的伤害值
    public int 塔ID; // 发射该子弹的塔的ID
    public int 塔等级; // 发射该子弹的塔的等级
    public GameObject 子弹预制体; // 对应子弹的预制体（需在Inspector赋值）
    public GameObject 特效预制体; // 命中特效预制体（需在Inspector赋值）

    // 初始化时调用
    void Start()
    {
        // 自动添加池标识（如果没有）
        if (GetComponent<池对象标识>() == null)
        {
            池对象标识 标识 = gameObject.AddComponent<池对象标识>();
            标识.预制体 = 子弹预制体;
        }
    }

    // 每帧更新
    protected virtual void Update()
    {
        // 游戏结束时销毁子弹（假设0代表游戏结束状态）
        if (保卫管理.实例.游戏状态 == 0)
        {
            销毁子弹();
            return;
        }
        // 游戏暂停时不执行逻辑
        if (保卫管理.实例.是否暂停)
        {
            return;
        }
        // 目标不存在或已失效时，销毁子弹
        if (目标位置 == null || !目标位置.gameObject.activeSelf)
        {
            销毁子弹();
            return;
        }

        // 子弹的移动与转向逻辑
        if (目标位置.gameObject.tag == "道具")
        {
            // 向道具位置移动（微调Z轴避免层级问题）
            transform.position = Vector3.Lerp(
                transform.position,
               目标位置.position + new Vector3(0, 0, 3),
               1 / Vector3.Distance(transform.position, 目标位置.position + new Vector3(0, 0, 3))
               * Time.deltaTime * 移动速度 * 保卫管理.实例.游戏速度
           );
            transform.LookAt(目标位置.position + new Vector3(0, 0, 3));
        }
        else
        {
            // 向怪物位置移动
            transform.position = Vector3.Lerp(
                transform.position,
               目标位置.position,
               1 / Vector3.Distance(transform.position, 目标位置.position)
               * Time.deltaTime * 移动速度 * 保卫管理.实例.游戏速度
           );
            transform.LookAt(目标位置.position);
        }

        // 修正旋转角度（避免模型朝向异常）
        if (transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
        }
    }

    /// <summary>
    /// 销毁子弹（回收至对象池）
    /// </summary>
    protected virtual void 销毁子弹()
    {
        目标位置 = null;
        对象池管理器.实例.回收对象(gameObject); // 使用对象池管理器回收
    }

    /// <summary>
    /// 创建子弹命中特效
    /// </summary>
    protected virtual void 创建特效()
    {
        if (特效预制体 == null)
        {
            Debug.LogWarning("未设置特效预制体！");
            return;
        }

        // 通过对象池管理器获取特效实例
        GameObject 特效对象 = 对象池管理器.实例.获取对象(
            特效预制体,
            transform.position,
            Quaternion.identity
        );
        特效对象.SetActive(true);

        // 自动给特效添加回收逻辑（示例：2秒后回收）
        StartCoroutine(延迟回收特效(特效对象, 2f));
    }

    /// <summary>
    /// 延迟回收特效
    /// </summary>
    IEnumerator 延迟回收特效(GameObject 特效, float 延迟时间)
    {
        yield return new WaitForSeconds(延迟时间);
        对象池管理器.实例.回收对象(特效);
    }

    /// <summary>
    /// 碰撞检测（命中目标时触发）
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 仅对怪物或道具生效
        if (collision.tag == "怪物" || collision.tag == "道具")
        {
            if (collision.gameObject.activeSelf) // 目标必须处于激活状态
            {
                // 目标位置无效时直接返回
                if (目标位置 == null)
                {
                    return;
                }

                // 命中正确目标时造成伤害
                if (collision.tag == "怪物" || (collision.tag == "道具" && 目标位置 == collision.transform))
                {
                    collision.SendMessage("受到伤害", 攻击数值);
                    创建特效();
                    销毁子弹();
                }
            }
        }
    }
}