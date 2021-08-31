using application.checkmarx.DisplayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.checkmarx.Queries
{
    public class GetChefsQueryHandler : IQueryHandler<GetChefsQuery>
    {
        private readonly IApplicationContext _context;

        public GetChefsQueryHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<IResult>> Handle(GetChefsQuery query)
        {
            var chefs = _context.Chefs.ToList();
            if (chefs == null)
                return null;

            var results = new List<IResult>();
            foreach (var p in chefs)
            {
                var chef = new ChefDisplay
                {
                    Id = p.Id,
                    Name = p.Name
                };
                results.Add(chef);

            }
            return await Task.FromResult(results);
        }
    }
}
