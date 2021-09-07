using application.checkmarx.DisplayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.checkmarx.Queries
{
    public class GetBillQueryHandler : IQueryHandler<GetBillQuery>
    {
        private readonly IApplicationContext _context;

        public GetBillQueryHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<IResult>> Handle(GetBillQuery query)
        {
            var order = _context.Orders.Where(x => x.OrderId.ToString() == query.OrderId.ToString()).FirstOrDefault();
            var dishes = _context.Dishes.Where(x => order.Dishes.Contains(x.Id)).ToList();
            var chef = _context.Chefs.Where(x => x.Id == order.ChefId).FirstOrDefault();
            var waiter = _context.Waiters.Where(x => x.Id == order.WaiterId).FirstOrDefault();
            
            var results = new List<IResult>();
         
                var bill = new BillDisplay
                {
                    Waiter = waiter.Name,
                    Chef = chef.Name,
                    Date = order.CreationDate.ToString(),
                    Total = dishes.Sum(x=>x.Price)
                };
                results.Add(bill);

          
            return await Task.FromResult(results);
        }
    }
}
