using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIODemo
{
    /// <summary>
    /// 压缩流
    /// </summary>
    public class CompressStreamHelper
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        public static void Main1()
        {
            string sourceFilePath = "C:\\MyFolder\\MyFile.txt";
            string zipFilePath = "C:\\MyFolder\\MyFile.zip";
            // 读取文本文件的内容
            string contentToCompress = File.ReadAllText(sourceFilePath);
            // 打开 Zip 文件以写入内容
            using (FileStream zipToCreate = new FileStream(zipFilePath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
                {
                    // 创建 ZipArchiveEntry 条目
                    ZipArchiveEntry entry = archive.CreateEntry("111.txt");

                    // 打开条目的内容流以写入数据
                    using (Stream entryStream = entry.Open())
                    {
                        // 将文本内容写入流
                        byte[] contentBytes = Encoding.UTF8.GetBytes(contentToCompress);
                        entryStream.Write(contentBytes, 0, contentBytes.Length);
                    }
                }
            }
        }
        /// <summary>
        /// 解压
        /// </summary>
        public static void Main2()
        {
            string zipFilePath = "C:\\MyFolder\\MyFile.zip";
            string decompressedFilePath = "C:\\MyFolder\\decompressed.txt";

            // 打开 Zip 文件以读取内容
            using (FileStream zipToRead = new FileStream(zipFilePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToRead, ZipArchiveMode.Read))
                {
                    // 获取 Zip 文件中的第一个条目（在这个例子中只有一个）
                    ZipArchiveEntry entry = archive.Entries[0];

                    // 打开解压后的文件以写入内容
                    using (FileStream decompressedFileStream = new FileStream(decompressedFilePath, FileMode.Create))
                    using (Stream entryStream = entry.Open())
                    using (StreamReader reader = new StreamReader(entryStream, Encoding.UTF8))
                    {
                        // 将 Zip 文件中的内容解压缩到新文件中
                        entryStream.CopyTo(decompressedFileStream);

                    }
                }
            }

            Console.WriteLine("File decompressed successfully.");
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        public static void Main3()
        {
            string sourceFolderPath = "C:\\MyFolder";
            string zipFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MyFolder.zip");
            // 压缩文件夹
            ZipFile.CreateFromDirectory(sourceFolderPath, zipFilePath);

            Console.WriteLine("Folder compressed successfully.");
        }
        /// <summary>
        /// 解压文件夹
        /// </summary>
        public static void Main4()
        {
            string zipFilePath = "compressed_folder.zip";
            string extractFolderPath = "extracted_folder";

            // 确保目标文件夹存在
            if (!Directory.Exists(extractFolderPath))
            {
                Directory.CreateDirectory(extractFolderPath);
            }

            // 使用 ZipFile 解压缩整个 Zip 文件到目标文件夹
            ZipFile.ExtractToDirectory(zipFilePath, extractFolderPath);

            Console.WriteLine("Zip file extracted successfully.");
        }
    }
}
