using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Repository;
using log4net.Config;
using MongoDB.Bson;
namespace MongoDbDemo
{
    public class MongoDemo2
    {
       public void demo_insert()
        {
          

            XmlConfigurator.Configure();
            string result = string.Empty;
           
            ILogger log = new LoggerImpl("Sabre_Sabre");
            log.Warning("test", new {
                name="1",
                sky="2",
                mycustomproperty = new
                {
                    h="123",
                    x ="44"
                }
            });
        }

    }
    public interface ILogger
    {
        void Debug(object message);
        void Debug(object message, object data);
        void Debug(object message, Exception exception);
        void Debug(object message, params object[] args);

        void Error(object message);
        void Error(object message, object data);
        void Error(object message, Exception exception);
        void Error(object message, params object[] args);

        void Fatal(object message);
        void Fatal(object message, object data);
        void Fatal(object message, Exception exception);
        void Fatal(object message, params object[] args);

        void Information(object message);
        void Information(object message, object data);
        void Information(object message, Exception exception);
        void Information(object message, params object[] args);

        void Warning(object message);
        void Warning(object message, object data);
        void Warning(object message, Exception exception);
        void Warning(object message, params object[] args);

    }

    public class LoggerImpl : ILogger
    {
        private readonly log4net.ILog log;

        public LoggerImpl(string type)
        {
            this.log = log4net.LogManager.GetLogger(type);
        }

        public void Debug(object message)
        {
            this.log.Debug(message);
        }

        public void Debug(object message, object data)
        {
            log4net.ThreadContext.Properties["data"] = data.ToBsonDocument();

            this.log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            this.log.Debug(message, exception);
        }
        public void Debug(object message, params object[] args)
        {
            this.log.DebugFormat((string)message, args);
        }

        public void Error(object message)
        {
            this.log.Error(message);
        }

        public void Error(object message, object data)
        {
            log4net.ThreadContext.Properties["data"] = data.ToBsonDocument();

            this.log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            this.log.Error(message, exception);
        }
        public void Error(object message, params object[] args)
        {
            this.log.ErrorFormat((string)message, args);
        }

        public void Fatal(object message)
        {
            this.log.Fatal(message);
        }

        public void Fatal(object message, object data)
        {
            log4net.ThreadContext.Properties["data"] = data.ToBsonDocument();

            this.log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            this.log.Fatal(message, exception);
        }
        public void Fatal(object message, params object[] args)
        {
            this.log.FatalFormat((string)message, args);
        }

        public void Information(object message)
        {
            this.log.Info(message);
        }
        public void Information(object message, object data)
        {
            log4net.ThreadContext.Properties["data"] = data.ToBsonDocument();

            this.log.Info(message);
        }

        public void Information(object message, Exception exception)
        {
            this.log.Info(message, exception);
        }
        public void Information(object message, params object[] args)
        {
            this.log.InfoFormat((string)message, args);
        }

        public void Warning(object message)
        {
            this.log.Warn(message);
        }

        public void Warning(object message, object data)
        {
            log4net.ThreadContext.Properties["data"] = data.ToBsonDocument();

            this.log.Warn(message);
        }

        public void Warning(object message, Exception exception)
        {
            this.log.Warn(message, exception);
        }
        public void Warning(object message, params object[] args)
        {
            this.log.WarnFormat((string)message, args);
        }
    }
}
