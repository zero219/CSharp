using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AttributesDemo.Attributes;

namespace AttributesDemo.Models
{
    [TableName(Tname = "User")]
    [Display(Name ="user")]
    public class User
    {
        [IntValidate(1, 10)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
