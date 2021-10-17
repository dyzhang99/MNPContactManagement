using System;
using log4net;

namespace MNPContactManagementWeb.Helpers
{
    public sealed class Logger
    {
        private static volatile Logger _instance; 
        private ILog logging;

        public static Logger Current
        {
            get
            {
                if (_instance == null)
                { 
                    if (_instance == null)
                        _instance = new Logger(); 
                }
                return _instance;
            }
        } 

        public void Refresh(Type type)
        {
            logging = LogManager.GetLogger(type);
        }

        public void Refresh(System.Reflection.MethodBase method, string httpMethod = System.Net.WebRequestMethods.Http.Get)
        {
            this.Refresh(method.Name, method.DeclaringType.FullName, httpMethod);
        }

        public void Refresh(string methodName, string controllerName, string httpMethod = System.Net.WebRequestMethods.Http.Get)
        {             
            if (System.Net.WebRequestMethods.Http.Get.Equals(httpMethod, StringComparison.InvariantCultureIgnoreCase))
            {
                logging = LogManager.GetLogger(string.Format("{0}({1})", controllerName, methodName));
            }
            else
            {
                logging = LogManager.GetLogger(string.Format("{0}({1}:{2})", controllerName, methodName, httpMethod));
            }
        }

        public void Debug(object message)
        { 
            if (logging != null)
                logging.Debug(message); 
        }
        public void Info(object message)
        {  
            if (logging != null)
                logging.Info(message); 
        }
        public void Error(object message)
        { 
            if (logging != null)
                logging.Error(message); 
        }
        public void Error(object message, Exception exception)
        { 
            if (logging != null)
                logging.Error(message, exception); 
        }
        public void Fatal(object message)
        { 
            if (logging != null)
                logging.Fatal(message); 
        }
        public void Fatal(object message, Exception exception)
        { 
            if (logging != null)
                logging.Fatal(message, exception); 
        }
        public void Warn(object message)
        { 
            if (logging != null)
                logging.Warn(message); 
        }
        public void Warn(object message, Exception exception)
        { 
            if (logging != null)
                logging.Warn(message, exception); 
        }

    }
}