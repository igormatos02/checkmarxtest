using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace application.checkmarx
{
    public interface ICommandHandler
    { }
    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        Task Handle(T command);
    }
}
