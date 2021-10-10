using System;
using System.Collections.Generic;
using System.Text;

namespace GenericDemo.Models
{
    #region 泛型类型
    public class GenericType<T> : IGenericType<T> where T : new()
    {
        #region 泛型属性
        public T Name { get; set; }
        #endregion

        #region 泛型方法
        public void GenericMethod<TObject>(TObject tParameter)
        {
            Console.WriteLine($"我是一个泛型方法,我的泛型类型{typeof(TObject)}，{tParameter}");
        }

        #endregion
    }
    #endregion

    #region 泛型接口
    public interface IGenericType<T> where T : new()
    {
        void GenericMethod<TObject>(TObject tParameter);
    }

    #endregion
}
