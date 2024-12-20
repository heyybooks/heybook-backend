using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Swap.Entity.Concrete;
using Swap.Entity.Enums;
using System;
using System.Collections.Generic;
using Swap.Business.Abstract;
using Swap.DataAccess.Abstract;



namespace Swap.Business.Concrete
{
    public class SwapManager : ISwapService
    {
        private readonly ISwapDal _swapDal;
        private readonly ISwapRatingDal _ratingDal;

        public SwapManager(ISwapDal swapDal, ISwapRatingDal ratingDal)
        {
            _swapDal = swapDal;
            _ratingDal = ratingDal;
        }

        public async Task<IDataResult<List<SwapRequest>>> GetAll()
        {
            var result = await _swapDal.GetUserSwaps(0);
            return new SuccessDataResult<List<SwapRequest>>(result);
        }

        public async Task<IDataResult<SwapRequest>> GetById(int id)
        {
            var result = _swapDal.Get(s => s.RequestId == id);
            return result != null 
                ? new SuccessDataResult<SwapRequest>(result)
                : new ErrorDataResult<SwapRequest>("Swap request not found");
        }

        public async Task<IDataResult<List<SwapRequest>>> GetByRequesterId(int requesterId)
        {
            var result = await _swapDal.GetUserSwaps(requesterId);
            return new SuccessDataResult<List<SwapRequest>>(result);
        }

        public async Task<IDataResult<List<SwapRequest>>> GetByStatus(SwapStatus status)
        {
            var result = _swapDal.GetAll(s => s.Status == status);
            return new SuccessDataResult<List<SwapRequest>>(result);
        }

        public async Task<IResult> CreateSwapRequest(SwapRequest swapRequest)
        {
            swapRequest.Status = SwapStatus.Pending;
            swapRequest.CreatedAt = DateTime.Now;
            swapRequest.UpdatedAt = DateTime.Now;
            await _swapDal.AddAsync(swapRequest);
            return new SuccessResult("Swap request created");
        }

        public async Task<IResult> UpdateSwapStatus(int requestId, SwapStatus status)
        {
            var request = await _swapDal.GetAsync(s => s.RequestId == requestId);
            if (request == null)
                return new ErrorResult("Request not found");

            request.Status = status;
            request.UpdatedAt = DateTime.Now;
            
            if (status == SwapStatus.Completed)
                request.CompletedAt = DateTime.Now;

            await _swapDal.UpdateAsync(request);
            return new SuccessResult("Status updated");
        }

        public async Task<IResult> AddRating(SwapRating rating)
        {
            rating.CreatedAt = DateTime.Now;
            await _ratingDal.AddAsync(rating);
            return new SuccessResult("Rating added successfully");
        }

        public async Task<IDataResult<List<SwapRating>>> GetUserRatings(int userId)
        {
            var ratings = await _ratingDal.GetUserRatingsWithDetailsAsync(userId);
            return new SuccessDataResult<List<SwapRating>>(ratings);
        }
    }


}
