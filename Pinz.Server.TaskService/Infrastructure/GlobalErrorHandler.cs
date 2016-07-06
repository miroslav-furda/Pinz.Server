using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace Com.Pinz.Server.TaskService.Infrastructure
{
    public class GlobalErrorHandler : IErrorHandler
    {
        private readonly ILog Log = LogManager.GetLogger<GlobalErrorHandler>();

        /// <summary>
        /// The method that's get invoked if any unhandled exception raised in service
        /// Here you can do what ever logic you would like to. 
        /// For example logging the exception details
        /// Here the return value indicates that the exception was handled or not
        /// Return true to stop exception propagation and system considers 
        /// that the exception was handled properly
        /// else return false to abort the session
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool HandleError(Exception error)
        {
            Log.Error(error.Message, error);
            return false;
        }

        /// <summary>
        /// If you want to communicate the exception details to the service client 
        /// as proper fault message
        /// here is the place to do it
        /// If we want to suppress the communication about the exception, 
        /// set fault to null
        /// </summary>
        /// <param name="error"></param>
        /// <param name="version"></param>
        /// <param name="fault"></param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var newEx = new FaultException(string.Format("Exception caught at Service Application GlobalErrorHandler{0} Method:{1}{2} Message:{3}",
                Environment.NewLine, error.TargetSite.Name,
                Environment.NewLine, error.Message));
            MessageFault msgFault = newEx.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, newEx.Action);
        }
    }
}