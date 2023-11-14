using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIODemo
{
    /// <summary>
    /// 文件、文件夹操作
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 从父路径名字符串和子路径名字符串创建新的 File实例
        /// </summary>
        public static void Main1()
        {
            string parentPath = "C:\\ParentFolder"; // 父路径名字符串
            string subPath = "SubFolder\\MyFile.txt"; // 子路径名字符串

            /**
             * 使用 Path.Combine 将父路径和子路径组合在一起；
             * 使用 Path.Combine 来创建通用路径，使用正斜杠作为分隔符
             * Path.Combine 方法用于创建文件路径，并使用 .Replace("\\", "/") 来确保使用正斜杠分隔符。
             * 需要注意的是，在C#中通常使用正斜杠 / 作为路径分隔符，因为它是跨平台的。
             * 而在Windows上，反斜杠 \ 也是有效的分隔符，但在Linux和macOS上，使用反斜杠可能会导致问题。
             * 因此，为了实现多类型系统的兼容性，建议在路径中使用正斜杠。
             */

            string fullPath = Path.Combine(parentPath, subPath).Replace("\\", "/");

            // 创建一个 FileInfo 对象，将组合后的路径名字符串转换为抽象路径名
            FileInfo fileInfo = new FileInfo(fullPath);

            // 现在你可以使用 fileInfo 对象来访问文件的属性和进行操作
            Console.WriteLine("文件名: " + fileInfo.Name);
            Console.WriteLine("文件扩展名: " + fileInfo.Extension);
        }

        public static void Main2()
        {
            string filePath = "C:\\MyFolder\\MyFile.txt"; // 示例文件路径

            // 创建一个 FileInfo 对象
            FileInfo fileInfo = new FileInfo(filePath);

            // 获取文件的绝对路径名字符串
            string absolutePath = fileInfo.FullName;
            Console.WriteLine("文件的绝对路径名字符串: " + absolutePath);

            // 获取文件或目录的名称
            string name = fileInfo.Name;
            Console.WriteLine("文件或目录的名称: " + name);

            // 检查文件或目录是否存在
            bool exists = fileInfo.Exists;
            if (exists)
            {
                Console.WriteLine("文件或目录存在。");

                // 获取文件的长度（以字节为单位）
                long length = fileInfo.Length;
                Console.WriteLine("文件的长度: " + length + " 字节");
            }
            else
            {
                Console.WriteLine("文件或目录不存在。");
            }

            // 检查文件是否为目录
            bool isDirectory = (fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
            if (isDirectory)
            {
                Console.WriteLine("此文件表示的是一个目录。");
            }
            else
            {
                Console.WriteLine("此文件表示的不是一个目录。");
            }
        }
        /// <summary>
        /// 创建、删除文件
        /// </summary>
        public static void Main3()
        {
            string parentPath = "C:\\MyFolder"; // 示例文件路径
            string subPath = "MyFile.txt";
            string filePath = Path.Combine(parentPath, subPath).Replace("\\", "/");
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (File.Exists(filePath))
                {
                    // 删除文件
                    fileInfo.Delete();
                    Console.WriteLine("文件已被删除。");
                }
                else
                {
                    // 判断目录是否存在
                    if (!Directory.Exists(parentPath))
                    {
                        // 创建目录
                        Directory.CreateDirectory(parentPath);
                        Console.WriteLine("目录已创建。");
                    }
                    // 创建一个新的空文件，如果文件已经存在，则会将其截断为空文件
                    FileStream fileStream = File.Create(filePath);
                    fileStream.Close();
                    Console.WriteLine("空文件已创建或截断。");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("创建或截断文件时出错：" + e.Message);
            }
        }

        /// <summary>
        /// 目录的遍历
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void TraverseDirectory(string directoryPath)
        {
            try
            {
                // 获取目录中的文件
                string[] files = Directory.GetFiles(directoryPath);
                foreach (string file in files)
                {
                    Console.WriteLine("文件: " + file);
                }

                // 获取目录中的子目录
                string[] subDirectories = Directory.GetDirectories(directoryPath);
                foreach (string subDirectory in subDirectories)
                {
                    Console.WriteLine("子目录: " + subDirectory);

                    // 递归遍历子目录
                    TraverseDirectory(subDirectory);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("遍历目录时出错：" + e.Message);
            }
        }
    }
}
