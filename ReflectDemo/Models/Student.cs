using System;

namespace ReflectDemo.Models
{
    public class Student
    {
        public Student(string name, int age, string sex)
        {
            this.name = name;
            this.age = age;
            this.sex = sex;
        }
        public Student(string sex)
        {
            this.sex = sex;
        }
        public Student()
        {

        }

        public int id;

        private string name;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int age;
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        private string sex;
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }



        public void Show()
        {
            Console.WriteLine("姓名：" + name + "\n" + "年龄：" + age + "\n" + "性别：" + sex);
        }
    }
}
