using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace application.checkmarx
{
    public interface IQueryHandler
    { }
    public interface IQueryHandler<T> : IQueryHandler where T : IQuery
    {
        Task<IList<IResult>> Handle(T query);
    }
}
