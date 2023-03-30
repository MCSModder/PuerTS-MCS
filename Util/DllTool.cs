using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PuerTsPlugin.PuerTS_MCS.Util
{
    public class DllTool
    {
        /// <summary>
        /// 手动加载 DLL 文件方法
        /// </summary>
        /// <param name="dir">DLL 文件存放路径</param>
        /// <param name="fileName">DLL 文件名称</param>
        public static void LoadDllFile(string dir, string fileName)
        {
            StringBuilder builder = new StringBuilder(255);
            GetDllDirectory(builder.Length, builder);
            SetDllDirectory(dir);
            LoadLibrary(fileName);
            SetDllDirectory(builder.ToString());
        }

        /// <summary>
        /// 委托 kernel32 获取 DLL 加载路径
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int GetDllDirectory(int bufferSize, StringBuilder builder);

        /// <summary>
        /// 委托 kernel32 设置 DLL 加载路径
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetDllDirectory(string dir);

        /// <summary>
        /// 委托 kernel32 加载 DLL 文件
        /// </summary>
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string fileName);
    }
}