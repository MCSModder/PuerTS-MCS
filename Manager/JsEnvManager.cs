using System.Collections.Generic;
using System.Linq;
using PuerTsPlugin.PuerTS_MCS.Loader;
using PuerTsPlugin.PuerTS_MCS.Puerts;
using UnityEngine;

namespace PuerTsPlugin.PuerTS_MCS.Manager
{
    /// <summary>
    /// JsEnv 管理器
    /// </summary>
    public class JsEnvManager: MonoBehaviour
    {
        /// <summary>
        /// JsEnvManager 单例对象
        /// </summary>
        public static JsEnvManager Inst;
        /// <summary>
        /// JsEnv 实例
        /// </summary>
        public JsEnv JsEnv;

        private void Awake()
        {
            Main.Log("正在加载 JsEnvManager 数据...");
            
            List<string> dllFiles = Main.Instance.dllFiles;
            
            if (!dllFiles.Any())
            {
                // 未能正确加载到引用 DLL 文件，直接销毁
                Destroy(this);
            }

            Inst = this;
        }

        private void Start()
        {
            InitJsEnv();
        }

        private void Update()
        {
            JsEnv?.Tick();
        }

        private void InitJsEnv()
        {
            Main.Log("初始化 JsEnv 数据...");
            JsEnv = new JsEnv(new McsLoader());
            JsEnv.Eval(@"
console.log('测试内容')
// 打印了obj变量
// 虽然是JS触发的，但实际上是C#调用JS函数，完成了console.log");
        }
    }
}