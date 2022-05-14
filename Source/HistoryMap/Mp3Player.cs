using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HistoryMap
{
    class Mp3Player
    {
        #region   -定义属性-

        /* extern修饰符用于声明在外部实现的方法, extern 修饰符的常见用法是在使用Interop 服务调入非托管代码时与 DllImport 属性一起使用；在这种情况下，该方法还必须声明为 static*/

        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(String command, StringBuilder buffer, Int32 bufferSize, IntPtr hwndCallback);
        private string m_musicPath = "";
        private IntPtr m_Handle;

        #endregion



        #region -定义构造函数-

        public Mp3Player(string musicPath, IntPtr Handle)
        {
            m_musicPath = musicPath;
            m_Handle = Handle;
        }

        #endregion


        #region - 播放音乐 -
        public void Open(string path)
        {
            if (path != "")
            {
                try
                {
                    mciSendString("open " + path + " alias media", null, 0, m_Handle);
                    mciSendString("play media", null, 0, m_Handle);
                }
                catch (Exception)
                {

                }
            }
        }

        public void Open()
        {
            Open(m_musicPath);
        }
        #endregion

        #region - 停止音乐播放 -
        void CloseMedia()
        {
            try
            {
                mciSendString("close all", null, 0, m_Handle);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}