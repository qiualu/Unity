using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 添加这个命名空间，用于识别 Text、Button、Image 等UI组件

public class 宠物类 : MonoBehaviour
{

    public 宠物数据 宠物数据实例;  //public MonsterPetData monsterPetData; MonsterPetData

    private GameObject[] 宠物对象组; //private GameObject [] monsterLevelGo;// 宠物对应的三个等级的游戏物体

    public Sprite[] 按钮精灵数组; //public Sprite [] buttonSprites;//0. 可用 milk 1. 不可用 milk 2 3

    //// 鸡蛋阶段
    private GameObject 提示图片对象; //private GameObject img_InstructionGo;

    //// 幼崽阶段
    private GameObject 喂养面板对象; //private GameObject emp_FeedGo;
    private Text 牛奶文本; //private Text tex_milk;
    private Text 饼干文本; //private Text tex_cookie;
    private Button 牛奶按钮; //private Button btn_Milk;
    private Button 饼干按钮; //private Button btn_Cookie;
    private Image 牛奶按钮图片; //private Image img_btn_Milk;
    private Image 饼干按钮图片; //private Image img_btn_Cookie;


    // 成年阶段
    private GameObject 右侧对话图片对象; // 原 img_TalkRightGo
    private GameObject 左侧对话图片对象; // 原 img_TalkLeftGo

    public 宠物巢穴面板 宠物巢穴面板实例; // 原 monsterNestPanel


    private void Awake()
    {
        宠物对象组 = new GameObject[3];
        宠物对象组[0] = transform.Find("Emp_Egg").gameObject; // 鸡蛋阶段
        宠物对象组[1] = transform.Find("Emp_Baby").gameObject; // 幼崽阶段
        宠物对象组[2] = transform.Find("Emp_Normal").gameObject; // 成年阶段

        // 初始化鸡蛋阶段UI
        提示图片对象 = 宠物对象组[0].transform.Find("Img_Instruction").gameObject;
        提示图片对象.SetActive(false);

        // 初始化幼崽阶段UI
        喂养面板对象 = 宠物对象组[1].transform.Find("Emp_Feed").gameObject;
        喂养面板对象.SetActive(false);
        牛奶按钮 = 宠物对象组[1].transform.Find("Emp_Feed/Btn_Milk").GetComponent<Button>();
        牛奶按钮图片 = 宠物对象组[1].transform.Find("Emp_Feed/Btn_Milk").GetComponent<Image>();
        饼干按钮 = 宠物对象组[1].transform.Find("Emp_Feed/Btn_Cookie").GetComponent<Button>();
        饼干按钮图片 = 宠物对象组[1].transform.Find("Emp_Feed/Btn_Cookie").GetComponent<Image>();
        牛奶文本 = 宠物对象组[1].transform.Find("Emp_Feed/Btn_Milk/Text").GetComponent<Text>();
        饼干文本 = 宠物对象组[1].transform.Find("Emp_Feed/Btn_Cookie/Text").GetComponent<Text>();

        // 初始化成年阶段UI
        左侧对话图片对象 = transform.Find("Img_TalkLeft").gameObject;
        右侧对话图片对象 = transform.Find("Img_TalkRight").gameObject;
    }

    //private void OnEnable()
    //{
    //    初始化怪物宠物();
    //}


    //// 点击宠物触发的事件方法
    //public void 点击宠物()
    //{
    //    //游戏管理.实例.音频源管理.播放音效(游戏管理.实例.工厂管理.音频片段工厂.获取单个资源("MonsterNest/PetSound" + 宠物数据实例.怪物等级.ToString()));

    //    switch (宠物数据实例.怪物等级)
    //    {
    //        case 1: // 鸡蛋阶段
    //            if (游戏管理.实例.玩家管理.巢穴数量 >= 1)
    //            {
    //                游戏管理.实例.玩家管理.巢穴数量--;
    //                成长至成年(); // 升级并更新显示
    //                宠物数据实例.怪物等级++;
    //                显示怪物();
    //                宠物巢穴面板实例.更新文本();
    //            }
    //            else
    //            {
    //                提示图片对象.SetActive(true);
    //                Invoke("关闭对话界面", 2);
    //            }
    //            break;

