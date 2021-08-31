using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx
{
    public interface IResult
    {
    }
    public interface IListResult : ICollection<IResult>
    {
    }
}
