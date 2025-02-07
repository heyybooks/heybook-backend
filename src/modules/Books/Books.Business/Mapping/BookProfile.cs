using AutoMapper;
using Books.Entity.Concrete;
using Books.Entity.DTOs;

namespace Books.Business.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            Console.WriteLine("BookProfile Loaded!");

            // BookWithImagesDto -> Book
            CreateMap<BookWithImagesDto, Book>()
                .ForMember(dest => dest.BookId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.BookImages, opt => opt.Ignore());

            // BookWithImagesDto -> BookImage
            CreateMap<BookWithImagesDto, BookImage>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.BookId, opt => opt.Ignore())
                .ForMember(dest => dest.UploadedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
