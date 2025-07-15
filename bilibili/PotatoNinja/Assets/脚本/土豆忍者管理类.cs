using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class 土豆忍者管理类 : MonoBehaviour
{

    public static 土豆忍者管理类 土豆忍者管理 { get; private set; }

    [SerializeField] private 刀痕迹类 刀痕迹;
    [SerializeField] private 土豆生成类 土豆生成;
    //[SerializeField] private Text 分数;
    [SerializeField] private TextMeshProUGUI 分数; // 使用 TMP 类型
    [SerializeField] private Image 爆炸白屏;

    public Button 开始按钮; // 在编辑器中将按钮拖入此字段
    public float 切割力 = 5f; // 在编辑器中将按钮拖入此字段

    public int 计分 { get; private set; } = 0;

    private void Awake()
    {
        if (土豆忍者管理 != null)
        {
            Destroy(gameObject);
            return;
        }
        土豆忍者管理 = this;
        DontDestroyOnLoad(gameObject); // 可选：跨场景保留
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
        分数.text = 计分.ToString();

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

        开始按钮.onClick.AddListener(按钮开始游戏);

        爆炸白屏.color = Color.clear;
        // 显示对象
        分数.gameObject.SetActive(false);
        爆炸白屏.gameObject.SetActive(true);


        刀痕迹.enabled = false;
        土豆生成.enabled = false;
    }

    public void 按钮开始游戏()
    {
        Debug.Log("按钮开始游戏！");
        Time.timeScale = 1f;  // 重置游戏时间流速
        // 您的逻辑代码
        //开始游戏();

        开始按钮.gameObject.SetActive(false);

        //StartCoroutine(DelayedAction());
        
        分数.gameObject.SetActive(true);

        计分 = 0;
        刀痕迹.enabled = true;
        土豆生成.enabled = true;
        //土豆生成.开始生成();

    }

    public void 爆炸结束游戏()
    {
        刀痕迹.enabled = false;
        土豆生成.enabled = false;

        //土豆生成.停止生成();
        StartCoroutine(爆炸处理());
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
