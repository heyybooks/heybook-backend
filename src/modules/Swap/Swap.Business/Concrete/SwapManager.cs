using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Swap.Entity.Concrete;
using Swap.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swap.Business.Abstract;
using Swap.DataAccess.Abstract;
using Books.Business.Concrete;
using Books.Business.Abstract;
using Microsoft.Extensions.Logging;



namespace Swap.Business.Concrete
{
    public class SwapManager : ISwapService
    {
        private readonly ISwapDal _swapDal;
        private readonly ISwapRatingDal _ratingDal;
        private readonly IBookService _bookService; 

         private readonly ILogger<SwapManager> _logger; 

        public SwapManager(ISwapDal swapDal, ISwapRatingDal ratingDal, IBookService bookService, ILogger<SwapManager> logger)
        {
            _swapDal = swapDal;
            _ratingDal = ratingDal;
            _bookService = bookService;
            _logger = logger; 
        }

        public async Task<IDataResult<List<SwapRequest>>> GetAll()
        {
            try
            {
                // Tüm kullanıcıların takas isteklerini çekecek bir metot gerekiyor
                var result = await _swapDal.GetAllSwapRequests(); // Bu metodu implemente etmeniz gerekecek
                        
                if (result == null)
                {
                    _logger.LogWarning("No swap requests found (null result).");
                    return new SuccessDataResult<List<SwapRequest>>(new List<SwapRequest>(), "No swap requests found.");
                }

                return new SuccessDataResult<List<SwapRequest>>(result, "Swap requests retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching swap requests: {ex.Message}");
                return new ErrorDataResult<List<SwapRequest>>(null, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IDataResult<SwapRequest>> GetById(int id)
        {
            var result = await _swapDal.GetAsync(s => s.RequestId == id);
            return result != null 
                ? new SuccessDataResult<SwapRequest>(result)
                : new ErrorDataResult<SwapRequest>("Swap request not found");
        }

        public async Task<IDataResult<List<SwapRequest>>> GetByRequesterId(int requesterId)
        {
            var result = await _swapDal.GetUserSwaps(requesterId);
            if (result == null || result.Count == 0)
            {
                return new DataResult<List<SwapRequest>>(null, false, "No swap requests found for this user.");
            }
            return new DataResult<List<SwapRequest>>(result, true);
        }

        public async Task<IDataResult<List<SwapRequest>>> GetByStatus(SwapStatus status)
        {
            var result = await _swapDal.GetAllAsync(s => s.Status == status);
            if (result == null || result.Count == 0)
            {
                return new DataResult<List<SwapRequest>>(null, false, "No swap requests found with this status.");
            }
            return new DataResult<List<SwapRequest>>(result, true);
        }

        public async Task<IResult> CreateSwapRequest(SwapRequest swapRequest)
        {
            // Kitapların varlığını kontrol et (asenkron)
            var offeredBookResult =  _bookService.GetById(swapRequest.OfferedBookId);
            var requestedBookResult =  _bookService.GetById(swapRequest.RequestedBookId);

            if (offeredBookResult == null || !offeredBookResult.IsSuccess)
                return new ErrorResult("Teklif edilen kitap bulunamadı");

            if (requestedBookResult == null || !requestedBookResult.IsSuccess)
                return new ErrorResult("İstenen kitap bulunamadı");

            // Kitapların aktif olup olmadığını kontrol et
            if (!offeredBookResult.Data.IsActive || !requestedBookResult.Data.IsActive)
                return new ErrorResult("Kitaplardan biri takasa uygun değil");

            // Takas isteğini oluştur
            swapRequest.Status = SwapStatus.Pending;
            swapRequest.CreatedAt = DateTime.UtcNow;
            swapRequest.UpdatedAt = DateTime.UtcNow;

            await _swapDal.AddAsync(swapRequest); // Asenkron ekleme
            return new SuccessResult("Takas isteği başarıyla oluşturuldu");
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
            return new DataResult<List<SwapRating>>(ratings, true); // Başarılı sonuç döndür
        }
    }
}
