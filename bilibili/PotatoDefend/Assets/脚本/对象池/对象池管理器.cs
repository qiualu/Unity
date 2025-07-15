using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 对象池管理器 : MonoBehaviour
{

    // 单例模式
    public static 对象池管理器 实例;

    // 预制体配置
    [System.Serializable]
    public class 预制体配置
    { 
        public GameObject 预制体;
        public int 初始数量 = 10;
        public bool 可扩展 = true;
    }

    [SerializeField] private List<预制体配置> 预制体列表;

    // 对象池字典
    private Dictionary<GameObject, 对象池> 对象池字典 = new Dictionary<GameObject, 对象池>();

    private void Awake()
    {
        // 初始化单例
        if (实例 == null)
        {
            实例 = this; // 将当前挂载的组件实例赋值给静态变量
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 防止重复实例
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        // 初始化所有预设的对象池
        foreach (var 配置 in 预制体列表)
        {
            创建对象池(配置.预制体, 配置.初始数量, 配置.可扩展);
        }
    }

    /// <summary>
    /// 创建对象池
    /// </summary>
    public 对象池 创建对象池(GameObject 预制体, int 初始数量, bool 可扩展 = true)
    {
        if (对象池字典.ContainsKey(预制体))
        {
            Debug.LogWarning($"对象池已存在: {预制体.name}");
            return 对象池字典[预制体];
        }
        Debug.Log($"  ****************** 对象池:  创建对象池 ******************  ");
        // 创建对象池容器
        GameObject 池容器 = new GameObject($"池_{预制体.name}");
        池容器.transform.SetParent(transform);

        // 创建对象池
        对象池 新池 = new 对象池(预制体, 初始数量, 可扩展, 池容器.transform);
        对象池字典.Add(预制体, 新池);

        return 新池;
    }

    /// <summary>
    /// 从对象池获取对象
    /// </summary>
    public GameObject 获取对象(GameObject 预制体, Vector3 位置, Quaternion 旋转)
    {
        if (!对象池字典.ContainsKey(预制体))
        {
            Debug.LogWarning($"对象池不存在: {预制体.name}，正在创建...");
            创建对象池(预制体, 5);
        }

        return 对象池字典[预制体].获取对象(位置, 旋转);
    }

    /// <summary>
    /// 回收对象到池
    /// </summary>
    public void 回收对象(GameObject 对象)
    {
        // 从对象中获取池标识组件
        池对象标识 池标识 = 对象.GetComponent<池对象标识>();
        if (池标识 == null || 池标识.预制体 == null)
        {
            Debug.LogError($"尝试回收未标记的对象: {对象.name}");
            return;
        }

        GameObject 预制体 = 池标识.预制体;

        if (!对象池字典.ContainsKey(预制体))
        {
            Debug.LogError($"找不到对应的对象池: {预制体.name}");
            return;
        }

        对象池字典[预制体].回收对象(对象);
    }
}
