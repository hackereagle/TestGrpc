using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SclinMiscLib
{
    public class ConsoleWriterWrapper
    {
        // check whether the current process has a main window
        static bool HasMainWindow()
        {
            return (Process.GetCurrentProcess().MainWindowHandle != IntPtr.Zero);
        }

        static void ConsoleWriteLine(string msg)
        { 
            if (HasMainWindow())
            {
                System.Diagnostics.Debug.WriteLine(msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        static DateTime _lastLogTime = DateTime.Now;
        // print the message to the console
        public static void Log(string msg, bool _performanceCounterStart = false, bool _performanceCounterEnd = false, bool _showThreadID = false)
        {
            DateTime curTime = DateTime.Now;
            if (_performanceCounterStart)
            {
                _lastLogTime = curTime;
            }

            if (_performanceCounterEnd)
            {
                TimeSpan ts = curTime - _lastLogTime;
                if (_showThreadID)
                    ConsoleWriteLine($"{curTime.ToString("HH:mm:ss.fff")} {msg}, at thread id = {System.Environment.CurrentManagedThreadId}, Elapsed time = [{ts.TotalMilliseconds}] ms");
                else
                    ConsoleWriteLine($"{curTime.ToString("HH:mm:ss.fff")} {msg}, Elapsed time = [{ts.TotalMilliseconds}] ms");
            }
            else
            {
                if (_showThreadID)
                    ConsoleWriteLine($"{curTime.ToString("HH:mm:ss.fff")} {msg}, at thread id = {System.Environment.CurrentManagedThreadId}");
                else
                    ConsoleWriteLine($"{curTime.ToString("HH:mm:ss.fff")} {msg}");
            }
        }

        public static void Log(string msg, Exception ex)
        {
            DateTime curTime = DateTime.Now;

            ConsoleWriteLine($"{curTime.ToString("HH:mm:ss.fff")} {msg}: {FlattenException(ex)}, at thread id = {System.Environment.CurrentManagedThreadId}");
        }

        private static string FlattenException(Exception? ex)
        {
            var stringBuilder = new StringBuilder();

            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);

                ex = ex.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}
