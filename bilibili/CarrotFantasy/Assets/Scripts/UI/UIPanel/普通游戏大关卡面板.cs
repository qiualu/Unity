using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 普通游戏大关卡面板 : BasePanel
{
    public Transform bigLevelContentTrans;//滚动视图的content
    public int bigLevelPageCount;//大关卡总数
    private 翻页控制类 翻页控制实例;
    private PlayerManager playerManager;
    private Transform[] bigLevelPage;//大关卡按钮数组

    private bool hasRigisterEvent;

 

    protected override void Awake()
    {
        base.Awake();
        playerManager = mUIFacade.mPlayerManager;
        bigLevelPage = new Transform[bigLevelPageCount];
        翻页控制实例 = transform.Find("Scroll View").GetComponent<翻页控制类>();
 

        // 初始化大关卡按钮
        for (int i = 0; i < bigLevelPageCount; i++)
        {
            bigLevelPage[i] = bigLevelContentTrans.GetChild(i); 
            BindLevelButton(bigLevelPage[i], i + 1); // i+1 作为大关卡ID（1-3）
        }
         
        hasRigisterEvent = true;

        

    }
 

 
    // 绑定关卡按钮点击事件
    private void BindLevelButton(Transform buttonTrans, int bigLevelID)
    {
        Button levelButton = buttonTrans.GetComponent<Button>();
        if (levelButton == null) return;

        // 激活按钮交互
        levelButton.interactable = true;
        // 隐藏锁图标，显示关卡信息（根据实际UI结构调整）
        buttonTrans.Find("Img_Lock")?.gameObject.SetActive(false);
        buttonTrans.Find("Img_Page")?.gameObject.SetActive(true);

        // 绑定点击事件：触发自定义函数
        levelButton.onClick.AddListener(() =>
        {
            // 播放按钮音效（不需要可删除）
            mUIFacade?.PlayButtonAudioClip();
            // 调用自定义处理函数，传入关卡ID
            OnLevelButtonClick(bigLevelID);
        });
    }
    // 自定义关卡按钮点击处理函数（核心逻辑在这里实现）
    private void OnLevelButtonClick(int levelID)
    {
        // 这里写你自己的逻辑，例如：
        Debug.Log($" 普通游戏大关卡面板 :{levelID} ");


        mUIFacade.PlayButtonAudioClip();
        //离开大关卡页面
        //mUIFacade.currentScenePanelDict[StringManager.GameNormalBigLevelPanel].ExitPanel();
        mUIFacade.currentScenePanelDict[资源名字.普通游戏大关卡面板].ExitPanel();
        //进入小关卡
        //GameNormalLevelPanel gameNormalLevelPanel = mUIFacade.currentScenePanelDict[StringManager.GameNormalLevelPanel] as GameNormalLevelPanel;
        普通游戏小关卡面板 gameNormalLevelPanel = mUIFacade.currentScenePanelDict[资源名字.普通游戏小关卡面板] as 普通游戏小关卡面板;
        gameNormalLevelPanel.ToThisPanel(levelID);
        //设置所在页面
        //GameNormalOptionPanel gameNormalOptionPanel = mUIFacade.currentScenePanelDict[StringManager.GameNormalOptionPanel] as GameNormalOptionPanel;
        普通游戏选项面板 gameNormalOptionPanel = mUIFacade.currentScenePanelDict[资源名字.普通游戏选项面板] as 普通游戏选项面板;
        gameNormalOptionPanel.isInBigLevelPanel = false;

    
    }


   


    //进入退出面板
    public override void EnterPanel()
    {
        base.EnterPanel();
        翻页控制实例.Init();
        gameObject.SetActive(true);
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        gameObject.SetActive(false);
    }

 

    //翻页按钮方法  
    public void ToNextPage()  // 向右
    { 
        mUIFacade.PlayButtonAudioClip();
        翻页控制实例.ToNextPage();
    }

    public void ToLastPage()  // 向左
    {
        mUIFacade.PlayButtonAudioClip();
        翻页控制实例.ToLastPage();
    }

     
    //翻页按钮方法  
    public void 翻页向右按钮()  
    {

        Debug.Log($"翻页向右按钮    ");

        mUIFacade.PlayButtonAudioClip();
        翻页控制实例.ToNextPage();
    }

    public void 翻页向左按钮() 
    {

        Debug.Log($"翻页向左按钮    ");

        mUIFacade.PlayButtonAudioClip();
        翻页控制实例.ToLastPage();
    }



}
