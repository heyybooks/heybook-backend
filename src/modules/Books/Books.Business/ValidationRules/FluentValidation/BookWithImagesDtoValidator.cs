using Books.Entity.DTOs;
using FluentValidation;

namespace Books.Business.ValidationRules.FluentValidation
{
    public class BookWithImagesDtoValidator : AbstractValidator<BookWithImagesDto>
    {
        public BookWithImagesDtoValidator()
        {
            RuleFor(b => b.BookName)
                .NotEmpty().WithMessage("Kitap adı boş olamaz.")
                .MaximumLength(200).WithMessage("Kitap adı en fazla 200 karakter olabilir.");

            RuleFor(b => b.Author)
                .NotEmpty().WithMessage("Yazar adı boş olamaz.");

            RuleFor(b => b.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(b => b.Publisher)
                .NotEmpty().WithMessage("Yayınevi boş olamaz.");

            RuleFor(b => b.PublicationYear)
                .InclusiveBetween(1800, DateTime.UtcNow.Year)
                .WithMessage($"Yayın yılı 1800 ile {DateTime.UtcNow.Year} arasında olmalıdır.");

            RuleFor(b => b.CategoryId)
                .GreaterThan(0).WithMessage("Kategori ID 0'dan büyük olmalıdır.");

            RuleFor(b => b.CityId)
                .GreaterThan(0).WithMessage("Şehir ID 0'dan büyük olmalıdır.");

            RuleFor(b => b.OwnerId)
                .GreaterThan(0).WithMessage("Sahip ID 0'dan büyük olmalıdır.");

            RuleForEach(b => b.ImageUrls)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Geçersiz resim URL formatı.");
        }
    }
}
