using application.checkmarx.DisplayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace application.checkmarx.Queries
{
    public class GetWaitersQueryHandler : IQueryHandler<GetWaitersQuery>
    {
        private readonly IApplicationContext _context;

        public GetWaitersQueryHandler(IApplicationContext context)
        {
            _context = context;
        }

        public IList<IResult> Handle(GetWaitersQuery query)
        {
            var waiters = _context.Waiters.ToList();
            if (waiters == null)
                return null;

            var results = new List<IResult>();
            foreach (var p in waiters)
            {
                var waiter = new WaiterDisplay
                {
                    Id = p.Id,
                    Name = p.Name
                };

                results.Add(waiter);

            }
            return results;
        }
    }
}
