using Attributes.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttributesDemo.Models
{
    [Custom("我是自定义特性")]
    public class SampleBase
    {
        public SampleBase()
        {
            Console.WriteLine("我是SampleBase类");
        }
    }
}
