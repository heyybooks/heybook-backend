using Core.DataAccess;
using Swap.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swap.DataAccess.Abstract
{
    public interface ISwapDal : IEntityReposity<SwapRequest> 
    {
        Task<List<SwapRequest>> GetUserSwaps(int userId);
}

}