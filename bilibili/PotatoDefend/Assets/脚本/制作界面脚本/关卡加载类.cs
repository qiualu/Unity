using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 关卡加载类 : MonoBehaviour
{
    public static 关卡加载类 关卡加载实例 { get; private set; }

    public 关卡数据 关卡 = new 关卡数据(); //  = 关卡解析器.解析关卡(关卡配置文件);

    // 新增：地板预制体（在Inspector中拖入你的地板预制体）
    [SerializeField] private GameObject 地板预制体;

    string 关卡配置文件 = "Levels/初级关卡";

    // 已点击地板列表
    private List<地板鼠标交互类> 已点击地板 = new List<地板鼠标交互类>();



    // 初始化
    private void Awake()
    {
        // 单例初始化逻辑
        if (关卡加载实例 != null)
        {
            Destroy(gameObject); // 销毁重复实例
            return;
        }

        关卡加载实例 = this;
        DontDestroyOnLoad(gameObject); // 跨场景保留
    }


    // Start is called before the first frame update
    public void 关卡加载()
    {
        
        Debug.Log($"关卡加载类 : 关卡加载！{保卫萝卜开始管理.游戏管理.关卡状态}");

        if (保卫萝卜开始管理.游戏管理.关卡状态 == 1) {
            Debug.Log($"关卡 1 加载 !");

            //关卡配置文件 = "初级关卡";
            关卡配置文件 = "Levels/初级关卡";
            Debug.Log($"关卡配置文件 加载 : {关卡配置文件}");
            // 初始化关卡字段（关键修改）
            关卡 = 关卡解析器.解析关卡(关卡配置文件);

            //关卡.名称 = "初级关卡";

            //关卡配置文件 = "Levels/初级关卡";
            //关卡.解析关卡(关卡配置文件);

            //Debug.Log("这是带有上下文的日志", this.gameObject);

            Debug.Log("保卫萝卜开始管理");
            //关卡数据 关卡 = 关卡解析器.解析关卡(关卡配置文件);
            // 使用解析后的数据初始化游戏
            Debug.Log($"加载关卡: {关卡.id} {关卡.名称}");
            Debug.Log($"路径点数量: {关卡.路径点列表.Count}");
            Debug.Log($"防御塔数量: {关卡.防御塔列表.Count}");
            Debug.Log($"波次数量: {关卡.波次列表.Count}");
            Debug.Log($"宽高： {关卡.基础变量.宽} {关卡.基础变量.高} 位置x: {关卡.基础变量.起点.x} 位置y: {关卡.基础变量.起点.y}");
            //铺满地板();
            //演示路图();
            地板加载XML铺设();
        }
    }

    private void 铺满地板()
    {
        if (地板预制体 == null)
        {
            Debug.LogError("请在Inspector中赋值地板预制体！");
            return;
        }

        // 1. 计算关卡需要的地板数量（根据关卡宽高和地板尺寸）
        //int 地板数量X = Mathf.CeilToInt(关卡.基础变量.宽 / 地板尺寸.x); // X方向数量
        //int 地板数量Y = Mathf.CeilToInt(关卡.基础变量.高 / 地板尺寸.y); // Y方向数量
        // 2. 计算起始位置（关卡起点为左下角，从起点开始铺）
        Vector3 起始位置 = new Vector3(
            关卡.基础变量.起点.x,
            关卡.基础变量.起点.y,
            10 // Z轴默认0，根据需要调整 
        );
  

        // 3. 循环实例化地板（从对象池获取）
        for (int x = 0; x < 关卡.基础变量.宽; x++)
        {
            for (int y = 0; y < 关卡.基础变量.高; y++)
            {
                // 计算当前地板的位置
                Vector3 地板位置 = 起始位置 + new Vector3(
                    x * 关卡.基础变量.地板大小,  // X方向偏移
                    y * 关卡.基础变量.地板大小,  // Y方向偏移
                    0
                );
                //Debug.Log($" 地板位置  对象: {地板位置} 位置:  {起始位置}");
                // 从对象池获取地板对象
                //GameObject 地板 = 对象池管理器.获取对象(地板预制体);
                // ✅ 补充位置和旋转参数（旋转用默认无旋转）

                //// 错误写法（直接通过类名调用非静态方法）
                //GameObject 地板 = 对象池管理器.获取对象(
                //    地板预制体,
                //    地板位置,
                //    Quaternion.identity
                //);

                // 正确写法（通过单例的实例对象调用）
                GameObject 地板 = 对象池管理器.实例.获取对象(
                    地板预制体,       // 预制体
                    地板位置,         // 位置
                    Quaternion.identity // 旋转
                );
                //地板.地板id = x * 14 + y;
                地板鼠标交互类 地板交互脚本 = 地板.GetComponent<地板鼠标交互类>();
                if (地板交互脚本 != null)
                {
                    // 给脚本里的地板id赋值
                    地板交互脚本.地板id = x * 14 + y;
                    地板交互脚本.网格坐标 = new Vector2Int(x, y); // x作为网格的x，y作为网格的y
                }
                else
                {
                    Debug.LogError("地板对象上没有挂载 地板鼠标交互类 脚本！");
                }


                if (地板 != null)
                {
                    //地板.transform.position = 地板位置; // 设置位置
                    地板.SetActive(true); // 激活对象
                }
            }
        }
    }

    private void 演示路图()
    {


        Vector3 起始位置 = new Vector3(
            关卡.基础变量.起点.x,
            关卡.基础变量.起点.y,
            10 // Z轴默认0，根据需要调整 
        );

        Debug.Log($" 起始位置 : {起始位置}  |-> {关卡.基础变量.起点.x} {关卡.基础变量.起点.y} ");

        foreach (var 路径点 in 关卡.路径点列表)
        {
            Debug.Log($" 路径点 : {路径点}  |-> {路径点.w} {路径点.h} ");

            int x = (int)路径点.w;  // 假设 路径点.w 是 float 类型（如 5.0f）
            int y = (int)路径点.h;  // 直接强制转换为 int

            // 计算当前地板的位置
            Vector3 地板位置 = 起始位置 + new Vector3(
                x * 关卡.基础变量.地板大小,  // X方向偏移
                y * 关卡.基础变量.地板大小,  // Y方向偏移
                0
            );
            //Debug.Log($" 地板位置  对象: {地板位置} 位置:  {起始位置}");
            // 从对象池获取地板对象
            //GameObject 地板 = 对象池管理器.获取对象(地板预制体);
            // ✅ 补充位置和旋转参数（旋转用默认无旋转）

            //// 错误写法（直接通过类名调用非静态方法）
            //GameObject 地板 = 对象池管理器.获取对象(
            //    地板预制体,
            //    地板位置,
            //    Quaternion.identity
            //);

            // 正确写法（通过单例的实例对象调用）
            GameObject 地板 = 对象池管理器.实例.获取对象(
                地板预制体,       // 预制体
                地板位置,         // 位置
                Quaternion.identity // 旋转
            );
            //地板.地板id = x * 14 + y;
            地板鼠标交互类 地板交互脚本 = 地板.GetComponent<地板鼠标交互类>();
            if (地板交互脚本 != null)
            {
                // 给脚本里的地板id赋值
                地板交互脚本.地板id = x * 14 + y;
                地板交互脚本.网格坐标 = new Vector2Int(x, y); // x作为网格的x，y作为网格的y
            }
            else
            {
                Debug.LogError("地板对象上没有挂载 地板鼠标交互类 脚本！");
            }


            if (地板 != null)
            {
                //地板.transform.position = 地板位置; // 设置位置
                地板.SetActive(true); // 激活对象
            }
        }

    }

    private void 地板加载XML铺设()
    {


        Vector3 起始位置 = new Vector3(
            关卡.基础变量.起点.x,
            关卡.基础变量.起点.y,
            10 // Z轴默认0，根据需要调整 
        );

        Debug.Log($" 起始位置 : {起始位置}  |-> {关卡.基础变量.起点.x} {关卡.基础变量.起点.y} ");

        foreach (var 路径点 in 关卡.基座位置)
        {
            Debug.Log($" 路径点 : {路径点}  |-> {路径点.w} {路径点.h} {路径点.名字} ");

            int x = (int)路径点.w;  // 假设 路径点.w 是 float 类型（如 5.0f）
            int y = (int)路径点.h;  // 直接强制转换为 int

            // 计算当前地板的位置
            Vector3 地板位置 = 起始位置 + new Vector3(
                x * 关卡.基础变量.地板大小,  // X方向偏移
                y * 关卡.基础变量.地板大小,  // Y方向偏移
                0
            );
            //Debug.Log($" 地板位置  对象: {地板位置} 位置:  {起始位置}");
            // 从对象池获取地板对象
            //GameObject 地板 = 对象池管理器.获取对象(地板预制体);
            // ✅ 补充位置和旋转参数（旋转用默认无旋转）

            //// 错误写法（直接通过类名调用非静态方法）
            //GameObject 地板 = 对象池管理器.获取对象(
            //    地板预制体,
            //    地板位置,
            //    Quaternion.identity
            //);

            // 正确写法（通过单例的实例对象调用）
            GameObject 地板 = 对象池管理器.实例.获取对象(
                地板预制体,       // 预制体
                地板位置,         // 位置
                Quaternion.identity // 旋转
            );
            // 设置地板对象的名字为路径点的名字
            地板.name = 路径点.名字;  // 核心修改：将路径点的名字赋值给对象名
            //地板.地板id = x * 14 + y;
            地板鼠标交互类 地板交互脚本 = 地板.GetComponent<地板鼠标交互类>();

            if (地板交互脚本 != null)
            {
                // 给脚本里的地板id赋值
                地板交互脚本.地板id = x * 14 + y;
                地板交互脚本.网格坐标 = new Vector2Int(x, y); // x作为网格的x，y作为网格的y
            }
            else
            {
                Debug.LogError("地板对象上没有挂载 地板鼠标交互类 脚本！");
            }


            if (地板 != null)
            {
                //地板.transform.position = 地板位置; // 设置位置
                地板.SetActive(true); // 激活对象
            }
        }
 
    }


    // 记录点击的地板
    public void 记录点击位置(地板鼠标交互类 地板)
    {
        // 如果列表中没有该地板，添加它
        if (!已点击地板.Contains(地板))
        {
            已点击地板.Add(地板);
            Debug.Log($"记录点击位置: 宽={地板.网格坐标.x}, 高={地板.网格坐标.y}");
        }
    }



    // 输出所有点击位置的宽高
    private void 输出所有点击位置的宽高()
    {
        if (已点击地板.Count == 0)
        {
            Debug.Log("没有记录的点击位置");
            return;
        }

        string 输出 = "已点击地板位置:\n";
        foreach (var 地板 in 已点击地板)
        {
            输出 += $"宽={地板.网格坐标.x}, 高={地板.网格坐标.y}\n";
        }
        Debug.Log(输出);
    }




    int printpp = 0;

    private void Update()
    {
        // 检测右键点击
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(" *********************************************  ");
            if (printpp == 0)
            {
                输出所有点击位置的宽高();
                printpp = 1;


                //地板id
            }
        }
    }







}












