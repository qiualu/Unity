using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 玩家管理器 
{

    public int 冒险模式解锁地图数; // adventrueModelNum; //冒险模式解锁的地图个数
    public int 隐藏关卡解锁地图数; // burriedLevelNum; //隐藏关卡解锁的地图个数
    public int BOSS模式击败数;// bossModelNum;//boss模式KO的BOSS
    public int 金币总数; // coin;//获得金币的总数
    public int 杀怪总数; // killMonsterNum;//杀怪总数
    public int 击败BOSS总数; // killBossNum;//杀掉BOSS的总数
    public int 清理道具总数; // clearItemNum;//清理道具的总数
    public List<bool> 已解锁普通模式大关卡列表;// unLockedNormalModelBigLevelList;//大关卡
    public List<关卡> 已解锁普通模式关卡列表;// unLockedNormalModelLevelList;//所有的小关卡
    public List<int> 已解锁普通模式关卡数量;// unLockedeNormalModelLevelNum;//解锁小关卡数量

 
    //怪物窝
    public int 饼干数量; // cookies;
    public int 牛奶数量; // milk;
    public int 巢穴数量; // nest;
    public int 钻石数量; // diamands;
    public List<宠物数据> 宠物数据列表;// MonsterPetData; //宠物喂养信息 public MonsterPetData monsterPetData;



    //用于玩家初始Json文件的制作
    //public 玩家管理器()  // public PlayerManager()
    //{
    //    冒险模式解锁地图数 = 0;  // adventrueModelNum = 0;
    //    隐藏关卡解锁地图数 = 0;  // burriedLevelNum = 0;
    //    BOSS模式击败数 = 0;  // bossModelNum = 0;
    //    金币总数 = 0;  // coin = 0;
    //    杀怪总数 = 0;  // killMonsterNum = 0;
    //    击败BOSS总数 = 0;  // killBossNum = 0;
    //    清理道具总数 = 0;  // clearItemNum = 0;
    //    饼干数量 = 100;  // cookies = 100;
    //    牛奶数量 = 100;  // milk = 100;
    //    巢穴数量 = 1;  // nest = 1;
    //    钻石数量 = 10;  // diamands = 10;
    //    已解锁普通模式关卡数量 = new List<int>()  // unLockedeNormalModelLevelNum = new List<int>()
    //    {
    //        1,0,0
    //    };
    //    已解锁普通模式大关卡列表 = new List<bool>()  // unLockedNormalModelBigLevelList = new List<bool>()
    //    {
    //        true,false,false
    //    };
    //    已解锁普通模式关卡列表 = new List<关卡>()  // unLockedNormalModelLevelList = new List<Stage>()
    //    {
    //           new 关卡(10,1,new int[]{ 1},false,0,1,1,true,false),  // new Stage(10,1,new int[]{ 1},false,0,1,1,true,false),
    //           new 关卡(9,1,new int[]{ 2},false,0,2,1,false,false),  // new Stage(9,1,new int[]{ 2},false,0,2,1,false,false),
    //           new 关卡(8,2,new int[]{ 1,2},false,0,3,1,false,false),  // new Stage(8,2,new int[]{ 1,2},false,0,3,1,false,false),
    //           new 关卡(10,1,new int[]{ 3},false,0,4,1,false,false),  // new Stage(10,1,new int[]{ 3},false,0,4,1,false,false),
    //           new 关卡(9,3,new int[]{ 1,2,3},false,0,5,1,false,true),  // new Stage(9,3,new int[]{ 1,2,3},false,0,5,1,false,true),
    //           new 关卡(8,2,new int[]{ 2,3},false,0,1,2,false,false),  // new Stage(8,2,new int[]{ 2,3},false,0,1,2,false,false),
    //           new 关卡(10,2,new int[]{ 1,3},false,0,2,2,false,false),  // new Stage(10,2,new int[]{ 1,3},false,0,2,2,false,false),
    //           new 关卡(9,1,new int[]{ 4},false,0,3,2,false,false),  // new Stage(9,1,new int[]{ 4},false,0,3,2,false,false),
    //           new 关卡(8,2,new int[]{ 1,4},false,0,4,2,false,false),  // new Stage(8,2,new int[]{ 1,4},false,0,4,2,false,false),
    //           new 关卡(10,2,new int[]{ 2,4},false,0,5,2,false,true),  // new Stage(10,2,new int[]{ 2,4},false,0,5,2,false,true),
    //           new 关卡(9,2,new int[]{ 3,4},false,0,1,3,false,false),  // new Stage(9,2,new int[]{ 3,4},false,0,1,3,false,false),
    //           new 关卡(8,1,new int[]{ 5},false,0,2,3,false,false),  // new Stage(8,1,new int[]{ 5},false,0,2,3,false,false),
    //           new 关卡(7,2,new int[]{ 4,5},false,0,3,3,false,false),  // new Stage(7,2,new int[]{ 4,5},false,0,3,3,false,false),
    //           new 关卡(10,3,new int[]{ 1,3,5},false,0,4,3,false,false),  // new Stage(10,3,new int[]{ 1,3,5},false,0,4,3,false,false),
    //           new 关卡(10,3,new int[]{ 1,4,5},false,0,5,3,false,true)  // new Stage(10,3,new int[]{ 1,4,5},false,0,5,3,false,true)
    //    };
    //    怪物宠物数据列表 = new List<怪物宠物数据>()  // monsterPetDataList = new List<MonsterPetData>()
    //    {
    //        new 怪物宠物数据()  // new MonsterPetData()
    //        {
    //            怪物ID=1,  // monsterID=1,
    //            怪物等级=1,  // monsterLevel=1,
    //            剩余饼干=0,  // remainCookies=0,
    //            剩余牛奶=0  // remainMilk=0
    //        }, 

    //    };
    //}

    //用于玩家所有关卡都解锁的Json文件的制作
    //public 玩家管理器()  // public PlayerManager()
    //{
    //    冒险模式解锁地图数 = 12;  // adventrueModelNum = 12;
    //    隐藏关卡解锁地图数 = 3;  // burriedLevelNum = 3;
    //    BOSS模式击败数 = 0;  // bossModelNum = 0;
    //    金币总数 = 999;  // coin = 999;
    //    杀怪总数 = 999;  // killMonsterNum = 999;
    //    击败BOSS总数 = 0;  // killBossNum = 0;
    //    清理道具总数 = 999;  // clearItemNum = 999;
    //    饼干数量 = 1000;  // cookies = 1000;
    //    牛奶数量 = 1000;  // milk = 1000;
    //    巢穴数量 = 10;  // nest = 10;
    //    钻石数量 = 1000;  // diamands = 1000;
    //    已解锁普通模式关卡数量 = new List<int>()  // unLockedeNormalModelLevelNum = new List<int>()
    //    {
    //        5,5,5
    //    };
    //    已解锁普通模式大关卡列表 = new List<bool>()  // unLockedNormalModelBigLevelList = new List<bool>()
    //    {
    //        true,true,true
    //    };
    //    已解锁普通模式关卡列表 = new List<关卡>()  // unLockedNormalModelLevelList = new List<Stage>()
    //    {
    //           new 关卡(10,1,new int[]{ 1},false,0,1,1,true,false),  // new Stage(10,1,new int[]{ 1},false,0,1,1,true,false),
    //           new 关卡(9,1,new int[]{ 2},false,0,2,1,true,false),  // new Stage(9,1,new int[]{ 2},false,0,2,1,true,false),
    //           new 关卡(8,2,new int[]{ 1,2},false,0,3,1,true,false),  // new Stage(8,2,new int[]{ 1,2},false,0,3,1,true,false),
    //           new 关卡(10,1,new int[]{ 3},false,0,4,1,true,false),  // new Stage(10,1,new int[]{ 3},false,0,4,1,true,false),
    //           new 关卡(9,3,new int[]{ 1,2,3},false,0,5,1,false,true),  // new Stage(9,3,new int[]{ 1,2,3},false,0,5,1,false,true),
    //           new 关卡(8,2,new int[]{ 2,3},false,0,1,2,true,false),  // new Stage(8,2,new int[]{ 2,3},false,0,1,2,true,false),
    //           new 关卡(10,2,new int[]{ 1,3},false,0,2,2,true,false),  // new Stage(10,2,new int[]{ 1,3},false,0,2,2,true,false),
    //           new 关卡(9,1,new int[]{ 4},false,0,3,2,true,false),  // new Stage(9,1,new int[]{ 4},false,0,3,2,true,false),
    //           new 关卡(8,2,new int[]{ 1,4},false,0,4,2,true,false),  // new Stage(8,2,new int[]{ 1,4},false,0,4,2,true,false),
    //           new 关卡(10,2,new int[]{ 2,4},false,0,5,2,false,true),  // new Stage(10,2,new int[]{ 2,4},false,0,5,2,false,true),
    //           new 关卡(9,2,new int[]{ 3,4},false,0,1,3,true,false),  // new Stage(9,2,new int[]{ 3,4},false,0,1,3,true,false),
    //           new 关卡(8,1,new int[]{ 5},false,0,2,3,true,false),  // new Stage(8,1,new int[]{ 5},false,0,2,3,true,false),
    //           new 关卡(7,2,new int[]{ 4,5},false,0,3,3,true,false),  // new Stage(7,2,new int[]{ 4,5},false,0,3,3,true,false),
    //           new 关卡(10,3,new int[]{ 1,3,5},false,0,4,3,true,false),  // new Stage(10,3,new int[]{ 1,3,5},false,0,4,3,true,false),
    //           new 关卡(10,3,new int[]{ 1,4,5},false,0,5,3,false,true)  // new Stage(10,3,new int[]{ 1,4,5},false,0,5,3,false,true)
    //    };
    //    怪物宠物数据列表 = new List<怪物宠物数据>()  // monsterPetDataList = new List<MonsterPetData>()
    //    {
    //        new 怪物宠物数据()  // new MonsterPetData()
    //        {
    //            怪物ID=1,  // monsterID=1,
    //            怪物等级=1,  // monsterLevel=1,
    //            剩余饼干=0,  // remainCookies=0,
    //            剩余牛奶=0  // remainMilk=0
    //        },
    //        new 怪物宠物数据()  // new MonsterPetData()
    //        {
    //            怪物ID=2,  // monsterID=2,
    //            怪物等级=1,  // monsterLevel=1,
    //            剩余饼干=0,  // remainCookies=0,
    //            剩余牛奶=0  // remainMilk=0
    //        },
    //        new 怪物宠物数据()  // new MonsterPetData()
    //        {
    //            怪物ID=3,  // monsterID=3,
    //            怪物等级=1,  // monsterLevel=1,
    //            剩余饼干=0,  // remainCookies=0,
    //            剩余牛奶=0  // remainMilk=0
    //        }
    //    };
    //}


    public void 保存数据()  // public void SaveData()
    {
        //Debug.Log($" ************************* 保存数据 ************************* ");
        游戏数据存储 游戏数据存储实例 = new 游戏数据存储();  // Memento memento = new Memento();
        游戏数据存储实例.通过Json保存();  // memento.SaveByJson();
    }

    public void 读取数据()  // public void ReadData()
    {
        游戏数据存储 游戏数据存储实例 = new 游戏数据存储();  // Memento memento = new Memento(); 游戏数据存储

        //玩家管理器 玩家管理实例 = 游戏数据存储实例.通过Json加载();  // PlayerManager playerManager = memento.LoadByJson();
        玩家管理器 玩家管理实例 = 游戏数据存储实例.加载Json数据();  // 中文版本
        //数据信息
        冒险模式解锁地图数 = 玩家管理实例.冒险模式解锁地图数;  // adventrueModelNum = playerManager.adventrueModelNum;
        隐藏关卡解锁地图数 = 玩家管理实例.隐藏关卡解锁地图数;  // burriedLevelNum = playerManager.burriedLevelNum;
        BOSS模式击败数 = 玩家管理实例.BOSS模式击败数;  // bossModelNum = playerManager.bossModelNum;
        金币总数 = 玩家管理实例.金币总数;  // coin = playerManager.coin;
        杀怪总数 = 玩家管理实例.杀怪总数;  // killMonsterNum = playerManager.killMonsterNum;
        击败BOSS总数 = 玩家管理实例.击败BOSS总数;  // killBossNum = playerManager.killBossNum;
        清理道具总数 = 玩家管理实例.清理道具总数;  // clearItemNum = playerManager.clearItemNum;
        饼干数量 = 玩家管理实例.饼干数量;  // cookies = playerManager.cookies;
        牛奶数量 = 玩家管理实例.牛奶数量;  // milk = playerManager.milk;
        巢穴数量 = 玩家管理实例.巢穴数量;  // nest = playerManager.nest;
        钻石数量 = 玩家管理实例.钻石数量;  // diamands = playerManager.diamands;
        //列表
        已解锁普通模式大关卡列表 = 玩家管理实例.已解锁普通模式大关卡列表;  // unLockedNormalModelBigLevelList = playerManager.unLockedNormalModelBigLevelList;
        已解锁普通模式关卡列表 = 玩家管理实例.已解锁普通模式关卡列表;  // unLockedNormalModelLevelList = playerManager.unLockedNormalModelLevelList;
        已解锁普通模式关卡数量 = 玩家管理实例.已解锁普通模式关卡数量;  // unLockedeNormalModelLevelNum = playerManager.unLockedeNormalModelLevelNum;
        宠物数据列表 = 玩家管理实例.宠物数据列表;  // monsterPetDataList = playerManager.monsterPetDataList;
    }

    public void 读取数据测试()  // public void ReadData()
    {
        游戏数据存储 游戏数据存储实例 = new 游戏数据存储();  // Memento memento = new Memento(); 游戏数据存储
        游戏数据存储实例.加载Json数据();  // PlayerManager playerManager = memento.LoadByJson();
    }


}

 
