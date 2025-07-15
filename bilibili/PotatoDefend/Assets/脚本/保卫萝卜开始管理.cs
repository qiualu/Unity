using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 确保添加这行
 


public class 保卫萝卜开始管理 : MonoBehaviour
{

    public static 保卫萝卜开始管理 游戏管理 { get; private set; }
    public int 关卡状态 { get; private set; } = 1;


    关卡加载类 关卡加载实例;

    //private 关卡加载类 关卡加载;

    [Header("界面引用")]
    [Tooltip("开始界面按钮对象")]
    public Button 开始按钮; // 在Inspector中分配的按钮
    //[Tooltip("开始界面图片对象")]
    public GameObject 开始界面;



    private void Awake()
    {
        // 确保全局唯一实例
        if (游戏管理 != null)
        {
            Destroy(gameObject);
            return;
        }
        游戏管理 = this;
        DontDestroyOnLoad(gameObject); // 跨场景保留 DontDestroyOnLoad(gameObject)

        //关卡加载.关卡加载(); 
        //关卡加载类.关卡加载实例.关卡加载();

        //关卡加载实例 = new 关卡加载类();
        //关卡加载实例.关卡加载();
    }
    private void OnDestroy()
    {
        // 清理静态引用
        if (游戏管理 == this)
        {
            游戏管理 = null;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
         
        // 使用解析后的数据初始化游戏

        开始按钮.onClick.AddListener(按钮开始游戏);
        开始按钮.gameObject.SetActive(true);
        开始界面.SetActive(true);

    }



    public void 按钮开始游戏()
    {
        Debug.Log("按钮开始游戏！");
        Time.timeScale = 1f;  // 重置游戏时间流速
        开始按钮.gameObject.SetActive(false);
        //GameObject 开始界面 = GameObject.Find("开始界面");
        //开始界面.SetActive(false); // 隐藏对象
        //GameObject 开始界面= GameObject.Find("背景/Canvas/开始界面");
        开始界面.SetActive(false);

        // 启动协程
        //StartCoroutine(DelayedExecution()); 
        关卡加载类.关卡加载实例.关卡加载();
    }


    //IEnumerator DelayedExecution()
    //{
    //    // 等待5秒
    //    yield return new WaitForSeconds(5f);

    //    Debug.Log("协程延迟5秒后执行");
    //    // 执行延迟后的操作
    //    开始按钮.gameObject.SetActive(true);
    //    //GameObject 开始界面 = GameObject.Find("开始界面");
    //    //开始界面.SetActive(true); // 隐藏对象

    //    //GameObject 开始界面 = GameObject.Find("背景/Canvas/开始界面");
    //    开始界面.SetActive(true);


    //}



    // Update is called once per frame
    void Update()
    {



        //// 检测鼠标左键按下
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("鼠标左键被按下");
        //    // 获取鼠标在屏幕空间的位置（像素坐标）
        //    Vector3 mousePosition = Input.mousePosition;
        //    Debug.Log("鼠标位置: " + mousePosition);

        //    // 将屏幕坐标转换为世界坐标（适用于2D游戏）
        //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //    worldPosition.z = 0; // 确保z轴为0（2D平面）
        //    Debug.Log("鼠标在世界坐标中的位置: " + worldPosition);

        //}

        //// 检测鼠标左键释放
        //if (Input.GetMouseButtonUp(0))
        //{
        //    Debug.Log("鼠标左键被释放");
        //}

        ////// 检测鼠标左键持续按住
        ////if (Input.GetMouseButton(0))
        ////{
        ////    Debug.Log("鼠标左键正在被按住");
        ////}


    }
}
