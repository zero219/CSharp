using GenericDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericDemo
{
    class Program
    {
        /*
         * https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/generics/generic-type-parameters
         * 泛型:通过泛型可以编写一个与任何数据类型一起工作的类或方法
         * 泛型方法
         * 泛型类:调用泛型类必须先声明类型;
         *        如果一个泛型类实现一个接口，则该类的所有实例均可强制转换为该接口。      
         * 泛型接口
         * 泛型委托
         * 泛型数组
         * 泛型事件
         * 泛型约束:多个泛型约束是且的关系
         * 
         * 协变:关键字out,修饰返回值
         * 逆变:关键字in,修饰传入参数
         * 协变、逆变只能放在接口或者委托的泛型参数前
         */

        #region 泛型委托
        public delegate string ShowSome<Tdelegate>(Tdelegate item);
        #endregion

        static void Main(string[] args)
        {
            StudentService generic = new StudentService();
            generic.GenericMethod<string>("调用泛型方法");
            GenericType<Student> student = new GenericType<Student>();
            student.GenericMethod<int>(3);
            //协变
            List<Teacher> list = new List<Student>().Select(s => (Teacher)s).ToList();
            IFlyOut<Teacher> flyOut = new FlyOut<Student>();
            //逆变
            IFlyIn<Student> flyIn = new FlyIn<Teacher>();
            Console.ReadKey();
        }

       

    }
}
