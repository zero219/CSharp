using ReflectDemo.Models;
using System;
using System.Reflection;

namespace ReflectDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student();
            //对象的获取
            Type type1 = student.GetType();
            //类名获取
            Type type2 = typeof(Student);
            //命名空间获取
            Type type3 = Type.GetType("ReflexDemo.Models.Student");
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// 反射构造函数
        /// </summary>
        public static void Constructor()
        {
            Student Student = new Student();
            Type type = Student.GetType();
            //获取所有的公共构造函数
            ConstructorInfo[] infos = type.GetConstructors();
            foreach (ConstructorInfo ci in infos)
            {
                ParameterInfo[] parameterInfos = ci.GetParameters();
                foreach (ParameterInfo pi in parameterInfos)
                {
                    //参数类型和参数名称
                    Console.WriteLine(pi.ParameterType.ToString() + "\n" + pi.Name + "\n");

                }
            }
            Console.ReadLine();
        }
        /// <summary>
        /// 用构造函数生成对象
        /// </summary>
        public static void ObjectPro()
        {
            Type type = typeof(Student);
            Type[] types = new Type[3];
            types[0] = typeof(string);
            types[1] = typeof(int);
            types[2] = typeof(string);
            ConstructorInfo ci = type.GetConstructor(types);
            object[] obj = new object[3] { "Admin", 21, "男" };

            #region 调用构造函数生成对象
            object o = ci.Invoke(obj);
            #endregion 

            #region 用Activator的CreateInstance静态方法，生成新对象
            //object o = Activator.CreateInstance(type, obj);
            #endregion

            ((Student)o).Show();
        }

        /// <summary>
        /// 查看类中属性
        /// </summary>
        public static void Properties()
        {
            Student Student = new Student();
            //当前类中所有的公共属性
            PropertyInfo[] infos = Student.GetType().GetProperties();
            foreach (PropertyInfo pi in infos)
            {
                Console.WriteLine(pi.Name);
            }
        }

        /// <summary>
        /// 类中方法
        /// </summary>
        public static void Method()
        {
            Student Student = new Student();
            //当前类中所有的公共方法
            MethodInfo[] infos = Student.GetType().GetMethods();
            foreach (MethodInfo mi in infos)
            {
                Console.WriteLine(mi.ReturnType + "\n" + mi.Name + "\n");
            }

            #region 获取单个方法
            MethodInfo info = Student.GetType().GetMethod("Show");
            Console.WriteLine(info.ReturnType + "\n" + info.Name + "\n");
            #endregion
        }

        /// <summary>
        /// 类中字段
        /// </summary>
        public static void Field()
        {
            Student Student = new Student();
            //当前所有的公共字段
            FieldInfo[] info = Student.GetType().GetFields();
            foreach (FieldInfo fi in info)
            {
                Console.WriteLine(fi.Name);
            }
        }

        /// <summary>
        /// 动态创建类
        /// </summary>
        public static void OperationrRef()
        {
            Student Student = new Student();
            Type t = Student.GetType();
            //动态创建类，这个类必须要public且无参构造函数
            object o = Activator.CreateInstance(t);
            FieldInfo info = t.GetField("id");
            //给ID字段赋值
            info.SetValue(o, 4);

            PropertyInfo propertyInfoName = t.GetProperty("Name");
            //属性赋值
            propertyInfoName.SetValue(o, "Admin", null);

            PropertyInfo propertyInfoAge = t.GetProperty("Age");
            propertyInfoAge.SetValue(o, 18, null);

            PropertyInfo propertyInfoSex = t.GetProperty("Sex");
            propertyInfoSex.SetValue(o, "男", null);

            MethodInfo memberInfo = t.GetMethod("Show");
            memberInfo.Invoke(o, null);

            Console.WriteLine("ID为：" + ((Student)o).id);
        }
        /// <summary>
        /// 反射类
        /// </summary>
        public static void Assemblys()
        {
            Assembly assembly = Assembly.Load("ReflexDemo");
            Type t = assembly.GetType("ReflexDemo.Models.Student"); //参数必须是类的全名
            object o = Activator.CreateInstance(t, "男");
            MethodInfo mi = t.GetMethod("Show");
            //调用当前实例
            mi.Invoke(o, null);
        }
        /// <summary>
        /// 反射DLL
        /// </summary>
        public static void AssmblysDll()
        {
            Assembly assembly = Assembly.LoadFrom("Newtonsoft.Json.dll");
            Type[] tArray = assembly.GetTypes();
            foreach (Type t in tArray)
            {
                Console.WriteLine(t.Name);
            }
        }
    }
}
