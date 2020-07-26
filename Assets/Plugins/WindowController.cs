using UnityEngine;
//using System.Collections;

using System;
using System.Runtime.InteropServices;

public class WindowController : MonoBehaviour
{
    private string windowName = "net_clock";
    static int w = 350;
    static int h = 350;
    static int right_margin = 50;
    static int bottom_margin = 50;
    private int width = w;
    private int height = h;
    private int x = 1920 - w - right_margin;
    private int y = 1080 - h - bottom_margin;

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);
    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int GetForegroundWindow();
    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    private static extern Boolean SetLayeredWindowAttributes(int hwnd, uint crKey, byte bAlpha, uint dwFlags);

    public void exec()
    {
        var os = Environment.OSVersion;
        int m_ver = os.Version.Major;
        Debug.Log("os : " + m_ver);
        if (m_ver != 10) {
            return;
        }

        var window = FindWindow(null, windowName);

        {
            const int GWL_STYLE = -16;
            const int WS_BORDER = 0x00800000; //window with border
            const int WS_DLGFRAME = 0x00400000; //window with double border but no title

            int style = GetWindowLong(window, GWL_STYLE);
            int WS_CAPTION;
            WS_CAPTION = WS_BORDER | WS_DLGFRAME;
            style &= ~WS_CAPTION;
            SetWindowLong(window, GWL_STYLE, style);
            SetWindowPos(window, 0, x, y, width, height, width * height == 0 ? 1 : 0);
        }
        {
            const int GWL_EXSTYLE = -20;
            const int WS_EX_LAYERED = 0x80000;

            int style = GetWindowLong(window, GWL_EXSTYLE);
            SetWindowLong(window, GWL_EXSTYLE, style | WS_EX_LAYERED);

            int handle = GetForegroundWindow();
            SetLayeredWindowAttributes(handle, 0x00000000, 0, 0x00000001);
        }
    }
}
