using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using PuerTsPlugin.PuerTS_MCS.Manager;
using PuerTsPlugin.PuerTS_MCS.Util;

namespace PuerTsPlugin.PuerTS_MCS
{
    /// <summary>
    /// PuerTS_MCS 插件入口方法
    /// </summary>
    [BepInPlugin("PuerTsPlugin.PuerTS_MCS", "PuerTS_MCS", "0.1.0")]
    public class Main: BaseUnityPlugin
    {
        /// <summary>
        /// PuerTS_MCS 插件版本信息
        /// </summary>
        public const string Version = "0.1.0";
        /// <summary>
        /// PuerTS_MCS 插件 Main 方法单例对象
        /// </summary>
        public static Main Instance { get; private set; }
        /// <summary>
        /// 项目 DLL 文件所在路径
        /// </summary>
        public readonly string ModPath = Directory.GetParent(typeof(Main).Assembly.Location)?.FullName;
        /// <summary>
        /// 引用 DLL 文件名列表
        /// </summary>
        public List<string> dllFiles;
        /// <summary>
        /// JsEnv 管理器单例对象
        /// </summary>
        public static JsEnvManager JsEnvManager => JsEnvManager.Inst;
        /// <summary>
        /// 封装静态 Log 方法
        /// </summary>
        /// <param name="message">普通信息</param>
        public static void Log(object message) => Instance.LoggerInfo(message);
        /// <summary>
        /// 封装静态 Log 方法
        /// </summary>
        /// <param name="message">普通信息</param>
        public static void LogInfo(object message) => Instance.LoggerInfo(message);
        /// <summary>
        /// 封装静态 Log 方法
        /// </summary>
        /// <param name="message">警告信息</param>
        public static void LogWarn(object message) => Instance.LoggerWarn(message);
        /// <summary>
        /// 封装静态 Log 方法
        /// </summary>
        /// <param name="message">异常信息</param>
        public static void LogError(object message) => Instance.LoggerError(message);

        private void Awake()
        {
            LoggerInfo("开始加载 PuerTS 数据...");
            Instance = this;
            InitDllFile();
            InitManager();
            LoggerInfo("PuerTS 数据加载完成");
        }

        /// <summary>
        /// 初始化外部 DLL 文件
        /// </summary>
        private void InitDllFile()
        {
            string dllPath = new Lazy<string>(() => Utility.CombinePaths(ModPath, "Library")).Value;
            
            if (dllPath.IsNullOrWhiteSpace())
            {
                return;
            }
            
            LoggerInfo("正在加载 DLL 引用...");

            dllFiles = Directory.GetFiles(dllPath).Select(Path.GetFileName).ToList();
            foreach (string dllName in dllFiles)
            {
                DllTool.LoadDllFile(dllPath, dllName);
            }
        }

        /// <summary>
        /// 初始化 Manager
        /// </summary>
        private void InitManager()
        {
            gameObject.AddComponent<JsEnvManager>();
        }

        /// <summary>
        /// 封装控制台常规信息打印方法
        /// </summary>
        /// <param name="message">常规信息</param>
        public void LoggerInfo(object message)
        {
            Logger.LogInfo(message);
        }
        
        /// <summary>
        /// 封装控制台警告信息打印方法
        /// </summary>
        /// <param name="message">警告信息</param>
        public void LoggerWarn(object message)
        {
            Logger.LogWarning(message);
        }
        
        /// <summary>
        /// 封装控制台异常信息打印方法
        /// </summary>
        /// <param name="message">异常信息</param>
        public void LoggerError(object message)
        {
            Logger.LogError(message);
        }
    }
}