using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx
{
    public interface ICommandDispatcher
    {
        void Send<T>(T command) where T : ICommand;
    }
}
