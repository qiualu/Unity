using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 所有定义汇总
{

    //玩家管理器: 

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
    public List<宠物类> 宠物数据列表;// MonsterPetData; //宠物喂养信息 public MonsterPetData monsterPetData;

    //public void 保存数据()
    //public void 读取数据()

    //宠物类
    public int 怪物等级;  // monsterLevel
    public int 剩余饼干;  // remainCookies
    public int 剩余牛奶;  // remainMilk
    public int 怪物ID;    // monsterID

    //public class 关卡
    public int[] 塔ID列表;  // 原 mTowerIDList;//本关卡可以建的塔种类
    public int 塔ID列表长度;  // 原 mTowerIDListLength;//建塔数组长度
    public bool 全清状态;  // 原 mAllClear;//是否清空此关卡道具
    public int 胡萝卜状态;  // 原 mCarrotState;//萝卜状态
    public int 关卡ID;  // 原 mLevelID;//小关卡ID
    public int 大关卡ID;  // 原 mBigLevelID;//大关卡ID
    public bool 已解锁;  // 原 unLocked;//此关卡是否解锁
    public bool 是否奖励关卡;  // 原 mIsRewardLevel;//是否为奖励关卡
    public int 总回合数;  // 原 mTotalRound;//一共几波怪




}
