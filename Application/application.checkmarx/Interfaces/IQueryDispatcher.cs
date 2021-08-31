using System;
using System.Collections.Generic;

namespace application.checkmarx
{
    public interface IQueryDispatcher
    {
        IList<IResult> Send<T>(T query) where T : IQuery;
    }
}
