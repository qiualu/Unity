using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using LitJson;
using System.IO;


public class 游戏数据存储 : MonoBehaviour
{
    //读取
    public 玩家管理器 通过Json加载()  // public PlayerManager LoadByJson()
    {
        玩家管理器 玩家管理实例 = new 玩家管理器();  // PlayerManager playerManager = new PlayerManager();
        string 文件路径 = "";  // string filePath="";
        if (游戏管理.实例.是否重置玩家数据)  // if (GameManager.Instance.initPlayerManager)
        {
            文件路径 = Application.streamingAssetsPath + "/Json" + "/playerManagerInitData.json";  // filePath = Application.streamingAssetsPath + "/Json" + "/playerManagerInitData.json";
            //Debug.Log("重 置 正在读取初始数据文件: " + 文件路径);  // 打印读取的文件路径
        }
        else
        {
            文件路径 = Application.streamingAssetsPath + "/Json" + "/playerManager.json";  // filePath = Application.streamingAssetsPath + "/Json" + "/playerManager.json";
            //Debug.Log("不重置 正在读取初始数据文件: " + 文件路径);  // 打印读取的文件路径
        }
        //正常解析
        if (File.Exists(文件路径))  // if (File.Exists(filePath))
        {
            StreamReader 流读取器 = new StreamReader(文件路径);  // StreamReader sr = new StreamReader(filePath);
            string json字符串 = 流读取器.ReadToEnd();  // string jsonStr = sr.ReadToEnd();
            流读取器.Close();  // sr.Close();

            //Debug.Log($"\n===== json字符串 =====  : {json字符串}");
     

            // 解析JSON到玩家管理实例
            玩家管理实例 = JsonMapper.ToObject<玩家管理器>(json字符串);



            //// 1. 打印基础数据字段（验证基本类型解析是否正确）
            //Debug.Log("\n===== 基础数据检查 =====");
            //Debug.Log("冒险模式解锁地图数: " + 玩家管理实例.冒险模式解锁地图数);
            //Debug.Log("隐藏关卡解锁地图数: " + 玩家管理实例.隐藏关卡解锁地图数);
            //Debug.Log("BOSS模式击败数: " + 玩家管理实例.BOSS模式击败数);
            //Debug.Log("金币总数: " + 玩家管理实例.金币总数);
            //Debug.Log("杀怪总数: " + 玩家管理实例.杀怪总数);
            //Debug.Log("击败BOSS总数: " + 玩家管理实例.击败BOSS总数);
            //Debug.Log("清理道具总数: " + 玩家管理实例.清理道具总数);
            //Debug.Log("饼干数量: " + 玩家管理实例.饼干数量);
            //Debug.Log("牛奶数量: " + 玩家管理实例.牛奶数量);
            //Debug.Log("巢穴数量: " + 玩家管理实例.巢穴数量);
            //Debug.Log("钻石数量: " + 玩家管理实例.钻石数量);

             
            return 玩家管理实例;  // return playerManager;

        }
        else
        {
            Debug.Log("玩家管理器读取失败");  // Debug.Log("PlayerManager读取失败");
        }
        return null;  // return null;
        //强制解析
        //if (File.Exists(文件路径))  // if (File.Exists(filePath))
        //{
        //    //创建一个StreamReader，用来读取流
        //    StreamReader 流读取器 = new StreamReader(文件路径);  // StreamReader sr = new StreamReader(filePath);
        //    //将读取到的流赋值给jsonStr
        //    string json字符串 = 流读取器.ReadToEnd();  // string jsonStr = sr.ReadToEnd();
        //    //关闭
        //    流读取器.Close();  // sr.Close();
        //    //将字符串jsonStr转换为Save对象
        //    JsonData json数据 = JsonMapper.ToObject<JsonData>(json字符串);  // JsonData jsonData = JsonMapper.ToObject<JsonData>(jsonStr);
        //    玩家管理实例.冒险模式解锁地图数 = (int)json数据[0];  // playerManager.adventureModelNum = (int)jsonData[0];
        //    玩家管理实例.隐藏关卡解锁地图数 = (int)json数据[1];  // playerManager.burriedLevelNum = (int)jsonData[1];
        //    玩家管理实例.BOSS模式击败数 = (int)json数据[2];  // playerManager.bossModelNum = (int)jsonData[2];
        //    玩家管理实例.金币总数 = (int)json数据[3];  // playerManager.coin = (int)jsonData[3];
        //    玩家管理实例.杀怪总数 = (int)json数据[4];  // playerManager.killMonsterNum = (int)jsonData[4];
        //    玩家管理实例.击败BOSS总数 = (int)json数据[5];  // playerManager.killBossNum = (int)jsonData[5];
        //    玩家管理实例.清理道具总数 = (int)json数据[6];  // playerManager.clearItemNum = (int)jsonData[6];

        //    玩家管理实例.已解锁普通模式大关卡列表 = new List<bool>();  // playerManager.UnlockedNormalModelBigLevel = new List<bool>();
        //    JsonData 大关卡json数据 = json数据[7];  // JsonData bigLevelJsonData = jsonData[7];
        //    for (int i = 0; i < 3; i++)  // for (int i = 0; i < 3; i++)
        //    {
        //        玩家管理实例.已解锁普通模式大关卡列表.Add((bool)大关卡json数据[i]);  // playerManager.UnlockedNormalModelBigLevel.Add((bool)bigLevelJsonData[i]);
        //    }



        //    玩家管理实例.已解锁普通模式关卡列表 = new List<关卡>();  // playerManager.UnlockedNormalModelLevel = new List<Stage>();
        //    JsonData 关卡json数据 = json数据[8];  // JsonData levelJsonData = jsonData[8];


        //    for (int j = 0; j < 15; j++)//单独的stage  // for (int j = 0; j < 15; j++)//单独的stage
        //    {
        //        JsonData 关卡json = 关卡json数据[j];  // JsonData stageJson = levelJsonData[j];
        //        JsonData 塔列表json = 关卡json[0];  // JsonData towerListJson = stageJson[0];
        //        int 列表长度 = (int)关卡json[1];  // int listLength = (int)stageJson[1];
        //        int[] 塔列表 = new int[列表长度];  // int[] towerList = new int[listLength];
        //        for (int i = 0; i < 列表长度; i++)  // for (int i = 0; i < listLength; i++)
        //        {
        //            塔列表[i] = (int)关卡json[0][i];  // towerList[i] = (int)stageJson[0][i];
        //        }
        //        关卡 关卡实例 = new 关卡((int)关卡json[8], 列表长度, 塔列表  // Stage stage = new Stage((int)stageJson[8], listLength, towerList
        //            , (bool)关卡json[2], (int)关卡json[3], (int)关卡json[4],  // , (bool)stageJson[2], (int)stageJson[3], (int)stageJson[4],
        //            (int)关卡json[5], (bool)关卡json[6], (bool)关卡json[7]);  // (int)stageJson[5], (bool)stageJson[6], (bool)stageJson[7]);
        //        玩家管理实例.已解锁普通模式关卡列表.Add(关卡实例);  // playerManager.UnlockedNormalModelLevel.Add(stage);
        //        Debug.Log(玩家管理实例.已解锁普通模式关卡列表[j].塔ID列表.Length);  // Debug.Log(playerManager.UnlockedNormalModelLevel[j].mTowerIDList.Length);
        //    }



        //    玩家管理实例.已解锁普通模式关卡数量 = new List<int>();  // playerManager.UnlockedNormalModelLevelNum = new List<int>();
        //    JsonData 已解锁关卡数量列表 = json数据[9];  // JsonData unLockedLevelNumList = jsonData[9];

        //    for (int i = 0; i < 3; i++)  // for (int i = 0; i < 3; i++)
        //    {
        //        玩家管理实例.已解锁普通模式关卡数量.Add((int)已解锁关卡数量列表[i]);  // playerManager.UnlockedNormalModelLevelNum.Add((int)unLockedLevelNumList[i]);
        //    }

        //    玩家管理实例.饼干数量 = (int)json数据[10];  // playerManager.cookies = (int)jsonData[10];
        //    玩家管理实例.牛奶数量 = (int)json数据[11];  // playerManager.milk = (int)jsonData[11];
        //    玩家管理实例.巢穴数量 = (int)json数据[12];  // playerManager.nest = (int)jsonData[12];
        //    玩家管理实例.钻石数量 = (int)json数据[13];  // playerManager.diamands = (int)jsonData[13];
        //    玩家管理实例.怪物宠物数据列表长度 = (int)json数据[14];  // playerManager.monsterPetDataListLength = (int)jsonData[14];
        //    玩家管理实例.怪物宠物数据列表 = new List<怪物宠物数据>();  // playerManager.monsterPetDataList = new List<MonsterPetData>();
        //    JsonData 怪物宠物数据列表 = json数据[15];  // JsonData monsterPetDataList = jsonData[15];
        //    for (int i = 0; i < 玩家管理实例.怪物宠物数据列表长度; i++)  // for (int i = 0; i < playerManager.monsterPetDataListLength; i++)
        //    {
        //        JsonData 怪物数据 = 怪物宠物数据列表[i];  // JsonData monsterData = monsterPetDataList[i];
        //        怪物宠物数据 怪物宠物数据实例 = new 怪物宠物数据()  // MonsterPetData monsterPetData = new MonsterPetData()
        //        {
        //            怪物等级 = (int)怪物数据[0],  // monsterLevel = (int)monsterData[0],
        //            剩余饼干 = (int)怪物数据[1],  // remainCookies = (int)monsterData[1],
        //            剩余牛奶 = (int)怪物数据[2],  // remainMilk = (int)monsterData[2],
        //            怪物ID = (int)怪物数据[3]  // monsterID = (int)monsterData[3]
        //        };
        //        玩家管理实例.怪物宠物数据列表.Add(怪物宠物数据实例);  // playerManager.monsterPetDataList.Add(monsterPetData);
        //    }
        //    Debug.Log(玩家管理实例.怪物宠物数据列表.Count);  // Debug.Log(playerManager.monsterPetDataList.Count);
        //    游戏管理.实例.是否重置玩家数据 = false;  // GameManager.Instance.initPlayerManager = false;
        //    return 玩家管理实例;  // return playerManager;
        //}
    }


