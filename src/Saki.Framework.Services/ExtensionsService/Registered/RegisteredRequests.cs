namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Saki.Framework.Logging;

    public class RegisteredRequests
    {
        public RegisteredItemDataType ItemDataType { get; private set; }
        private List<RegisteredRequest> _requests = new List<RegisteredRequest>();

        public RegisteredRequests(RegisteredItemDataType itemDataType)
        {
            ItemDataType = itemDataType;
        }

        public RegisteredRequest GetOrAddRequest(string requestName)
        {
            var command = _requests.FirstOrDefault(c => c.RequestName == requestName);
            if (command == null)
            {
                command = new RegisteredRequest(requestName, ItemDataType);
                _requests.Add(command);
            }

            return command;
        }

        public void LogFullTree(ILogger log)
        {
            if (_requests.Count == 0) return;

            log = log.CreateChildLogger("Requests:");

            foreach (var request in _requests)
            {
                request.LogFullTree(log);
            }
        }

        internal RegisteredRequest GetRequestOrDefault(string requestName)
        {
            return _requests.FirstOrDefault(c => c.RequestName == requestName);
        }
    }
}
