using System.Diagnostics;
using Extension;
using Helper;
using NLog;

namespace Logger
{
    using System;
    using System.Runtime.CompilerServices;

    public static class Log
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        [DebuggerStepThrough]
        public static void Info(string message)
        {
            Check.Argument.IsNotEmpty(message, "message");

            GetLog().Info(message);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [DebuggerStepThrough]
        public static void Info(string format, params object[] args)
        {
            Check.Argument.IsNotEmpty(format, "format");

            GetLog().Info(Format(format, args));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [DebuggerStepThrough]
        public static void Warning(string message)
        {
            Check.Argument.IsNotEmpty(message, "message");

            GetLog().Warn(message);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [DebuggerStepThrough]
        public static void Warning(string format, params object[] args)
        {
            Check.Argument.IsNotEmpty(format, "format");

            GetLog().Warn(Format(format, args));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [DebuggerStepThrough]
        public static void Error(string message)
        {
            Check.Argument.IsNotEmpty(message, "message");

            GetLog().Error(message);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [DebuggerStepThrough]
        public static void Error(string format, params object[] args)
        {
            Check.Argument.IsNotEmpty(format, "format");

            GetLog().Error(Format(format, args));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [DebuggerStepThrough]
        public static void Exception(Exception exception)
        {
            Check.Argument.IsNotNull(exception, "exception");

            GetLog().Error(exception);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static NLog.Logger GetLog()
        {
            return LogManager.GetCurrentClassLogger();
            //return IoC.Resolve<ILog>();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string Format(string format, params object[] args)
        {
            Check.Argument.IsNotEmpty(format, "format");

            return format.FormatWith(args);
        }
    }
}
