using AttributesDemo.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttributesDemo.Enums
{
    public enum Sex
    {
        [Remark("男的")]
        man = 1,
        [Remark("男的")]
        women = 2,
    }
}
