using UnityEngine;
//using System.Collections;

using System;
using System.Runtime.InteropServices;

public class WindowController : MonoBehaviour
{
    private string windowName = "net_clock";
    static int w = 465; //350;
    static int h = 330; //350;
    static int right_margin = 50;
    static int bottom_margin = 50;
    private int width = w;
    private int height = h;
    private int x = 1920 - w - right_margin;
    private int y = 1080 - h - bottom_margin;

    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);
    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    private static extern Boolean SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

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
            const int WS_CAPTION = 0x00C00000;

            int style = GetWindowLong(window, GWL_STYLE);
            style &= ~WS_CAPTION;
            SetWindowLong(window, GWL_STYLE, style);
            Debug.Log("w,h: " + width + ", " + height);

            const int HWND_TOP = 0;
            SetWindowPos(window, HWND_TOP, x, y, width, height, 0);
        }
        {
            const int GWL_EXSTYLE = -20;
            const int WS_EX_LAYERED = 0x80000;

            int style = GetWindowLong(window, GWL_EXSTYLE);
            SetWindowLong(window, GWL_EXSTYLE, style | WS_EX_LAYERED);

            const int LWA_COLORKEY = 1;
            SetLayeredWindowAttributes(window, 0x00000000, 0, LWA_COLORKEY);
        }
    }
}
