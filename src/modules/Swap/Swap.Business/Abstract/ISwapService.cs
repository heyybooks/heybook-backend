using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
//using Microsoft.EntityFrameworkCore;
using Swap.Entity.Concrete;
using Swap.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swap.DataAccess.Abstract;

namespace Swap.Business.Abstract
{
   public interface ISwapService
    {  
        Task<IDataResult<List<SwapRequest>>> GetAll();
        Task<IDataResult<SwapRequest>> GetById(int id);
        Task<IDataResult<List<SwapRequest>>> GetByRequesterId(int requesterId);
        Task<IDataResult<List<SwapRequest>>> GetByStatus(SwapStatus status);
        Task<IResult> CreateSwapRequest(SwapRequest swapRequest);
        Task<IResult> UpdateSwapStatus(int requestId, SwapStatus status);
        Task<IResult> AddRating(SwapRating rating);
        Task<IDataResult<List<SwapRating>>> GetUserRatings(int userId);
    }

}