using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 土豆生成类 : MonoBehaviour
{
    // 新增：跟踪已生成的所有对象
    private List<GameObject> 已生成对象列表 = new List<GameObject>();

    private Collider 生成区域;
    private Coroutine 协程对象;
    private bool 生成状态 = false;

    public GameObject[] 土豆组;
    public GameObject 炸弹;
    //[Range(0f, 1f)] 

    public float 炸弹概率 = 0.05f;

    public float 最小生成间隔 = 0.25f;
    public float 最大生成间隔 = 1f;
    public float 最小生成角度 = -15f;
    public float 最大生成角度 = 15f;
    public float 最小生成力度 = 18f;
    public float 最大生成力度 = 22f;
    public float 最小自旋力度 = 1f;
    public float 最大自旋力度 = 10f;
    public float 生命周期 = 5f;
    public float 启动后等待生成时间 = 2f;

    public enum 旋转模式
    {
        完全随机,
        主要Z轴,
        自定义轴
    }
    [Header("旋转设置")]
    public 旋转模式 当前旋转模式 = 旋转模式.完全随机;
    public Vector3 主要旋转轴 = Vector3.forward;


    private void Awake()
    {
        生成区域 = GetComponent<Collider>();

        炸弹概率 = 0.05f;

    }

    private void OnEnable()
    {

        if (土豆忍者管理类.土豆忍者管理.炸弹概率 == 0)
        {
            炸弹概率 = 0.05f;
            最小生成间隔 = 0.25f;
            最大生成间隔 = 1f;
        }
        else {
            炸弹概率 = 0.3f;
            最小生成间隔 = 0.1f;
            最大生成间隔 = 0.4f;
        }


        开始生成();
    }

    private void OnDisable()
    {
        停止生成();
    }

    // 开始生成（确保重新开始时清理残留对象）
    public void 开始生成()
    {
        Debug.Log($"开始生成！当前状态：{生成状态}");
        if (!生成状态)
        {
            // 清理可能的残留对象（可选：根据需求决定是否保留旧对象）
            清理残留对象();

            生成状态 = true;
            协程对象 = StartCoroutine(水果生成());
        }
    }

    // 停止生成（仅停止新对象，已有对象自然销毁）
    public void 停止生成()
    {
        if (生成状态)
        {
            生成状态 = false;
            if (协程对象 != null)
            {
                StopCoroutine(协程对象);
                协程对象 = null;
            }
            Debug.Log("已停止生成新对象，等待现有对象自然销毁...");
        }
    }

    // 生成协程（核心逻辑不变，仅添加对象跟踪）
    private IEnumerator 水果生成()
    {
        yield return new WaitForSeconds(启动后等待生成时间);

        while (生成状态) // 使用生成状态控制循环，更直观
        {
            GameObject prefab = 土豆组[Random.Range(0, 土豆组.Length)]; // 修复原代码中Random.Range(4, ...)可能越界的问题

            if (Random.value < 炸弹概率 && 炸弹 != null)
            {
                prefab = 炸弹;
            }

            Vector3 position = new Vector3
            {
                x = Random.Range(生成区域.bounds.min.x, 生成区域.bounds.max.x),
                y = Random.Range(生成区域.bounds.min.y, 生成区域.bounds.max.y),
                z = Random.Range(生成区域.bounds.min.z, 生成区域.bounds.max.z)
            };

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(最小生成角度, 最大生成角度));
            GameObject fruit = Instantiate(prefab, position, rotation);

            // 跟踪生成的对象
            已生成对象列表.Add(fruit);
            // 注册销毁回调，移除跟踪
            Destroy(fruit, 生命周期);
            StartCoroutine(等待对象销毁(fruit));

            Rigidbody rb = fruit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float force = Random.Range(最小生成力度, 最大生成力度);
                rb.AddForce(fruit.transform.up * force, ForceMode.Impulse);

                float torque = Random.Range(最小自旋力度, 最大自旋力度);
                Vector3 torqueDirection = 获取随机旋转方向();
                rb.AddTorque(torqueDirection * torque, ForceMode.Impulse);
            }

            // 播放音效
            //土豆忍者管理类.土豆忍者管理.音频管理.播放("发射土豆");

            yield return new WaitForSeconds(Random.Range(最小生成间隔, 最大生成间隔));
        }
    }

    // 等待对象销毁并从列表中移除
    private IEnumerator 等待对象销毁(GameObject obj)
    {
        while (obj != null)
        {
            yield return null;
        }
        已生成对象列表.Remove(obj);
        Debug.Log($"对象已销毁，剩余对象数：{已生成对象列表.Count}");
    }

    // 清理残留对象（重新开始时调用，可选）
    private void 清理残留对象()
    {
        foreach (var obj in 已生成对象列表)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        已生成对象列表.Clear();
    }

    // 检查是否所有对象都已销毁
    public bool 所有对象已销毁()
    {
        return 已生成对象列表.Count == 0;
    }

    // 旋转方向逻辑（不变）
    private Vector3 获取随机旋转方向()
    {
        switch (当前旋转模式)
        {
            case 旋转模式.主要Z轴:
                return new Vector3(0f, 0f, Random.Range(-1f, 1f)).normalized;
            case 旋转模式.自定义轴:
                return 主要旋转轴.normalized * (Random.value > 0.5f ? 1f : -1f);
            case 旋转模式.完全随机:
            default:
                return new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                ).normalized;
        }
    }
}








//public class 土豆生成类 : MonoBehaviour
//{

//    private Collider 生成区域; // spawnArea

//    private Coroutine 协程对象;

//    private bool 生成状态 = false;