    //        case 2: // 幼崽阶段
    //            if (喂养面板对象.activeSelf)
    //            {
    //                喂养面板对象.SetActive(false);
    //            }
    //            else
    //            {
    //                喂养面板对象.SetActive(true);
    //                // 牛奶按钮状态
    //                if (游戏管理.实例.玩家管理.牛奶数量 == 0)
    //                {
    //                    牛奶按钮图片.sprite = 按钮精灵数组[1];
    //                    牛奶按钮.interactable = false;
    //                }
    //                else
    //                {
    //                    牛奶按钮图片.sprite = 按钮精灵数组[0];
    //                    牛奶按钮.interactable = true;
    //                }
    //                // 饼干按钮状态
    //                if (游戏管理.实例.玩家管理.饼干数量 == 0)
    //                {
    //                    饼干按钮图片.sprite = 按钮精灵数组[3];
    //                    饼干按钮.interactable = false;
    //                }
    //                else
    //                {
    //                    饼干按钮图片.sprite = 按钮精灵数组[2];
    //                    饼干按钮.interactable = true;
    //                }
    //                // 显示剩余牛奶（为0则隐藏按钮）
    //                if (宠物数据实例.剩余牛奶 == 0)
    //                {
    //                    牛奶按钮.gameObject.SetActive(false);
    //                }
    //                else
    //                {
    //                    牛奶文本.text = 宠物数据实例.剩余牛奶.ToString();
    //                    牛奶按钮.gameObject.SetActive(true);
    //                }
    //                // 显示剩余饼干（为0则隐藏按钮）
    //                if (宠物数据实例.剩余饼干 == 0)
    //                {
    //                    饼干按钮.gameObject.SetActive(false);
    //                }
    //                else
    //                {
    //                    饼干文本.text = 宠物数据实例.剩余饼干.ToString();
    //                    饼干按钮.gameObject.SetActive(true);
    //                }
    //            }
    //            break;

    //        case 3: // 成年阶段
    //            int 随机数 = Random.Range(0, 2);
    //            if (随机数 == 1)
    //            {
    //                右侧对话图片对象.SetActive(true);
    //                Invoke("关闭对话界面", 2);
    //            }
    //            else
    //            {
    //                左侧对话图片对象.SetActive(true);
    //                Invoke("关闭对话界面", 2);
    //            }
    //            break;

    //        default:
    //            break;
    //    }
    //}


    //// 关闭对话框
    //private void 关闭对话界面()
    //{
    //    提示图片对象.SetActive(false);
    //    右侧对话图片对象.SetActive(false);
    //    左侧对话图片对象.SetActive(false);
    //}


    //// 显示当前等级的怪物
    //private void 显示怪物()
    //{
    //    for (int i = 0; i < 宠物对象组.Length; i++)
    //    {
    //        宠物对象组[i].SetActive(false);
    //        if ((i + 1) == 宠物数据实例.怪物等级)
    //        {
    //            宠物对象组[i].SetActive(true);
    //            Sprite 宠物精灵 = null;

    //            // 根据等级加载对应精灵
    //            switch (宠物数据实例.怪物等级)
    //            {
    //                case 1:
    //                    宠物精灵 = 游戏管理.实例.获取精灵("MonsterNest/Monster/Egg/" + 宠物数据实例.怪物ID.ToString());
    //                    break;
    //                case 2:
    //                    宠物精灵 = 游戏管理.实例.获取精灵("MonsterNest/Monster/Baby/" + 宠物数据实例.怪物ID.ToString());
    //                    break;
    //                case 3:
    //                    宠物精灵 = 游戏管理.实例.获取精灵("MonsterNest/Monster/Normal/" + 宠物数据实例.怪物ID.ToString());
    //                    break;
    //                default:
    //                    break;
    //            }

    //            // 设置宠物精灵
    //            Image 怪物图片 = 宠物对象组[i].transform.Find("Img_Pet").GetComponent<Image>();
    //            怪物图片.sprite = 宠物精灵;
    //            怪物图片.SetNativeSize();

    //            // 缩放比例
    //            float 图片缩放 = 0;
    //            if (宠物数据实例.怪物等级 == 1)
    //            {
    //                图片缩放 = 2;
    //            }
    //            else
    //            {
    //                图片缩放 = 1 + (宠物数据实例.怪物等级 - 1) * 0.5f;
    //            }
    //            怪物图片.transform.localScale = new Vector3(图片缩放, 图片缩放, 1);
    //        }
    //    }
    //}


    //// 喂牛奶
    //public void 喂牛奶()
    //{
    //    // 播放喂养音效
    //    游戏管理.实例.音频源管理.播放音效(游戏管理.实例.工厂管理.音频片段工厂.获取单个资源("MonsterNest/Feed01"));

