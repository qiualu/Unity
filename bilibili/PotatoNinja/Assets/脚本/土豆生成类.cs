using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 土豆生成类 : MonoBehaviour
{

    private Collider 生成区域; // spawnArea

    private Coroutine 协程对象;

    private bool 生成状态 = false;

    public GameObject[] 土豆组;    // fruitPrefabs
    public GameObject 炸弹;        // bombPrefab

    [Range(0f, 1f)]
    public float 炸弹概率 = 0.05f;  // bombChance
    // 生成对象的最小和最大时间间隔（单位：秒）
    public float 最小生成间隔 = 0.25f;  // minSpawnDelay
    public float 最大生成间隔 = 1f;     // maxSpawnDelay

    // 生成对象的发射角度范围（相对于正上方） 
    public float 最小生成角度 = -15f;   // minAngle 
    public float 最大生成角度 = 15f;    // maxAngle 

    //生成对象时施加的力度范围
    public float 最小生成力度 = 18f;   // minForce 
    public float 最大生成力度 = 22f;   // maxForce 

    public float 最小自旋力度 = 1f;
    public float 最大自旋力度 = 10f;


    public float 生命周期 = 5f; // maxLifetime

    public float 启动后等待生成时间 = 2f;


    // 增强版旋转方向控制
    public enum 旋转模式
    {
        完全随机,
        主要Z轴,
        自定义轴
    }
    [Header("旋转设置")]
    public 旋转模式 当前旋转模式 = 旋转模式.完全随机;
    public Vector3 主要旋转轴 = Vector3.forward; // 当选择自定义轴时使用



    private void Awake()
    {
        生成区域 = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(水果生成());
        //StopAllCoroutines();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // 开始生成水果
    public void 开始生成()
    {
        Debug.Log($"开始生成水果 开始生成！{ 生成状态 }");
        if (!生成状态)
        {
            生成状态 = true;
            协程对象 = StartCoroutine(水果生成());
        }
    }

    // 停止生成水果
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
        }
    }

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

    private IEnumerator 水果生成()
    {

        //Debug.Log($"水果生成 : {enabled}");

        yield return new WaitForSeconds(启动后等待生成时间);

        //Debug.Log($"水果生成 while : {enabled}");
        // 2. 主生成循环
        while (enabled)
        {
            // 3. 随机选择水果预制体
            GameObject prefab = 土豆组[Random.Range(0, 土豆组.Length)];

            //prefab = 土豆组[0];


            // 4. 随机决定是否替换为炸弹
            if (Random.value < 炸弹概率)
            {
                prefab = 炸弹;
            }

            Debug.Log($"土豆组 : {prefab.name}");
            // 5. 计算随机生成位置
            Vector3 position = new Vector3
            {
                x = Random.Range(生成区域.bounds.min.x, 生成区域.bounds.max.x),
                y = Random.Range(生成区域.bounds.min.y, 生成区域.bounds.max.y),
                z = Random.Range(生成区域.bounds.min.z, 生成区域.bounds.max.z)
            };

            // 6. 计算随机旋转角度
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(最小生成角度, 最大生成角度));

            //position.z = 0.0;

            // 7. 实例化对象
            GameObject fruit = Instantiate(prefab, position, rotation);

            // 8. 设置自动销毁时间
            Destroy(fruit, 生命周期);

            // 获取刚体组件
            Rigidbody rb = fruit.GetComponent<Rigidbody>();


            // 9. 施加随机力
            float force = Random.Range(最小生成力度, 最大生成力度);
            //fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            rb.AddForce(fruit.transform.up * force, ForceMode.Impulse);

            //// 添加自旋扭矩（让物体旋转）
            //float torque = Random.Range(最小自旋力度, 最大自旋力度);
            //Vector3 torqueDirection = new Vector3(
            //    Random.Range(-1f, 1f),
            //    Random.Range(-1f, 1f),
            //    Random.Range(-1f, 1f)
            //).normalized;

            //rb.AddTorque(torqueDirection * torque, ForceMode.Impulse);

            // 添加自旋扭矩（让物体旋转）
            float torque = Random.Range(最小自旋力度, 最大自旋力度);
            Vector3 torqueDirection = 获取随机旋转方向(); // 使用新的随机方向函数

            rb.AddTorque(torqueDirection * torque, ForceMode.Impulse);


            // 10. 等待随机时间后生成下一个
            yield return new WaitForSeconds(Random.Range(最小生成间隔, 最大生成间隔));
        }


    }

 
}