//    public GameObject[] 土豆组;    // fruitPrefabs
//    public GameObject 炸弹;        // bombPrefab

//    [Range(0f, 1f)]
//    public float 炸弹概率 = 0.05f;  // bombChance
//    // 生成对象的最小和最大时间间隔（单位：秒）
//    public float 最小生成间隔 = 0.25f;  // minSpawnDelay
//    public float 最大生成间隔 = 1f;     // maxSpawnDelay

//    // 生成对象的发射角度范围（相对于正上方） 
//    public float 最小生成角度 = -15f;   // minAngle 
//    public float 最大生成角度 = 15f;    // maxAngle 

//    //生成对象时施加的力度范围
//    public float 最小生成力度 = 18f;   // minForce 
//    public float 最大生成力度 = 22f;   // maxForce 

//    public float 最小自旋力度 = 1f;
//    public float 最大自旋力度 = 10f;


//    public float 生命周期 = 5f; // maxLifetime

//    public float 启动后等待生成时间 = 2f;


//    // 增强版旋转方向控制
//    public enum 旋转模式
//    {
//        完全随机,
//        主要Z轴,
//        自定义轴
//    }
//    [Header("旋转设置")]
//    public 旋转模式 当前旋转模式 = 旋转模式.完全随机;
//    public Vector3 主要旋转轴 = Vector3.forward; // 当选择自定义轴时使用



//    private void Awake()
//    {
//        生成区域 = GetComponent<Collider>();
//    }

//    private void OnEnable()
//    {
//        StartCoroutine(水果生成());
//        //StopAllCoroutines();
//    }

//    private void OnDisable()
//    {
//        StopAllCoroutines();
//    }

//    // 开始生成水果
//    public void 开始生成()
//    {
//        Debug.Log($"开始生成水果 开始生成！{ 生成状态 }");
//        if (!生成状态)
//        {
//            生成状态 = true;
//            协程对象 = StartCoroutine(水果生成());
//        }
//    }

//    // 停止生成水果
//    public void 停止生成()
//    {
//        if (生成状态)
//        {
//            生成状态 = false;
//            if (协程对象 != null)
//            {
//                StopCoroutine(协程对象);
//                协程对象 = null;
//            }
//        }
//    }

//    private Vector3 获取随机旋转方向()
//    {
//        switch (当前旋转模式)
//        {
//            case 旋转模式.主要Z轴:
//                return new Vector3(0f, 0f, Random.Range(-1f, 1f)).normalized;

//            case 旋转模式.自定义轴:
//                return 主要旋转轴.normalized * (Random.value > 0.5f ? 1f : -1f);

//            case 旋转模式.完全随机:
//            default:
//                return new Vector3(
//                    Random.Range(-1f, 1f),
//                    Random.Range(-1f, 1f),
//                    Random.Range(-1f, 1f)
//                ).normalized;
//        }
//    }

//    private IEnumerator 水果生成()
//    {

//        //Debug.Log($"水果生成 : {enabled}");

//        yield return new WaitForSeconds(启动后等待生成时间);

//        //Debug.Log($"水果生成 while : {enabled}");
//        // 2. 主生成循环
//        while (enabled)
//        {
//            // 3. 随机选择水果预制体
//            GameObject prefab = 土豆组[Random.Range(4, 土豆组.Length)];

//            //prefab = 土豆组[6];
//            //prefab = 土豆组[6];


//            // 4. 随机决定是否替换为炸弹
//            if (Random.value < 炸弹概率)
//            {
//                prefab = 炸弹;
//            }

//            //Debug.Log($"土豆组 : {prefab.name}");
//            // 5. 计算随机生成位置
//            Vector3 position = new Vector3
//            {
//                x = Random.Range(生成区域.bounds.min.x, 生成区域.bounds.max.x),
//                y = Random.Range(生成区域.bounds.min.y, 生成区域.bounds.max.y),
//                z = Random.Range(生成区域.bounds.min.z, 生成区域.bounds.max.z)
//            };

//            // 6. 计算随机旋转角度
//            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(最小生成角度, 最大生成角度));

//            //position.z = 0.0;

//            // 7. 实例化对象
//            GameObject fruit = Instantiate(prefab, position, rotation);

//            // 8. 设置自动销毁时间
//            Destroy(fruit, 生命周期);

//            // 获取刚体组件
//            Rigidbody rb = fruit.GetComponent<Rigidbody>();


//            // 9. 施加随机力
//            float force = Random.Range(最小生成力度, 最大生成力度);
//            //fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

//            rb.AddForce(fruit.transform.up * force, ForceMode.Impulse);

//            //// 添加自旋扭矩（让物体旋转）
//            //float torque = Random.Range(最小自旋力度, 最大自旋力度);
//            //Vector3 torqueDirection = new Vector3(
//            //    Random.Range(-1f, 1f),
//            //    Random.Range(-1f, 1f),
//            //    Random.Range(-1f, 1f)
//            //).normalized;

//            //rb.AddTorque(torqueDirection * torque, ForceMode.Impulse);

//            // 添加自旋扭矩（让物体旋转）
//            float torque = Random.Range(最小自旋力度, 最大自旋力度);
//            Vector3 torqueDirection = 获取随机旋转方向(); // 使用新的随机方向函数

//            rb.AddTorque(torqueDirection * torque, ForceMode.Impulse);

//            土豆忍者管理类.土豆忍者管理.音频管理.播放("发射土豆");
//            // 10. 等待随机时间后生成下一个
//            yield return new WaitForSeconds(Random.Range(最小生成间隔, 最大生成间隔));
//        }


//    }


//}
