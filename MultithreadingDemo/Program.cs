using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingDemo
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //AsyncCall();
            //Threads();
            //ThreadCallback();
            //ThreadWithReturn();
            //ThreadNum();
            //ThreadPools();
            //ThreadMre();
            //TaskFactorys();
            //ParallelMethods();
            //Abnormal();
            //AbnormalWaitAll();
            //TheadCancel();
            //LockMethods();
            Console.WriteLine("AsyncAwaitStart,线程ID:{0}", Thread.CurrentThread.ManagedThreadId);
            var result = await Methonds();
            Console.WriteLine("AsyncAwait结果:{0},线程ID:{1}", result, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("AsyncAwaitEnd,{0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }

        #region async和await
        /* c#5.0之前会把await作为标识符;async修饰符会让编译器将await当做关键字.
         * 它的返回值有void、Task、Task<TResult>
         * 如果一切正常返回值则赋值给await表达式
         * 碰到await,那么主线程切换子线程;await后面的代码也是切换的子线程来执行
         */
        public static async Task<string> Methonds()
        {
            Console.WriteLine("MethondsStart,线程ID:{0}", Thread.CurrentThread.ManagedThreadId);
            var result = await Task.Run(() =>
            {
                Thread.Sleep(5000);
                return "1";
            });
            Console.WriteLine("MethondsEnd,线程ID:{0}", Thread.CurrentThread.ManagedThreadId);
            return result;
        }
        #endregion


        #region 同步异步
        /*
         * 多线程指的是CLR线程，异步是IO线程
         * 异步方法：发起一个调用，不等着计算结束，启动一个线程去运行下一行
         * 同步方法：只有一个线程在计算
         * 同步方法因为UI线程忙于计算，卡界面；异步方法不卡界面，因为主线程闲置，计算任务交给子线程在做。
         * 异步多线程是无序的；启动无序，执行时间不确定；结束无序。
         * 注：不要通过启动顺序或时间来控制异步多线程的流程；多线程消耗资源，线程不是越多越好。
         */

        /// <summary>
        /// 异步回调
        /// </summary>
        public static void AsyncCall()
        {
            Action<string> action = DoSomethingLong;
            IAsyncResult asyncResult = null;
            AsyncCallback callback = ar =>
            {
                Console.WriteLine(ar.AsyncState);//异步线程状态
            };
            //BeginInvoke在.NET CORE3.0后不支持了
            asyncResult = action.BeginInvoke("张三在吃饭", callback, "我是状态结果");
            Console.WriteLine(asyncResult);
        }
        #endregion

        #region 线程1.0,Thread类.常用的方法有Slpee()、Join()
        public static void Threads()
        {
            Action action = () =>
            {
                Console.WriteLine("Action");
            };
            //Thread threadAction = new Thread(action);//Action和ThreadStart约束一致，但是类型不同
            ThreadStart start = new ThreadStart(() =>
            {
                Thread.Sleep(5000);
                DoSomethingLong("ThreadStart");
            });
            Thread thread = new Thread(start);
            //变成后台线程(如果是前台线程，前台线程推出后，还会继续执行)
            thread.IsBackground = true;
            thread.Start();
            thread.Join();//等待(主线程等待完成)
        }

        /// <summary>
        /// 基于Thread封装支持回调(无返回值)
        /// BeginInvoke的回调
        /// </summary>
        public static void ThreadCallback()
        {
            ThredsWithCallback(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"这里是ThreadStart {Thread.CurrentThread.ManagedThreadId}");
            }, () =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"这里是callback {Thread.CurrentThread.ManagedThreadId}");
            });
        }
        public static void ThredsWithCallback(ThreadStart start, Action action)
        {
            ThreadStart threadStart = new ThreadStart(() =>
            {
                //按顺序执行
                start.Invoke();
                action.Invoke();
            });
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        /// <summary>
        /// 基于Thread封装支持回调(有返回值)
        /// </summary>
        public static void ThreadWithReturn()
        {
            Func<int> func = ThreadWithReturn(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine($"这里是ThreadStart {Thread.CurrentThread.ManagedThreadId}");
                return 12345;
            });
            Console.WriteLine("已经执行到这里了。。。");
            int iResult = func.Invoke();//endinvoke
            Console.WriteLine("返回值{0}", iResult);
        }
        /// <summary>
        /// 带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<T> ThreadWithReturn<T>(Func<T> func)
        {
            T t = default(T);
            ThreadStart threadStart = new ThreadStart(() =>
            {
                t = func.Invoke();
            });
            Thread thread = new Thread(threadStart);
            thread.Start();
            return new Func<T>(() =>
            {
                thread.Join();
                return t;
            });
        }
        #endregion


        #region 多线程2.0,线程池
        /*
         *  1.去掉各种api避免滥用，降低复杂度
         *  2.池化：减少创建/销毁的成本 
         *          限制最大线程数量
         */
        public static void ThreadPools()
        {
            ThreadPool.QueueUserWorkItem(x =>
            {
                new Action(() =>
                {
                    Thread.Sleep(5000);
                    DoSomethingLong("线程池");
                }).Invoke();
                Console.WriteLine("123456789");
            });
            //带参数
            ThreadPool.QueueUserWorkItem(x =>
            {
                Console.WriteLine(x.ToString());
                new Action(() =>
                {
                    Thread.Sleep(5000);
                    DoSomethingLong("带参数线程池");
                }).Invoke();
            }, "ParameterValue");
        }

        /// <summary>
        /// 线程数
        /// </summary>
        public static void ThreadNum()
        {
            ThreadPool.SetMaxThreads(8, 8);//最多线程数
            ThreadPool.SetMinThreads(8, 8);//最小线程数
            int workerThreads = 0;
            int ioThreads = 0;
            ThreadPool.GetMaxThreads(out workerThreads, out ioThreads);
            ThreadPool.GetMinThreads(out workerThreads, out ioThreads);
            Console.WriteLine(String.Format("最大工作线程数: {0}; 最大I/O线程数: {1}", workerThreads, ioThreads));
            Console.WriteLine(String.Format("最小工作线程数: {0}; 最小I/O线程数: {1}", workerThreads, ioThreads));

            ThreadPool.GetAvailableThreads(out workerThreads, out ioThreads);
            Console.WriteLine(String.Format("可用工作线程: {0}; 可用的I/O线程: {1}", workerThreads, ioThreads));
        }

        /// <summary>
        /// 线程池阻塞
        /// </summary>
        public static void ThreadMre()
        {
            //信号结构ManualResetEvent;
            //状态标志,false关闭
            ManualResetEvent mre = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(o =>
            {
                new Action(() =>
                {
                    Thread.Sleep(5000);
                    DoSomethingLong("线程池阻塞");
                }).Invoke();
                //打开,控制ManualResetEventd的状态
                mre.Set();
                Console.WriteLine(o.ToString());
            }, "ParameterValue");

            /* 
             * 阻止线程,使线程在此阻塞;使用mre.Set()继续执行
             * 注:没有需求不要阻塞线程,容易让程序死锁.死锁就要重启程序了.
             */
            mre.WaitOne();

            Console.WriteLine("ThreadMreEnd");

            //关闭,控制ManualResetEventd的状态
            mre.Reset();

            new Action(() =>
            {
                Console.WriteLine("ThreadMre调用");
                mre.Set();
            }).Invoke();
            mre.WaitOne();

            Console.WriteLine("ThreadMre调用后。。。");
        }
        #endregion


        #region 线程3.0,Task使用的是线程池线程,都是后台线程
        public static void TaskFactorys()
        {
            //.net3.5左右使用TaskFactory;
            //.net4.5后使用Task.Run(()=>{  Console.WriteLine("123"); });
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();
            taskList.Add(taskFactory.StartNew(() => DoSomethingLong("Event_1")));
            taskList.Add(taskFactory.StartNew(() => DoSomethingLong("Event_2")));
            taskList.Add(taskFactory.StartNew(() => DoSomethingLong("Event_3")));

            //快速完成，某个任务完成之后才返回
            Task.WaitAny(taskList.ToArray());
            Console.WriteLine("某个任务完成，才执行");

            taskFactory.ContinueWhenAny(taskList.ToArray(), t =>
            {
                Console.WriteLine("ContinueWhenAny,{0}", Thread.CurrentThread.ManagedThreadId.ToString());
            });//回调（某个任务完成，不卡界面）

            taskFactory.ContinueWhenAll(taskList.ToArray(), t =>
            {
                Console.WriteLine("ContinueWhenAll,{0}", Thread.CurrentThread.ManagedThreadId.ToString());
            }); //回调（全部任务完成，不卡界面）

            //快速完成，同时又全部完成之后才返回
            Task.WaitAll(taskList.ToArray());//卡界面
            Console.WriteLine("全部任务完成，才执行");

            //回调（全部任务完成）
            Task task = taskFactory.StartNew(t => DoSomethingLong("Event_4"), "异步的")
                .ContinueWith(t =>
                {
                    Console.WriteLine(t.AsyncState);
                });

            Task<int> intTask = taskFactory.StartNew(t => 123, "异步的");
            Console.WriteLine("结果:{0},状态:{1}", intTask.Result, intTask.AsyncState);
        }
        #endregion

        #region Parallel
        public static void ParallelMethods()
        {
            //和Task相似，主线程参与计算，会把主线程当作子线程使用，节约一个线程
            //因为主线程参与计算,该线程会卡界面,使用委托包裹可以解决
            Parallel.Invoke(() => DoSomethingLong("Parallel_1"),
                () => DoSomethingLong("Parallel_2"),
                () => DoSomethingLong("Parallel_3"),
                () => DoSomethingLong("Parallel_4"));
            //循环,0开始索引,5结束索引
            Parallel.For(0, 5, t => { DoSomethingLong($"ParallelFor_{t}"); });

            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 3//控制线程数
            };
            //泛型
            Parallel.ForEach(new int[] { 0, 1, 2, 3, 4 }, options, (t, state) =>
            {
                DoSomethingLong($"ParallelForEach_{t}");
                //state.Break(); //结束当前
                state.Stop();//结束所有
            });
        }
        #endregion

        #region 捕获线程异常
        /*
         * 在线程包裹的方法中添加try...catch...捕获异常
         * 如果没有,那么多线程中的异常是会被吞掉;不过可以使用WaitAll来捕获,这个虽然是一种方法，但是不建议使用
         */

        /// <summary>
        /// try...catch...捕获异常
        /// </summary>
        public static void Abnormal()
        {
            TaskFactory taskFactory = new TaskFactory();
            for (int i = 0; i < 20; i++)
            {
                string name = string.Format("线程{0}", i);
                Action<object> action = t =>
                {
                    try
                    {
                        Thread.Sleep(2000);
                        if (t.ToString().Equals("线程11"))
                        {
                            throw new Exception(string.Format($"{t} 执行失败"));
                        }
                        if (t.ToString().Equals("线程12"))
                        {
                            throw new Exception(string.Format($"{t} 执行失败"));
                        }
                        Console.WriteLine($"{t}执行成功");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                };
                taskFactory.StartNew(action, name);
            }
        }

        /// <summary>
        /// WaitAll捕获
        /// </summary>
        public static void AbnormalWaitAll()
        {
            try
            {
                TaskFactory taskFactory = new TaskFactory();
                List<Task> taskList = new List<Task>();
                for (int i = 0; i < 20; i++)
                {
                    string name = string.Format("线程{0}", i);
                    Action<object> action = t =>
                    {
                        Thread.Sleep(2000);
                        if (t.ToString().Equals("线程11"))
                        {
                            throw new Exception(string.Format($"{t} 执行失败"));
                        }
                        if (t.ToString().Equals("线程12"))
                        {
                            throw new Exception(string.Format($"{t} 执行失败"));
                        }
                        Console.WriteLine($"{t}执行成功");
                    };
                    taskList.Add(taskFactory.StartNew(action, name));
                }
                Task.WaitAll(taskList.ToArray());
            }
            catch (AggregateException ae)//打印source.Token异常
            {
                foreach (var item in ae.InnerExceptions)
                {
                    Console.WriteLine(item.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 线程取消
        /*
         * 线程取消不是操作线程,而是操作信号量
         * 每个线程在执行过程中,经常去查看这个信号量,然后自己结束自己
         * 线程不能别人终止,只能自己干掉自己,延迟少不了
         */

        public static void TheadCancel()
        {
            try
            {
                TaskFactory taskFactory = new TaskFactory();
                List<Task> taskList = new List<Task>();
                CancellationTokenSource source = new CancellationTokenSource();//信号标,bool值
                for (int i = 0; i < 20; i++)
                {
                    string name = string.Format("线程{0}", i);
                    Action<object> action = t =>
                    {
                        try
                        {
                            Thread.Sleep(2000);
                            if (t.ToString().Equals("线程5"))
                            {
                                throw new Exception(string.Format($"{t} 执行失败"));
                            }
                            if (t.ToString().Equals("线程6"))
                            {
                                throw new Exception(string.Format($"{t} 执行失败"));
                            }
                            //检查信号量
                            if (source.IsCancellationRequested)
                            {
                                Console.WriteLine($"{t}放弃执行");
                            }
                            else
                            {
                                Console.WriteLine($"{t}执行成功");
                            }
                        }
                        catch (Exception ex)
                        {
                            //取消;CancellationTokenSource可以在Cancel后，取消没有启动的任务
                            source.Cancel();
                            Console.WriteLine(ex.Message);
                        }
                    };
                    taskList.Add(taskFactory.StartNew(action, name, source.Token));
                }
                Task.WaitAll(taskList.ToArray());
            }
            catch (AggregateException exToken)//打印source.Token异常
            {
                foreach (var item in exToken.InnerExceptions)
                {
                    Console.WriteLine(item.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion


        #region 锁,保证线程安全
        private static readonly object locker = new object();
        //默认的bool值是false
        private static bool done;
        public static void LockMethods()
        {
            /*
             * 加lock就输出一个Done,没有则有一定概率输出两个Done;
             * 原因是一个线程在评估if语句;另一个线程在执行Console.WriteLine("Done")语句;它还没来得及将done设置成true.
             * 当两个线程竞争一个锁的时候,一个线程会等待或者阻塞,一直到锁变成可用状态.
             */
            Go();
            Task.Run(() => { Go(); });
        }
        public static void Go()
        {
            lock (locker)
            {
                if (!done)
                {
                    Console.WriteLine("Done");
                    done = true;
                }
            }
        }
        #endregion


        /// <summary>
        /// 耗时的方法
        /// </summary>
        /// <param name="name"></param>
        private static void DoSomethingLong(string name)
        {
            Console.WriteLine($"DoSomethingLongStart{name},线程ID:{Thread.CurrentThread.ManagedThreadId},时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            long lResult = 0;
            for (int i = 0; i < 100000000; i++)
            {
                lResult += i;
            }
            //Thread.Sleep(2000);
            Console.WriteLine($"DoSomethingLongEnd{name},线程ID:{Thread.CurrentThread.ManagedThreadId},时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
        }
    }
}
