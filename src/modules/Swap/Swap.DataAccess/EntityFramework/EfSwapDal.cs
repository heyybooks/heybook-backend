using Core.DataAccess.EntityFramework;
using Swap.DataAccess.Abstract;
using Swap.Entity.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Swap.DataAccess.EntityFramework
{
    public class EfSwapDal : EfEntityRepositoryBase<SwapRequest, SwapDbContext>, ISwapDal
    {
        public async Task<List<SwapRequest>> GetUserSwaps(int userId)
        {
            using (var context = new SwapDbContext())
            {
                return await context.SwapRequests
                    .Include(s => s.RequestedBook)
                    .Include(s => s.OfferedBook)
                    .Where(s => s.RequesterId == userId)
                    .ToListAsync();
            }
        }
    }


}
