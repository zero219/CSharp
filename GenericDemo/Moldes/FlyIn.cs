using System;
using System.Collections.Generic;
using System.Text;

namespace GenericDemo.Models
{
    /*
     * 逆变
     */

    public class FlyIn<inT> : IFlyIn<inT>
    {
        public void Show(inT inT)
        {
            throw new NotImplementedException();
        }
    }
    public interface IFlyIn<in inT>
    {
        void Show(inT inT);
    }
}
