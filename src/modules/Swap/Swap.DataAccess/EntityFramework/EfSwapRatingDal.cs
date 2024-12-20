using Microsoft.EntityFrameworkCore;
using Core.DataAccess.EntityFramework;
using Swap.DataAccess.Abstract;
using Swap.Entity.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Swap.DataAccess.EntityFramework
{
    public class EfSwapRatingDal : EfEntityRepositoryBase<SwapRating, SwapDbContext>, ISwapRatingDal
    {
        public async Task<List<SwapRating>> GetUserRatingsWithDetailsAsync(int userId)
        {
            using (var context = new SwapDbContext())
            {
                return await context.SwapRatings
                    .Include(r => r.SwapRequest)
                    .Include(r => r.RatedByUser)
                    .Include(r => r.RatedUser)
                    .Where(r => r.RatedUserId == userId)
                    .ToListAsync();
            }
        }
    }
}
