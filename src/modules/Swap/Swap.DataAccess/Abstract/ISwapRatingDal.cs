using Core.DataAccess;
using Swap.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;


namespace Swap.DataAccess.Abstract
{
    public interface ISwapRatingDal : IEntityReposity<SwapRating>
    {
        Task<List<SwapRating>> GetUserRatingsWithDetailsAsync(int userId);
    }
}
