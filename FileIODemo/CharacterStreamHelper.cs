using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIODemo
{
    /// <summary>
    /// 字符流,主要针对文本类；字符输入输出流自带缓冲
    /// </summary>
    public static class CharacterStreamHelper
    {
        /// <summary>
        /// 字符输出流
        /// </summary>
        public static void Main1()
        {
            string parentPath = "C:\\MyFolder"; // 示例文件路径
            string subPath = "Output.txt";
            string filePath = Path.Combine(parentPath, subPath).Replace("\\", "/");
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (!File.Exists(filePath))
                {
                    // 判断目录是否存在
                    if (!Directory.Exists(parentPath))
                    {
                        // 创建目录
                        Directory.CreateDirectory(parentPath);
                        Console.WriteLine("目录已创建。");
                    }
                    // 创建一个StreamWriter对象，将文件流链接到该写入器
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        // 写入字符串数据
                        writer.WriteLine("Hello, File Output Stream!");
                        writer.WriteLine("很帅很低调！");
                        writer.WriteLine((char)97);

                        Console.WriteLine("输入完成");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("创建或截断文件时出错：" + ex.Message);
            }
        }
        /// <summary>
        /// 字符输入流，字符串方式读取 
        /// </summary>
        public static void Main2()
        {
            string parentPath = "C:\\MyFolder"; // 示例文件路径
            string subPath = "Output.txt";
            string filePath = Path.Combine(parentPath, subPath).Replace("\\", "/");

            // 使用StreamReader打开文件流并读取字符数据
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // 处理每一行的字符数据
                    Console.WriteLine(line);
                }
            }
        }
        /// <summary>
        /// 字符输入流，获取ASCII值，转字符
        /// </summary>
        public static void Main3()
        {
            string parentPath = "C:\\MyFolder"; // 示例文件路径
            string subPath = "Output.txt";
            string filePath = Path.Combine(parentPath, subPath).Replace("\\", "/");
            // 使用 StreamReader 打开文件流并读取字符
            using (StreamReader reader = new StreamReader(filePath))
            {
                int intValue;
                string value = "[";
                var character = "";
                while ((intValue = reader.Read()) != -1)
                {
                    character = string.Concat(character, (char)intValue);
                    value = string.Concat(value, intValue, ",");
                }
                value += "]";
                Console.WriteLine($"ASCII值: {value}, Character: {character.ToString()}");
            }
        }

        /// <summary>
        /// 转换流
        /// </summary>
        public static void Main4()
        {
            // 注册 GBK 编码提供程序
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string utf8FilePath = "C:\\MyFolder\\input_utf8.txt";  // 输入文件的路径（UTF-8 编码）
            string gbkFilePath = "C:\\MyFolder\\output_gbk.txt"; // 输出文件的路径（GBK 编码）
            // 使用 StreamReader 读取 UTF-8 编码的文件
            using (StreamReader gbkReader = new StreamReader(utf8FilePath, Encoding.UTF8))
            {
                // 使用 StreamWriter 写入GBK 编码的文件
                using (StreamWriter utf8Writer = new StreamWriter(gbkFilePath, false, Encoding.GetEncoding("GBK")))
                {
                    string line;
                    while ((line = gbkReader.ReadLine()) != null)
                    {
                        // 写入 UTF-8 编码的文件
                        utf8Writer.WriteLine(line);
                    }
                }
            }
        }
    }
}
