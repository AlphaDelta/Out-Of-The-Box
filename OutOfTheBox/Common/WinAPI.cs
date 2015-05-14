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
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);
        public delegate IntPtr LowLevelProc(int nCode, uint wParam, IntPtr lParam);

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
        [StructLayout(LayoutKind.Sequential)]
        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        public static LowLevelProc MouseHookGC;
        public static LowLevelProc KeyboardHookGC;

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

        public class KeyboardHook
        {
            public static bool[] States = new bool[0xFF];
            public static string[] Names = new string[0xFF]
            {
                "VK_LBUTTON", /* Left mouse button */
                "VK_RBUTTON", /* Right mouse button */
                "VK_CANCEL",
                "VK_MBUTTON", /* Middle mouse button*/
                "VK_XBUTTON1", /* X mouse button 1 */
                "VK_XBUTTON2", /* X mouse button 2 */
                "Undefined",
                "VK_BACK",
                "VK_TAB",
                "Reserved",
                "Reserved",
                "VK_CLEAR",
                "VK_RETURN",
                "Undefined",
                "Undefined",
                "VK_SHIFT",
                "VK_CONTROL",
                "VK_MENU", /* Alt */
                "VK_PAUSE",
                "VK_CAPITAL", /* Capslock */
                "VK_KANA",
                "Undefined",
                "VK_JUNJA",
                "VK_FINAL",
                "VK_HANJA",
                "VK_KANJI",
                "Undefined",
                "VK_ESCAPE",
                "VK_CONVERT",
                "VK_NONCONVERT",
                "VK_ACCEPT",
                "VK_MODECHANGE",
                "VK_SPACE",
                "VK_PRIOR",
                "VK_NEXT",
                "VK_END",
                "VK_HOME",
                "VK_LEFT",
                "VK_UP",
                "VK_RIGHT",
                "VK_DOWN",
                "VK_SELECT",
                "VK_PRINT",
                "VK_EXECUTE",
                "VK_SNAPSHOT", /* PRINT SCRN */
                "VK_INSERT",
                "VK_DELETE",
                "VK_HELP",
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "Undefined",
                "Undefined",
                "Undefined",
                "Undefined",
                "Undefined",
                "Undefined",
                "Undefined",
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "I",
                "J",
                "K",
                "L",
                "M",
                "N",
                "O",
                "P",
                "Q",
                "R",
                "S",
                "T",
                "U",
                "V",
                "W",
                "X",
                "Y",
                "Z",
                "VK_LWIN",
                "VK_RWIN",
                "VK_APPS",
                "Reserved",
                "VK_SLEEP",
                "VK_NUMPAD0",
                "VK_NUMPAD1",
                "VK_NUMPAD2",
                "VK_NUMPAD3",
                "VK_NUMPAD4",
                "VK_NUMPAD5",
                "VK_NUMPAD6",
                "VK_NUMPAD7",
                "VK_NUMPAD8",
                "VK_NUMPAD9",
                "VK_MULTIPLY",
                "VK_ADD",
                "VK_SEPARATOR",
                "VK_SUBTRACT",
                "VK_DECIMAL",
                "VK_DIVIDE",
                "VK_F1",
                "VK_F2",
                "VK_F3",
                "VK_F4",
                "VK_F5",
                "VK_F6",
                "VK_F7",
                "VK_F8",
                "VK_F9",
                "VK_F10",
                "VK_F11",
                "VK_F12",
                "VK_F13",
                "VK_F14",
                "VK_F15",
                "VK_F16",
                "VK_F17",
                "VK_F18",
                "VK_F19",
                "VK_F20",
                "VK_F21",
                "VK_F22",
                "VK_F23",
                "VK_F24",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "VK_NUMLOCK",
                "VK_SCROLL",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "VK_LSHIFT",
                "VK_RSHIFT",
                "VK_LCONTROL",
                "VK_RCONTROL",
                "VK_LMENU",
                "VK_RMENU",
                "VK_BROWSER_BACK",
                "VK_BROWSER_FORWARD",
                "VK_BROWSER_REFRESH",
                "VK_BROWSER_STOP",
                "VK_BROWSER_SEARCH",
                "VK_BROWSER_FAVORITES",
                "VK_BROWSER_HOME",
                "VK_VOLUME_MUTE",
                "VK_VOLUME_DOWN",
                "VK_VOLUME_UP",
                "VK_MEDIA_NEXT_TRACK",
                "VK_MEDIA_PREV_TRACK",
                "VK_MEDIA_STOP",
                "VK_MEDIA_PLAY_PAUSE",
                "VK_LAUNCH_MAIL",
                "VK_LAUNCH_MEDIA_SELECT",
                "VK_LAUNCH_APP1",
                "VK_LAUNCH_APP2",
                "Reserved",
                "Reserved",
                "VK_OEM_1",
                "VK_OEM_PLUS",
                "VK_OEM_COMMA",
                "VK_OEM_MINUS",
                "VK_OEM_PERIOD",
                "VK_OEM_2",
                "VK_OEM_3",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Reserved",
                "Unassigned",
                "Unassigned",
                "Unassigned",
                "VK_OEM_4",
                "VK_OEM_5",
                "VK_OEM_6",
                "VK_OEM_7",
                "VK_OEM_8",
                "Reserved",
                "OEM specific",
                "VK_OEM_102",
                "OEM specific",
                "OEM specific",
                "VK_PROCESSKEY",
                "OEM specific",
                "VK_PACKET",
                "Unassigned",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "OEM specific",
                "VK_ATTN",
                "VK_CRSEL",
                "VK_EXSEL",
                "VK_EREOF",
                "VK_PLAY",
                "VK_ZOOM",
                "VK_NONAME",
                "VK_PA1",
                "VK_OEM_CLEAR"
            };

            static IntPtr hook = IntPtr.Zero;
            public static void Hook()
            {
                if (hook != IntPtr.Zero) return;
                KeyboardHookGC = proc;
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    hook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookGC, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
            public static void Unhook()
            {
                if (hook != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(hook);
                    KeyboardHookGC = null;
                    DownEvent = null;

                    hook = IntPtr.Zero;
                }
            }

            public static IntPtr proc(int nCode, uint wParam, IntPtr lParam)
            {
                if (nCode >= 0)
                {
                    if (wParam == WM_KEYDOWN && DownEvent != null)
                    {
                        KBDLLHOOKSTRUCT hookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                        Delegate[] receivers = DownEvent.GetInvocationList();
                        foreach (DownEventHandler receiver in receivers) receiver.BeginInvoke(new DownEventArgs(hookStruct.vkCode), EndDownEvent, null);
                    }
                }
                return CallNextHookEx(hook, nCode, wParam, lParam);
            }

            public delegate void DownEventHandler(DownEventArgs e);
            public static event DownEventHandler DownEvent;

            public class DownEventArgs : EventArgs
            {
                public int Key;

                public DownEventArgs(int key)
                {
                    Key = key;
                }
            }

            #region Async event terminators
            public static void EndDownEvent(IAsyncResult iar)
            {
                System.Runtime.Remoting.Messaging.AsyncResult ar = (System.Runtime.Remoting.Messaging.AsyncResult)iar;
                object o = ar.AsyncDelegate;
                DownEventHandler invokedMethod = (DownEventHandler)ar.AsyncDelegate;

                try { invokedMethod.EndInvoke(iar); }
                catch { }
            }
            #endregion
        }
    }
}
