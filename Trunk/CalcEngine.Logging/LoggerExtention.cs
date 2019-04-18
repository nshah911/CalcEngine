using System;
using System.Collections.Generic;

namespace CalcEngine.Logging
{
    public static class LoggerExtention
    {
        private static Dictionary<string, NLog.Logger> loggerDic = new Dictionary<string, NLog.Logger>();
        /// <summary>
        /// The currentobj
        /// </summary>
        private static object currentobj = new object();
        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
        public static void Trace(this object obj, string message)
        {
            try
            {
                var logger = GetLogger(obj);
                logger.Trace(message);
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
        public static void Info(this object obj, string message)
        {
            try
            {
                var logger = GetLogger(obj);
                logger.Info(message);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="thisObject">The this object.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Info(this object thisObject, string message, params object[] args)
        {
            try
            {
                var logger = GetLogger(thisObject);
                logger.Info(string.Format(message, args));
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
        public static void Debug(this object obj, string message, params object[] args)
        {
            try
            {
                var logger = GetLogger(obj);
                logger.Debug(string.Format(message, args));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
        public static void Debug(this object obj, string message)
        {
            try
            {
                var logger = GetLogger(obj);
                logger.Debug(message);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
        public static void Error(this object obj, string message)
        {
            try
            {
                var logger = GetLogger(obj);
                logger.Error(message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Errors the specified ex.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="message">The message.</param>
        public static void Error(this object obj, Exception ex, string message)
        {
            try
            {
                var logger = GetLogger(obj);
                logger.Error(ex, message);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Errors the specified ex.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="message">The message.</param>
        public static void Error(this object obj, Exception ex, string message, params object[] args)
        {
            try
            {
                var logger = GetLogger(obj);
                logger.Error(ex, string.Format(message, args));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="thisObject">The this object.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(this object thisObject, string message, params object[] args)
        {
            try
            {
                var logger = GetLogger(thisObject);
                logger.Error(string.Format(message, args));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>NLog.Logger.</returns>
        private static NLog.Logger GetLogger(object obj)
        {
            //lock (currentobj)
            //{
            var objFullName = obj.GetType().FullName;
            if (loggerDic.ContainsKey(objFullName))
                return loggerDic[objFullName];

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger(obj.GetType());
            loggerDic.Add(objFullName, logger);
            return logger;
            //}
        }
    }
}
