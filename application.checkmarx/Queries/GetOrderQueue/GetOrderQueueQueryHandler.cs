using application.checkmarx.DisplayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace application.checkmarx.Queries
{
    public class GetOrderQueueQueryHandler : IQueryHandler<GetOrderQueueQuery>
    {
        private readonly IApplicationContext _context;

        public GetOrderQueueQueryHandler(IApplicationContext context)
        {
            _context = context;
        }

        public IList<IResult> Handle(GetOrderQueueQuery query)
        {
            var orders = _context.Orders.Where(p => p.Status == query.Status).ToList();
            if (orders == null)
                return null;

            var results = new List<IResult>();
            foreach (var p in orders)
            {
                var order = new OrderDisplay
                {
                    OrderId = p.OrderId,
                    WaiterId = p.WaiterId,
                    Status = p.Status,
                    TableNumber = p.TableNumber,
                    Dishes = p.Dishes
                };
                results.Add(order);

            }
            return results;
        }
    }
}
