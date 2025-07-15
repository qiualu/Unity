using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;


[DefaultExecutionOrder(-1)]
public class 切土豆管理 : MonoBehaviour
{

    public static 切土豆管理 Instance { get; private set; }

    //[SerializeField] private 刀片类 刀片;
    //[SerializeField] private Spawner spawner;
    //[SerializeField] private Text scoreText;
    //[SerializeField] private Image fadeImage;

    [SerializeField] private 刀片类 刀片;
    [SerializeField] private 切土豆生成区 生成水果;
    //[SerializeField] private Text 分数;
    [SerializeField] private TextMeshProUGUI 分数; // 使用 TMP 类型
    [SerializeField] private Image 爆炸白屏;

    public Button 开始按钮; // 在编辑器中将按钮拖入此字段

    public int 计分 { get; private set; } = 0;


    private int frameCounter = 0;
    private const int targetFrames = 3000; // 60帧=1秒(假设60FPS)


    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }




 
    void Start()
    {
        Debug.Log("土豆切割系统已启动"); // 修改为Unity专用日志输出

        //刀片 = GetComponent<刀片类>();
        //if (刀片 == null)
        //{
        //    Debug.LogError("未找到刀片组件！");
        //    enabled = false; // 禁用脚本
        //}
        开始游戏();
        开始按钮.onClick.AddListener(按钮开始游戏);

        生成水果.停止生成();


        爆炸白屏.color = Color.clear;
        // 显示对象
        爆炸白屏.gameObject.SetActive(true);

    }

    private void 开始游戏()
    {
        Time.timeScale = 1f;

        清空场景();

        刀片.enabled = true;
        生成水果.enabled = true;

        计分 = 0;
        分数.text = 计分.ToString();

        生成水果.开始生成();

    }

    private void 清空场景()
    {
        水果[] 水果实例 = FindObjectsOfType<水果>();

        foreach (水果 fruit in 水果实例)
        {
            Destroy(fruit.gameObject);
        }

        炸弹[] 炸弹实例 = FindObjectsOfType<炸弹>();

        foreach (炸弹 bomb in 炸弹实例)
        {
            Destroy(bomb.gameObject);
        }
    }

    public void 按钮开始游戏()
    {
        //Debug.Log("土豆被切了！");
        // 您的逻辑代码
        开始游戏();

        开始按钮.gameObject.SetActive(false);

        //StartCoroutine(DelayedAction());

    }


    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(5f);
        YourFunction(); // 5秒后执行的目标函数
    }

    void YourFunction()
    {
        Debug.Log("5秒后触发的函数");
        //开始按钮.gameObject.SetActive(true);
        StartCoroutine(爆炸处理());
    }


    public void 计分函数(int 得分)
    {
        计分 += 得分;
        分数.text = 计分.ToString();

        float 存储计分 = PlayerPrefs.GetFloat("存储计分", 0);

        if (计分 > 存储计分)
        {
            存储计分 = 计分;
            PlayerPrefs.SetFloat("存储计分", 存储计分);
        }
    }

    public void 爆炸结束游戏()
    {
        刀片.enabled = false;
        生成水果.enabled = false;

        StartCoroutine(爆炸处理());
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
        开始按钮.gameObject.SetActive(true);

        elapsed = 0f;

        // Fade back in
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            爆炸白屏.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
}
