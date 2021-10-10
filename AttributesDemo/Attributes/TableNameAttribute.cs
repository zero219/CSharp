using AttributesDemo.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttributesDemo.Attributes
{
    /// <summary>
    /// 表名称
    /// </summary>
    public class TableNameAttribute : Attribute
    {
        public TableNameAttribute()
        {

        }
        public string Tname { get; set; }
       
    }
}
