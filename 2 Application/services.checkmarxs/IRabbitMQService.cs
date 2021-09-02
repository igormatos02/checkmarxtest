using System;
using System.Collections.Generic;
using System.Text;

namespace services.checkmarxs
{
    public interface IRabbitMQService
    {
        void Connect(string queue,string method);
     
        void Send(string message, string queue);
      
    }
}
