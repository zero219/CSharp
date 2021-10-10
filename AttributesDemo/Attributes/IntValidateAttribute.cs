using System;
using System.Collections.Generic;
using System.Text;

namespace AttributesDemo.Attributes
{
    /// <summary>
    /// 验证特性
    /// </summary>
    public class IntValidateAttribute : Attribute
    {
        /// <summary>
        /// 最小值
        /// </summary>
        private int minValue { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        private int maxValue { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public IntValidateAttribute(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        /// <summary>
        /// 检验值是否合法
        /// </summary>
        /// <param name="checkValue"></param>
        /// <returns></returns>
        public bool Validate(int checkValue)
        {
            return checkValue >= minValue && checkValue <= maxValue;
        }
    }
}
