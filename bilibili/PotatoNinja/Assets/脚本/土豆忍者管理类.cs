using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class 土豆忍者管理类 : MonoBehaviour
{

    // 新增：暂停状态（0=暂停，1=运行）
    public  int 游戏状态 = 1; // 默认为运行状态
    public  int 游戏模式 = 1; // 默认为运行状态

    public static 土豆忍者管理类 土豆忍者管理 { get; private set; }

    [SerializeField] private 刀痕迹类 刀痕迹;
    [SerializeField] private 土豆生成类 土豆生成;
    //[SerializeField] private Text 分数;
    [SerializeField] private TextMeshProUGUI 分数; // 使用 TMP 类型
    [SerializeField] private TextMeshProUGUI 分数阴影; // 使用 TMP 类型  土豆忍者管理类.土豆忍者管理.bzx
    [SerializeField] private Image 爆炸白屏;


    // 声明变量用于保存初始坐标
    private Vector3 分数初始世界坐标;
    private Vector3 分数阴影初始世界坐标;
    private Vector3 分数初始局部坐标;
    private Vector3 分数阴影初始局部坐标;


    public Button 开始按钮; // 在编辑器中将按钮拖入此字段
    public Button 计时按钮; // 在编辑器中将按钮拖入此字段
    public Button 结束按钮; // 在编辑器中将按钮拖入此字段


    public GameObject 开始背景;
    public GameObject 游戏背景;
    public GameObject 结束背景;
    public GameObject 倒计时;



    public GameObject 生命值1;
    public GameObject 生命值2;
    public GameObject 生命值3;
    public GameObject 生命总画面;


    public int bzx = -1920;
    public int bzy = -1080;

    public int 生命值 = 3; // 暂停秒数


    public float 切割力 = 5f; // 在编辑器中将按钮拖入此字段

    public int 计分 { get; private set; } = 0;

 
    public int 连击状态 = 0;
    public int 连击加分 = 1;


    public float 暂停时长 = 2f; // 暂停秒数
    private SphereCollider 炸弹碰撞体; 
    // 存储需要暂停的脚本列表
    private MonoBehaviour[] 可暂停脚本;


    private void Awake()
    {
        if (土豆忍者管理 != null)
        {
            Destroy(gameObject);
            return;
        }
        土豆忍者管理 = this;
        DontDestroyOnLoad(gameObject); // 可选：跨场景保留


        // 备份世界坐标
        分数初始世界坐标 = 分数.transform.position;
        分数阴影初始世界坐标 = 分数阴影.transform.position;

        // 备份局部坐标（相对于父物体）
        分数初始局部坐标 = 分数.transform.localPosition;
        分数阴影初始局部坐标 = 分数阴影.transform.localPosition;


        开始背景.SetActive(true);
        游戏背景.SetActive(false);
        结束背景.SetActive(false);

        开始按钮.gameObject.SetActive(true);
        计时按钮.gameObject.SetActive(true);
        //结束按钮.gameObject.SetActive(false);

        生命值1.SetActive(true);
        生命值2.SetActive(true);
        生命值3.SetActive(true);
        生命总画面.SetActive(false);

        倒计时.gameObject.SetActive(false);

        连击状态 = 0;
        连击加分 = 1;

}

    private void OnDestroy()
    {
        if (土豆忍者管理 == this)
        {
            土豆忍者管理 = null;
        }
    }
 


    public void 计分函数(int 得分)
    {
        计分 += 得分; 
        if (连击状态 == 0) { 
            连击状态 = 1;
            连击加分 = 1;
        }
        else
        {
            if (连击加分 < 9) { 
                连击加分 += 1;
            }
        }
        if (得分 < 0) {
            连击状态 = 0;
            连击加分 = 1;
        }


        分数.text = 计分.ToString();
        分数阴影.text = 计分.ToString();

        float 存储计分 = PlayerPrefs.GetFloat("存储计分", 0);

        if (计分 > 存储计分)
        {
            存储计分 = 计分;
            PlayerPrefs.SetFloat("存储计分", 存储计分);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        开始按钮.onClick.AddListener(按钮开始游戏1);
        计时按钮.onClick.AddListener(按钮开始游戏2);

        爆炸白屏.color = Color.clear;
        // 显示对象
        分数.gameObject.SetActive(false);
        分数阴影.gameObject.SetActive(false);

        爆炸白屏.gameObject.SetActive(true);


        刀痕迹.enabled = false;
        土豆生成.enabled = false;
    }
            
    public void 按钮开始游戏()
    {
        复原局部坐标();
        Debug.Log("按钮开始游戏！");
        Time.timeScale = 1f;  // 重置游戏时间流速
        // 您的逻辑代码
        //开始游戏();

        开始按钮.gameObject.SetActive(false);
        计时按钮.gameObject.SetActive(false);
        结束按钮.gameObject.SetActive(false);

        //StartCoroutine(DelayedAction());

        分数.gameObject.SetActive(true);
        分数阴影.gameObject.SetActive(true);

        计分 = 0;
        刀痕迹.enabled = true;
        土豆生成.enabled = true;
        //土豆生成.开始生成();

        //开始背景.SetActive(false);
        游戏背景.SetActive(true);
        结束背景.SetActive(false);

        生命值1.SetActive(true);
        生命值2.SetActive(true);
        生命值3.SetActive(true);
        生命总画面.SetActive(true);

        //游戏模式 = 1;


    }
    public void 按钮开始游戏1()
    {
        倒计时.gameObject.SetActive(false);
        游戏模式 = 1;
        按钮开始游戏();


    }
    public void 按钮开始游戏2()
    {
        倒计时.gameObject.SetActive(true);
        游戏模式 = 2;
        按钮开始游戏();
        倒计时 倒计时组件 = 倒计时.GetComponent<倒计时>();
        if (倒计时组件 != null)
        {
            倒计时组件.开始倒计时(); // 触发倒计时开始
            //Debug.Log("已启动倒计时");
        }
        else
        {
            游戏模式 = 1;
            Debug.LogError("倒计时对象上未找到'倒计时'脚本组件！", this);
        }


        生命总画面.SetActive(false);
    }




    public void 爆炸结束游戏()
    {
        Debug.Log($"*********  结束游戏 {游戏模式}");
        if (生命值 == 3)
        {
            生命值 = 2;
            生命值1.SetActive(true);
            生命值2.SetActive(true);
            生命值3.SetActive(false);
        }
        else if (生命值 == 2)
        {
            生命值 = 1;
            生命值1.SetActive(true);
            生命值2.SetActive(false);
            生命值3.SetActive(false);
        }
        else {
            生命值1.SetActive(false);
            生命值2.SetActive(false);
            生命值3.SetActive(false);

            if (游戏模式 == 1) {
                刀痕迹.enabled = false;
                土豆生成.enabled = false;
                土豆生成.停止生成();
                //StartCoroutine(爆炸处理());
                StartCoroutine(爆炸处理2()); 
            } 
        }
         

    }
    public void 爆炸结束游戏_倒计时()
    {
  
        if (游戏模式 == 2)
        {
            刀痕迹.enabled = false;
            土豆生成.enabled = false;
            土豆生成.停止生成();
            //StartCoroutine(爆炸处理());
            StartCoroutine(爆炸处理2());
        }

    }




    private void 清空场景()
    {
        Debug.Log("清空场景！");
        土豆类[] 土豆 = FindObjectsOfType<土豆类>();

        foreach (土豆类 fruit in 土豆)
        {
            Destroy(fruit.gameObject);
        }

        炸弹类[] 炸弹实例 = FindObjectsOfType<炸弹类>();

        foreach (炸弹类 bomb in 炸弹实例)
        {
            Destroy(bomb.gameObject);
        }
    }



    private IEnumerator 爆炸处理()
    {
        Debug.Log("爆炸处理");
        float elapsed = 0f;
        float duration = 0.5f;

        // Fade to white
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            爆炸白屏.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        清空场景();

        yield return new WaitForSecondsRealtime(1f);

        // NewGame();
        //开始按钮.gameObject.SetActive(true);
        //计时按钮.gameObject.SetActive(true);
        //结束按钮.gameObject.SetActive(true);


        Debug.Log("等待后执行 5f, 要执行的函数_5秒钟恢复！");
        StartCoroutine(等待后执行(5f, 要执行的函数_5秒钟恢复));

        elapsed = 0f;

        // Fade back in
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            爆炸白屏.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        设置局部位置();
        //开始背景.SetActive(false);
        游戏背景.SetActive(false);
        结束背景.SetActive(true);

    }


    private IEnumerator 爆炸处理2()
    {
        Debug.Log("爆炸处理2");
        float 过渡时长 = 1.5f; // 淡入淡出总时长
        float 已用时间 = 0f;

        // 确保背景初始状态正确
        游戏背景.SetActive(true);
        结束背景.SetActive(true);

        // 获取背景的透明度组件（假设使用Image或RawImage）
        CanvasGroup 游戏背景组 = 游戏背景.GetComponent<CanvasGroup>();
        CanvasGroup 结束背景组 = 结束背景.GetComponent<CanvasGroup>();

        // 其他后续操作
        设置局部位置();

        // 初始化组件（如果没有则添加）
        if (游戏背景组 == null)
        {
            游戏背景组 = 游戏背景.AddComponent<CanvasGroup>();
            游戏背景组.alpha = 1f; // 游戏背景初始完全显示
            游戏背景组.interactable = true;
            游戏背景组.blocksRaycasts = true;
        }

        if (结束背景组 == null)
        {
            结束背景组 = 结束背景.AddComponent<CanvasGroup>();
            结束背景组.alpha = 0f; // 结束背景初始完全透明
            结束背景组.interactable = false;
            结束背景组.blocksRaycasts = false;
        }

        // 同步过渡：游戏背景淡出，结束背景淡入
        while (已用时间 < 过渡时长)
        {
            float 进度 = Mathf.Clamp01(已用时间 / 过渡时长);

            // 游戏背景透明度从1→0（淡出）
            游戏背景组.alpha = 1f - 进度;
            // 结束背景透明度从0→1（淡入）
            结束背景组.alpha = 进度;

            // 时间缩放效果保留（可选）
            Time.timeScale = 1f - 进度;

            已用时间 += Time.unscaledDeltaTime;
            yield return null;
        }

        // 确保过渡完成
        游戏背景组.alpha = 0f;
        结束背景组.alpha = 1f;
        游戏背景.SetActive(false); // 完全隐藏游戏背景
        结束背景组.interactable = true;
        结束背景组.blocksRaycasts = true;

        // 清空场景
        清空场景();

        // 等待1秒
        yield return new WaitForSecondsRealtime(1f);

        Debug.Log("等待后执行 5f, 要执行的函数_5秒钟恢复！");
        StartCoroutine(等待后执行(5f, 要执行的函数_5秒钟恢复));

        // 重置时间缩放
        Time.timeScale = 1f;

        //开始背景.SetActive(false);
    }



    // 设置世界坐标位置
    // 设置世界坐标位置
    public void 设置世界位置()
    {
        if (分数 != null)
        {
            分数.transform.position = new Vector3(100, 100, 0);
            分数阴影.transform.position = new Vector3(110, 110, 0);

        }
    }

    // 设置局部坐标位置（相对于父物体）
    public void 设置局部位置()
    {
        if (分数 != null)
        {
            分数.transform.localPosition = new Vector3(100, 100, 0);
            分数阴影.transform.localPosition = new Vector3(110, 90, 0);
        }
    }

    // 复原到初始世界坐标
    public void 复原世界坐标()
    {
        if (分数 != null)
        {
            分数.transform.position = 分数初始世界坐标;
            分数阴影.transform.position = 分数阴影初始世界坐标;
        }
    }

    // 复原到初始局部坐标
    public void 复原局部坐标()
    {
        if (分数 != null)
        {
            分数.transform.localPosition = 分数初始局部坐标;
            分数阴影.transform.localPosition = 分数阴影初始局部坐标;
        }
    }

    /// <summary>
    /// 这是要被延迟执行的示例函数
    /// </summary>
    private void 要执行的函数_5秒钟恢复()
    {
        Debug.Log("5秒到了，执行指定操作！");
        // 在这里添加需要延迟执行的代码
        开始按钮.gameObject.SetActive(true);
        计时按钮.gameObject.SetActive(true);
        //结束按钮.gameObject.SetActive(true);
        分数.gameObject.SetActive(false);
        分数阴影.gameObject.SetActive(false);
        生命总画面.SetActive(false);

        开始背景.SetActive(true);
        游戏背景.SetActive(false);
        结束背景.SetActive(false);

    }

    /// <summary>
    /// 等待指定时间后执行函数
    /// </summary>
    /// <param name="等待时间">等待的秒数</param>
    /// <param name="要执行的函数">延迟后执行的函数</param>
    /// <returns></returns>
    IEnumerator 等待后执行(float 等待时间, System.Action 要执行的函数)
    {
        // 等待指定秒数
        yield return new WaitForSecondsRealtime(等待时间);

        // 确保函数不为空再执行
        if (要执行的函数 != null)
        {
            要执行的函数();
        }
    }
    /// <summary>
    /// 外部调用接口：5秒后执行指定函数
    /// </summary>
    /// <param name="目标函数">要延迟执行的函数</param>
    public void 五秒后执行(System.Action 目标函数)
    {
        StartCoroutine(等待后执行(5f, 目标函数));
    }


    private void Update()
    {
        // 检测鼠标左键按下（0代表左键，1右键，2中键）
        if (Input.GetMouseButtonDown(0))
        {
 
        }

        // 检测鼠标左键抬起
        if (Input.GetMouseButtonUp(0))
        { 
            连击状态 = 0;
            连击加分 = 1;
        }

 
    }



}
