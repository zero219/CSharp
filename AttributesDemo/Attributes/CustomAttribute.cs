using System;
using System.Collections.Generic;
using System.Text;

namespace Attributes.Attributes
{
    /*
     * AttributeUsage特性来定义您想怎样使用这些特性
     * AttributeTargets.Class表示该特性只能作用于class上
     * Inherited = false表示该类型是非继承的
     * AttributeTargets.All表示可以对任何应用程序元素应用属性
     * AllowMultiple = true如果允许指定多个实例，则为 true；否则为 false。 默认值为 false。
     */
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    class CustomAttribute : Attribute
    {
        private string discretion;

        public string Discretion
        {
            get { return discretion; }
            set { discretion = value; }
        }
        public DateTime date;
        public CustomAttribute(string discretion)
        {
            this.discretion = discretion;
            date = DateTime.Now;
        }
    }
}
