using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 对象池标识组件，用于标记对象属于哪个预制体池
/// </summary>
public class 池对象标识 : MonoBehaviour
{
    public GameObject 预制体;
}

public class 对象池 : MonoBehaviour
{

    private GameObject 预制体;
    private int 初始数量;
    private bool 可扩展;
    private Transform 父容器;
    private Queue<GameObject> 空闲对象队列 = new Queue<GameObject>();
    //private int 当前ID = 0; // ID计数器


    public 对象池(GameObject 预制体, int 初始数量, bool 可扩展, Transform 父容器)
    {
        this.预制体 = 预制体;
        this.初始数量 = 初始数量;
        this.可扩展 = 可扩展;
        this.父容器 = 父容器;

        // 初始化池
        填充池(初始数量);
    }

    /// <summary>
    /// 填充池
    /// </summary>
    private void 填充池(int 数量)
    {
        for (int i = 0; i < 数量; i++)
        {
            创建新对象();
        }
    }

    /// <summary>
    /// 创建新对象并添加到池中
    /// </summary>
    private GameObject 创建新对象()
    {
        GameObject 对象 = GameObject.Instantiate(预制体, 父容器);
        对象.SetActive(false);

        // 添加池标识组件
        池对象标识 池标识 = 对象.AddComponent<池对象标识>();
        池标识.预制体 = 预制体;

        空闲对象队列.Enqueue(对象);
        return 对象;
    }

    /// <summary>
    /// 从池中获取对象
    /// </summary>
    public GameObject 获取对象(Vector3 位置, Quaternion 旋转)
    {
        GameObject 对象 = null;

        // 如果池中有对象，取出一个
        if (空闲对象队列.Count > 0)
        {
            对象 = 空闲对象队列.Dequeue();
            对象.transform.position = 位置;
            对象.transform.rotation = 旋转;
            对象.SetActive(true);
        }
        // 如果池为空且允许扩展，创建新对象
        else if (可扩展)
        {
            对象 = 创建新对象();
            对象.transform.position = 位置;
            对象.transform.rotation = 旋转;
            对象.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"对象池已空且不可扩展: {预制体.name}");
        }
        //Debug.Log($" 获取对象  对象: {对象.transform.position} 位置:  {位置}");
        return 对象;
    }

    /// <summary>
    /// 回收对象到池中
    /// </summary>
    public void 回收对象(GameObject 对象)
    {
        // 确保对象已禁用
        对象.SetActive(false);
        对象.transform.SetParent(父容器);

        // 加入空闲队列
        空闲对象队列.Enqueue(对象);
    }
}