    public 玩家管理器 加载Json数据()  // public PlayerManager LoadByJson()
    {
        玩家管理器 玩家管理实例 = new 玩家管理器();  // PlayerManager playerManager = new PlayerManager();
        string 文件路径 = "";  // string filePath="";
        if (游戏管理.实例.是否重置玩家数据)  // if (GameManager.Instance.initPlayerManager)
        {
            文件路径 = Application.streamingAssetsPath + "/数据" + "/玩家管理初始化数据.json";  // filePath = Application.streamingAssetsPath + "/Json" + "/playerManagerInitData.json";
            //Debug.Log("重 置 正在读取初始数据文件: " + 文件路径);  // 打印读取的文件路径
        }
        else
        {
            文件路径 = Application.streamingAssetsPath + "/数据" + "/玩家管理数据.json";  // filePath = Application.streamingAssetsPath + "/Json" + "/playerManager.json";
            //Debug.Log("不重置 正在读取初始数据文件: " + 文件路径);  // 打印读取的文件路径
        }
        //正常解析
        if (File.Exists(文件路径))  // if (File.Exists(filePath))
        {
            StreamReader 流读取器 = new StreamReader(文件路径);  // StreamReader sr = new StreamReader(filePath);
            string json字符串 = 流读取器.ReadToEnd();  // string jsonStr = sr.ReadToEnd();
            流读取器.Close();  // sr.Close();

            //Debug.Log($"\n===== json字符串 =====  : {json字符串}");

            // 解析JSON到玩家管理实例
            玩家管理实例 = JsonMapper.ToObject<玩家管理器>(json字符串);  // playerManager = JsonMapper.ToObject<PlayerManager>(jsonStr);

            // 1. 打印基础数据字段（验证基本类型解析是否正确）
            //Debug.Log("\n===== 基础数据检查 =====");
            //Debug.Log("冒险模式解锁地图数: " + 玩家管理实例.冒险模式解锁地图数);
            //Debug.Log("隐藏关卡解锁地图数: " + 玩家管理实例.隐藏关卡解锁地图数);
            //Debug.Log("BOSS模式击败数: " + 玩家管理实例.BOSS模式击败数);
            //Debug.Log("金币总数: " + 玩家管理实例.金币总数);
            //Debug.Log("杀怪总数: " + 玩家管理实例.杀怪总数);
            //Debug.Log("击败BOSS总数: " + 玩家管理实例.击败BOSS总数);
            //Debug.Log("清理道具总数: " + 玩家管理实例.清理道具总数);
            //Debug.Log("饼干数量: " + 玩家管理实例.饼干数量);
            //Debug.Log("牛奶数量: " + 玩家管理实例.牛奶数量);
            //Debug.Log("巢穴数量: " + 玩家管理实例.巢穴数量);
            //Debug.Log("钻石数量: " + 玩家管理实例.钻石数量);



            return 玩家管理实例;  // return playerManager;

        }
        else
        {
            Debug.Log("玩家管理器读取失败");  // Debug.Log("PlayerManager读取失败");
        }
        return null;

    }


    //存贮
    public void 通过Json保存()  // public void SaveByJson()
    {
        Debug.Log(" ************ 通过Json保存 ************ ");

        玩家管理器 玩家管理实例 = 游戏管理.实例.玩家管理;  // PlayerManager playerManager = GameManager.Instance.playerManager;
        string 文件路径 = Application.streamingAssetsPath + "/数据" + "/玩家管理数据.json";  // string filePath = Application.streamingAssetsPath + "/Json" + "/playerManager.json";
        string 保存的json字符串 = JsonMapper.ToJson(玩家管理实例);  // string saveJsonStr = JsonMapper.ToJson(playerManager);
        StreamWriter 流写入器 = new StreamWriter(文件路径);  // StreamWriter sw = new StreamWriter(filePath);
        流写入器.Write(保存的json字符串);  // sw.Write(saveJsonStr);
        流写入器.Close();  // sw.Close();
    }

}
