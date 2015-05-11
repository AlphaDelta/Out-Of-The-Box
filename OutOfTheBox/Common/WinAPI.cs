using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace OutOfTheBox.Common
{
    class WinAPI
    {
        /*
         * Windows messages
         */
        public const int
        /* Windows hooks */
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14,
        /* Windows messages */
        WM_NULL = 0x0000,
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_MOUSEWHEEL = 0x020A,
        WM_MOUSEHWHEEL = 0x020E;


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);
        public delegate IntPtr LowLevelMouseProc(int nCode, uint wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, uint wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        public static LowLevelMouseProc MouseHookGC;

        public class MouseHook
        {
            static IntPtr hook = IntPtr.Zero;
            public static void Hook()
            {
                if (hook != IntPtr.Zero) return;
                MouseHookGC = proc;
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    hook = SetWindowsHookEx(WH_MOUSE_LL, MouseHookGC, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
            public static void Unhook()
            {
                if (hook != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(hook);
                    MouseHookGC = null;
                    MoveEvent = null;

                    hook = IntPtr.Zero;
                }
            }

            public static IntPtr proc(int nCode, uint wParam, IntPtr lParam)
            {
                if (nCode >= 0)
                {
                    if (wParam == WM_MOUSEMOVE && MoveEvent != null)
                    {
                        MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                        Delegate[] receivers = MoveEvent.GetInvocationList();
                        foreach (MoveEventHandler receiver in receivers) receiver.BeginInvoke(new MoveEventArgs(hookStruct.pt.x, hookStruct.pt.y), EndMoveEvent, null);
                    }
                    else if ((wParam == WM_LBUTTONDOWN || wParam == WM_RBUTTONDOWN) && ClickEvent != null)
                    {
                        MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                        //ClickEvent.BeginInvoke(new ClickEventArgs(hookStruct, wParam == WM_LBUTTONDOWN), EndClickEvent, null);

                        ClickEventArgs evt = new ClickEventArgs(hookStruct, wParam == WM_LBUTTONDOWN);
                        Delegate[] receivers = ClickEvent.GetInvocationList();
                        foreach (ClickEventHandler receiver in receivers) receiver.BeginInvoke(evt, EndClickEvent, null);
                    }
                }
                return CallNextHookEx(hook, nCode, wParam, lParam);
            }

            public delegate void MoveEventHandler(MoveEventArgs e);
            public static event MoveEventHandler MoveEvent;

            public delegate void ScrollEventHandler(ScrollEventArgs e);
            public static event ScrollEventHandler ScrollEvent;

            public delegate void ClickEventHandler(ClickEventArgs e);
            public static event ClickEventHandler ClickEvent;

            public enum MouseButton
            {
                Left,
                Middle,
                Right
            }

            public class MoveEventArgs : EventArgs
            {
                public int X, Y;

                public MoveEventArgs(int x, int y)
                {
                    X = x;
                    Y = y;
                }
            }

            public class ScrollEventArgs : EventArgs
            {
                public int X, Y, Delta;
                public bool Left, Right;

                public ScrollEventArgs(WinAPI.MSLLHOOKSTRUCT Struct, bool left, bool right)
                {
                    X = Struct.pt.x;
                    Y = Struct.pt.y;
                    Delta = Struct.mouseData;

                    Left = left;
                    Right = right;
                }
            }

            public class ClickEventArgs : EventArgs
            {
                public int X, Y;
                public MouseButton Button;

                public ClickEventArgs(WinAPI.MSLLHOOKSTRUCT Struct, bool button)
                {
                    X = Struct.pt.x;
                    Y = Struct.pt.y;

                    Button = (button ? MouseButton.Left : MouseButton.Right);
                }
            }

            #region Async event terminators
            public static void EndMoveEvent(IAsyncResult iar)
            {
                System.Runtime.Remoting.Messaging.AsyncResult ar = (System.Runtime.Remoting.Messaging.AsyncResult)iar;
                object o = ar.AsyncDelegate;
                MoveEventHandler invokedMethod = (MoveEventHandler)ar.AsyncDelegate;

                try { invokedMethod.EndInvoke(iar); }
                catch { }
            }

            public static void EndClickEvent(IAsyncResult iar)
            {
                System.Runtime.Remoting.Messaging.AsyncResult ar = (System.Runtime.Remoting.Messaging.AsyncResult)iar;
                object o = ar.AsyncDelegate;
                ClickEventHandler invokedMethod = (ClickEventHandler)ar.AsyncDelegate;

                try { invokedMethod.EndInvoke(iar); }
                catch { }
            }

            public static void EndScrollEvent(IAsyncResult iar)
            {
                System.Runtime.Remoting.Messaging.AsyncResult ar = (System.Runtime.Remoting.Messaging.AsyncResult)iar;
                object o = ar.AsyncDelegate;
                ScrollEventHandler invokedMethod = (ScrollEventHandler)ar.AsyncDelegate;

                try { invokedMethod.EndInvoke(iar); }
                catch { }
            }
            #endregion
        }
    }
}
