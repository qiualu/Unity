using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using DG.Tweening; // 必须引用命名空间



public class cude测试 : MonoBehaviour
{

    // 鼠标进入地板范围时
    private void OnMouseEnter()
    {
        Debug.Log($"鼠标进入地板 ");

    }

    // 鼠标离开地板范围时
    private void OnMouseExit()
    {
        Debug.Log($"鼠标离开地板 ");


    }

    // 鼠标按下时
    private void OnMouseDown()
    {
        Debug.Log($"鼠标按下时 {游戏管理.实例.是否重置玩家数据} ");

        // 示例：让物体在2秒内移动到(5,0,0)位置
        transform.DOMove(new Vector3(5, 0, 0), 2f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Debug.Log("移动完成！"));


        //玩家管理数据输出();


        var 工厂管理 = 游戏管理.实例.工厂管理;
        var 界面管理 = 游戏管理.实例.界面管理;
        Debug.Log($"冒险模式解锁地图数 : {界面管理.界面外观实例} ");



    }




    // 鼠标松开时
    private void OnMouseUp()
    {
        Debug.Log($"鼠标松开时  ");


    }


    private void 玩家管理数据输出()
    {


        // 获取玩家管理实例（简化代码）
        var 玩家管理 = 游戏管理.实例.玩家管理;

        Debug.Log($"冒险模式解锁地图数 : {游戏管理.实例.玩家管理.冒险模式解锁地图数} ");
        Debug.Log($"隐藏关卡解锁地图数 : {游戏管理.实例.玩家管理.隐藏关卡解锁地图数} ");
        Debug.Log($"BOSS模式击败数 : {游戏管理.实例.玩家管理.BOSS模式击败数} ");
        Debug.Log($"金币总数 : {游戏管理.实例.玩家管理.金币总数} ");
        Debug.Log($"杀怪总数 : {游戏管理.实例.玩家管理.杀怪总数} ");
        Debug.Log($"击败BOSS总数 : {游戏管理.实例.玩家管理.击败BOSS总数} ");
        Debug.Log($"清理道具总数 : {游戏管理.实例.玩家管理.清理道具总数} ");

        Debug.Log($"已解锁普通模式大关卡列表 : {玩家管理.已解锁普通模式大关卡列表.Count} ");
        Debug.Log($"已解锁普通模式关卡列表 : {玩家管理.已解锁普通模式关卡列表.Count} ");
        Debug.Log($"已解锁普通模式关卡数量 : {玩家管理.已解锁普通模式关卡数量.Count} ");


        //游戏管理.实例.玩家管理.冒险模式解锁地图数 = 100;
        //游戏管理.实例.玩家管理.隐藏关卡解锁地图数 = 88;
        //游戏管理.实例.玩家管理.BOSS模式击败数 = 76;

        // 1. 遍历 已解锁普通模式大关卡列表（List<bool>）
        if (玩家管理.已解锁普通模式大关卡列表 != null)
        {
            Debug.Log("\n===== 已解锁普通模式大关卡列表 =====");
            for (int i = 0; i < 玩家管理.已解锁普通模式大关卡列表.Count; i++)
            {
                // 索引+1对应大关卡编号（第1关、第2关...）
                Debug.Log($"大关卡 {i + 1} 解锁状态: {玩家管理.已解锁普通模式大关卡列表[i]}");
            }
        }
        else
        {
            Debug.LogWarning("已解锁普通模式大关卡列表为null！");
        }

        // 2. 遍历 已解锁普通模式关卡数量（List<int>）
        if (玩家管理.已解锁普通模式关卡数量 != null)
        {
            Debug.Log("\n===== 已解锁普通模式关卡数量 =====");
            for (int i = 0; i < 玩家管理.已解锁普通模式关卡数量.Count; i++)
            {
                // 每个元素对应"第i+1个大关卡"包含的已解锁小关卡数量
                Debug.Log($"第 {i + 1} 个大关卡已解锁小关卡数量: {玩家管理.已解锁普通模式关卡数量[i]}");
            }
        }
        else
        {
            Debug.LogWarning("已解锁普通模式关卡数量数量列表为null！");
        }

        // 3. 遍历 已解锁普通模式关卡列表（List<关卡>）
        if (玩家管理.已解锁普通模式关卡列表 != null)
        {
            Debug.Log("\n===== 已解锁普通模式关卡列表（共 " + 玩家管理.已解锁普通模式关卡列表.Count + " 个关卡） =====");
            for (int i = 0; i < 玩家管理.已解锁普通模式关卡列表.Count; i++)
            {
                关卡 当前关卡 = 玩家管理.已解锁普通模式关卡列表[i];
                if (当前关卡 != null)
                {
                    // 打印关卡详细信息
                    Debug.Log($"关卡 {i + 1} 信息:");
                    Debug.Log($" - 关卡ID: {当前关卡.关卡ID}");
                    Debug.Log($" - 所属大关卡: {当前关卡.大关卡ID}");
                    Debug.Log($" - 解锁状态: {当前关卡.已解锁}");
                    Debug.Log($" - 塔ID列表: [{string.Join(",", 当前关卡.塔ID列表)}]"); // 数组转字符串
                    Debug.Log($" - 总回合数: {当前关卡.总回合数}");
                    Debug.Log($" - 是否奖励关卡: {当前关卡.是否奖励关卡}");
                }
                else
                {
                    Debug.Log($"关卡 {i + 1} 为null（未初始化）");
                }
            }
        }
        else
        {
            Debug.LogWarning("已解锁普通模式关卡列表为null！");
        }

        宠物数据 当前宠物 = 玩家管理.宠物数据列表[0];

        // 3. 遍历 宠物数据列表（List<关卡>）
        if (!当前宠物.Equals(default(宠物数据)))
        {
            Debug.Log("\n===== 宠物数据列表（共 " + 玩家管理.宠物数据列表.Count + " 个关卡） =====");
            for (int i = 0; i < 玩家管理.宠物数据列表.Count; i++)
            {
                当前宠物 = 玩家管理.宠物数据列表[i];

                // 打印关卡详细信息
                Debug.Log($"当前宠物 {i + 1} 信息:");
                Debug.Log($" - 关卡ID: {当前宠物.怪物等级}");
                Debug.Log($" - 所属大关卡: {当前宠物.剩余饼干}");
                Debug.Log($" - 解锁状态: {当前宠物.剩余牛奶}");
                Debug.Log($" - 是否奖励关卡: {当前宠物.怪物ID}");


            }
        }
        else
        {
            Debug.LogWarning("已解锁普通模式关卡列表为null！");
        }


        游戏管理.实例.玩家管理.保存数据();

        Debug.LogWarning(" ------------------------------------------ end ！");
    }



}
