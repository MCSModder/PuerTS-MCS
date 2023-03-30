using System;
using System.IO;
using BepInEx;
using PuerTsPlugin.PuerTS_MCS.Puerts;

namespace PuerTsPlugin.PuerTS_MCS.Loader
{
    /// <summary>
    /// 觅长生数据加载器
    /// </summary>
    public class McsLoader: ILoader, IModuleChecker
    {
        /// <summary>
        /// 验证 PuerTs 文件是否存在
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public bool FileExists(string fileName)
        {
            string puerTsPath = new Lazy<string>(() => Utility.CombinePaths(Main.Instance.ModPath)).Value;
            return File.Exists(Utility.CombinePaths(puerTsPath, fileName));
        }

        /// <summary>
        /// 文件读取方式
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="debugPath">文件路径</param>
        /// <returns>读取的文件数据或空</returns>
        public string ReadFile(string fileName, out string debugPath)
        {
            string filePath = Utility.CombinePaths(Main.Instance.ModPath, fileName);
            Main.Log($"读取数据文件: {filePath}\\{fileName}");
            if (File.Exists(filePath))
            {
                debugPath = filePath;
                return File.ReadAllText(filePath);
            }

            debugPath = string.Empty;
            return debugPath;
        }

        public bool IsESM(string filepath)
        {
            return filepath.Length >= 4 && filepath.EndsWith(".mjs");
        }
    }
}