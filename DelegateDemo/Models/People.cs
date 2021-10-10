using System;
using System.Collections.Generic;
using System.Text;

namespace DelegateDemo.Models
{
    public class People
    {
        /*
         * 事件就是带event关键字的实例委托
         * 事件可以限制变量被外部调用/直接赋值
         * 委托和事件的区别：委托是个类型，事件就是委托类型的一个实例。
         */
        public delegate void PeopleBehavior();
        public event PeopleBehavior PeopleEventHnadler;
        public void Behavior()
        {
           
            new Motion().Run();
            new Nature().DrinkWater();
            new Rice().HavingDinner();
            new Rest().Sleep();
        }

        public void BehaviorEventHnadler()
        {
            Console.WriteLine("{0}", this.GetType().Name);
            if (PeopleEventHnadler != null)
            {
                PeopleEventHnadler.Invoke();
            }
        }
    }
}
