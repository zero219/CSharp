using System;
using System.Collections.Generic;
using System.Text;

namespace AttributesDemo.Attributes
{
    public class RemarkAttribute : Attribute
    {
        private string Remark { get; set; }
        public RemarkAttribute(string _remark)
        {
            this.Remark = _remark;
        }
        public string GetRemark()
        {
            return this.Remark;
        }
    }
}
