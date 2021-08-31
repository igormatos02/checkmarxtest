using System;
using System.Collections.Generic;
using System.Text;

namespace services.checkmarxs
{
    public interface IRabbitMQService
    {
        void Connect();

        void Send(string message);
        string Receive();
    }
}
