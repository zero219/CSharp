using DelegateDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegateDemo.Data
{
    public class InfoData
    {
        public delegate bool WithReturnPara(Student student);


        public List<Student> studentList;
        public InfoData()
        {
            studentList = new List<Student>()
            {
                new Student()
                {
                    Id=1,
                    Name="打兔子的猎人",
                    ClassId=2,
                    Age=35
                },
                new Student()
                {
                    Id=2,
                    Name="Alpha Go",
                    ClassId=2,
                    Age=23
                },
                 new Student()
                {
                    Id=3,
                    Name="白开水",
                    ClassId=2,
                    Age=27
                },
                 new Student()
                {
                    Id=4,
                    Name="狼牙道",
                    ClassId=2,
                    Age=26
                },
                new Student()
                {
                    Id=5,
                    Name="Nine",
                    ClassId=2,
                    Age=25
                },
                new Student()
                {
                    Id=6,
                    Name="Y",
                    ClassId=2,
                    Age=24
                },
                new Student()
                {
                    Id=1,
                    Name="小昶",
                    ClassId=2,
                    Age=21
                },
                 new Student()
                {
                    Id=1,
                    Name="yoyo",
                    ClassId=2,
                    Age=22
                },
                 new Student()
                {
                    Id=1,
                    Name="冰亮",
                    ClassId=2,
                    Age=34
                },
                 new Student()
                {
                    Id=1,
                    Name="瀚",
                    ClassId=2,
                    Age=30
                },
                new Student()
                {
                    Id=1,
                    Name="毕帆",
                    ClassId=1,
                    Age=30
                },
                new Student()
                {
                    Id=1,
                    Name="一点半",
                    ClassId=1,
                    Age=30
                },
                new Student()
                {
                    Id=1,
                    Name="小石头",
                    ClassId=1,
                    Age=28
                },
                new Student()
                {
                    Id=1,
                    Name="大海",
                    ClassId=2,
                    Age=30
                },
                 new Student()
                {
                    Id=3,
                    Name="yoyo",
                    ClassId=3,
                    Age=30
                },
                  new Student()
                {
                    Id=4,
                    Name="unknown",
                    ClassId=4,
                    Age=30
                }
            };
        }
        /// <summary>
        /// 委托的使用场景
        /// </summary>
        public void ShowMethod()
        {
            GetWhereData(studentList, AgeMethod);
            GetWhereData(studentList, IdMethod);

            //演变
            Func<Student, bool> func = new Func<Student, bool>((x) => x.Age > 25);
            GetWhereDataNew(studentList, func);
        }

        public List<Student> GetWhereData(List<Student> listStudent, WithReturnPara withReturnPara)
        {
            List<Student> list = new List<Student>();
            foreach (var student in listStudent)
            {

                if (withReturnPara.Invoke(student))
                {
                    list.Add(student);
                }
            }
            return list;
        }

        #region 演变
        public List<Student> GetWhereDataNew(List<Student> listStudent, Func<Student, bool> func)
        {
            List<Student> list = new List<Student>();
            foreach (var student in listStudent)
            {

                if (func.Invoke(student))
                {
                    list.Add(student);
                }
            }
            return list;
        }
        #endregion

        #region 初始调用方法
        public bool AgeMethod(Student student)
        {
            return student.Age > 25;
        }
        public bool IdMethod(Student student)
        {
            return student.Id > 5;
        }
        public void InfoDataMethod()
        {
            Console.WriteLine("我是InfoDataMethod");
        }
        #endregion
    }
}
