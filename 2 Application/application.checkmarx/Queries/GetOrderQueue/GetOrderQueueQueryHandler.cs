using application.checkmarx.DisplayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using crosscutting.checkmarx.Enums;
using System.Threading.Tasks;

namespace application.checkmarx.Queries
{
    public class GetOrderQueueQueryHandler : IQueryHandler<GetOrderQueueQuery>
    {
        private readonly IApplicationContext _context;

        public GetOrderQueueQueryHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<IResult>> Handle(GetOrderQueueQuery query)
        {
            var orders = _context.Orders.Where(p => p.Status == query.Status 
            || query.Status==OrderStatus.None).OrderByDescending(x=>x.CreationDate).ToList();
            if (orders == null)
                return null;

            var results = new List<IResult>();
            foreach (var p in orders)
            {
                var order = new OrderDisplay
                {
                    OrderId = p.OrderId,
                    WaiterId = p.WaiterId,
                    ChefId = p.ChefId,
                    Status = p.Status,
                    TableNumber = p.TableNumber,
                    Dishes = p.Dishes,
                    CreationDate = p.CreationDate
                };
                results.Add(order);

            }
            return await Task.FromResult(results);
           
        }
    }
}
