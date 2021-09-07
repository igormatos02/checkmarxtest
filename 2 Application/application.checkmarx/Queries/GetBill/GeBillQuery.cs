using crosscutting.checkmarx.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx.Queries
{
    public class GetBillQuery : IQuery
    {
        public Guid OrderId { get; set; }
    }
}
