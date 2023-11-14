using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIODemo
{
    /// <summary>
    /// 字节输入输出流
    /// </summary>
    public static class ByteStreamHelper
    {
        /// <summary>
        /// 文件输出流，程序向文件输出
        /// </summary>
        public static void Main1()
        {
            string parentPath = "C:\\MyFolder"; // 示例文件路径
            string subPath = "MyFile.txt";
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
                    /**
                     * 创建一个FileStream对象
                     * FileMode.Create 表示：如果文件不存在将创建文件，如果文件存在则覆盖
                     * FileMode.Append 表示：如果文件不存在将创建文件，如果文件存在则追加数据
                     * 使用using语句的情况下，它会在作用域结束时自动关闭流
                     * 如果没有使用using,则使用fileStream.Close()显式关闭流
                     * 输出流换行：windows：\r\n；linux\macos：\n
                     */

                    /**
                     * 创建一个BinaryWriter，输入小写字母a(ASCII值97)
                     */
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                    {
                        // 创建一个字节数组并写入数据
                        byte[] data = Encoding.UTF8.GetBytes("很帅很低调!");
                        fileStream.Write(data, 0, data.Length);
                        binaryWriter.Write(97);
                    }
                    Console.WriteLine("写入完成...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("创建或截断文件时出错：" + e.Message);
            }

        }
        /// <summary>
        /// 文件输入流,BinaryReader对象读取
        /// </summary>
        public static void Main2()
        {
            string filePath = "C:\\MyFolder\\MyFile.txt"; // 输入文件的路径

            // 打开文件流并创建 BinaryReader 对象
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                // 读取字节数组
                byte[] byteArray = reader.ReadBytes(1024 * 1024 * 5); // 读取5MB字节

                // 处理字节数组
                foreach (byte b in byteArray)
                {
                    Console.Write(b + " ");
                }
                Console.WriteLine();
                string text = Encoding.UTF8.GetString(byteArray);
                Console.WriteLine(text);
            }
        }

        /// <summary>
        /// 字节输入流
        /// </summary>
        public static void Main3()
        {
            string filePath = "C:\\MyFolder\\MyFile.txt"; // 输入文件的路径
            // 使用StreamReader打开文件流并读取文本
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int bytesRead;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // 将字节数组转换为字符串
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // 打印到控制台
                    Console.Write(data);
                }
            }
        }
        /// <summary>
        /// 字节缓冲输出流，BufferedStream
        /// </summary>
        public static void Main4()
        {
            string filePath = "C:\\MyFolder\\MyFile.txt"; // 输入文件的路径

            // 使用FileStream打开文件流
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                // 创建BufferedStream来包装文件流
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    byte[] data = Encoding.UTF8.GetBytes("Hello, BufferedStream!");

                    // 向BufferedStream中写入数据
                    bufferedStream.Write(data, 0, data.Length);
                }
            }
        }
        /// <summary>
        /// 字符缓冲输入流
        /// </summary>
        public static void Main5()
        {
            string filePath = "C:\\MyFolder\\MyFile.txt"; // 输入文件的路径

            // 使用 FileStream 打开文件流
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                // 创建 BufferedStream 来包装文件流
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    byte[] buffer = new byte[1024 * 1024 * 5];
                    int bytesRead;

                    // 从 BufferedStream 中读取数据
                    while ((bytesRead = bufferedStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // 将字节数组转换为字符串
                        string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        // 打印到控制台
                        Console.WriteLine(data);
                    }
                }
            }
        }
    }
}
