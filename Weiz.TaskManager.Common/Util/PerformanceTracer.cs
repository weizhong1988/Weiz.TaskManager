using System;
using System.Configuration;
using System.Diagnostics;

namespace Weiz.TaskManager.Common
{
    /// <summary>
    /// 性能跟踪工具。
    /// </summary>
    public static class PerformanceTracer
    {
        private static int performanceTracer;

        static PerformanceTracer()
        {
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["PerformanceTracer"]))
            {
                int.TryParse(ConfigurationManager.AppSettings["PerformanceTracer"], out performanceTracer);
            }
            else
            {
                performanceTracer = -1;
            }
        }

        /// <summary>
        /// 跟踪指定代码块的执行时间。
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="traceName">跟踪名称。</param>
        /// <returns>返回类型为<typeparamref name="TResult"/>的结果。</returns>
        public static TResult Invoke<TResult>(Func<TResult> func, string traceName = null)
        {
            if (performanceTracer >= 0)
            {
                if (string.IsNullOrWhiteSpace(traceName))
                {
                    var method = new StackTrace().GetFrames()[1].GetMethod();

                    traceName = string.Format("{0}.{1}", method.ReflectedType.FullName, method.Name);
                }

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                TResult result = func();

                stopwatch.Stop();

                if (stopwatch.Elapsed.TotalMilliseconds > performanceTracer)
                {
                    if (!Debugger.IsAttached)
                    {
                        LogHelper.WriteLog("性能问题(" + traceName + ")" + stopwatch.Elapsed.TotalMilliseconds + "ms");
                    }

                    Debug.WriteLine("性能问题({0})，耗时{1}毫秒。", traceName, stopwatch.Elapsed.TotalMilliseconds);
                }

                return result;
            }
            else
            {
                return func();
            }
        }

        /// <summary>
        /// 调用并且跟踪指定代码块的执行时间。
        /// </summary>
        /// <param name="action"></param>
        /// <param name="traceName">跟踪名称。</param>
        /// <param name="enableThrow">指定是否允许抛出异常。</param>
        public static void Invoke(Action action, string traceName = null, bool enableThrow = true)
        {
            if (performanceTracer >= 0)
            {
                if (string.IsNullOrWhiteSpace(traceName))
                {
                    var method = new StackTrace().GetFrames()[1].GetMethod();

                    traceName = string.Format("{0}.{1}", method.ReflectedType.FullName, method.Name);
                }

                try
                {
                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();

                    action();

                    stopwatch.Stop();

                    if (stopwatch.Elapsed.TotalMilliseconds > performanceTracer)
                    {
                        if (!Debugger.IsAttached)
                        {
                            LogHelper.WriteLog("性能问题(" + traceName + ")" + stopwatch.Elapsed.TotalMilliseconds + "ms");
                        }

                        Debug.WriteLine("性能问题({0})，耗时{1}毫秒。", traceName, stopwatch.Elapsed.TotalMilliseconds);
                    }
                }
                catch (Exception ex)
                {
                    if (enableThrow)
                    {
                        throw;
                    }

                    //LogHelper.HandleError(ex, traceName ?? ex.Message);
                    LogHelper.WriteLog(ex.Message, ex);
                }
            }
            else
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    if (enableThrow)
                    {
                        throw;
                    }

                    LogHelper.WriteLog(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// 通过异步方式调用的方式处理一些不太重要的工作。
        /// </summary>
        /// <param name="action">指定要执行的操作。</param>
        /// <param name="traceName"></param>
        public static void InvokeAsync(Action action, string traceName = null)
        {
            if (performanceTracer >= 0)
            {
                if (string.IsNullOrWhiteSpace(traceName))
                {
                    var method = new StackTrace().GetFrames()[1].GetMethod();

                    traceName = string.Format("{0}.{1}", method.ReflectedType.FullName, method.Name);
                }

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                action.BeginInvoke(delegate
                {
                    stopwatch.Stop();

                    if (stopwatch.Elapsed.TotalMilliseconds > performanceTracer)
                    {
                        if (!Debugger.IsAttached)
                        {
                            LogHelper.WriteLog("性能问题(" + traceName + ")" + stopwatch.Elapsed.TotalMilliseconds + "ms");
                        }

                        Debug.WriteLine("性能问题({0})，耗时{1}毫秒。", traceName, stopwatch.Elapsed.TotalMilliseconds);
                    }
                }, null);
            }
            else
            {
                action.BeginInvoke(null, null);
            }
        }
        /// <summary>
        /// 通过异步方式调用的方式处理一些不太重要的工作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="callback"></param>
        /// <param name="traceName"></param>
        public static void InvokeAsync(Action action, Action callback, string traceName = null)
        {
            if (performanceTracer >= 0)
            {
                if (string.IsNullOrWhiteSpace(traceName))
                {
                    var method = new StackTrace().GetFrames()[1].GetMethod();

                    traceName = string.Format("{0}.{1}", method.ReflectedType.FullName, method.Name);
                }

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                action.BeginInvoke(delegate
                {
                    callback();
                    stopwatch.Stop();

                    if (stopwatch.Elapsed.TotalMilliseconds > performanceTracer)
                    {
                        if (!Debugger.IsAttached)
                        {
                            LogHelper.WriteLog("性能问题(" + traceName + ")" + stopwatch.Elapsed.TotalMilliseconds + "ms");
                        }

                        Debug.WriteLine("性能问题({0})，耗时{1}毫秒。", traceName, stopwatch.Elapsed.TotalMilliseconds);

                    }
                }
                    , null);
            }
            else
            {
                action.BeginInvoke(null, null);
            }
        }
    }
}
