using UnityEngine;

public class 固定窗口设置 : MonoBehaviour
{
    // 固定窗口大小
    public int 窗口宽度 = 1920;
    public int 窗口高度 = 1080;

    void Start()
    {
        // 在游戏启动时设置窗口
        设置固定窗口();
    }

    void 设置固定窗口()
    {
        // 确保在独立运行时才执行窗口设置
#if UNITY_STANDALONE
        // 设置窗口为无边框
        Screen.fullScreen = false;
        PlayerPrefs.SetInt("Screenmanager Is Fullscreen mode", 0);

        // 设置窗口大小
        Screen.SetResolution(窗口宽度, 窗口高度, false);

        // 计算居中位置
        int 屏幕宽度 = Screen.currentResolution.width;
        int 屏幕高度 = Screen.currentResolution.height;

        int 横坐标 = (屏幕宽度 - 窗口宽度) / 2;
        int 纵坐标 = (屏幕高度 - 窗口高度) / 2;

        // 设置窗口位置（仅Windows有效）
#if UNITY_STANDALONE_WIN
        设置窗口位置(横坐标, 纵坐标);
#endif
#endif
    }

    // Windows API调用设置窗口位置
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool SetWindowPos(System.IntPtr 窗口句柄, System.IntPtr 插入位置, int X, int Y, int 宽度, int 高度, uint 标志);

    private void 设置窗口位置(int x, int y)
    {
        System.IntPtr 窗口句柄 = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
        if (窗口句柄 != System.IntPtr.Zero)
        {
            // SWP_NOSIZE = 0x0001，保持窗口大小
            // SWP_NOZORDER = 0x0004，保持Z序
            SetWindowPos(窗口句柄, System.IntPtr.Zero, x, y, 0, 0, 0x0001 | 0x0004);
        }
    }
}
