using application.checkmarx.DisplayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.checkmarx.Queries
{
    public class GetDishesQueryHandler : IQueryHandler<GetDishesQuery>
    {
        private readonly IApplicationContext _context;

        public GetDishesQueryHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<IResult>> Handle(GetDishesQuery query)
        {
            var dishes = _context.Dishes.ToList();
            if (dishes == null)
                return null;

            var results = new List<IResult>();
            foreach (var p in dishes)
            {
                var dish = new DishDisplay
                {
                    Id = p.Id,
                    Description = p.Description,
                    PreparationTime = p.PreparationTime,
                    Price = p.Price
                };
                results.Add(dish);

            }
            return await Task.FromResult(results);
        }
    }
}
