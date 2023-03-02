using System;
using System.Runtime.InteropServices;

namespace TAO.Engine
{
    public class OSInfo
    {
        public PlatformID Platform;
        public Architecture SystemArchitecture;
        public Architecture ProcessArchitecture;

        public static OSInfo GetClientInfo()
        {
            OSInfo info = new OSInfo();

            info.Platform = Environment.OSVersion.Platform;
            info.SystemArchitecture = RuntimeInformation.OSArchitecture;
            info.ProcessArchitecture = RuntimeInformation.ProcessArchitecture;

            return info;
        }

        public static void ExecuteAction(Action action, bool WriteOnLog = true)
        {
            if (action != null)
            {
                if (WriteOnLog)
                {
                    Log.WriteWarning("Executing third-party action.");
                }

                try
                {
                    action.Invoke();
                }
                catch
                {

                }
            }
        }

        public static void ExecuteAction<T>(Action<T> action, T data, bool WriteOnLog = true)
        {
            if (action != null)
            {
                if (WriteOnLog)
                {
                    Log.WriteWarning("Executing third-party action.");
                }

                try
                {
                    action.Invoke(data);
                }
                catch
                {

                }
            }
        }
    }
}