    //    // 创建爱心特效
    //    GameObject 爱心对象 = 游戏管理.实例.工厂管理.工厂字典[工厂类型.界面工厂].获取物品("Img_Heart");
    //    爱心对象.transform.position = transform.position;
    //    宠物巢穴面板实例.设置画布层级(爱心对象.transform);

    //    // 处理牛奶消耗
    //    if (游戏管理.实例.玩家管理.牛奶数量 >= 宠物数据实例.剩余牛奶)
    //    {
    //        游戏管理.实例.玩家管理.牛奶数量 -= 宠物数据实例.剩余牛奶;
    //        宠物数据实例.剩余牛奶 = 0;
    //        宠物巢穴面板实例.更新文本(); // 更新显示
    //    }
    //    else
    //    {
    //        宠物数据实例.剩余牛奶 -= 游戏管理.实例.玩家管理.牛奶数量;
    //        游戏管理.实例.玩家管理.牛奶数量 = 0;
    //        牛奶按钮.gameObject.SetActive(false);
    //    }

    //    喂养面板对象.SetActive(false);
    //    Invoke("成长至成年", 0.433f);
    //}


    //// 喂饼干
    //public void 喂饼干()
    //{
    //    // 播放喂养音效
    //    游戏管理.实例.音频源管理.播放音效(游戏管理.实例.工厂管理.音频片段工厂.获取单个资源("MonsterNest/Feed02"));

    //    // 创建爱心特效
    //    GameObject 爱心对象 = 游戏管理.实例.工厂管理.工厂字典[工厂类型.界面工厂].获取物品("Img_Heart");
    //    爱心对象.transform.position = transform.position;
    //    宠物巢穴面板实例.设置画布层级(爱心对象.transform);

    //    // 处理饼干消耗
    //    if (游戏管理.实例.玩家管理.饼干数量 >= 宠物数据实例.剩余饼干)
    //    {
    //        游戏管理.实例.玩家管理.饼干数量 -= 宠物数据实例.剩余饼干;
    //        宠物数据实例.剩余饼干 = 0;
    //        宠物巢穴面板实例.更新文本(); // 更新显示
    //    }
    //    else
    //    {
    //        宠物数据实例.剩余饼干 -= 游戏管理.实例.玩家管理.饼干数量;
    //        游戏管理.实例.玩家管理.饼干数量 = 0;
    //        饼干按钮.gameObject.SetActive(false);
    //    }

    //    喂养面板对象.SetActive(false);
    //    Invoke("成长至成年", 0.433f);
    //}


    //// 成长方法（升级逻辑）
    //private void 成长至成年()
    //{
    //    if (宠物数据实例.剩余牛奶 == 0 && 宠物数据实例.剩余饼干 == 0)
    //    {
    //        游戏管理.实例.音频源管理.播放音效(游戏管理.实例.工厂管理.音频片段工厂.获取单个资源("MonsterNest/PetChange"));
    //        宠物数据实例.怪物等级++;

    //        if (宠物数据实例.怪物等级 >= 3)
    //        {
    //            游戏管理.实例.玩家管理.已解锁普通模式关卡列表[宠物数据实例.怪物ID * 5 - 1].已解锁 = true;
    //            游戏管理.实例.玩家管理.隐藏关卡解锁地图数++;
    //            显示怪物();
    //        }
    //        else
    //        {
    //            显示怪物();
    //        }
    //    }
    //    保存宠物数据实例();
    //}


    //// 保存宠物数据
    //private void 保存宠物数据实例()
    //{
    //    for (int i = 0; i < 游戏管理.实例.玩家管理.宠物数据实例列表.Count; i++)
    //    {
    //        if (游戏管理.实例.玩家管理.宠物数据实例列表[i].怪物ID == 宠物数据实例.怪物ID)
    //        {
    //            游戏管理.实例.玩家管理.宠物数据实例列表[i] = 宠物数据实例;
    //        }
    //    }
    //}


    //// 初始化宠物
    //public void 初始化怪物宠物()
    //{
    //    if (宠物数据实例.剩余牛奶 == 0)
    //    {
    //        宠物数据实例.剩余牛奶 = 宠物数据实例.怪物ID * 60;
    //    }
    //    if (宠物数据实例.剩余饼干 == 0)
    //    {
    //        宠物数据实例.剩余饼干 = 宠物数据实例.怪物ID * 30;
    //    }
    //    显示怪物();
    //}



}


//宠物数据信息
[System.Serializable]
public struct 宠物数据  // MonsterPetData
{
    public int 怪物等级;  // monsterLevel
    public int 剩余饼干;  // remainCookies
    public int 剩余牛奶;  // remainMilk
    public int 怪物ID;    // monsterID
}  

 
