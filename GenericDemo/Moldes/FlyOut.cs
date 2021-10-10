using System;
using System.Collections.Generic;
using System.Text;

namespace GenericDemo.Models
{
    /*
     * 协变
     */
    public interface IFlyOut<out outT>
    {
        outT Show();
    }

    public class FlyOut<outT> : IFlyOut<outT>
    {
        public outT Show()
        {
            throw new NotImplementedException();
        }
    }
}
