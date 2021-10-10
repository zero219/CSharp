using DelegateDemo.Data;
using DelegateDemo.Models;
using System;
using static DelegateDemo.Models.People;

namespace DelegateDemo
{
    class Program
    {

        #region c#1.0
        //声明委托
        public delegate void NoReturnNoPara();
        public delegate void NoReturnWithPara(int x, int y);
        public delegate int WithReturnNoPara();
        public delegate int ReturnWithPara(int x);
        public delegate string WithReturnWithPara(out int x, ref int y);
        #endregion


        /// <summary>
        /// 泛型委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public delegate void GenericDelegate<T>();

        static void Main(string[] args)
        {
            //Show();
            //AnonymousShow();
            //AnonymousMethod();
            //NewAnonymousMethod();
            //ActionShow();
            //NewActionShow();
            //FuncShow();
            //NewFuncShow();
            //MulticastDelegation();
            //InfoData infoData = new InfoData();
            //infoData.ShowMethod();
            EventHnadlerMethod();
            Console.ReadKey();
        }

        /// <summary>
        /// 调用委托
        /// </summary>
        public static void Show()
        {
            //无参数无返回值委托
            //实例化委托
            NoReturnNoPara noReturnNoPara = new NoReturnNoPara(CustomMethod);
            //调用委拖
            noReturnNoPara.Invoke();

            //有参数无返回值委托
            NoReturnWithPara noReturnWithPara = new NoReturnWithPara(CustomMethod);
            noReturnWithPara.Invoke(1, 2);

            //无参数有返回值委托
            WithReturnNoPara with = new WithReturnNoPara(GetCustomMethod);
            int num = with.Invoke();

            //泛型委托
            GenericDelegate<int> generic = new GenericDelegate<int>(CustomMethod);
            generic.Invoke();
        }

        #region c#2.0
        /// <summary>
        /// 匿名委托
        /// </summary>
        public static void AnonymousShow()
        {
            NoReturnWithPara withPara = new NoReturnWithPara(delegate (int x, int y)
            {
                Console.WriteLine("{0}", x + y);
            });
            withPara.Invoke(1, 2);
        }
        #endregion

        #region c#3.0框架自带委托

        /// <summary>
        /// 匿名方法
        /// </summary>
        public static void AnonymousMethod()
        {
            //lambda表达式本质就是个匿名方法=>goes to
            ReturnWithPara noReturnWithPara = new ReturnWithPara((x) =>
            {
                return x + 1;
            });
            Console.WriteLine(noReturnWithPara);
        }
        /// <summary>
        /// 变化
        /// </summary>
        public static void NewAnonymousMethod()
        {
            //如果方法体只有一行那么可以去掉return,花括号和分号
            ReturnWithPara noReturnWithPara = new ReturnWithPara((x) => x + 1);
            Console.WriteLine(noReturnWithPara);
        }

        /// <summary>
        /// 无返回值
        /// </summary>
        public static void ActionShow()
        {
            //无返回值
            Action action = CustomMethod;
            action.Invoke();
            //有参无返回值
            Action<int, int> actionInt = CustomMethod;
            actionInt.Invoke(1, 1);
        }
        /// <summary>
        /// 变化
        /// </summary>
        public static void NewActionShow()
        {
            //无返回值
            Action action = () => { };
            action.Invoke();
            //有参无返回值
            Action<int, int> actionInt = (x, y) => Console.WriteLine("{0}", x + y); ;
            actionInt.Invoke(1, 2);
        }

        /// <summary>
        /// 有返回值
        /// </summary>
        public static void FuncShow()
        {
            //有返回值无参数
            Func<int> func = GetCustomMethod;
            int num = func.Invoke();
            Console.WriteLine(num);
            //有参有返回值,最多有16个返回值
            Func<int, int> func2 = CustomMethod;
            int num1 = func2.Invoke(2);
            Console.WriteLine(num1);
        }

        /// <summary>
        /// 变化
        /// </summary>
        public static void NewFuncShow()
        {
            //有返回值无参数
            Func<int> func = () => 1;
            int num = func.Invoke();
            Console.WriteLine(num);
            //有参有返回值,最多有16个返回值
            Func<int, int, int> func2 = (x, y) => x + y;
            int num1 = func2.Invoke(2, 3);
            Console.WriteLine(num1);
        }
        #endregion


        #region 多播委托
        /// <summary>
        /// 多播委托
        /// </summary>
        public static void MulticastDelegation()
        {
            //+=表示向一个委托实例中添加方法，形成方法链，按添加顺序执行
            Action action = new Action(CustomMethod);
            action += CustomMethod1;
            action += CustomMethod2;
            action += CustomMethod3;
            action += new InfoData().InfoDataMethod;
            action.Invoke();

            //-=表示向一个委托实例中移除方法，从方法尾部开始匹配，遇到第一个完全吻合，移除且只移除一个；没有匹配，也不异常。
            action -= CustomMethod;
            action -= CustomMethod1;
            action -= CustomMethod2;
            action -= new InfoData().InfoDataMethod;
            action -= CustomMethod3;
            action.Invoke();
            //InfoData().ShowMethod移除不掉是因为它实例化了，这样它就不是同一个实例了，也不是同一个方法。
        }
        #endregion

        #region 事件
        
        /// <summary>
        /// 事件
        /// </summary>
        public static void EventHnadlerMethod()
        {
            /*
             * 例如：我们人有一系列行为，跑步、喝水、吃饭、休息。
             * 平常我们都是实例化这些方法，虽然功能实现了，但是不够稳定。
             * 那么我们用事件来解决这个问题
             */
            People p = new People();
            //事件前解决问题
            p.Behavior();
            //事件后解决问题
            p.PeopleEventHnadler += new PeopleBehavior(new Motion().Run);
            p.PeopleEventHnadler += new PeopleBehavior(new Nature().DrinkWater);
            p.PeopleEventHnadler += new PeopleBehavior(new Rice().HavingDinner);
            p.PeopleEventHnadler += new PeopleBehavior(new Rest().Sleep);
            p.BehaviorEventHnadler();
        }
        #endregion

        #region 方法
        public static void CustomMethod()
        {
            Console.WriteLine("调用了无参数无返回值方法");
        }
        public static void CustomMethod1()
        {
            Console.WriteLine("调用了无参数无返回值方法1");
        }
        public static void CustomMethod2()
        {
            Console.WriteLine("调用了无参数无返回值方法2");
        }
        public static void CustomMethod3()
        {
            Console.WriteLine("调用了无参数无返回值方法3");
        }

        public static int CustomMethod(int x)
        {
            return x + 1;
        }
        public static void CustomMethod(int x, int y)
        {
            Console.WriteLine("调用了有参数无返回值方法,参数x={0},y={1}", x, y);
        }

        public static int GetCustomMethod()
        {
            return 6;
        }
        #endregion

    }
}
