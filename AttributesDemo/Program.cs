using Attributes.Attributes;
using AttributesDemo.Attributes;
using AttributesDemo.Enums;
using AttributesDemo.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AttributesDemo
{
    class Program
    {
        /* 
         * 
         * Attribute用来在代码中附加一些元信息，这些元信息可被编译器、.NET Framework、或者我们的程序使用。
         * 方法、属性、类等上都可以标注Attribute。
         * 特性继承Attribute类；命名一般以Attribute结尾，如果以Attribute结尾的话使用的时候可以省略Attribute。
         * 在Type、MethodInfo、PropertyInfo等上都可以调用object[] GetCustomAttributes(Type attributeType, bool inherit)获取标注的注解对象，因为同一个Attribute可能标注多次，所以返回值是数组。
         * 特性编译后是metadata
         */
        static void Main(string[] args)
        {
            //获取自定义的特性
            GetAttributeInfo(typeof(SampleBase));
            var result = GetRemarkMethod(Sex.women);
            Console.WriteLine(result);
            TableMethod(typeof(User));
            string msgSuccess = Insert<User>(new User() { Id = 2, Name = "zero219" });
            string msgFail = Insert<User>(new User() { Id = 0, Name = "zero219" });
            Console.WriteLine("{0}\n{1}", msgSuccess, msgFail);
            Console.ReadKey();
        }
        /// <summary>
        /// 使用反射,获取特性
        /// </summary>
        /// <param name="t"></param>
        public static void GetAttributeInfo(Type t)
        {
            //检索CustomAttribute
            var custom = (CustomAttribute)Attribute.GetCustomAttribute(t, typeof(CustomAttribute));
            if (custom == null)
            {
                Console.WriteLine(t.ToString() + "类中自定义特性不存在");
            }
            else
            {
                Console.WriteLine("特性描述:{0}\n加入事件时间:{1}", custom.Discretion, custom.date);

            }
        }
        /// <summary>
        /// 利用反射获取枚举字段中的特性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetRemarkMethod(Enum model)
        {
            if (model is Sex)
            {
                Type t = typeof(Sex);
                FieldInfo fi = t.GetField(model.ToString());
                object[] o = fi.GetCustomAttributes(true);
                foreach (var attr in o)
                {
                    if (attr is RemarkAttribute)
                    {
                        RemarkAttribute remark = (RemarkAttribute)attr;
                        return remark.GetRemark();
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 所有自定义属性的数组
        /// </summary>
        /// <param name="t"></param>
        public static void TableMethod(Type t)
        {
            Type type = t;
            object[] obj = type.GetCustomAttributes(true);
            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i] is TableNameAttribute)
                {
                    TableNameAttribute tnAttr = (TableNameAttribute)obj[i];
                    Console.WriteLine(tnAttr.Tname);
                }
                if (obj[i] is DisplayAttribute)
                {
                    DisplayAttribute tnAttr = (DisplayAttribute)obj[i];
                    Console.WriteLine(tnAttr.Name);
                }
            }
        }

        /// <summary>
        /// TableNameAttribute的tname和字段验证特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Insert<T>(T model)
        {
            string name = string.Empty;
            Type type = typeof(T);
            TableNameAttribute tableName = (TableNameAttribute)Attribute.GetCustomAttribute(type, typeof(TableNameAttribute));
            if (tableName == null)
            {
                return "TableName特性不存在";
            }
            name = tableName.Tname;
            //反射该类型的所有属性
            PropertyInfo[] infos = type.GetProperties();
            bool boIsCheck = true;
            foreach (var item in infos)
            {
                //找寻所有的特性的
                object[] o = item.GetCustomAttributes(true);
                if (item.PropertyType.Name.ToLower().Contains("int"))
                {
                    foreach (var attr in o)
                    {
                        IntValidateAttribute intValidate = (IntValidateAttribute)attr;
                        //执行特性的验证逻辑
                        boIsCheck = intValidate.Validate((int)item.GetValue(model));
                    }
                }
                if (!boIsCheck)
                {
                    break;
                }
            }
            if (boIsCheck)
            {
                return string.Format("验证通过，{0}插入数据库成功", name);
            }
            else
            {
                return string.Format("验证失败, {0}数据范围在1 - 10", name);
            }
        }
    }
}
