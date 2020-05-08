using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Macros
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
public class ChromeWrapper
{
    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    // the keystroke signals. you can look them up at the msdn pages
    private static uint WM_KEYDOWN = 0x100, WM_KEYUP = 0x101;

    // the reference to the chrome process
    private System.Diagnostics.Process chromeProcess;

    public ChromeWrapper(string url)
    {
            System.Diagnostics.Process.Start("chrome.exe", url); //no need to keep reference to this process, because if chrome is already opened, this is NOT the correct reference.
        System.Threading.Thread.Sleep(600);

            System.Diagnostics.Process[] procsChrome = System.Diagnostics.Process.GetProcessesByName("chrome");
        foreach (System.Diagnostics.Process chrome in procsChrome)
        {
            if (chrome.MainWindowHandle == IntPtr.Zero)// the chrome process must have a window
                continue;
                chromeProcess = chrome; //now you have a handle to the main chrome (either a new one or the one that was already open).
            return;
        }
    }

    public void SendKey(char key)
    {
        try
        {
            if (chromeProcess.MainWindowHandle != IntPtr.Zero)
            {
                // send the keydown signal
                SendMessage(chromeProcess.MainWindowHandle, ChromeWrapper.WM_KEYDOWN, (IntPtr)key, IntPtr.Zero);
                // give the process some time to "realize" the keystroke
                Thread.Sleep(30); //On my system it works fine without this Sleep.
                // send the keyup signal
                SendMessage(chromeProcess.MainWindowHandle, ChromeWrapper.WM_KEYUP, (IntPtr)key, IntPtr.Zero);
            }
        }
        catch (Exception e) //without the GetProcessesByName you'd get an exception.
        {
        }
    }
}
}

