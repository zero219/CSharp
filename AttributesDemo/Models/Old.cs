using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributesDemo.Models
{
    /*
     *  Obsolete系统自带的特性。
     *  当我们实例化该类的时候，编译器直接告诉我们该类已过时，运行的时候直接报错。
     */
    [Obsolete("该类已经过时", true)]
    public class Old
    {
        [method: Obsolete("该方法已经过时")]
        public void OldMethod()
        {
            Console.WriteLine("我是一个方法");
        }
    }
}
