using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace PersistentStackVisualization.TraceLog
{
    /// <summary>
    /// Static logging class
    /// </summary>
    internal static class Logger
    {
        /// <summary>
        /// Static method of information logging
        /// </summary>
        /// <param name="TextMessage">Message of log</param>
        /// <param name="file"> Caller file</param>
        /// <param name="line"> Caller line </param>
        /// <param name="member"> Caller member </param>
        public static void LogInformation(string TextMessage,
            [CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = null
            )
        {
            Trace.TraceInformation(TextMessage + " file: " + file.Substring(file.LastIndexOf('\\')) + " member: " + member + " line: " + line);
        }

        /// <summary>
        /// Static method of error logging
        /// </summary>
        /// <param name="TextMessage"> Error message </param>
        /// <param name="file"> Caller file </param>
        /// <param name="line"> Caller line </param>
        /// <param name="member"> Caller member </param>
        public static void LogError(string TextMessage,
            [CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = null
            )
        {
            Trace.TraceError("# ERROR " + TextMessage + " file: " + file.Substring(file.LastIndexOf('\\')) + " member: " + member + " line: " + line);
        }

        /// <summary>
        /// Static method of warning logging
        /// </summary>
        /// <param name="TextMessage"> Message warning</param>
        /// <param name="file"> Caller file </param>
        /// <param name="line"> Caller line </param>
        /// <param name="member"> Caller member </param>
        public static void LogWarning(string TextMessage,
            [CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = null
            )
        {
            Trace.TraceWarning("! Warning " + TextMessage + " file: " + file.Substring(file.LastIndexOf('\\')) + " member: " + member + " line: " + line);
        }
    }
}
