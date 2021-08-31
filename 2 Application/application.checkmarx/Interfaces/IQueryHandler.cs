using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx
{
    public interface IQueryHandler
    { }
    public interface IQueryHandler<T> : IQueryHandler where T : IQuery
    {
        IList<IResult> Handle(T query);
    }
}
