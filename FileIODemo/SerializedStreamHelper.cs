using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileIODemo
{
    /// <summary>
    /// 序列化流
    /// </summary>
    public class SerializedStreamHelper
    {
        // 创建一个包含多个对象的列表
        private static List<Person> peopleList = new List<Person>
        {
            new Person { Name = "John Doe", Age = 30 },
            new Person { Name = "Jane Doe", Age = 25 },
            // 添加更多 Person 对象...
        };
        /// <summary>
        /// 序列化流
        /// </summary>
        public static void Main1()
        {
            // 创建一个对象
            Person person = new Person { Name = "John Doe", Age = 30 };

            // 序列化对象到流
            using (MemoryStream memoryStream = new MemoryStream())
            {
                JsonSerializer.Serialize(memoryStream, peopleList);

                // 在这里你可以将 memoryStream 中的数据写入文件、发送到网络等
                File.WriteAllBytes("C:\\MyFolder\\output.txt", memoryStream.ToArray());
            }
        }
        /// <summary>
        /// 反序列化流
        /// </summary>
        public static void Main2()
        {
            // 读取文件中的二进制数据并反序列化为对象
            byte[] dataFromFile = File.ReadAllBytes("C:\\MyFolder\\output.txt");
            using (MemoryStream memoryStream = new MemoryStream(dataFromFile))
            {
                // 使用 System.Text.Json.JsonSerializer 进行反序列化
                List<Person> deserializedPeopleList = JsonSerializer.Deserialize<List<Person>>(memoryStream);

                // 打印反序列化后的列表中的对象属性
                foreach (Person person in deserializedPeopleList)
                {
                    Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
                }
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
